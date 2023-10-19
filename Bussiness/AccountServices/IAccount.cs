using DataTransferObject;
using DataTransferObject.Login;
using DataTransferObject.RegisterDto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.AccountServices
{
    public interface IAccount
    {
        public Task<bool> RegisterUserAsync(RegisterDto registerDto);
        public Task<bool> LoginUser(LoginDto loginDto);
        public Task<bool> LogoutUser();
    }
}
