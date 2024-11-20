using Dto;
using Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repository;

namespace Cls.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CourseController : ApiBaseController
{
    public CourseController(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
    [HttpGet("GetAll")]
    public IActionResult GetAll()
    {
        return Ok(_unitOfWork.Courses.GetAll());
    }
    [HttpGet("GetAllAsync")]
    public async Task<IActionResult> GetAllAsync()
    {
        return Ok(await _unitOfWork.Courses.GetAllAsync());
    }

    [HttpPost("Add")]
    public IActionResult Add(CourseDto model)
    {
        //Automapper Package you can use it if you like 
        Course Course = new Course()
        {
            CourseCode = model.CourseCode,
            Name = model.CourseName,
            CourseSpecialityId = model.SpecialityId

        };
        _unitOfWork.Courses.Add(Course);
        _unitOfWork.Save();
        return Ok("Done");
    }

    [HttpPut("Update")]
    public IActionResult Update(Course model)
    {
        _unitOfWork.Courses.Update(model);
        _unitOfWork.Save();
        return Ok(model);
    }
    [HttpDelete("Delete")]
    public IActionResult Delete(Course model)
    {
        _unitOfWork.Courses.Delete(model);
        _unitOfWork.Save();
        return Ok("delete");
    }

    [HttpDelete("DeleteById")]
    public IActionResult Delete(int id)
    {
        var emp = _unitOfWork.Courses.GetById(id);
        _unitOfWork.Courses.Delete(emp);
        _unitOfWork.Save();
        return Ok("delete");
    }

}
