namespace ControleResidencial.DTOs.Transacao.Request
{
    public class CreateTransacaoRequestDTO
    {
        public string Descricao { get; set; }
        public string Valor { get; set; }
        public string UsuarioId { get; set; }
        public string Tipo { get; set; }
        public string CategoriaId { get; set; }

    }
}
