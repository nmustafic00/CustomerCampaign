using CustomerCampaign.DTOs;
using CustomerCampaign.Entities;

namespace CustomerCampaign.Services.Interfaces
{
    public interface IAuthService
    {
        Task<string?> LoginAsync(LoginDto dto);
        Task<User> CreateAgentAsync(CreateAgentDto dto);
    }
}
