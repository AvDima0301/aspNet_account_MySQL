using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using News.Web.Data.Entities.Identity;
using News.Web.Models;

namespace News.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        public AccountController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody]RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            AppUser appUser = new AppUser()
            {
                Email = model.Email,
                UserName=model.Email,
                FirstName = model.FirstName,
                SecondName = model.SecondName,
                Photo = model.Photo,
                Phone = model.Phone
            };
            var result = await _userManager.CreateAsync(appUser, model.Password);
            return Ok(new { token="Сало" });
        }
    }
}
