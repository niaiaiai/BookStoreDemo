using Ids4.Exceptions;
using Ids4.UserModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ids4.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        public UserController(UserManager<IdentityUser> userManager) 
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterInput input)
        {
            IdentityUser user = new IdentityUser { UserName = input.Email, Email = input.Email };
            var result = await _userManager.CreateAsync(user, input.Password);
            if (result.Succeeded)
            {
                return Ok();
            }
            string errors = JsonSerializer.Serialize(result.Errors);
            throw new CreateUserException(errors);
        }

        [HttpGet]
        public async Task Migration()
        {
            
        }
    }
}
