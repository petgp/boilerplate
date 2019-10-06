using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Boilerplate.Models;
using Boilerplate.SQLite;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.EntityFrameworkCore;

namespace Boilerplate.Controllers
{

  [Route("api/[controller]")]
  [ApiController]
  public class ApplicationUserController : ControllerBase
  {
    private UserManager<ApplicationUser> _userManager;
    private SignInManager<ApplicationUser> _signInManager;
    private readonly ApplicationSettings _appSettings;

    public ApplicationUserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IOptions<ApplicationSettings> appSettings)
    {
      _appSettings = appSettings.Value; //accessing injected instances from services.AddDefaultIdentity
      _signInManager = signInManager;
      _userManager = userManager;
    }

    [HttpGet]
    public ObjectResult GetAllUsers()
    {
      try
      {
        var result = _userManager.Users;
        return Ok(result);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex);
        return UnprocessableEntity(ex.Message);
      }
    }
    [HttpGet("{id}")]
    public async Task<Object> GetSingleUser(string id)
    {
      try
      {
        ApplicationUser result = await _userManager.FindByIdAsync(id);
        return Ok(result);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex);
        return UnprocessableEntity(ex.Message);
      }
    }
    [HttpPost]
    [Route("update")]
    public async Task<Object> UpdateUser(ApplicationUser user)
    {
      try
      {
        ApplicationUser model = await _userManager.FindByIdAsync(user.Id);
        // Update it with the values from the user model
        model.UserName = user.UserName;
        model.Email = user.Email;
        model.EmailConfirmed = user.EmailConfirmed;
        model.PhoneNumber = user.PhoneNumber;
        model.PhoneNumberConfirmed = user.PhoneNumberConfirmed;
        model.TwoFactorEnabled = user.TwoFactorEnabled;
        model.LockoutEnd = user.LockoutEnd;
        model.LockoutEnabled = user.LockoutEnabled;
        model.AccessFailedCount = user.AccessFailedCount;
        var result = await _userManager.UpdateAsync(model);
        return Ok();
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex);
        return UnprocessableEntity(ex.Message);
      }
    }
    [HttpPost]
    [Route("Register")]
    //POST : /api/ApplicationUser/Register

    public async Task<Object> PostApplicationUser(ApplicationUserModel model)
    {
      try
      {
        var applicationUser = new ApplicationUser()
        {
          UserName = model.UserName,
          Email = model.Email,
        };
        var result = await _userManager.CreateAsync(applicationUser, model.Password);
        return Ok(result);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex);
        return UnprocessableEntity(ex.Message);
      }
    }

    [HttpPost]
    [Route("Login")]
    //POST /ApplicationUser/Login

    public async Task<IActionResult> Login(LoginModel model)
    {
      try
      {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
        {
          var tokenDescriptor = new SecurityTokenDescriptor
          {
            Subject = new ClaimsIdentity(new Claim[]
              {
                        new Claim("UserId", user.Id.ToString())
              }),
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
          };

          var tokenHandler = new JwtSecurityTokenHandler();
          var securityToken = tokenHandler.CreateToken(tokenDescriptor);
          var token = tokenHandler.WriteToken(securityToken);
          return Ok(new { token });
        }
        else
        {
          return BadRequest(new { message = "Email or password is incorrect" });
        }
      }
      catch (Exception ex)
      {
        string mes = ex.Message;
        return BadRequest(new { message = "Email or password is incorrect" });
      }
    }
  }
}
