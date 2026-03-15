using ControleResidencial.DTOs.Transacao.Response;
using DeliveryCo.DTOs;

namespace ControleResidencial.DTOs.Categoria.Response
{
    public class CategoriaFilterResponseDTO
    {
        public virtual int Total { get; set; }
        public List<CategoriaFilterDataDTO> Lista { get; set; }
        public IdentityResultCustom IdentityResult { get; set; }
    }
    public class CategoriaFilterDataDTO
    {
        public string Id { get; set; }
        public string Descricao { get; set; }
        public string Finalidade { get; set; }
    }
}
