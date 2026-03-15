using ControleResidencial.DTOs.Transacao.Response;
using Microsoft.AspNetCore.Identity;

namespace ControleResidencial.DTOs.Categoria.Response
{
    public class CategoriaResponseDTO
    {
        public IdentityResult IdentityResult { get; set; }
        public CategoriaResponseDataDTO Data { get; set; }
    }
    public class CategoriaResponseDataDTO
    {
        public string Id { get; set; }
        public string Descricao { get; set; }
        public string Finalidade { get; set; }
    }
}
