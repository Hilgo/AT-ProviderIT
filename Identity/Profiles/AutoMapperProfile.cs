using AutoMapper;
using GerenciamentoTarefasApi.Data.Dtos;
using GerenciamentoTarefasApi.Models;
using UsuariosApi.Data.Dtos;
using UsuariosApi.Models;

namespace UsuariosApi.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateUsuarioDto, Usuario>();
            CreateMap<CreateTarefaDto, Tarefa>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status)); ;
        }
    }
}
