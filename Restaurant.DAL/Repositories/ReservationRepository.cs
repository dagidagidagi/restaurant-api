using Restaurant.DAL.Interfaces;
using Restaurant.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DAL.Repositories
{
    public class ReservationRepository : IBaseRepository<ReservationsQuery>
    {
        private readonly AppDbContext _dbContext;
        public ReservationRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Create(ReservationsQuery entity)
        {
            await _dbContext.ReservationsQueries.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(ReservationsQuery entity)
        {
            _dbContext.ReservationsQueries.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public IQueryable<ReservationsQuery> GetAll()
        {
            return _dbContext.ReservationsQueries;
        }

        public async Task Update(ReservationsQuery entity)
        {
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
