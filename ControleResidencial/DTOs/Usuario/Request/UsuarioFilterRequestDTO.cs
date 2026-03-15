namespace ControleResidencial.DTOs.Usuario.Request
{
    //padrao que eu estou acostumado a fazer listagens
    public class UsuarioFilterRequestDTO
    {
        public int OrdenarColuna { get; set; } = 1;
        public string OrdenarColunaDirecao { get; set; } = "ASC";
        public string? Pesquisa { get; set; }
        public int Regs { get; set; }
        public int Page { get; set; }
    }
}
