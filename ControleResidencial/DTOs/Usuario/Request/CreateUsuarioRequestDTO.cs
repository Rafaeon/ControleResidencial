namespace ControleResidencial.DTOs.Usuario.Request
{
    public class CreateUsuarioRequestDTO
    {
        public string Nome { get; set; }
        public int Idade { get; set; }
        public bool IsAtivo { get; set; }
    }
}
