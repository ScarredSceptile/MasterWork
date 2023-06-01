using MasterRankingAPI.APIModels;
using MasterRankingAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace MasterRankingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly AppDbContext _context;

        public LoginController(AppDbContext context)
        {
            _context = context;
        }

        //TODO: Add salt to the token
        [HttpPost("login")]
        public async Task<ActionResult<Appdata>> Login(LoginDetails loginDetails)
        {
            var user = await _context.User.Where(n => n.UserName == loginDetails.UserName).FirstOrDefaultAsync();
            if (user == null)
            {
                return BadRequest("User or password not Found");
            }
            HashAlgorithm sha = SHA256.Create();
            var pasw = sha.ComputeHash(Encoding.ASCII.GetBytes(loginDetails.Password + user.Salt));
            if (user.Password == Encoding.Default.GetString(pasw))
            {
                var token = Functions.GetSalt();
                var savedToken = Encoding.Default.GetString(sha.ComputeHash(Encoding.ASCII.GetBytes(token + user.Salt)));
                user.ConnectionHash = savedToken;
                await _context.SaveChangesAsync();
                return new Appdata() { UserName = user.UserName, IsLoggedIn = true, SelectedCourse = 1, ConnectionToken = token };
            }
            else
            {
                return BadRequest("User or password not Found");
            }
        }

        [HttpPost("register")]
        public async Task<ActionResult<Appdata>> Register(LoginDetails loginDetails)
        {
            if (_context.User.Any(n => n.UserName == loginDetails.UserName))
                return BadRequest("User already exists");
            var salt = Functions.GetSalt();
            HashAlgorithm sha = SHA256.Create();
            var pasw = Encoding.Default.GetString(sha.ComputeHash(Encoding.ASCII.GetBytes(loginDetails.Password + salt)));
            var token = Functions.GetSalt();
            var savedToken = Encoding.Default.GetString(sha.ComputeHash(Encoding.ASCII.GetBytes(token + salt)));
            var user = new User() { UserName = loginDetails.UserName, Password = pasw, Salt = salt, ConnectionHash = savedToken };
            _context.User.Add(user);
            await _context.SaveChangesAsync();
            return new Appdata() { UserName = user.UserName, ConnectionToken = token, IsLoggedIn = true, SelectedCourse = 1 };
        }

        [HttpPost("logout")]
        public async Task<ActionResult> Logout(Appdata appdata)
        {
            var loggedin = await Functions.IsUserLoggedIn(_context, appdata);
            if (!loggedin)
                return BadRequest("User has already been logged out");

            var user = _context.User.Where(n => n.UserName == appdata.UserName).First();
            HashAlgorithm sha = SHA256.Create();
            var token = Functions.GetSalt();
            user.ConnectionHash = Encoding.Default.GetString(sha.ComputeHash(Encoding.ASCII.GetBytes(token + user.Salt)));
            _context.SaveChanges();
            return Ok();

        }
    }
}
