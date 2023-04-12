using Domain;
using Domain.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Service.AuthenService;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NeticoProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly IAuthenticate _authenticate;
        private readonly IConfiguration _configuration;
        public LoginController(IAuthenticate authenticate, IConfiguration configuration, ILogger<LoginController> logger)
        {
            _authenticate = authenticate;
            _configuration = configuration;
            _logger = logger;
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            try
            {
                _logger.LogInformation(model.LoginName);
                var secretAppsettings = _configuration.GetValue<string>("AppSettings:SecretKey");
                var tokenHandler = new JwtSecurityTokenHandler();
                if (model.LoginName is null || model.Password is null)
                {
                    return BadRequest("Invalid client request");
                }
                else
                {
                    var userLogin = _authenticate.CheckLogin(model);
                    if (userLogin != null)
                    {
                        var secretKey = Encoding.UTF8.GetBytes(secretAppsettings);
                        var tokenDescriptor = new SecurityTokenDescriptor
                        {
                            Subject = new ClaimsIdentity(new[] { new Claim("id", userLogin.Id.ToString()) }),
                            Expires = DateTime.UtcNow.AddMinutes(30),
                            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature)
                        };
                        var token = tokenHandler.CreateToken(tokenDescriptor);
                        var tokenResponse = tokenHandler.WriteToken(token);
                        var response = new AuthenResponse();
                        response.User = userLogin;
                        response.Token = tokenResponse;
                        return Ok(response);
                    }
                    else
                    {
                        return Ok("Username or Password is incorrect");
                    }
                }
            }catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);

            }

        }
    }
}
