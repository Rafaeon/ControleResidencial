namespace ControleResidencial.DTOs.Usuario.Request
{
    public class UpdateUsuarioRequestDTO
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public int Idade { get; set; }
        public bool IsAtivo { get; set; }
    }
}
