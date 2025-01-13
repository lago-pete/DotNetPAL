using DotnetAPI.Data;
using AutoMapper;
using DotnetAPI.Models;
using DotnetAPI.Dto;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAPI.Controllers;


[ApiController]
[Route("[controller]")]

public class UserEFController : ControllerBase
{
    IUserRepository _userRepository;
    IMapper _mapper;

     
    public UserEFController(IUserRepository userRepository)
    {
        _userRepository = userRepository;

        _mapper = new Mapper(new MapperConfiguration(cfg =>{
            cfg.CreateMap<UserToAddDto, User>();
            cfg.CreateMap<User,User>();
        }));

    }
  

    ///////////USER//////////////////
    
    [HttpGet("GetUsers")]
    public IEnumerable<User> GetUser()
    {
        IEnumerable<User> users = _userRepository.GetUser();
        return users;
    }



    [HttpGet ("GetSingleUser/{userId}")]
    public User GetSingleUser(int userId)
    {
        return _userRepository.GetSingleUser(userId);        
    }





    [HttpPost("AddUser")]
  
    public IActionResult AddUser(UserToAddDto user)
    {
        User userDb = _mapper.Map<User>(user);
        _userRepository.AddEntity<User>(userDb);
        if (_userRepository.SaveChanges())
        {
            return Ok();
        }
        throw new Exception("Failed to Add User");
    }




    [HttpPut("EditUser")]

    public IActionResult EditUser(User user)
    {
        User? userDb = _userRepository.GetSingleUser(user.UserId);

        if(userDb != null)
        {
            _mapper.Map(user,userDb);
            if (_userRepository.SaveChanges())
            {
                return Ok();
            }

            throw new Exception("Failed to Update User");

            
        }

        throw new Exception("Failed to Get User");

    }





    [HttpDelete("DeleteUser/{userId}")]
  
    public IActionResult DeleteUser(int userId)
    {
        User? userDb = _userRepository.GetSingleUser(userId);

        if(userDb != null)
        {
            _userRepository.RemoveEntity<User>(userDb);

                     
            if (_userRepository.SaveChanges())
            {
                return Ok();
            }

            throw new Exception("Failed to Delete User");

            
        }

        throw new Exception("Failed to Get User");

    }



//////////////USER//////////////////////////////////////////////////////






  








}




