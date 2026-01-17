

using CustomerCampaign.DTOs;

namespace CustomerCampaign.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomerPreviewDto?> GetCustomerPreviewAsync(string customerId);
    }

}
