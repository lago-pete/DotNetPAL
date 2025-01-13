using DotnetAPI.Data;
using AutoMapper;
using DotnetAPI.Models;
using DotnetAPI.Dto;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAPI.Controllers;


[ApiController]
[Route("[controller]")]

public class UserJobInfoEFController : ControllerBase
{



    IUserRepository _userRepository;
    IMapper _mapper;

     
    public UserJobInfoEFController( IUserRepository userRepository)
    {
        

        _userRepository = userRepository;


        _mapper = new Mapper(new MapperConfiguration(cfg =>{
            cfg.CreateMap<UserJobInfo, UserJobInfo>();
        }));

    }
  




    [HttpGet("GetAllUsersJobs")]

    public IEnumerable<UserJobInfo> GetAllUserJobs ()
    {
        IEnumerable<UserJobInfo> users = _userRepository.GetAllUserJobs();
        return users;
    }






    [HttpGet("UsersJob/{userId}")]

    public UserJobInfo GetUsersJob(int userId)
    {
        return _userRepository.GetUsersJob(userId);
    }





    [HttpPost("AddUsersJob")]

    public IActionResult AddUsersJob (UserJobInfo user)
    {

        _userRepository.AddEntity<UserJobInfo>(user);

        if(_userRepository.SaveChanges())
        {
            return Ok();
        }

        throw new Exception ("Adding a New Uses's Salary Failed");
    }








    [HttpPut("EditUserJob")]

    public IActionResult EditUserSalary(UserJobInfo user)
    {
        UserJobInfo? userDb = _userRepository.GetUsersJob(user.UserId);

        if(userDb != null)
        {

            _mapper.Map(user, userDb);
            if(_userRepository.SaveChanges())
            {
                return Ok();
            }

            throw new Exception("Failed to Update User");
            
        }

        throw new Exception("Faild to Get User");


    }




    [HttpDelete("DeleteUserJob/{userId}")]

    public IActionResult DeleteUserSalary(int userId)
    {
        UserJobInfo? tbdUser =  _userRepository.GetUsersJob(userId);

        if(tbdUser != null)
        {
            _userRepository.RemoveEntity<UserJobInfo>(tbdUser);

            if(_userRepository.SaveChanges())
            {
                return Ok();
            }

            throw new Exception("Failed to Delete User");

        }


        throw new Exception("Failed to Get User");


    }




}




