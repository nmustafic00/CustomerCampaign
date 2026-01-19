using CustomerCampaign.Data;
using CustomerCampaign.DTOs;
using CustomerCampaign.Entities;
using CustomerCampaign.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CustomerCampaign.Services
{
    public class CampaignService : ICampaignService
    {
        private readonly CustomerCampaignDbContext _dbContext;

        public CampaignService(CustomerCampaignDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Campaign> CreateCampaignAsync(CreateCampaignDto dto)
        {
            var campaign = new Campaign
            {
                Name = dto.Name,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                DailyLimitPerAgent = dto.DailyLimitPerAgent
            };

            _dbContext.Campaigns.Add(campaign);
            await _dbContext.SaveChangesAsync();
            return campaign;
        }

        public async Task<AgentRewardEntry> RewardCustomerAsync(int campaignId, CampaignRewardDto dto)
        {
            var campaign = await _dbContext.Campaigns
                .Include(c => c.RewardEntries)
                .FirstOrDefaultAsync(c => c.Id == campaignId);

            if (campaign == null)
                throw new Exception("Campaign not found");

            var today = DateTime.UtcNow.Date;

            int rewardsToday = campaign.RewardEntries
                .Count(x => x.AgentId == dto.AgentId && x.RewardDate.Date == today);
      
            bool customerAlreadyRewarded = campaign.RewardEntries
                .Any(x => x.CustomerId == dto.CustomerId);

            var entry = new AgentRewardEntry
            {
                AgentId = dto.AgentId,
                CustomerId = dto.CustomerId,
                CampaignId = campaignId,
                RewardDate = DateTime.UtcNow
            };

            if (customerAlreadyRewarded)
                entry.Status = RewardStatus.Mistake;
            else if (rewardsToday >= campaign.DailyLimitPerAgent)
                entry.Status = RewardStatus.Failed;
            else
                entry.Status = RewardStatus.Success;

            _dbContext.AgentRewardEntries.Add(entry);
            await _dbContext.SaveChangesAsync();

            return entry;
        }

    }
}
