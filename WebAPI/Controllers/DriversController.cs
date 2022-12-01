using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.IdentityModel.Tokens;
using DateTime = System.DateTime;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class DriversController : ControllerBase
{
    private readonly IConfiguration config;
    private readonly IDriverLogic driverLogic;

    public DriversController(IDriverLogic driverLogic)
    {
        this.driverLogic = driverLogic;
    }
    
    [HttpPost]
    public async Task<ActionResult<Driver>> Register([FromBody]RegisterDto dto)
    {
        try
        {
            Driver driver = await driverLogic.Register(dto);
            return Ok(driver);
            // return Created($"/user/{driver.Id}", driver);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpPost, Route("login")]
    public async Task<ActionResult<Driver>> Login ([FromBody]LoginDto dto)
    {
        try
        {
            var user = await driverLogic.Login(dto);
            string token = GenerateJwt(user);
            return Ok(token);

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    private string GenerateJwt(Driver driver)
    {
        List<Claim> claims = GenerateClaims(driver);
    
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

    private List<Claim> GenerateClaims(Driver driver)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, config["Jwt:Subject"]),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
            new Claim(ClaimTypes.Name, driver.Name),
            new Claim("Id", driver.Id.ToString())
            //I guess there will be an Id added in the Driver's class
        };

        return claims.ToList();
    }
}