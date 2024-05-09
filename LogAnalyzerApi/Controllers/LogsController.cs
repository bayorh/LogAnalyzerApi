using LogAnalyzerApi.Interefaces;
using Microsoft.AspNetCore.Mvc;

namespace LogAnalyzerApi.Controllers
{
    [ApiController]
    [Route("api/")]
    public class LogsController : ControllerBase
    {
        private readonly ILogService _logService;

        public LogsController(ILogService logService)
        {
            _logService = logService;
        }

        [HttpGet("{path}/{minSizeInKByte}/{maxSizeinKByte}")]
        public IActionResult GetLogsBySize(string path, long minSizeInKByte, long maxSizeinKByte)
        {
            var result =_logService.SearchLogsSize(path, minSizeInKByte, maxSizeinKByte);
            return Ok(result);
        }
        [HttpGet("{directoriespaths}")]
        public IActionResult SearchLogsInDirectories([FromQuery] string[] directoriespaths)
        {
            var result = _logService.SearchLogsInDirectories(directoriespaths);
            return Ok(result);
        }

        [HttpGet("SearchLogsInADirectory{path}")]
        public IActionResult SearchLogsInADirectory(string path)
        {
            var result = _logService.SearchLogsInADirectory(path);
            return Ok(result);
        }
        [HttpGet("{directoryPath}/{startDate}/{endDate}")]
        public IActionResult GetLogsCountsInADirectoryForAPeriod(string directoryPath, DateTime startDate, DateTime endDate)
        {
            var result = _logService.GetLogsCountsInADirectoryForAPeriod(directoryPath, startDate, endDate);
            return Ok(result);
        }

        [HttpDelete("DeleteArchive{targetDirectory}/{startDate}/{endDate}")]
        public IActionResult DeleteArchive(string targetDirectory, DateTime startDate, DateTime endDate)
        {
            var result = _logService.DeleteArchive(targetDirectory, startDate, endDate);
            return Ok(result);
        }

        [HttpDelete("{targetDirectory}/{startDate}/{endDate}")]
        public IActionResult DeleteAllLogsInAPeriod(string targetDirectory, DateTime startDate, DateTime endDate)
        {
            var result = _logService.DeleteAllLogsInAPeriod(targetDirectory, startDate, endDate);
            return Ok(result);
        }

        [HttpPost("{sourceDirectory}/{targetDirectory}/{startDate}/{endDate}")]
        public IActionResult ArchiveLogFiles(string sourceDirectory,
            string targetDirectory, DateTime startDate, DateTime endDate)
        {
            var result = _logService.ArchiveLogFiles(sourceDirectory, targetDirectory, startDate, endDate);
            return Ok(result);
        }

    }
}
