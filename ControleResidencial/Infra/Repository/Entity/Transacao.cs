using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace ControleResidencial.Infra.Repository.Entity
{
    public class Transacao
    {
        [Key]
        [Required]
        [StringLength(36)]
        public string Id { get; set; }
        public string Valor { get; set; }
        //tipo 1 = despesa, tipo 2 = receita
        public string Tipo { get; set; }    
        [Required]
        [MaxLength(400)]
        public string Descricao { get; set; }
        [Required]
        public string CategoriaId { get; set; }
        [Required]
        public string UsuarioId { get; set; }
        public DateTime DataTransacao { get; set; }
    }
}
