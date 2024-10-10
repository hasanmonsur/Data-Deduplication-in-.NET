using DataDeduplicationAPI.Contacts;
using DataDeduplicationAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DataDeduplicationAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DeduplicationController : ControllerBase
    {
        private readonly IDeduplicationService _deduplicationService;

        public DeduplicationController(IDeduplicationService deduplicationService)
        {
            _deduplicationService = deduplicationService;
        }

        [HttpPost]
        [Route("check")]
        public async Task<IActionResult> CheckForDuplicates([FromBody] DataDto dataDto)
        {
            string key = GenerateKeyFromData(dataDto.Data);  // Generate a unique key from the data
            bool isDuplicate = await _deduplicationService.IsDuplicateAsync(key);

            if (isDuplicate)
            {
                return Conflict("Duplicate data detected.");
            }

            await _deduplicationService.AddDataAsync(key, dataDto.Data);
            return Ok("Data processed successfully.");
        }

        private string GenerateKeyFromData(string data)
        {
            // Use a hash of the data or some unique identifier as the key
            return $"data_key:{data.GetHashCode()}";
        }
    }
}
