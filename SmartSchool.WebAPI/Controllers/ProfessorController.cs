using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfessorController : ControllerBase
    {
        private DataContext _context { get; }
        public ProfessorController(DataContext context) { 
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return new OkObjectResult("Professors: fulano, cicrano, beltrano");
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var professor = _context.Professores.FirstOrDefault(a => a.Id == id);
            return professor == null ? NotFound("Não encontrado") : Ok(professor);
        }

        [HttpPost]
        public IActionResult Post(Professor professor)
        {
            _context.Add(professor);
            _context.SaveChanges();
            return Ok(professor);
        }

        [HttpPut("{id:int}")]
        public IActionResult Put(int id, Professor professor)
        {
            var query = _context.Professores.AsNoTracking().FirstOrDefault(a => a.Id == id);
            if(query == null) return BadRequest("Professor não encontrado!");

            _context.Update(professor);
            _context.SaveChanges();
            return Ok(professor);
        }

        [HttpPatch("{id:int}")]
        public IActionResult Patch(int id, Professor professor)
        {
            var query = _context.Professores.AsNoTracking().FirstOrDefault(a => a.Id == id);
            if(query == null) return BadRequest("Professor não encontrado!");

            _context.Update(professor);
            _context.SaveChanges();
            return Ok(professor);
        }


        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var professor = _context.Professores.FirstOrDefault(a => a.Id == id);
            if(professor == null) return BadRequest("Professor não encontrado!");

            _context.Remove(professor);
            _context.SaveChanges();
            return Ok();
        }
    }
}