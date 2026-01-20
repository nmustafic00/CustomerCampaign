using CustomerCampaign.Entities;

namespace CustomerCampaign.DTOs
{
    public class RewardCustomerResponseDto
    {
        public int Id { get; set; }
        public int CampaignId { get; set; }
        public int AgentId { get; set; }
        public int CustomerId { get; set; }
        public RewardStatus Status { get; set; }
        public DateTime RewardDate { get; set; }
    }
}
