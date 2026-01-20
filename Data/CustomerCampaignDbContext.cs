using CustomerCampaign.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomerCampaign.Data
{
    public class CustomerCampaignDbContext : DbContext
    {
        public CustomerCampaignDbContext(DbContextOptions<CustomerCampaignDbContext> options)
          : base(options)
        {
        }

        public DbSet<AgentRewardEntry> AgentRewardEntries => Set<AgentRewardEntry>();
        public DbSet<Campaign> Campaigns => Set<Campaign>();
        public DbSet<CampaignResult> CampaignResults => Set<CampaignResult>();
        public DbSet<User> Users => Set<User>();
    }
}
