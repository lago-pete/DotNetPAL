using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using DotnetApi.Dtos;
using DotnetAPI.Data;
using DotnetAPI.Dto;
using DotnetAPI.Dtos;
using DotnetAPI.Helpers;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
namespace DotnetAPI.Controllers;





[Authorize]                     //This requires that the user must have a Token to use Controller
[ApiController]
[Route("[controller]")]


public class AuthController : ControllerBase
{


    private readonly DataContextEF _entityFramework;

    private readonly IConfiguration _config;

    private readonly AuthHelper _authHelper;

    IMapper _mapper;


    public AuthController(IConfiguration config)
    {
        _config = config;
        _entityFramework = new DataContextEF(config);
        _authHelper = new AuthHelper(config);


        _mapper = new Mapper(new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<UserForRegistrationDto, Auth>();
            cfg.CreateMap<UserForRegistrationDto, User>();

        }));

    }




    [AllowAnonymous]                                                //Except for this call, this is where a user without a token will be sent ( it gets made here)
    [HttpPost("Register")]

    public IActionResult Register(UserForRegistrationDto userForReg)
    {
        if (userForReg.Password != userForReg.PasswordConformation)
        {
            throw new Exception(" Passwords do not match!");
        }


        byte[] passwordSalt = new byte[128 / 8];

        using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
        {
            rng.GetNonZeroBytes(passwordSalt);
        }



        string passwordSaltPlusSalt = _config.GetSection("AppSettings:PasswordKey").Value + Convert.ToBase64String(passwordSalt);

        byte[] passwordHash = _authHelper.GetPasswordHash(userForReg.Password, passwordSalt);

        User userDb = _mapper.Map<User>(userForReg);
        _entityFramework.Users.Add(userDb);

        Auth auth = new Auth
        {
            Email = userForReg.Email,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt
        };


        _entityFramework.Auths.Add(auth);

        if (_entityFramework.SaveChanges() > 0)
        {
            return Ok();
        }

        throw new Exception("Failed to Add User");


    }



    [AllowAnonymous]
    [HttpPost("Login")]
    public IActionResult Login(UserForLoginDto userForLog)
    {
        UserForConfirmationDto authCheck = new UserForConfirmationDto();

        authCheck.PasswordHash = _entityFramework.Auths.Where(u => u.Email == userForLog.Email).Select(u => u.PasswordHash).FirstOrDefault();
        authCheck.PasswordSalt = _entityFramework.Auths.Where(u => u.Email == userForLog.Email).Select(u => u.PasswordSalt).FirstOrDefault();


        byte[] passwordHashCheck = _authHelper.GetPasswordHash(userForLog.Password, authCheck.PasswordSalt);


        int userId = _entityFramework.Users.Where(u => u.Email == userForLog.Email).Select(u => u.UserId).FirstOrDefault();

        if (passwordHashCheck.SequenceEqual(authCheck.PasswordHash))
        {
            return Ok(new Dictionary<string, string>
            {
                {"token", _authHelper.CreateToken(userId)}
            });
        }


        return StatusCode(401, "Wrong Password");

    }

    [HttpGet("RefreshToken")]

    public IActionResult RefreshToken()
    {
        int userId = Convert.ToInt32(User.FindFirst("userId")?.Value + " ");

        return Ok(new Dictionary<string, string>
            {
                {"token", _authHelper.CreateToken(userId)}
        });

    }




}