using Microsoft.EntityFrameworkCore;

namespace CustomerCampaign.Data
{
    public class CustomerCampaignDbContext : DbContext
    {
        public CustomerCampaignDbContext(DbContextOptions<CustomerCampaignDbContext> options)
          : base(options)
        {
        }
    }
}
