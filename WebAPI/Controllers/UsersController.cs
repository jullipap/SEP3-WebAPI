using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application;
using Microsoft.AspNetCore.Mvc;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using DateTime = System.DateTime;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;
using User = Domain.Models.User;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IConfiguration config;
    private readonly IUserLogic userLogic;

    public UsersController(IConfiguration config,IUserLogic userLogic)
    {
        this.config = config;
        this.userLogic = userLogic;
    }
    
    [HttpPost]
    public async Task<ActionResult<User>> Register([FromBody]RegisterDto dto)
    {
        try
        {
            User user = await userLogic.Register(dto);
            return Ok(user);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpPost, Route("authentication")]
    public async Task<ActionResult<User>> Login ([FromBody]LoginDto dto)
    {
        try
        {
            User user = await userLogic.Login(dto);
            string token = GenerateJwt(user);
            return Ok(token);

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<User>> GetUserByIdAsync([FromRoute] int id)
    {
        try
        {
            User user = await userLogic.GetUserByIdAsync(id);
            return Ok(user);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    [Authorize]
   [HttpPatch, Route("license")]
    public async Task<ActionResult> UpdateTheLicenseNo([FromBody]UpdateLicenseDto dto)
    {
        
        try
        {
            await userLogic.UpdateTheLicenseNo(dto);
            string token = GenerateJwt(new User(){Id = dto.UserId, LicenseNumber = dto.LicenseNo});
            return Ok(token);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    private string GenerateJwt(User user)
    {
        List<Claim> claims = GenerateClaims(user);
        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
        SigningCredentials signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
        
        JwtHeader header = new JwtHeader(signIn);
    
        JwtPayload payload = new JwtPayload(
            config["Jwt:Issuer"],
            config["Jwt:Audience"],
            claims, 
            null,
            DateTime.UtcNow.AddMinutes(60));
    
        JwtSecurityToken token = new JwtSecurityToken(header, payload);
    
        string serializedToken = new JwtSecurityTokenHandler().WriteToken(token);
        return serializedToken;   
    }

    private List<Claim> GenerateClaims(User user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, config["Jwt:Subject"]),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
            new Claim("Id", user.Id.ToString()),
            new Claim("hasLicenseNumber", user.LicenseNumber == null ? "False" : "True")

        };

        return claims.ToList();
    }
}