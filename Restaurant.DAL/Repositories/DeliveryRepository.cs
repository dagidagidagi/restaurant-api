using Restaurant.DAL.Interfaces;
using Restaurant.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DAL.Repositories
{
    public class DeliveryRepository : IBaseRepository<Delivery>
    {
        private readonly AppDbContext _dbContext;
        public DeliveryRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Create(Delivery entity)
        {
            await _dbContext.Deliveries.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(Delivery entity)
        {
            _dbContext.Deliveries.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public IQueryable<Delivery> GetAll()
        {
            return _dbContext.Deliveries;
        }

        public async Task Update(Delivery entity)
        {
            _dbContext.Deliveries.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
