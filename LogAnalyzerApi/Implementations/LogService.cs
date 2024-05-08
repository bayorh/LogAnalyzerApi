using LogAnalyzerApi.DTOS;
using LogAnalyzerApi.Interefaces;
using System.IO;
using System.IO.Compression;

namespace LogAnalyzerApi.Implementations;

public class LogService : ILogService
{
    public BaseResponse ArchiveLogFiles(string sourceDirectory, string targetDirectory, DateTime startDate, DateTime endDate)
    {
        try
        {
            //check if directory exist, else create
            if (!Directory.Exists(targetDirectory))
            {
                Directory.CreateDirectory(targetDirectory);
            }

            
            string zipFileName = $"{startDate:dd_MM_yyyy}-{endDate:dd_MM_yyyy}.zip";

            // Path to the zip file
            string zipFilePath = Path.Combine(targetDirectory, zipFileName);

            // Create a new zip archive
            using (ZipArchive archive = ZipFile.Open(zipFilePath, ZipArchiveMode.Create))
            {
               
                string[] files = Directory.GetFiles(sourceDirectory);

                // Loop through each file and add it to the zip archive if it falls within the specified period
                foreach (var file in files)
                {
                    DateTime fileCreationTime = File.GetCreationTime(file);
                    if (fileCreationTime >= startDate && fileCreationTime <= endDate)
                    {
                        string entryName = Path.GetFileName(file);
                        archive.CreateEntryFromFile(file, entryName);
                    }
                }
            }

            return new BaseResponse
            {
                Message = $"files successfully archived",
                Status = true
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse
            {
                Message = $"An error occurred: {ex.Message}",
                Status = false
            };
            
        }
    }

    public BaseResponse DeleteAllLogsInAPeriod(string directoryPath, DateTime startDate, DateTime endDate)
    {
        try
        {
            var logs = Directory.GetFiles(directoryPath, "*", SearchOption.AllDirectories); ;

            foreach (var log in logs)
            {

                var creationTime = File.GetCreationTime(log);

                if (creationTime >= startDate && creationTime <= endDate)
                {
                    File.Delete(log);
                   
                }
              
            }
            return new BaseResponse
            {
                Message = $"files successfully deleted",
                Status = true
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse
            {
                Message = $"An error occurred: {ex.Message}",
                Status = false
            };
            
        }

    }

    public BaseResponse DeleteArchive(string targetDirectory, DateTime startDate, DateTime endDate)
    {
        try
        {
            // Format the date range for the archive file name
            string archiveFileName = $"{startDate:yyyy-MM-dd}-{endDate:yyyy-MM-dd}.zip";

            // Path to the archive file
            string archiveFilePath = Path.Combine(targetDirectory, archiveFileName);

            // Check if the archive file exists
            if (File.Exists(archiveFilePath))
            {
                // Delete the archive file
                File.Delete(archiveFilePath);
                return new BaseResponse
                {
                    Message = $"Archive file successfully deleted",
                    Status = true
                };
            }
            else
            {
                return new BaseResponse
                {
                    Message = $"Archive file not found: {archiveFilePath}",
                    Status = false
                };

            }
        }
        catch (Exception ex)
        {
            return new BaseResponse
            {
                Message = $"Some Errors Occured:{ex.Message}",
                Status = false
            };
        }
    }

    public int GetLogsCountsInADirectoryForAPeriod(string directoryPath, DateTime startDate, DateTime endDate)
    {
        int logcount = 0;

        try
        {
            var logs = Directory.GetFiles(directoryPath, "*", SearchOption.AllDirectories); ;

            foreach (var log in logs)
            {

                var creationTime = File.GetCreationTime(log);

                if (creationTime >= startDate && creationTime <= endDate)
                {

                    logcount++;
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred: {ex.Message}");
        }

        return logcount;
    }

    public string[] SearchLogsInADirectory(string path)
    {
        string[] result = Directory.GetFiles(path, "*", SearchOption.AllDirectories);
        return result;
    }

    public List<string> SearchLogsInDirectories(string[] directoriespaths)
    {
        List<string> logFiles  = new List<string>();

        try
        {
            
            foreach (var directory in directoriespaths)
            {
                
                if (Directory.Exists(directory))
                {
                   
                    var _logFiles = Directory.GetFiles(directory, "*.log", SearchOption.AllDirectories);
                    logFiles.AddRange(_logFiles);
                }
                else
                {
                     throw new Exception($"Directory not found: {directory}");
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred: {ex.Message}");
        }

        return logFiles;
    }

    public List<string> SearchLogsSize(string path, long minSizeInKByte, long maxSizeinKByte)
    {
        List<string> LogFiles = new List<string>();

        try
        {
           
            if (Directory.Exists(path))
            {
                
                string[] logs = Directory.GetFiles(path, "*.log", SearchOption.AllDirectories);

     
                foreach (var log in logs)
                {
                    var fileInfo = new FileInfo(log);
                    long fileSize = fileInfo.Length;

                    if (fileSize >= minSizeInKByte && fileSize <= maxSizeinKByte)
                    {
                        LogFiles.Add(log);
                    }
                }
            }
            else
            {
                throw new Exception($"Directory not found: {path}");
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred: {ex.Message}");
        }

        return LogFiles;

    }

}
