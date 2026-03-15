using AutoMapper;
using ControleResidencial.DTOs.Categoria.Response;
using ControleResidencial.DTOs.Transacao.Response;
using ControleResidencial.DTOs.Usuario.Response;
using ControleResidencial.Infra.Repository.Entity;

namespace ControleResidencial.Mappings
{
    public class DomainsToDtoMappings : Profile
    {
        public DomainsToDtoMappings()
        {
            CreateMap<Usuario, UsuarioResponseDataDTO>();
            CreateMap<Transacao, TransacaoResponseDataDTO>();
            CreateMap<Categoria, CategoriaResponseDataDTO>();
        }
    }
}
