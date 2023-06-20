using DataAccess.Entities;
using DataTransferObject.RegisterDto;
using DataTransferObject.Roles;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Business.RoleServices
{
    public class RoleServices : IRoleServices
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRoles> _roleManager;

        public RoleServices(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRoles> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<bool> AssignRole(string Role, RegisterDto userDto)
        {
            var user = await _userManager.FindByNameAsync(userDto.Name);
            if (user != null)
            {
                IdentityResult result = await _userManager.AddToRoleAsync(user, Role);
                return result.Succeeded;
            }
            return false;
        }

        public async Task<bool> CreateRole(RolesDto rolesDto)
        {
            if (await _roleManager.FindByNameAsync(rolesDto.Name) == null)
            {
                await _roleManager.CreateAsync(new() { Name = rolesDto.Name, Description = rolesDto.Description });
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteRole(string Id)
        {
            var role = await _roleManager.FindByIdAsync(Id);
            if (role != null)
            {
               var result = await _roleManager.DeleteAsync(role);
                return result.Succeeded;
            }
            return false;
        }

        public RolesDto GetRole(string Id)
        {
            var role = _roleManager.FindByIdAsync(Id).GetAwaiter().GetResult();
            RolesDto roleDto = new RolesDto();
            if (role != null)
            {
                roleDto.Name = role.Name;
                roleDto.Description = role.Description;
                roleDto.Id = role.Id;
                return roleDto;
            }

            return roleDto;
        }

        public List<RolesDto> RoleList()
        {
            
            var roles = _roleManager.Roles.Select(x => new RolesDto()
            {
                Name = x.Name,
                Description = x.Description,
                Id = x.Id,
            });

            return roles.ToList();
        }

        public async Task<bool> UpdateRole(RolesDto roleDto)
        {
            var role = await _roleManager.FindByIdAsync(roleDto.Id);
            if (role != null)
            {
                role.Name = roleDto.Name;
                role.Description = roleDto.Description;
                var result = await _roleManager.UpdateAsync(role);
                return result.Succeeded;
            }

            return false;
        }

        public List<RegisterDto> UsersList()
        {
            var users = _userManager.Users.Select(x => new RegisterDto()
            {
                Name = x.Name,
                Email = x.Email,
                PostalCode = x.PostalCode,
                City = x.City,
                State = x.State,
                StreetAddress = x.StreetAddress,
            });
            return users.ToList();
        }
    }
}
