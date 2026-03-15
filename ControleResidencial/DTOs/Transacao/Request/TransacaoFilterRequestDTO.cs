namespace ControleResidencial.DTOs.Transacao.Request
{
    public class TransacaoFilterRequestDTO
    {
        public int OrdenarColuna { get; set; } = 1;
        public string OrdenarColunaDirecao { get; set; } = "ASC";
        public string? Pesquisa { get; set; }
        public int Regs { get; set; }
        public int Page { get; set; }
    }
}
