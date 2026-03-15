using ControleResidencial.DTOs.Usuario.Response;
using DeliveryCo.DTOs;

namespace ControleResidencial.DTOs.Transacao.Response
{
    public class TransacaoFilterResponseDTO
    {
        public virtual int Total { get; set; }
        public List<TransacaoFilterDataDTO> Lista { get; set; }
        public IdentityResultCustom IdentityResult { get; set; }
    }

    public class TransacaoFilterDataDTO
    {
        public string Id { get; set; }
        public string Descricao { get; set; }
        public string Valor { get; set; }
        public DateTime DataTransacao { get; set; }
        public string UsuarioId { get; set; }
        public UsuarioFilterDataDTO Usuario { get; set; }
    }
}
