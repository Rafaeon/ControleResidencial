namespace ControleResidencial.DTOs.Categoria.Request
{
    public class CategoriaFilterRequestDTO
    {
        public int OrdenarColuna { get; set; } = 1;
        public string OrdenarColunaDirecao { get; set; } = "ASC";
        public string? Pesquisa { get; set; }
        public int Regs { get; set; }
        public int Page { get; set; }
    }
}
