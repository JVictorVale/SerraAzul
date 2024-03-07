using AutoMapper;
using SerraAzul.Application.DTOs.V1.Auth;
using SerraAzul.Application.DTOs.V1.User;
using SerraAzul.Core.Extensions;
using SerraAzul.Domain.Entities;

namespace SerraAzul.Application.Configurations;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<UsuarioDto, Usuario>().ReverseMap()
            .ForMember(dest => dest.Cpf, opt => opt.MapFrom(src => src.Cpf.SomenteNumeros()));
        CreateMap<LoginDto, Usuario>().ReverseMap();
        CreateMap<AdicionarUsuarioDto, Usuario>().ReverseMap()
            .ForMember(dest => dest.Cpf, opt => opt.MapFrom(src => src.Cpf.SomenteNumeros()));
        CreateMap<AtualizarUsuarioDto, Usuario>().ReverseMap()
            .ForMember(dest => dest.Cpf, opt => opt.MapFrom(src => src.Cpf.SomenteNumeros()));
        
    }
}