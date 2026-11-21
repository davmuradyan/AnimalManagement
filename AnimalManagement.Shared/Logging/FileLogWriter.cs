namespace AnimalManagement.Shared.Logging;

public class FileLogWriter : IDisposable
{
    private readonly string _filePath;
    private readonly Mutex _mutex;

    public FileLogWriter(string filePath)
    {
        _filePath = filePath;
        _mutex = new Mutex(false, "AnimalTask_Log_Mutex");
    }

    public void Write(string message)
    {
        bool acquired = false;

        try
        {
            acquired = _mutex.WaitOne(TimeSpan.FromSeconds(5));

            if (!acquired)
                throw new TimeoutException("Could not acquire log file mutex.");

            var directory = Path.GetDirectoryName(_filePath);

            if (directory is not null)
                Directory.CreateDirectory(directory);

            File.AppendAllText(_filePath, message + Environment.NewLine);
        }
        catch (Exception ex)
        {
            try
            {
                Console.Error.WriteLine($"File logging failed: {ex.Message}\nOriginal log: {message}");
            }
            catch
            {
            }
        }
        finally
        {
            if (acquired)
                _mutex.ReleaseMutex();
        }
    }

    public void Dispose()
    {
        _mutex.Dispose();
    }
}
