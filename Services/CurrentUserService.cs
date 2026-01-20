using CustomerCampaign.Services.Interfaces;
using System.Security.Claims;

namespace CustomerCampaign.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public int UserId { get; }
        public string Role { get; }

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            var user = httpContextAccessor.HttpContext?.User;

            UserId = int.Parse(
                user?.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? throw new Exception("UserId not found in token")
            );

            Role = user.FindFirst(ClaimTypes.Role)?.Value
                ?? throw new Exception("Role not found in token");
        }
    }
}
