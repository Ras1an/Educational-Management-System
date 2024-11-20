using Dto;
using Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repository;
using System.Linq.Expressions;

namespace Cls.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ApiBaseController
{
    public UserController(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
    [HttpGet("GetAll")]
    public IActionResult GetAll()
    {
        return Ok(_unitOfWork.Users.GetAll());
    }


    //
    [HttpGet("GetUser")]
    public IActionResult GetUser(string username)
    {
        Expression<Func<User, bool>> criteria = u => u.Username == username;

        // Call the Find method
        var user = _unitOfWork.Users.Find(criteria);

        // Check if the user was found

        return Ok(user);
    }


    //    var userId = User.Claims.FirstOrDefault(c => c.Type == "id")?.Value; // Extract user ID from token

    [HttpGet("GetUserById")]
    public IActionResult GetUserById(int id)
    {
        Expression<Func<User, bool>> criteria = u => u.UserId == id;

        // Call the Find method
        var user = _unitOfWork.Users.Find(criteria);

        // Check if the user was found

        return Ok(user);
    }


   // [Authorize(Roles ="1")]
    [HttpGet("GetProfile")]
    public IActionResult GetProfile()
    {
        int userId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == "id")?.Value); // Extract user ID from token

        var user = GetUserById(userId); // Get user profile

        if (user == null)
            return NotFound();

        return Ok(user);
    }

    [HttpGet("EnrolledCourses")]
    public IActionResult EnrolledCourses(int id)
    {
        Expression<Func<Enrollment, bool>> criteria = u => u.StudentId == id;

        var Enrollments = _unitOfWork.Enrollments.FindAll(criteria);


        //Expression<Func<User, bool>> criteria = u => u.UserId == id;

        //// Call the Find method
        //var user = _unitOfWork.Users.Find(criteria);

        return Ok(Enrollments);
    }



    //
    [HttpGet("GetAllAsync")]
    public async Task<IActionResult> GetAllAsync()
    {
        return Ok(await _unitOfWork.Users.GetAllAsync());
    }

    [HttpPost("Add")]
    public IActionResult Add(User user)
    {

        User User = new User()
        {
            Name = user.Name,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            Address = user.Address,
            Username = user.Username,
            Password = user.Password,
            UserType = user.UserType

        };
        _unitOfWork.Users.Add(User);
        _unitOfWork.Save();
        return Ok("Done");
    }

    [HttpPut("Update")]
    public IActionResult Update(User model)
    {
        _unitOfWork.Users.Update(model);
        _unitOfWork.Save();
        return Ok(model);
    }
    [HttpDelete("Delete")]
    public IActionResult Delete(User model)
    {
        _unitOfWork.Users.Delete(model);
        _unitOfWork.Save();
        return Ok("delete");
    }

    [HttpDelete("DeleteById")]
    public IActionResult Delete(int id)
    {
        var emp = _unitOfWork.Users.GetById(id);
        _unitOfWork.Users.Delete(emp);
        _unitOfWork.Save();
        return Ok("delete");
    }

}
