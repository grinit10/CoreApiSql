using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BL;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LambdaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public SchoolsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        [HttpGet]
        [Route("health-check")]
        public IActionResult HealthCheckTask()
        {
            return Ok();
        }
        // GET: api/Schools
        [HttpGet]
        public async Task<IEnumerable<School>> GetSchoolsAsync()
        {
            return await _unitOfWork.schoolRepository.FindAllAsync();
        }

        // GET: api/Schools/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSchool([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var school = await _unitOfWork.schoolRepository.FindByConditionAsync(sc => sc.Id == id);

            if (school == null)
            {
                return NotFound();
            }

            return Ok(school);
        }

        // PUT: api/Schools1/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSchool([FromRoute] Guid id, [FromBody] School school)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != school.Id)
            {
                return BadRequest();
            }

            try
            {
                _unitOfWork.schoolRepository.Update(school);
                _unitOfWork.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await SchoolExistsAsync(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Schools1
        [HttpPost]
        public IActionResult PostSchool([FromForm]string school)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdSchool = _unitOfWork.schoolRepository.Create(new School() { Name = school});
            _unitOfWork.Save();

            return CreatedAtAction("GetSchool", new { id = createdSchool.Id }, school);
        }

        // DELETE: api/Schools/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchool([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var schoolx = await _unitOfWork.schoolRepository.FindByConditionAsync(c => c.Id == id);
            School sch = schoolx.ToList().FirstOrDefault();

            if (sch == null)
            {
                return NotFound();
            }
            _unitOfWork.schoolRepository.Delete(sch);
            _unitOfWork.Save();

            return Ok();
        }

        [NonAction]
        private async Task<bool> SchoolExistsAsync(Guid id)
        {
            return await _unitOfWork.schoolRepository.FindByConditionAsync(e => e.Id == id) == null ? false : true;
        }
    }
}