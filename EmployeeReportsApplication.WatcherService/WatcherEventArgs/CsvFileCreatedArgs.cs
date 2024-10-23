
namespace EmployeeReportsApplication.WatcherService.WatcherEventArgs;

public class CsvFileCreatedArgs : EventArgs
{
    public required string Name { get; set; }
    public required string Path { get; set; }
}
