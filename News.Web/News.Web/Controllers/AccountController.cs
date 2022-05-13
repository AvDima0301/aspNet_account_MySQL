using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using News.Web.Data;
using News.Web.Data.Entities.Identity;
using News.Web.Helpers;
using News.Web.Models;
using News.Web.Services;
using System.Data.Entity.Core;
using System.Drawing.Imaging;
using System.Security.Cryptography;

namespace News.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly IJwtTokenService _tokenService;
        private readonly AppEFContext _context;
        public AccountController(UserManager<AppUser> userManager, IMapper mapper, AppEFContext context,
            IJwtTokenService tokenService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _context = context;
            _tokenService = tokenService;
        }
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            var img = ImageWorker.FromBase64StringToImage(model.Photo);
            string randomFilename = Path.GetRandomFileName() + ".jpeg";
            var dir = Path.Combine(Directory.GetCurrentDirectory(), "uploads", randomFilename);
            img.Save(dir, ImageFormat.Jpeg);
            var user = _mapper.Map<AppUser>(model);
            user.Photo = randomFilename;
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                return BadRequest(new { errors = result.Errors });
            return Ok(new { token = _tokenService.CreateToken(user) });
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                throw new ObjectNotFoundException("Error: Invalid Email\nUser not found");
            }

            if (await _userManager.CheckPasswordAsync(user, model.Password))
            {
                return Ok(new { token = _tokenService.CreateToken(user) });
            }
            throw new Exception($"Password is wrong");
        }

        [HttpGet]
        [Authorize]
        [Route("users")]
        public async Task<IActionResult> Users()
        {
            //throw new AppException("Problem server get users");
            //Thread.Sleep(2000);
            var list = _context.Users.Select(x => _mapper.Map<UserItemViewModel>(x)).ToList();

            return Ok(list);
        }
    }
}
