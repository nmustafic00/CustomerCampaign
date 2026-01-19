namespace CustomerCampaign.DTOs
{
    public class CampaignResultDto
    {
        public int CustomerId { get; set; }
        public string FullName { get; set; } = null!;
        public DateTime? DateOfBirth { get; set; }
        public int Age { get; set; }
        public decimal AmountWithoutDiscount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal AmountWithDiscount { get; set; }
        public DateTime PurchaseDateTime { get; set; }
        public int? AgentId { get; set; }
        public string AgentName { get; set; }
        public DateTime? RewardDate { get; set; }
    }
}
