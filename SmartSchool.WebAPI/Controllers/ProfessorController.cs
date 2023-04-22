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
        public IRepository _repo { get; }
        public ProfessorController(IRepository repo)
        {
            _repo = repo;            
        }

        [HttpGet]
        public IActionResult Get()
        {
            var professores = _repo.GetAllProfessores();
            return Ok(professores);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var professor = _repo.GetProfessorById(id);
            return professor == null ? NotFound("Não encontrado") : Ok(professor);
        }

        [HttpPost]
        public IActionResult Post(Professor professor)
        {
            _repo.Add(professor);
            if (_repo.SaveChanges())
            {
                return Ok(professor);
            }
            return BadRequest("Aluno não cadastrado!");
        }

        [HttpPut("{id:int}")]
        public IActionResult Put(int id, Professor professor)
        {
            var query = _repo.GetProfessorById(id);
            if (query == null) return BadRequest("Professor não encontrado!");

            _repo.Update(professor);            
            return Ok(professor);
        }

        [HttpPatch("{id:int}")]
        public IActionResult Patch(int id, Professor professor)
        {
            var query = _repo.GetProfessorById(id);
            if (query == null) return BadRequest("Professor não encontrado!");

            _repo.Update(professor);            
            return Ok(professor);
        }


        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var professor = _repo.GetProfessorById(id);
            if (professor == null) return BadRequest("Professor não encontrado!");

            _repo.Delete(professor);            
            return Ok();
        }
    }
}