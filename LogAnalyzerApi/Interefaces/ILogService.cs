using LogAnalyzerApi.DTOS;

namespace LogAnalyzerApi.Interefaces;

public interface ILogService
{
    string[] SearchLogsInADirectory(string path);
    int GetLogsCountsInADirectoryForAPeriod(string directoryPath, DateTime startDate,DateTime endDate);
    BaseResponse DeleteAllLogsInAPeriod(string directoryPath, DateTime startDate, DateTime endDate);
    List<string> SearchLogsInDirectories(string[] directoriespaths);
    List<string> SearchLogsSize(string path, long minSizeInKByte, long maxSizeinKByte);
    BaseResponse ArchiveLogFiles(string sourceDirectory, string targetDirectory, DateTime startDate, DateTime endDate);
    BaseResponse DeleteArchive(string targetDirectory, DateTime startDate, DateTime endDate);







 }
