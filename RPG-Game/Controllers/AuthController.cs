using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RPG_Game.Data;
using RPG_Game.Dtos.User;
using RPG_Game.Models;

namespace RPG_Game.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;

        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register(UserRegisterDto register)
        {

            var response = await _authRepository.Register(new User { Username = register.Username, }, register.Password);

            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);

        }

        [HttpPost("login")]
        public async Task<ActionResult<ServiceResponse<string>>> Login(UserLoginDto login)
        {

            var response = await _authRepository.Login(login.UserName, login.Password);

            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);

        }

    }
}
