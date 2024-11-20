using Dto;
using Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repository;

namespace Cls.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SpecialityController : ApiBaseController
{
    public SpecialityController(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
    [HttpGet("GetAll")]
    public IActionResult GetAll()
    {
        return Ok(_unitOfWork.Specialities.GetAll());
    }
    [HttpGet("GetAllAsync")]
    public async Task<IActionResult> GetAllAsync()
    {
        return Ok(await _unitOfWork.Specialities.GetAllAsync());
    }

    [HttpPost("Add")]
    public IActionResult Add(SpecialityDto model)
    {
        //Automapper Package you can use it if you like 
        Speciality Speciality = new Speciality()
        {
            Name = model.SpecialtyName

        };
        _unitOfWork.Specialities.Add(Speciality);
        _unitOfWork.Save();
        return Ok("Done");
    }

    [HttpPut("Update")]
    public IActionResult Update(Speciality model)
    {
        _unitOfWork.Specialities.Update(model);
        _unitOfWork.Save();
        return Ok(model);
    }
    [HttpDelete("Delete")]
    public IActionResult Delete(Speciality model)
    {
        _unitOfWork.Specialities.Delete(model);
        _unitOfWork.Save();
        return Ok("delete");
    }

    [HttpDelete("DeleteById")]
    public IActionResult Delete(int id)
    {
        var emp = _unitOfWork.Specialities.GetById(id);
        _unitOfWork.Specialities.Delete(emp);
        _unitOfWork.Save();
        return Ok("delete");
    }

}
