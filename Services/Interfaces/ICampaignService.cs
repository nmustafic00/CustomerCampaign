using CustomerCampaign.DTOs;
using CustomerCampaign.Entities;

namespace CustomerCampaign.Services.Interfaces
{
    public interface ICampaignService
    {
        Task<Campaign> CreateCampaignAsync(CreateCampaignDto dto);
        Task<RewardCustomerResponseDto> RewardCustomerAsync(int campaignId, CampaignRewardDto dto);
    }
}
