using Restaurant.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant.Domain.Entities;

namespace Restaurant.DAL.Repositories
{
    public class UserRepository : IBaseRepository<User>
    {
        private readonly AppDbContext _dbContext;
        public UserRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Create(User entity)
        {
           await _dbContext.Users.AddAsync(entity);
           await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(User entity)
        {
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public IQueryable<User> GetAll()
        {
            return _dbContext.Users;
        }

        public async Task Update(User entity)
        {
            _dbContext.Users.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
