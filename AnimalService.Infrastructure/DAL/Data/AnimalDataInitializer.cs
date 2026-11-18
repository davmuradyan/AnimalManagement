using System.Data;
using System.Text.Json;
using AnimalManagement.Shared.Enums;
using AnimalService.Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace AnimalService.Infrastructure.DAL.Data;

public class AnimalDataInitializer(IConfiguration configuration)
{
    public async Task InitializeAsync(CancellationToken cancellationToken = default)
    {
        var connectionString = configuration.GetConnectionString("MainConnectionString")
            ?? throw new InvalidOperationException("Connection string not found.");

        var filePath = Path.Combine(
            AppContext.BaseDirectory,
            "Data",
            "animals.json");

        if (!File.Exists(filePath))
            throw new FileNotFoundException("Animals data file not found.", filePath);

        var json = await File.ReadAllTextAsync(filePath, cancellationToken);

        var rows = JsonSerializer.Deserialize<List<AnimalImportRow>>(
            json,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? [];

        var animals = rows.Select(x => Animal.Create(
            x.Name,
            x.Age,
            x.Weight,
            Enum.Parse<Gender>(x.Gender, true),
            Enum.Parse<Color>(x.Color, true),
            "D")).ToList();

        if (animals.Count == 0)
            return;

        await using var connection = new SqlConnection(connectionString);
        await connection.OpenAsync(cancellationToken);

        await using var transaction = (SqlTransaction)
            await connection.BeginTransactionAsync(
                IsolationLevel.Serializable,
                cancellationToken);

        try
        {
            await using var command = new SqlCommand(
                "SELECT TOP (1) 1 FROM dbo.Animals",
                connection,
                transaction);

            var hasAnimals = await command.ExecuteScalarAsync(cancellationToken);

            if (hasAnimals is not null)
            {
                await transaction.CommitAsync(cancellationToken);
                return;
            }

            using var table = CreateDataTable(animals);

            using var bulkCopy = new SqlBulkCopy(
                connection,
                SqlBulkCopyOptions.CheckConstraints,
                transaction)
            {
                DestinationTableName = "dbo.Animals",
                BatchSize = 5000
            };

            foreach (DataColumn column in table.Columns)
                bulkCopy.ColumnMappings.Add(column.ColumnName, column.ColumnName);

            await bulkCopy.WriteToServerAsync(table, cancellationToken);

            await transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    private static DataTable CreateDataTable(List<Animal> animals)
    {
        var table = new DataTable();

        table.Columns.Add("Name", typeof(string));
        table.Columns.Add("Age", typeof(int));
        table.Columns.Add("Weight", typeof(double));
        table.Columns.Add("Gender", typeof(string));
        table.Columns.Add("Color", typeof(string));
        table.Columns.Add("CreatedAt", typeof(DateTime));
        table.Columns.Add("UpdatedAt", typeof(DateTime));
        table.Columns.Add("ClosedDate", typeof(DateTime));

        foreach (var animal in animals)
        {
            table.Rows.Add(
                animal.Name,
                animal.Age,
                animal.Weight,
                animal.Gender.ToString(),
                animal.Color.ToString(),
                animal.CreatedAt,
                animal.UpdatedAt,
                DBNull.Value);
        }

        return table;
    }

    private sealed record AnimalImportRow(
        string Name,
        int Age,
        double Weight,
        string Gender,
        string Color);
}
