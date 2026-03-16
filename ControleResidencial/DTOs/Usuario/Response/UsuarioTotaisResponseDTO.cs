using Microsoft.AspNetCore.Identity;

namespace ControleResidencial.DTOs.Usuario.Response
{
    public class UsuarioTotaisResponseDTO
    {
        public List<UsuarioTotaisDataDTO> Lista { get; set; } = new();
        public IdentityResult IdentityResult { get; set; }
        public decimal TotalReceitas { get; set; }
        public decimal TotalDespesas { get; set; }
        public decimal Saldo { get; set; }
    }
    public class UsuarioTotaisDataDTO
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public decimal TotalReceitas { get; set; }
        public decimal TotalDespesas { get; set; }
        public decimal Saldo { get; set; }
    }
}
