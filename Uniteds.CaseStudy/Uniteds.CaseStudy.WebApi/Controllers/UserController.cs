using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Uniteds.CaseStudy.Domain.DTOs;
using Uniteds.CaseStudy.Domain.Interfaces;

namespace Uniteds.CaseStudy.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserDto userdto)
        {
            var user = _userRepository.GetUserByEmail(userdto.Email);

            if (user == null)
            {
                return NotFound("Kullanıcı bulunamadı");
            }

            if (user.Password != userdto.Password)
            {
                return BadRequest("Geçersiz parola");
            }

            // Giriş başarılı, kullanıcı bilgilerini döndür
            return Ok(user);
        }
    }
}
