using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace TokenAuthentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post(LoginViewModel loginViewModel)
        {
            //Check if user exists or not
            if (loginViewModel.UserName == "Hamed" && loginViewModel.Password == "123456")
            {

                //Generate Token
                Guid userId = Guid.NewGuid();
                string fullName = loginViewModel.UserName;
                string token = GenerateToken(userId);
                var info = new AuthenticationViewModel {
                FullName= fullName,
                Token= token,
                UserId =userId 
                };
                return Ok(info);
            }


            return BadRequest();
        }

        private static string GenerateToken(Guid userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var Key = Encoding.UTF8.GetBytes("this is my custom Secret key for authentication");//from appsetting
            int tokenTimeOut = 10;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                              {
                                    new Claim ("userGuid",userId.ToString())
                              }),
                Expires = DateTime.UtcNow.AddMinutes(tokenTimeOut),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }







    public class LoginViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class AuthenticationViewModel
    {
        public Guid UserId { get; set; }
        public string FullName { get; set; }
        public string Token { get; set; }
    }
}
