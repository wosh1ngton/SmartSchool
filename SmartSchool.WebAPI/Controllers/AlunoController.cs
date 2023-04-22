using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.Dtos;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {
        private readonly DataContext _context;
        public IRepository _repo { get; }
        private readonly IMapper _mapper;
        public AlunoController(IRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }



        [HttpGet]
        //Retorna uma lista de alunos
        //public List<Aluno> Get() => Alunos;
        public IActionResult Get()
        {
            var alunos = _repo.GetAllAlunos(true);         

            
            return Ok(_mapper.Map<IEnumerable<AlunoDto>>(alunos));
        }

        //QueryString -> http://localhost:5241/api/aluno/byId?id=1
        // [HttpGet("{byId}")]
        // public IActionResult GetById(int id) 

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {

            var aluno = _repo.GetAlunoById(id);
            if (aluno == null) return BadRequest("O aluno não foi encontrado");
            var alunoDto = _mapper.Map<AlunoDto>(aluno);
            return Ok(alunoDto);
        }

        // [HttpGet("{nome}")]
        // public IActionResult GetByName(string nome)
        // {
        //     var aluno = _context.Alunos.FirstOrDefault(a => a.Nome!.Contains(nome));
        //     if (aluno == null) return BadRequest("O aluno não foi encontrado");

        //     return Ok(aluno);
        // }

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
        public IActionResult Post(AlunoRegistrarDto model)
        {
            var aluno = _mapper.Map<Aluno>(model);
            _repo.Add(aluno);

            if (aluno != null)
            {
                return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));
            }
            return BadRequest("Aluno não cadastrado!");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, AlunoRegistrarDto model)
        {
            var query = _repo.GetAlunoById(id);
            if (query == null) return BadRequest("Aluno não encontrado");

            _mapper.Map(model, query);

            _repo.Update(query);
            if (_repo.SaveChanges())
            {
               return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(query));
            }
            return BadRequest("Aluno não cadastrado!");
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, AlunoRegistrarDto model)
        {
            var query = _repo.GetAlunoById(id);
            if (query == null) return BadRequest("Aluno não encontrado");

            _mapper.Map(model, query);

            _repo.Update(query);
            if (_repo.SaveChanges())
            {
                return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(query));
            }
            return BadRequest("Aluno não cadastrado!");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var aluno = _repo.GetAlunoById(id);
            if (aluno == null) return BadRequest("Aluno não encontrado");

            _repo.Delete(aluno);
            if (_repo.SaveChanges())
            {
                return Ok("Aluno deletado");
            }
            return BadRequest("Aluno não deletado!");
        }


    }
}