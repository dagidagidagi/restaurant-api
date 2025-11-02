using Restaurant.DAL.Interfaces;
using Restaurant.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DAL.Repositories
{
    public class PositionsRepository : IBaseRepository<Position>
    {
        private readonly AppDbContext _dbContext;
        public PositionsRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Create(Position entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(Position entity)
        {
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public IQueryable<Position> GetAll()
        {
            var positions = _dbContext.Positions;
            return positions;
        }

        public async Task Update(Position entity)
        {
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
