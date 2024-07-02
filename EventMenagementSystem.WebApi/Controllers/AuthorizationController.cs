using EventManagementSystem.Model;
using EventManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class AuthorizationController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IConfiguration _configuration;

    public AuthorizationController(IUserService userService, IConfiguration configuration)
    {
        _userService = userService;
        _configuration = configuration;
    }

    [HttpPost("Register")]
    public async Task<ActionResult<User>> Register(User request)
    {
        var existingUser = await _userService.GetUserByUsername(request.Username);
        if (existingUser != null)
        {
            return BadRequest("Username already exists.");
        }

        var user = new User
        {
            Username = request.Username,
            Password = request.Password, 
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            UserType = request.UserType
        };

        var createdUser = await _userService.CreateUser(user);

        return Ok(createdUser);
    }

    [HttpPost("Login")]
    public async Task<ActionResult<string>> Login(LoginRequest request)
    {
        var user = await _userService.GetUserByUsername(request.Username);
        if (user == null)
        {
            return BadRequest("User not found.");
        }

        // Validate password (this should ideally be done with hashed comparison)
        if (request.Password != user.Password)
        {
            return BadRequest("Wrong password.");
        }

        string token = CreateToken(user);

        // Log the token to console
        Console.WriteLine($"Generated JWT Token: {token}");

        return Ok(token);
    }

    [HttpGet("DecodeToken")]
    public ActionResult<DecodedToken> DecodeToken(string jwtToken)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["AppSettings:Token"]);
            tokenHandler.ValidateToken(jwtToken, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtTokenDecoded = (JwtSecurityToken)validatedToken;

            // Extract claims from decoded token
            var username = jwtTokenDecoded.Claims.First(x => x.Type == ClaimTypes.Name).Value;
            var userType = jwtTokenDecoded.Claims.First(x => x.Type == ClaimTypes.Role).Value;

            return Ok(new DecodedToken
            {
                Username = username,
                UserType = userType
            });
        }
        catch (Exception ex)
        {
            return BadRequest("Invalid token: " + ex.Message);
        }
    }

    private string CreateToken(User user)
    {
        List<Claim> claims = new List<Claim> {
        new Claim(ClaimTypes.Name, user.Username),
        new Claim(ClaimTypes.Role, user.UserType.ToString())
    };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AppSettings:Token"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(7), // Token traje 5 minuta
                signingCredentials: creds
             );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }

    [HttpGet("GetUserByToken")]
    public async Task<ActionResult<User>> GetUserByToken(string jwtToken)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["AppSettings:Token"]);
            tokenHandler.ValidateToken(jwtToken, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtTokenDecoded = (JwtSecurityToken)validatedToken;

            // Extract username from decoded token
            var username = jwtTokenDecoded.Claims.First(x => x.Type == ClaimTypes.Name).Value;

            // Get user by username
            var user = await _userService.GetUserByUsername(username);
            if (user == null)
            {
                return BadRequest("User not found.");
            }

            return Ok(user);
        }
        catch (Exception ex)
        {
            return BadRequest("Invalid token: " + ex.Message);
        }
    }


    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class DecodedToken
    {
        public string Username { get; set; }
        public string UserType { get; set; }
    }
}

