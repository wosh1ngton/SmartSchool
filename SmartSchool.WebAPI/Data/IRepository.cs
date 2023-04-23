using SmartSchool.WebAPI.Helpers;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Data
{
    public interface IRepository
    {
         void Add<T>(T entity) where T : class;
         void Update<T>(T entity) where T : class;
         void Delete<T>(T entity) where T : class;
         bool SaveChanges();
        Task<PageList<Aluno>> GetAllAlunosAsync(PageParams pageParams,bool includeProfessor = false);
         Aluno[] GetAllAlunos(bool includeProfessor = false);
         Aluno[] GetAllAlunosByDisciplinaId(int disciplinaId, bool includeProfessor = false);
         Aluno GetAlunoById(int alunodId, bool includeProfessor = false);
         Professor[] GetAllProfessores();
         Professor[] GetAllProfessoresByDisciplina(int disciplinaId);
         Professor GetProfessorById(int professorId);

    }
}