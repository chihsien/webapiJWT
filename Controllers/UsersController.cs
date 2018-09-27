using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using System.Text;
using System.Security.Claims;

namespace webapiJWT.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate(string username, string password)
        {
            // check database to make sure username & password is correct
            // otherwise return not authorized message
            
            string secret = "THIS IS USED TO SIGN AND VERIFY JWT TOKENS, REPLACE IT WITH YOUR OWN SECRET, IT CAN BE ANY STRING";
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] 
                {
                    new Claim(ClaimTypes.Name, "101")
                    // new Claim(JwtRegisteredClaimNames.)
                }),
                // Expires = DateTime.UtcNow.AddDays(7),
                NotBefore = DateTime.Now,
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            
            // return basic user info (without password) and token to store client side
            return Ok(new {
                Id = "101",
                Username = "user.Username",
                FirstName = "user.FirstName",
                LastName = "user.LastName",
                Token = tokenString
            });


            // return null;
        }

       [HttpGet]
        public IActionResult GetAll()
        {
            List<User> _users = new List<User>
            { 
                new User { Id = 1, FirstName = "Test", LastName = "User", Username = "test", Password = "test" } 
            }; 

            _users.Add(new User(){
                 Id = 2, FirstName = "Test2", LastName = "User2", Username = "test2", Password = "test2" 
            });
            return Ok(_users);
            // var users =  _userService.GetAll();
            // var userDtos = _mapper.Map<IList<UserDto>>(users);
            // return Ok(userDtos);
        }

        // public class User
        // {
        //     public int Id { get; set; }
        //     public string FirstName { get; set; }
        //     public string LastName { get; set; }
        //     public string Username { get; set; }
        //     public string Password { get; set; }
        //     public string Token { get; set; }
        // }

        // GET api/values
        // [HttpGet]
        // public IEnumerable<string> Get()
        // {
        //     return new string[] { "value1", "value2" };
        // }

        // // GET api/values/5
        // [HttpGet("{id}")]
        // public string Get(int id)
        // {
        //     return "value";
        // }

        // // POST api/values
        // [HttpPost]
        // public void Post([FromBody]string value)
        // {
        // }

        // // PUT api/values/5
        // [HttpPut("{id}")]
        // public void Put(int id, [FromBody]string value)
        // {
        // }

        // // DELETE api/values/5
        // [HttpDelete("{id}")]
        // public void Delete(int id)
        // {
        // }
    }
}
