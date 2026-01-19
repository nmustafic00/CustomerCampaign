namespace CustomerCampaign.Entities
{
    public class CampaignResult
    {
        public int Id { get; set; }
        public int CampaignId { get; set; }
        public int CustomerId { get; set; } 
        public DateTime PurchaseDateTime { get; set; }
        public decimal AmountWithoutDiscount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal AmountWithDiscount { get; set; }

        public Campaign Campaign { get; set; } = null!;
    }

}
