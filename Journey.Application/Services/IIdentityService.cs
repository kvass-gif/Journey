namespace Journey.Application.Services
{
    public interface IIdentityService
    {
        Task<IEnumerable<string>> GetAllStringRolesAsync();
    }
}
