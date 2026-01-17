using CustomerCampaign.DTOs;
using CustomerCampaign.Services;
using CustomerCampaign.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CustomerCampaign.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CampaignController : ControllerBase
    {
        private readonly ICampaignService _campaignService;

        public CampaignController(ICampaignService campaignService)
        {
            _campaignService = campaignService;
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateCampaign([FromBody] CreateCampaignDto dto)
        {
            var campaign = await _campaignService.CreateCampaignAsync(dto);
            return Ok(campaign);
        }

        [HttpPost("{campaignId}/reward")]
        public async Task<IActionResult> RewardCustomer([FromBody] CampaignRewardDto dto)
        {
            var entry = await _campaignService.RewardCustomerAsync(dto);
            return Ok(entry);
        }
    }
}
