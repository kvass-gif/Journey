using System.Security.Claims;

namespace Journey.Core.Services.Impl
{
    public class ClaimPrincipalService : IClaimService
    {
        private readonly ClaimsPrincipal _claimPrincipal;
        public ClaimPrincipalService()
        {
            var identity = new ClaimsIdentity();
            _claimPrincipal = new ClaimsPrincipal(identity);
        }
        public string GetUserId()
        {
            return GetClaim("Id");
        }
        public string GetClaim(string key)
        {
            var value = _claimPrincipal.FindFirst(key)?.Value;
            if (value == null)
            {
                throw new NullReferenceException("No such user");
            }
            return value;
        }
    }
}
