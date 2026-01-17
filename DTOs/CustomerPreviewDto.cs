namespace CustomerCampaign.DTOs
{
    public class CustomerPreviewDto
    {
        public string CustomerId { get; set; }
        public string FullName { get; set; } = null!;
        public DateTime? DateOfBirth { get; set; }
        public int Age { get; set; }
        public AddressDto HomeAddress { get; set; } = null!;
    }

}
