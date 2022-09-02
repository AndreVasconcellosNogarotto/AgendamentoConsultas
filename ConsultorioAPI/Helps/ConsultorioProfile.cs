using AutoMapper;
using Consultorio.Models.Entities;
using ConsultorioAPI.Models.DTOs;
using System.Linq;

namespace ConsultorioAPI.Helps
{
    public class ConsultorioProfile : Profile
    {
        public ConsultorioProfile()
        {
            CreateMap<Paciente, PacienteDetailsDTO>().ReverseMap();

            CreateMap<Paciente, PacienteDTO>();

            CreateMap<ConsultaDTO, Consulta>()
                .ForMember(dest => dest.Especialidade, opt => opt.Ignore())
                .ForMember(dest => dest.Profissional, opt => opt.Ignore()); 
               
            CreateMap<Consulta, ConsultaDTO>()
                .ForMember(dest => dest.Especialidade, opt => opt.MapFrom(src => src.Especialidade.Nome))
                .ForMember(dest => dest.Profissional, opt => opt.MapFrom(src => src.Profissional.Nome));

            CreateMap<Consulta, ConsultaDetailsDTO>();

            CreateMap<ConsultaAdicionarDTO, Consulta>();

            CreateMap<ConsultaAtualizarDTO, Consulta>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<PacienteAdicionarDTO, Paciente>();

            CreateMap<PacienteAtualizacaoDTO, Paciente>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember !=null));

            CreateMap<Profissional, ProfissionalDetailsDTO>()
                .ForMember(dest => dest.TotalConsultas, opt => opt.MapFrom(src => src.Consultas.Count()))
                .ForMember(dest => dest.Especialidades, opt => opt.MapFrom(src => src.Especialidades.Select(x => x.Nome).ToArray()));

            CreateMap<Profissional, ProfissionalDTO>();

            CreateMap<ProfissionalAdicionarDTO,Profissional>();

            CreateMap<ProfissionalAtualizacaoDTO, Profissional>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<Especialidade, EspecialidadeDTO>();

            CreateMap<Especialidade, EspecialidadeDetailsDTO>();

        }
    }
}
