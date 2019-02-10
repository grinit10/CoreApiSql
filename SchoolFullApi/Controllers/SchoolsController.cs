﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;
using Domain.Models;
using Repositories;
using BL;

namespace Api.Controllers
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

        // GET: api/Schools1
        [HttpGet]
        public async Task<IEnumerable<School>> GetSchoolsAsync()
        {
            return await _unitOfWork.schoolRepository.FindAllAsync();
        }

        // GET: api/Schools1/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSchool([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var school = await _unitOfWork.schoolRepository.FindByConditionAync(sc => sc.Id == id);

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
        public IActionResult PostSchool([FromBody] School school)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _unitOfWork.schoolRepository.Create(school);
                _unitOfWork.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return CreatedAtAction("GetSchool", new { id = school.Id }, school);
        }

        //// DELETE: api/Schools1/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteSchool([FromRoute] Guid id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var school = await _context.Schools.FindAsync(id);
        //    if (school == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Schools.Remove(school);
        //    await _context.SaveChangesAsync();

        //    return Ok(school);
        //}

        private async Task<bool> SchoolExistsAsync(Guid id)
        {
            return await _unitOfWork.schoolRepository.FindByConditionAync(e => e.Id == id) == null? false: true;
        }
    }
}