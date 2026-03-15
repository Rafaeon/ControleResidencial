using Microsoft.AspNetCore.Identity;

namespace ControleResidencial.DTOs.Usuario.Response
{
    public class UsuarioResponseDTO
    {
        public IdentityResult IdentityResult { get; set; }
        public UsuarioResponseDataDTO Data { get; set; }
    }
    public class UsuarioResponseDataDTO
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public int Idade { get; set; }
        public bool IsAtivo { get; set; }
        public DateTime DataInclusao { get; set; }
    }
}
