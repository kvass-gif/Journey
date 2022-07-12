using Journey.DataAccess.Repositories;

namespace Journey.Application.Services.Impl
{
    public class IdentityService : IIdentityService
    {
        private readonly IUnitOfWork _unitOfWork;
        public IdentityService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<string>> GetAllStringRolesAsync()
        {
            var list = await _unitOfWork.RoleRepo.GetAllAsync();
            string[] strRoles = new string[list.Count];
            for (int i = 0; i < list.Count; i++)
            {
                strRoles[i] = list[i].Name;
            }
            return strRoles;
        }
    }
}
