using CustomerCampaign.Data;
using CustomerCampaign.DTOs;
using CustomerCampaign.Entities;
using CustomerCampaign.Exceptions;
using CustomerCampaign.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CustomerCampaign.Services
{
    public class CampaignService : ICampaignService
    {
        private readonly CustomerCampaignDbContext _dbContext;
        private readonly ICurrentUserService _currentUser;

        public CampaignService( CustomerCampaignDbContext dbContext, ICurrentUserService currentUser)
        {
            _dbContext = dbContext;
            _currentUser = currentUser;
        }

        public async Task<IEnumerable<CampaignBasicDto>> GetAllCampaignsAsync()
        {
            return await _dbContext.Campaigns
                .Select(c => new CampaignBasicDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    StartDate = c.StartDate,
                    EndDate = c.EndDate
                })
                .ToListAsync();
        }

        public async Task<CampaignDetailDto?> GetCampaignByIdAsync(int id)
        {
            var campaign = await _dbContext.Campaigns
                .Include(c => c.RewardEntries)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (campaign == null)
                throw new NotFoundException("Campaign not found");

            return new CampaignDetailDto
            {
                Id = campaign.Id,
                Name = campaign.Name,
                StartDate = campaign.StartDate,
                EndDate = campaign.EndDate,
                DailyLimitPerAgent = campaign.DailyLimitPerAgent
            };
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

        public async Task<RewardCustomerResponseDto> RewardCustomerAsync(int campaignId, CampaignRewardDto dto)
        {
            if (_currentUser.Role != "Agent")
                throw new ForbiddenException("Only agents can reward customers");

            var agentId = _currentUser.UserId;

            var campaign = await _dbContext.Campaigns
                .Include(c => c.RewardEntries)
                .FirstOrDefaultAsync(c => c.Id == campaignId);

            if (campaign == null)
                throw new NotFoundException("Campaign not found");

            var today = DateTime.UtcNow.Date;

            if (today < campaign.StartDate.Date || today > campaign.EndDate.Date)
                throw new BadRequestException("Campaign is not active");

            int rewardsToday = campaign.RewardEntries
                .Count(x => x.AgentId == agentId && x.RewardDate.Date == today);
      
            bool customerAlreadyRewarded = campaign.RewardEntries
                .Any(x => x.CustomerId == dto.CustomerId);

            var entry = new AgentRewardEntry
            {
                AgentId = agentId,
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

            return new RewardCustomerResponseDto
            {
                Id = entry.Id,
                CampaignId = entry.CampaignId,
                AgentId = entry.AgentId,
                CustomerId = entry.CustomerId,
                Status = entry.Status,
                RewardDate = entry.RewardDate
            };
        }
    }
}
