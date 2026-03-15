using ControleResidencial.Helper.Enums;

namespace ControleResidencial.DTOs.Categoria.Request
{
    public class CreateCategoriaRequestDTO
    {
        public string Descricao { get; set; }
        public FinalidadeEnum Finalidade { get; set; }
    }
}
