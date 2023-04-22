namespace SmartSchool.WebAPI.Models
{
    public class Professor
    {
        public Professor() {}
        public Professor(int id, int registro, string nome, string sobreNome)
        {
            this.Id = id;
            this.Registro = registro;
            this.SobreNome = sobreNome;
            this.Nome = nome;
        }
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? SobreNome { get; set; }
        public int Registro { get; set; }
        public DateTime? DataInicio { get; set; } = DateTime.Now;
        public DateTime? DataFim {get; set;} = null;
        public bool Ativo {get; set;} = true;
        public IEnumerable<Disciplina>? Disciplinas { get; set; }
    }
}