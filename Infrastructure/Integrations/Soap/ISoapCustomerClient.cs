using CustomerCampaign.DTOs;

namespace CustomerCampaign.Infrastructure.Integrations.Soap
{
    public interface ISoapCustomerClient
    {
        Task<CustomerPreviewDto?> FindPersonAsync(string customerId);
    }
}
