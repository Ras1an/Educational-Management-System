using Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repository;
using Dto;

namespace Cls.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InstructorController : ApiBaseController
{
    public InstructorController(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
    [HttpGet("GetAll")]
    public IActionResult GetAll()
    {
        return Ok(_unitOfWork.Instructors.GetAll());
    }
    [HttpGet("GetAllAsync")]
    public async Task<IActionResult> GetAllAsync()
    {
        return Ok(await _unitOfWork.Instructors.GetAllAsync());
    }

    [HttpPost("Add")]
    public IActionResult Add(Instructor ins)
    {
        //Automapper Package you can use it if you like 
      
        _unitOfWork.Instructors.Add(ins);
        _unitOfWork.Save();
        return Ok("Done");
    }

    [HttpPut("Update")]
    public IActionResult Update(Instructor model)
    {
        _unitOfWork.Instructors.Update(model);
        _unitOfWork.Save();
        return Ok(model);
    }
    [HttpDelete("Delete")]
    public IActionResult Delete(Instructor model)
    {
        _unitOfWork.Instructors.Delete(model);
        _unitOfWork.Save();
        return Ok("delete");
    }

    [HttpDelete("DeleteById")]
    public IActionResult Delete(int id)
    {
        var emp = _unitOfWork.Instructors.GetById(id);
        _unitOfWork.Instructors.Delete(emp);
        _unitOfWork.Save();
        return Ok("delete");
    }

}
