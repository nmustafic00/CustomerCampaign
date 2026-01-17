namespace CustomerCampaign.Entities
{
    public class Campaign
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int DailyLimitPerAgent { get; set; } = 5;

        public ICollection<AgentRewardEntry> RewardEntries { get; set; } = new List<AgentRewardEntry>();

    }

}
