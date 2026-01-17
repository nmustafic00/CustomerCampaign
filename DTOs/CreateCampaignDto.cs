namespace CustomerCampaign.DTOs
{
    public class CreateCampaignDto
    {
        public string Name { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int DailyLimitPerAgent { get; set; } = 5;
    }
}
