using Microsoft.EntityFrameworkCore;
using Restaurant.DAL.Interfaces;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Enums;
using Restaurant.Domain.Response;
using Restaurant.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Services.Implementations
{
    public class PositionService : IPositionService
    {
        private readonly IBaseRepository<Position> _positionRepository;
        public PositionService(IBaseRepository<Position> positionRepository)
        {
            _positionRepository = positionRepository;
        }

        public async Task<IBaseResponse<IEnumerable<Position>>> GetAll()
        {
           try
           {
                var result = await _positionRepository.GetAll().ToListAsync();
                return new BaseResponse<IEnumerable<Position>>
                {
                    Description = "Получение всех позиций меню из таблицы Positions",
                    StatusCode = StatusCode.OK,
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<Position>>
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }

        }
        public async Task<IBaseResponse<IEnumerable<object>>> GetAllWithSections()
        {
            try
            {
                IEnumerable<object> positions = await _positionRepository.GetAll().Include(p => p.Section)
                       .Select(p => new
                       {
                           p.Id,
                           p.Name,
                           p.Description,
                           p.Cost,
                           p.ImageSource,
                           SectionId = p.Section.Id,
                           SectionName = p.Section.Name
                       })
                        .ToListAsync();


                return new BaseResponse<IEnumerable<object>>
                {
                    Description = "Получение меню с разделами",
                    StatusCode = StatusCode.OK,
                    Data = positions
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<object>>
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
