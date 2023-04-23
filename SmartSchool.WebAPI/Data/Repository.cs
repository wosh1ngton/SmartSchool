using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Helpers;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Data
{
    public class Repository : IRepository
    {
        public DataContext _context { get; }
        public Repository(DataContext context)
        {
            _context = context;

        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
            _context.SaveChanges();            
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public bool SaveChanges()
        {
            var val = _context.SaveChanges();
            return val > 0;
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
            _context.SaveChanges();
        }

        public Aluno[] GetAllAlunos(bool includeProfessor = false)
        {
            IQueryable<Aluno> query = _context.Alunos;
            if(includeProfessor)
            {
                query = query.Include(a => a.AlunosDisciplina)
                    .ThenInclude(ad => ad.Disciplina)
                    .ThenInclude(d => d.Professor);
            }
            query = query.AsNoTracking().OrderBy(a => a.Id);
            return query.ToArray();

        }
        public async Task<PageList<Aluno>> GetAllAlunosAsync(
            PageParams pageParams,
            bool includeProfessor = false
            )
        {
            
            IQueryable<Aluno> query = _context.Alunos;
            if(includeProfessor)
            {
                query = query.Include(a => a.AlunosDisciplina)
                    .ThenInclude(ad => ad.Disciplina)
                    .ThenInclude(d => d.Professor);
            }
            query = query.AsNoTracking().OrderBy(a => a.Id);

            if(!string.IsNullOrEmpty(pageParams.Nome))
                query = query.Where(aluno => aluno.Nome.ToUpper().Contains(pageParams.Nome.ToUpper()) ||
                                             aluno.Sobrenome.ToUpper().Contains(pageParams.Nome.ToUpper()));
            
            if(pageParams.Matricula > 0) 
                query = query.Where(aluno => aluno.Matricula == pageParams.Matricula);

            if(pageParams.Ativo != null) 
                query = query.Where(aluno => aluno.Ativo == (pageParams.Ativo != 0));
            //return await query.ToListAsync();
            return await PageList<Aluno>.CreateAsync(query, pageParams.PageNumber, pageParams.PageSize);

        }

        public Aluno[] GetAllAlunosByDisciplinaId(int disciplinaId, bool includeProfessor = false)
        {
            IQueryable<Aluno> query = _context.Alunos;
            if(includeProfessor)
            {
                query = query.Include(a => a.AlunosDisciplina)
                    .ThenInclude(ad => ad.Disciplina)
                    .ThenInclude(d => d.Professor);
            }
            query = query.AsNoTracking().OrderBy(a => a.Id).Where(aluno => aluno.AlunosDisciplina.Any(ad =>
                ad.DisciplinaId == disciplinaId));
            return query.ToArray();
        }

        public Aluno GetAlunoById(int alunodId, bool includeProfessor = false)
        {
            IQueryable<Aluno> query = _context.Alunos;
            if(includeProfessor)
            {
                query = query.Include(a => a.AlunosDisciplina)
                    .ThenInclude(ad => ad.Disciplina)
                    .ThenInclude(d => d.Professor);
            }
            query = query.AsNoTracking().OrderBy(a => a.Id)
                        .Where(aluno => aluno.Id == alunodId);

            return query.FirstOrDefault();
        }

        public Professor[] GetAllProfessores()
        {
            IQueryable<Professor> query = _context.Professores;
           
            query = query.AsNoTracking().OrderBy(a => a.Id);
            return query.ToArray();

        }

        public Professor[] GetAllProfessoresByDisciplina(int disciplinaId)
        {
            IQueryable<Professor> query = _context.Professores;
           
            query = query.AsNoTracking().OrderBy(a => a.Id).Where(professor => 
                professor.Disciplinas.Any(ad => ad.Id == disciplinaId));
            
            return query.ToArray();
        }

        public Professor GetProfessorById(int professorId)
        {
             IQueryable<Professor> query = _context.Professores;
            
            query = query.AsNoTracking().OrderBy(a => a.Id)
                        .Where(prof => prof.Id == professorId);

            return query.FirstOrDefault();
        }
    }
}