using ControleResidencial.DTOs.Usuario.Response;
using Microsoft.AspNetCore.Identity;

namespace ControleResidencial.DTOs.Transacao.Response
{
    public class TransacaoResponseDTO
    {
        public IdentityResult IdentityResult { get; set; }
        public TransacaoResponseDataDTO Data { get; set; }
        public List<TransacaoResponseDataDTO> DataList { get; set; }
    }
    public class TransacaoResponseDataDTO
    {
        public string Id { get; set; }
        public string Descricao { get; set; }
        public string Valor { get; set; }
        public DateTime DataTransacao { get; set; }
        public string UsuarioId { get; set; }
        public UsuarioFilterDataDTO Usuario { get; set; }
    }
}
