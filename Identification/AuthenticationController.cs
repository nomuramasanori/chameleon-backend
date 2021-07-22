using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Identification
{
    public class AuthenticationController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("/test")]
        public IActionResult Test()
        {
            return Content("hogehoge");
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("/login")]
        public IActionResult Login([Required][FromBody]LoginParam loginParam)
        {
            var handler = new JwtSecurityTokenHandler();
            var claims = new[] {
                new Claim(ClaimTypes.Name, loginParam.rawData)
            };
            var subject = new ClaimsIdentity(claims);
            var credentials = new SigningCredentials(
                JwtSecurityConfiguration.SecurityKey,
                SecurityAlgorithms.HmacSha256);
            var token = handler.CreateJwtSecurityToken(
                audience: loginParam.rawData,
                issuer: JwtSecurityConfiguration.Issuer,
                subject: subject,
                signingCredentials: credentials);
            var tokenText = handler.WriteToken(token);
            //var result = new
            //{
            //    token = tokenText
            //};



            //var cookieOptions = new Microsoft.AspNetCore.Http.CookieOptions()
            //{
            //    Path = "/",
            //    HttpOnly = false,
            //    IsEssential = true, //<- there
            //    Expires = DateTime.Now.AddMonths(1),
            //};
            //Response.Cookies.Append("jwt", tokenText, cookieOptions);

            return Ok(tokenText);
        }

        [Authorize]
        [AllowAnonymous]
        [HttpGet]
        [Route("/jwttest")]
        public IActionResult JwtTest()
        {
            Console.WriteLine(User.Claims);
            return Ok("hogehgoe");
        }

        public class LoginParam
        {
            public string rawData { get; set; }
        }
    }
}
