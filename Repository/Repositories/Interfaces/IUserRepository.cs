using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Interfaces
{
    public interface IUserRepository
    {
        User Create(User user);
        User GetByUsername(string username);
        User GetByEmail(string email);
        bool UsernameExists(string username);
        bool EmailExists(string email);
    }
}
