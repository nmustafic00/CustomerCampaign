using CsvHelper.Configuration.Attributes;

namespace CustomerCampaign.DTOs
{
    public class PurchaseCsvRowDto
    {
        [Name("customer_id")]
        public string CustomerId { get; set; } = null!;

        [Name("purchase_datetime")]
        public DateTime PurchaseDateTime { get; set; }

        [Name("amount_without_discount")]
        public decimal AmountWithoutDiscount { get; set; }

        [Name("discount_amount")]
        public decimal DiscountAmount { get; set; }

        [Name("amount_with_discount")]
        public decimal AmountWithDiscount { get; set; }
    }

}
