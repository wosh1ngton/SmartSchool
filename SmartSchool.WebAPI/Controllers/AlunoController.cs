using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {
        private readonly DataContext _context;
        public AlunoController(DataContext context)
        {
            _context = context;
        }

        

        [HttpGet]
        //Retorna uma lista de alunos
        //public List<Aluno> Get() => Alunos;
        public IActionResult Get() => Ok(_context.Alunos);

        //QueryString -> http://localhost:5241/api/aluno/byId?id=1
        // [HttpGet("{byId}")]
        // public IActionResult GetById(int id) 

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var aluno = _context.Alunos.FirstOrDefault(a => a.Id == id);
            if (aluno == null) return BadRequest("O aluno não foi encontrado");

            return Ok(aluno);
        }

        [HttpGet("{nome}")]
        public IActionResult GetByName(string nome)
        {
            var aluno = _context.Alunos.FirstOrDefault(a => a.Nome!.Contains(nome));
            if (aluno == null) return BadRequest("O aluno não foi encontrado");

            return Ok(aluno);
        }

        // [HttpGet("{ByName}")]
        // public IActionResult GetByNameAndSurname(string nome, string sobrenome) 
        // {
        //     var aluno = Alunos.FirstOrDefault(a => 
        //         a.Nome!.Contains(nome) && a.Sobrenome!.Contains(sobrenome)
        //     );
        //     if(aluno == null) return BadRequest("O aluno não foi encontrado");

        //     return Ok(aluno);
        // }
        [HttpPost]
        public IActionResult Post(Aluno aluno)
        {
            _context.Add(aluno);
            _context.SaveChanges();
            return Ok(aluno);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Aluno aluno)
        {
             var query = _context.Alunos.AsNoTracking().FirstOrDefault(a => a.Id == id);
             if(query == null) return BadRequest("Aluno não encontrado");

             _context.Update(aluno);
            _context.SaveChanges();
            return Ok(aluno);
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Aluno aluno)
        {
            var query = _context.Alunos.AsNoTracking().FirstOrDefault(a => a.Id == id);
             if(query == null) return BadRequest("Aluno não encontrado");

             _context.Update(aluno);
            _context.SaveChanges();
            return Ok(aluno);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var aluno = _context.Alunos.FirstOrDefault(a => a.Id == id);
            if(aluno == null) return BadRequest("Aluno não encontrado");
             _context.Remove(aluno);
            _context.SaveChanges();
            return Ok();
        }


    }
}