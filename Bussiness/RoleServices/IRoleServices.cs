
using DataAccess.Entities;
using DataTransferObject.RegisterDto;
using DataTransferObject.Roles;

namespace Business.RoleServices
{
    public interface IRoleServices
    {
        public Task<bool> CreateRole(RolesDto rolesDto);
        public Task<bool> DeleteRole(string Id);
        public RolesDto GetRole(string Id);
        public Task<bool> UpdateRole(RolesDto roleDto);
        public Task<bool> AssignRole(string role, RegisterDto user);
        public List<RegisterDto> UsersList();
        public List<RolesDto> RoleList();
    }
}
