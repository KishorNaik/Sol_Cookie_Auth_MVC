using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;
using Sol_Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Sol_Demo.Repository
{
    public interface IUserRepository
    {
        Task<dynamic> LoginAsync(UserModel usersModel);
    }

    public class UserRepository : IUserRepository
    {
        public UserRepository()
        {
        }

        async Task<dynamic> IUserRepository.LoginAsync(UserModel usersModel)
        {
            // Demo Purpose
            string tempFullName = "Kishor Naik";
            string tempUserName = "kishor11";
            string tempPassword = "123";
            int? tempId = 1;
            String tempRole = "Admin";

            dynamic data = null;

            // Demo Purpose (Validate Login Credentails from database)
            if (usersModel.UserName == tempUserName && usersModel.Password == tempPassword)
            {
                // Demo Purpose (Id,Role & fullName will get from database)
                usersModel.Id = tempId;
                usersModel.Role = tempRole;
                usersModel.FullName = tempFullName;

                // Add Claims for Authorization and Authentication with Jwt Token

                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, usersModel.UserName)); // Id Base
                claims.Add(new Claim(ClaimTypes.Role, usersModel.Role)); // Role Base

                var identity = new ClaimsIdentity(claims?.ToArray(), CookieAuthenticationDefaults.AuthenticationScheme);

                var claimPrinciple = new ClaimsPrincipal(identity);

                data = claimPrinciple;
            }
            else
            {
                data = new
                {
                    Message = "User Name and Password does not match"
                };
            }

            return await Task.FromResult<dynamic>(data);
        }
    }
}