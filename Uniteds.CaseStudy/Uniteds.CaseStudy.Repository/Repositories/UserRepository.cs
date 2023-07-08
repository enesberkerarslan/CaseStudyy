using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uniteds.CaseStudy.Domain.Interfaces;
using Uniteds.CaseStudy.Domain.Models;
using Uniteds.CaseStudy.Repository.Context;

namespace Uniteds.CaseStudy.Repository.Repositories
{
    internal class UserRepository : IUserRepository
    {
        private readonly AppDbContext _dbContext;

        public UserRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public User GetUserByEmail(string email)
        {
            return _dbContext.Users.FirstOrDefault(u => u.Email == email);
        }

        public void AddUser(User user)
        {
            _dbContext.Users.Add(user);
        }
    }
}
