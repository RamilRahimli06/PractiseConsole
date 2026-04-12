using Domain.Entities;
using Repository.Repositories.Data;
using Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public User Create(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public User GetByUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return null;
            }

            string normalized = username.Trim();
            return _context.Users
                .FirstOrDefault(x => x.Username == normalized);
        }

        public User GetByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return null;
            }

            string normalized = email.Trim();
            return _context.Users
               .FirstOrDefault(x => x.Email == normalized);
        }

        public bool UsernameExists(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return false;
            }

            return _context.Users.Any(x => x.Username == username.Trim());
        }

        public bool EmailExists(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            return _context.Users.Any(x => x.Email == email.Trim());
        }
    }
}
