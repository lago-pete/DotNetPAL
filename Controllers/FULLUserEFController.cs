using DotnetAPI.Data;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Mvc;


namespace DotnetAPI.Controllers;


[ApiController]
[Route("[controller]")]

public class FULLUserEFController : ControllerBase
{
    IUserRepository _userRepository;


    public FULLUserEFController(IUserRepository userRepository, IConfiguration config)
    {
        _userRepository = userRepository;

    }


    ///////////USER//////////////////

    [HttpGet("GetFULLUsers")]
    public IEnumerable<UserComplete> GetFULLUsers()
    {
        IEnumerable<UserComplete> FULLusers =  _userRepository.GetFULLUser();
        return FULLusers;
    }



    [HttpGet("GetSingleFULLUsers/{userId}")]
    public UserComplete GetSingleFULLUsers(int userId)
    {
        return _userRepository.GetSingleFULLUser(userId);
    }



}




