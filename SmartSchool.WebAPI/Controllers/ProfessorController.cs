using Microsoft.AspNetCore.Mvc;

namespace SmartSchool.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfessorController
    {
        public ProfessorController() {}

        [HttpGet]
        public IActionResult Get()
        {
            return new OkObjectResult("Professors: fulano, cicrano, beltrano");
        }
    }
}