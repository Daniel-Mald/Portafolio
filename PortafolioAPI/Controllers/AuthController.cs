using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.Mvc;
using PortafolioAPI.Models.DTOs;
using PortafolioAPI.Repositories.Interfaces;

namespace PortafolioAPI.Controllers
{
    public class AuthController : Controller
    {
        IAuthRepository authRepository;
        public AuthController(IAuthRepository authRepository)
        {
            this.authRepository = authRepository;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            var response = await authRepository.Login(dto);
            if (response.Success)
            {
                return Ok(response.Data);
            }
            
            return BadRequest(response.Errors);
        }

        [HttpPost("CreateUser")]
        public IActionResult Register(AddUserDTO dto)
        {
            var response = authRepository.CreateUser(dto);
            if(response.Success)
            {
                return Ok(response.Message);
            }
            return BadRequest(response.Errors);
        }

        
    }
}
