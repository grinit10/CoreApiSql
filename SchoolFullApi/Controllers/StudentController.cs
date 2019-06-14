using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BL;
using Domain.Models;
using Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public StudentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IEnumerable<Student>> GetAllStudents()
        {
            return await _unitOfWork.studentRepository.FindAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentById([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var student = await _unitOfWork.studentRepository.FindByConditionAsync(c => c.Id == id);
            return Ok(student);
        }

        [HttpPost]
        public IActionResult PostStudent([FromBody] PostStudentVM studnt)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var std = new Student
            {
                FirstName = studnt.FirstName,
                LastName = studnt.LastName,
                SectionId = studnt.SectionId
            };

            _unitOfWork.studentRepository.Create(std);
            _unitOfWork.Save();

            return CreatedAtAction("GetStudentById", new {id = std.Id}, std);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateStudent([FromRoute] Guid id, [FromBody] Student student)
        {
            if (id != student.Id)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                _unitOfWork.studentRepository.Update(student);
                _unitOfWork.Save();

            }
            catch (DbUpdateConcurrencyException)
            {

                if (_unitOfWork.studentRepository.FindByConditionAsync(c => c.Id == id) == null)
                    return NotFound();

                throw;
            }


            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var student = await _unitOfWork.studentRepository.FindByConditionAsync(c => c.Id == id);

            Student std = student.ToList().FirstOrDefault();

            _unitOfWork.studentRepository.Delete(std);
            _unitOfWork.Save();

            return NoContent();
        }

        [HttpPost("{id}")]
        public IActionResult StudentWithMultipleCourses([FromRoute] Guid id, [FromBody] StudentCoursesVM stdCourses)
        {
            var std = _unitOfWork.studentRepository.
                FindByConditionAsync(s => s.Id == id).Result.FirstOrDefault();

            if ((std == null) || !ModelState.IsValid)
                return BadRequest(ModelState);

            stdCourses.CourseIds.ForEach(cid =>
                _unitOfWork.courseStudentRepository.Create(
                    new CourseStudent()
                    {
                        CourseId = cid,
                        StudentId = std.Id,
                        Student = std
                    }));
            _unitOfWork.Save();

            return Ok();
        }
    }
}