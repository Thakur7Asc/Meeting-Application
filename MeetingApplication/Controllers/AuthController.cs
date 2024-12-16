//namespace MeetingApplicationAPI.Controllers;

//using MeetingApplicationAPI.Models;
//using System;
//using Microsoft.AspNetCore.Mvc;
//using MeetingApplicationAPI.Data;

//[Route("auth")]
//[ApiController]
//public class AuthController : ControllerBase
//{
//    private readonly ApplicationDbContext _context;

//    public AuthController(ApplicationDbContext context)
//    {
//        _context = context;
//    }

//    [HttpPost("register")]
//    public IActionResult Register(User user)
//    {
//        if (_context.Users.Any(u => u.Username == user.Username))
//            return BadRequest("Username already exists.");

//        _context.Users.Add(user);
//        _context.SaveChanges();

//        return Ok("User registered successfully.");
//    }

//    [HttpPost("login")]
//    public IActionResult Login(User loginUser)
//    {
//        var user = _context.Users.FirstOrDefault(u => u.Username == loginUser.Username && u.Password == loginUser.Password);

//        if (user == null)
//            return Unauthorized("Invalid username or password.");

//        return Ok("Login successful.");
//    }
//}




using MeetingApplicationAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<User> _userManager;
  //  private readonly SignInManager<User> _signInManager;
    private readonly IConfiguration _configuration;

    public AuthController(UserManager<User> userManager,  IConfiguration configuration)
    {
        _userManager = userManager;
    //    _signInManager = signInManager;
        _configuration = configuration;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        var user = new User { UserName = model.Email, Email = model.Email };
        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            return Ok("User registered successfully.");
        }
        return BadRequest(result.Errors);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
        {
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
        }
        return Unauthorized();
    }
}



