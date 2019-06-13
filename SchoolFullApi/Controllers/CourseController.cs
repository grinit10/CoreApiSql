using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BL;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CourseController(IUnitOfWork IUnitOfWork)
        {
            _unitOfWork = IUnitOfWork;
        }

        [HttpGet]
        public async Task<IEnumerable<Course>> GetAllCourses()
        {
            return await _unitOfWork.courseRepository.FindAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourseById([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var course = await _unitOfWork.courseRepository.FindByConditionAsync(c => c.Id == id);

            return Ok(course);
        }

        [HttpPost]
        public IActionResult PostCourse([FromBody] string name)
        {
            var crc = new Course { Name = name };
            //if (!ModelState.IsValid)
            //    return BadRequest();

            _unitOfWork.courseRepository.Create(crc);
            _unitOfWork.Save();

            return CreatedAtAction("GetCourseById", new { id = crc.Id }, crc);
        }

        [HttpPut("{id}")]
        public IActionResult PutCourse([FromRoute] Guid id, [FromBody] Course course)
        {
            if (id != course.Id)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _unitOfWork.courseRepository.Update(course);
                _unitOfWork.Save();
            }
            catch
            {
                if (_unitOfWork.courseRepository.FindByConditionAsync(c => c.Id == id) == null)
                    return NotFound();

                throw;
            }
            

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var course = await _unitOfWork.courseRepository.FindByConditionAsync(c => c.Id == id);
            Course crc = course.ToList().FirstOrDefault();
            
            _unitOfWork.courseRepository.Delete(crc);
            _unitOfWork.Save();

            return NoContent();
        }
    }

}