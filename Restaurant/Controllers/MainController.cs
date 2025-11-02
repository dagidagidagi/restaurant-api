using Microsoft.AspNetCore.Mvc;
using Restaurant.Domain.Entities;
using Restaurant.Domain.DTO;
using Restaurant.DAL;
using Restaurant.Services;
using Microsoft.EntityFrameworkCore;
using Restaurant.Services.Interfaces;
using Restaurant.Services.Implementations;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Security.Claims;
namespace Restaurant.Controllers
{
    public class MainController : Controller
    {
        private readonly IPositionService _positionService;
        private readonly IAuthService _authService;
        private readonly IReservationService _reservationService;
        private readonly IDeliveryService _deliveryService;
        private readonly IUserService _userService;
        public MainController(IPositionService positionService, IAuthService authService, IReservationService reservationService, IDeliveryService deliveryService, IUserService userService)
        {
           
            _positionService = positionService;
            _authService = authService;
            _reservationService = reservationService;
            _deliveryService = deliveryService;
            _userService = userService;
        }
        [HttpGet("/menu/positions")]
        public async Task<IActionResult> GetPositions()
        {
                var response = await _positionService.GetAllWithSections();
                if(response.StatusCode == Domain.Enums.StatusCode.OK)
                {
                   return Ok(response.Data);
                }
                else if (response.StatusCode == Domain.Enums.StatusCode.InternalServerError)
                {
                  return Unauthorized(response.Description);
                }
                else
                {
                   return BadRequest(response.Description);
                }
        }
        [HttpPost("/login")]
        public async Task<IActionResult> AuthUser()
        {
            var userData = await Request.ReadFromJsonAsync<User>();
            var response = await _authService.AuthUser(userData.Login, userData.Password);
            if (response.StatusCode == Domain.Enums.StatusCode.OK)
            {
                UserDto user = response.Data;
                string JWTToken = _authService.GenerateToken(user);
                object res = new
                {
                   userInfo = user,
                   token = JWTToken
                };
                return Ok(res);
            }
            else if (response.StatusCode == Domain.Enums.StatusCode.AccessDenied)
            {
                return Unauthorized(response.Description);
            }
            else
            {
                return BadRequest(response.Description);
            }
        }
        [HttpPost("/registration")]
        public async Task<IActionResult> RegistrUser([FromBody] CreateUserDto user)
        {
            var response = await _userService.RegistrUser(user);
            if (response.StatusCode == Domain.Enums.StatusCode.OK)
            {
                return Ok();
            }
            else
            {
                return BadRequest(response.Description);
            }
        }
        // Закоментированный код для проверки работы авторизации
     //   [Route("/data")]
    //    [Authorize(Roles = "user")]
     //   public IActionResult SecuredData()
      //  {
       //     string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
       //     string userName = User.FindFirst(ClaimTypes.Name).Value;
       //     string role = User.FindFirst(ClaimTypes.Role).Value;
        //    return Ok(new
      //      {
      //          Id = userId,
      //          username = userName,
      //          role = role
   //         });
   //     }
        [HttpPost("/deliveries")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> AddDelivery([FromBody] CreateDeliveryDto data)
        {
            UserDto currentUser = new UserDto()
            {
                Id = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value),
                Name = User.FindFirst(ClaimTypes.Name).Value,
            };
            var response = await _deliveryService.CreateDelivery(data, currentUser);
            if (response.StatusCode == Domain.Enums.StatusCode.OK)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("/reservations")]
        public async Task<IActionResult> AddReservation([FromBody] CreateReservationsQuery query)
        {
            
            var response = await _reservationService.AddQuery(query);
            if (response.StatusCode == Domain.Enums.StatusCode.OK)
            {
                return Ok();
            }
            else
            {
                return BadRequest(response.Description);
            }
        }
    }
}
