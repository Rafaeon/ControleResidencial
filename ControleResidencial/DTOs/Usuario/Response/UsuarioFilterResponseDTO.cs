using DeliveryCo.DTOs;

namespace ControleResidencial.DTOs.Usuario.Response
{
    public class UsuarioFilterResponseDTO
    {
        public virtual int Total { get; set; }
        public List<UsuarioFilterDataDTO> Lista { get; set; }
        public IdentityResultCustom IdentityResult { get; set; }
    }

    public class UsuarioFilterDataDTO
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public int Idade { get; set; }
        public bool IsAtivo { get; set; }
        public DateTime DataInclusao { get; set; }
        public DateTime DataAtualizacao { get; set; }
    }
}
