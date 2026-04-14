using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IAuthService
    {
        
            (bool Success, string Message) Register(string fullName, string username, string email, string password, string confirmPassword);

            User Login(string username, string password);

            (bool Success, string Message) VerifyOtpPending(string code);
        
    }
}
