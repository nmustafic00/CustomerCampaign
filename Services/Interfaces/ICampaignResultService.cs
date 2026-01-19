using CustomerCampaign.DTOs;
using CustomerCampaign.Entities;

namespace CustomerCampaign.Services.Interfaces
{
    public interface ICampaignResultService
    {
        Task<IEnumerable<CampaignResult>> SaveCsvAsync(int campaignId, IFormFile csvFile);
        Task<IEnumerable<CampaignResultDto>> GetCampaignResultsAsync(int campaignId);
    }

}
