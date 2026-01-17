namespace CustomerCampaign.Entities
{
    public class AgentRewardEntry
    {
        public int Id { get; set; }
        public int AgentId { get; set; }
        public int CustomerId { get; set; }
        public int CampaignId { get; set; }        
        public Campaign Campaign { get; set; } = null!;
        public DateTime RewardDate { get; set; }     
        public RewardStatus Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
