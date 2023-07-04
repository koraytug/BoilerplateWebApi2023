using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BoilerplateWebApi.Controllers
{
    [Route("api/authentication")]
    public class AuthenticationController : Controller
    {
        public IConfiguration configuration { get; }

        public class AuthanticationRequestBody
        {
            public string? Username { get; set; }
            public string? Password { get; set; }
        }

        private class CustomerInfoUser
        {
            public int UserId { get; set; }
            public string UserName { get; set; } = string.Empty;
            public string FirstName { get; set; } = string.Empty;
            public string LastName { get; set; } = string.Empty;
            public string City { get; set; } = string.Empty;

            public CustomerInfoUser(int userId, string userName, string firstName, string lastName, string city)
            {
                UserId = userId;
                UserName = userName;
                FirstName = firstName;
                LastName = lastName;
                City = city;
            }
        }

        public AuthenticationController(IConfiguration configurationobj)
        {
            configuration = configurationobj ??
                throw new ArgumentNullException(nameof(configurationobj));
        }

        [HttpPost("authenticate")]
        public ActionResult<string> Authenticate(AuthanticationRequestBody authanticationRequestBody)
        {
            // step 1 validate the username/password
            var user = ValidateUserCredentials(
                authanticationRequestBody.Username,
                authanticationRequestBody.Password);

            if (user == null)
            {
                return Unauthorized();
            }

            // step 2: create a token
            var securityKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(configuration["Authentication:SecretForKey"])
                );

            var signingCredentials = new SigningCredentials( securityKey ,SecurityAlgorithms.HmacSha256);

            // the claims that
            var claimsForToken = new List<Claim>();
            claimsForToken.Add(new Claim("sub", user.UserId.ToString()));
            claimsForToken.Add(new Claim("given_name", user.UserName.ToString()));
            claimsForToken.Add(new Claim("family_name", user.LastName.ToString()));
            claimsForToken.Add(new Claim("city", user.City.ToString()));

            var jwtSecurityToken = new JwtSecurityToken(
                configuration["Authentication:Issuer"],
                configuration["Authentication:Audience"],
                claimsForToken,
                DateTime.Now,
                DateTime.Now.AddHours(1),
                signingCredentials
                );

            var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            
            return Ok(tokenToReturn);
        }

        private CustomerInfoUser ValidateUserCredentials(string? username, string? password)
        {
            //we dont have a user DB or table. If you have, cjheck the passed-through
            // username/password against what's stored in the db,
            //
            // for demo purposes, assumed the credentials are valid

            // return a new CustomerInfoUser (values would normally come from user DB/table)
            return new CustomerInfoUser(
                1,
                username ?? "",
                "Kevin",
                "Dockx",
                "Antwerp"
                );
        }
    }
}
