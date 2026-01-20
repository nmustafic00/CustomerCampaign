using CustomerCampaign.DTOs;
using CustomerCampaign.Services;
using CustomerCampaign.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CustomerCampaign.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CampaignController : ControllerBase
    {
        private readonly ICampaignService _campaignService;
        private readonly ICampaignResultService _campaignResultService;

        public CampaignController(ICampaignService campaignService, ICampaignResultService campaignResultService)
        {
            _campaignService = campaignService;
            _campaignResultService = campaignResultService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("create")]
        public async Task<IActionResult> CreateCampaign([FromBody] CreateCampaignDto dto)
        {
            var campaign = await _campaignService.CreateCampaignAsync(dto);
            return Ok(campaign);
        }

        [Authorize(Roles = "Agent")]
        [HttpPost("{campaignId}/reward")]
        public async Task<IActionResult> RewardCustomer(int campaignId, [FromBody] CampaignRewardDto dto)
        {
            var entry = await _campaignService.RewardCustomerAsync(campaignId, dto);
            return Ok(entry);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("results/{campaignId}")]
        public async Task<IActionResult> UploadCampaignCsv(int campaignId, IFormFile csvFile)
        {
            if (csvFile == null)
                return BadRequest("CSV file is required.");

            var result = await _campaignResultService.SaveCsvAsync(campaignId, csvFile);
            return Ok(result);
        }

        [Authorize(Roles = "Admin,Agent")]
        [HttpGet("results/{campaignId}")]
        public async Task<IActionResult> GetCampaignResults(int campaignId)
        {
            var results = await _campaignResultService.GetCampaignResultsAsync(campaignId);
            return Ok(results);
        }

    }
}
