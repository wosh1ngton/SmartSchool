using AutoMapper;
using SmartSchool.WebAPI.Dtos;
using SmartSchool.WebAPI.Extensions;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Aluno, AlunoDto>()
                .ForMember(
                    dest => dest.Nome,
                    opt => opt.MapFrom(src => $"{src.Nome} {src.Sobrenome}")
                )
                .ForMember(
                    dest => dest.Idade,
                    opt => opt.MapFrom(src => src.DataNasc.GetCurrentAge())
                );
                
            CreateMap<AlunoDto, Aluno>();
            CreateMap<AlunoDto, AlunoRegistrarDto>().ReverseMap();
        }
    }
}