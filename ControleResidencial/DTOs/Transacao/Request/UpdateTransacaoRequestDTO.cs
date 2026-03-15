namespace ControleResidencial.DTOs.Transacao.Request
{
    public class UpdateTransacaoRequestDTO
    {
        public string Id { get; set; }
        public string Descricao { get; set; }
        public string Valor { get; set; }
        public string UsuarioId { get; set; }
    }
}
