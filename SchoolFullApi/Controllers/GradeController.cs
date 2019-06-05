using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BL;
using Domain.Models;
using Domain.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradeController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public GradeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        //Get : /api/GetGrade
        [HttpGet]
        public async Task<IEnumerable<Grade>> GetGradeAsync()
        {
            return await _unitOfWork.gradeRepository.FindAllAsync();
        }


        //GetById : /api/GetGrade
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGradeByIdAsync([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var grade = await _unitOfWork.gradeRepository.FindByConditionAsync(c => c.Id == id);

            if (grade == null)
                return NotFound();

            return Ok(grade);
        }

        //Post : /api/PostGrade
        [HttpPost]
        public IActionResult PostGrade([FromBody] PostGrade grade)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdGrade = _unitOfWork.gradeRepository.Create(new Grade
            {
                Name = grade.Name,
                SchoolId = grade.SchoolId
            });
            _unitOfWork.Save();

            return CreatedAtAction("GetGradeByIdAsync", new { id = createdGrade.Id }, createdGrade);
        }

        //Put : /api/PutGrade
        //    [HttpPut("{id}")]
        //    public IActionResult PutGrade([FromRoute] Guid id, [FromBody] Grade grade)
        //    {
        //        if (!ModelState.IsValid)
        //            return BadRequest(ModelState);

        //        if (id != grade.Id)
        //            return BadRequest();

        //        try
        //        {
        //            _unitOfWork.gradeRepository.Update(grade);
        //            _unitOfWork.Save();
        //        }
        //        catch(DbUpdateConcurrencyException)
        //        {
        //            if (!GradeExist(grade.Id))
        //                return NotFound();

        //            throw;
        //        }

        //        return NoContent();
        //    }

        //    public  bool GradeExist(Guid id)
        //    {
        //      return  _unitOfWork.gradeRepository.FindByConditionAsync(c => c.Id == id) == null ? false : true;
        //    }

    }


}