using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BL;
using Domain.Models;
using Domain.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SectionController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public SectionController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //Get : /api/GetAllSection
        [HttpGet]
        public async Task<IEnumerable<Section>> GetAllSectionAsync()
        {
            return await _unitOfWork.sectionRepository.FindAllAsync();
        }

        //Get : /api/GetSectionById/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSectionBYIdAsync([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var section = await _unitOfWork.sectionRepository.FindByConditionAsync(c => c.Id == id);

            if (section == null)
                return NotFound();

            return Ok(section);
        }

        //Post: /api/PostSection
        [HttpPost]
        public IActionResult PostSection([FromBody] PostSectionVM PostSection)
        {
            if (!ModelState.IsValid)
                return BadRequest();

           var section = _unitOfWork.sectionRepository.Create(new Section
            {
                Name = PostSection.Name,
                Strength =  PostSection.Strength,
                GradeId = PostSection.GradeId
            });

            _unitOfWork.Save();

            return CreatedAtAction("GetSectionBYIdAsync", new {id = section.Id}, section);
        }


        //Put: /api/PutSection
        [HttpPut("{id}")]
        public IActionResult PutSection([FromRoute] Guid id, [FromBody] Section section)
        {
            //var section = _unitOfWork.sectionRepository.FindByConditionAsync(c => c.Id == id);


            if (id != section.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                _unitOfWork.sectionRepository.Update(section);
                _unitOfWork.Save();
            }
            catch(DbUpdateConcurrencyException)
            {
                var SectionDb = _unitOfWork.sectionRepository.FindByConditionAsync(c => c.Id == id);
                if (SectionDb == null)
                    return NotFound();
                throw;
            }

            return NoContent();
        }
    }
}