using Microsoft.AspNetCore.Mvc;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {
        public List<Aluno> Alunos = new List<Aluno>() {
            new Aluno() {
                Id = 1,
                Nome = "Marcos",
                Sobrenome = "Lisboa",
                Telefone = "123456"
            },
            new Aluno() {
                Id = 2,
                Nome = "Wos",
                Sobrenome = "Rodrigues",
                Telefone = "484548"
            },
            new Aluno() {
                Id = 1,
                Nome = "João2",
                Sobrenome = "Neves",
                Telefone = "859855"
            },
        };
        public AlunoController() {}

        [HttpGet]
        //Retorna uma lista de alunos
        //public List<Aluno> Get() => Alunos;
        public IActionResult Get() => Ok(Alunos);

        //QueryString -> http://localhost:5241/api/aluno/byId?id=1
        // [HttpGet("{byId}")]
        // public IActionResult GetById(int id) 
      
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id) 
        {
            var aluno = Alunos.FirstOrDefault(a => a.Id == id);
            if(aluno == null) return BadRequest("O aluno não foi encontrado");
            
            return Ok(aluno);
        }

        [HttpGet("{nome}")]
        public IActionResult GetByName(string nome) 
        {
            var aluno = Alunos.FirstOrDefault(a => a.Nome!.Contains(nome));
            if(aluno == null) return BadRequest("O aluno não foi encontrado");
            
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
            return Ok(aluno);
        }
            
        [HttpPut("{id}")]
        public IActionResult Put(int id, Aluno aluno) 
        {
            return Ok(aluno);
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Aluno aluno) 
        {
            return Ok(aluno);
        }
            
        [HttpDelete("{id}")]
        public IActionResult Delete(int id) 
        {
            return Ok();
        }
            
        
    }
}