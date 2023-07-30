using AutoMapper;
using KShop.Core.DTOs;
using KShop.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper _mapper;
        public AuthController(UserManager<ApplicationUser> userManager, IMapper mapper, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _signInManager = signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ApplicationUser user = _mapper.Map<ApplicationUser>(model);
            var result = await _userManager.CreateAsync(user, model.Password);
            string error = string.Empty;
            if (!result.Succeeded)
            {
                foreach (var e in result.Errors)
                    error += " , " + e.Description;
                return BadRequest(error);
            }
            //await _userManager.AddToRoleAsync(user, "User");
            return Ok();
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto model)
        {
            var userName = model.Email.ToUpper();
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.NormalizedEmail == userName || u.NormalizedUserName == userName);
            if (user == null)
                return Ok("You don't have an account");
            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, lockoutOnFailure: true);
            if (result.Succeeded)
                return Ok(user);
            return BadRequest("wrong username or password");
        }
    }
}
