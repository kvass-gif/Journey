using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Journey.DataAccess.Services.Impl
{
    public class ClaimService : IClaimService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ClaimService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public string GetUserId()
        {
            return GetClaim(ClaimTypes.NameIdentifier);
        }
        public string GetClaim(string key)
        {
            var value = _httpContextAccessor.HttpContext?.User.FindFirst(key)?.Value;
            if(value == null)
            {
                throw new NullReferenceException("No such user");
            }
            return value;
        }
    }
}
