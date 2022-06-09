using API.DTOs;
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        public readonly UserManager<AppUser> _userManager;
        public readonly SignInManager<AppUser> _signInMannager;

        public AccountController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInMannager)
        {
            _userManager = userManager;
            _signInMannager = signInMannager;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user= await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null) return Unauthorized();

            var result = await _signInMannager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            
            if(result.Succeeded)
            {
                return new UserDto
                {
                    DisplayName = user.DisplayName,
                    Image = "",
                    Token = "This will be atoken",
                    Username = user.UserName
                };
            }

            return Unauthorized();
        }
       
    }
}
