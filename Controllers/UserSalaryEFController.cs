using DotnetAPI.Data;
using AutoMapper;
using DotnetAPI.Models;
using DotnetAPI.Dto;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAPI.Controllers;


[ApiController]
[Route("[controller]")]

public class UserSalaryEFController : ControllerBase
{

    IUserRepository _userRepository;
    IMapper _mapper;

     
    public UserSalaryEFController(IUserRepository userRepository)
    {
        _userRepository = userRepository;

        _mapper = new Mapper(new MapperConfiguration(cfg =>{
            cfg.CreateMap<UserSalary, UserSalary>();
        }));

    }
  




    [HttpGet("GetAllSalaries")]

    public IEnumerable<UserSalary> GetAllUserSalaries ()
    {
        IEnumerable<UserSalary> users = _userRepository.GetAllUserSalaries();
        return users;
    }






    [HttpGet("UserSalary/{userId}")]

    public UserSalary GetUserSalary(int userId)
    {
        return _userRepository.GetUserSalary(userId);
    }





    [HttpPost("AddUserSalary")]

    public IActionResult AddUserSalary (UserSalary user)
    {
        _userRepository.AddEntity<UserSalary>(user);

        if(_userRepository.SaveChanges())
        {
            return Ok();
        }

        throw new Exception ("Adding a New Uses's Salary Failed");

    }








    [HttpPut("EditUserSalary")]

    public IActionResult EditUserSalary(UserSalary user)
    {
        UserSalary? userDb =  _userRepository.GetUserSalary(user.UserId);

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




    [HttpDelete("DeleteUserSalary/{userId}")]

    public IActionResult DeleteUserSalary(int userId)
    {
        UserSalary? tbdUser = _userRepository.GetUserSalary(userId);;

        if(tbdUser != null)
        {
            _userRepository.RemoveEntity<UserSalary>(tbdUser);

            if(_userRepository.SaveChanges())
            {
                return Ok();
            }

            throw new Exception("Failed to Delete User");

        }


        throw new Exception("Failed to Get User");


    }




}




