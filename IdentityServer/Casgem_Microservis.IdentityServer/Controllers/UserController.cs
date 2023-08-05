using Casgem_Microservis.IdentityServer.Dtos;
using Casgem_Microservis.IdentityServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Casgem_Microservis.IdentityServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        [HttpPut]
        public async Task<IActionResult> SignUp(SignUpDto signUpDto)
        {
            var user = new ApplicationUser
            {
                UserName = signUpDto.UserName,
                Email = signUpDto.Email,
                City = signUpDto.City,
            };
            var result = await _userManager.CreateAsync(user,signUpDto.Password);
            if (result.Succeeded)
            {
                return Ok(result);
            }
            else
            {
                return Ok("Bir Hata oluştu");
            }
        }
    }
}
