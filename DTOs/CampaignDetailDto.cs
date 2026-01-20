namespace CustomerCampaign.DTOs
{
    public class CampaignDetailDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int DailyLimitPerAgent { get; set; }
    }
}
