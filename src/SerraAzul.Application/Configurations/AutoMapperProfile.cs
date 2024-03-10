using AutoMapper;
using SerraAzul.Application.DTOs.V1.Pagamento;
using SerraAzul.Application.DTOs.V1.Usuario;
using SerraAzul.Core.Extensions;
using SerraAzul.Domain.Entities;

namespace SerraAzul.Application.Configurations;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        #region Usuario
        
        CreateMap<Usuario, UsuarioDto>().ReverseMap()
            .AfterMap((_, dest) => dest.Cpf = dest.Cpf.SomenteNumeros()!);
        CreateMap<Usuario, AdicionarUsuarioDto>().ReverseMap()
            .AfterMap((_, dest) => dest.Cpf = dest.Cpf.SomenteNumeros()!);
        CreateMap<Usuario, AtualizarUsuarioDto>().ReverseMap()
            .AfterMap((_, dest) => dest.Cpf = dest.Cpf.SomenteNumeros()!);
        #endregion

        #region Pagamento

        CreateMap<Pagamento, PagamentoDto>().ReverseMap();
        CreateMap<Pagamento, AdicionarPagamentoDto>().ReverseMap();
        CreateMap<Pagamento, AtualizarPagamentoDto>().ReverseMap();

        #endregion
    }
}