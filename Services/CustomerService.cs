using CustomerCampaign.DTOs;
using CustomerCampaign.Infrastructure.Integrations.Soap;
using CustomerCampaign.Services.Interfaces;

namespace CustomerCampaign.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ISoapCustomerClient _soapClient;

        public CustomerService(ISoapCustomerClient soapClient)
        {
            _soapClient = soapClient;
        }

        public async Task<CustomerPreviewDto?> GetCustomerPreviewAsync(string customerId)
        {
            return await _soapClient.FindPersonAsync(customerId);
        }
    }

}
