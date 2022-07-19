namespace Journey.Core.Services
{
    public interface IClaimService
    {
        string GetUserId();
        string GetClaim(string key);
    }
}
