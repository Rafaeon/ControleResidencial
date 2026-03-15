using System.ComponentModel.DataAnnotations;

namespace ControleResidencial.Infra.Repository.Entity
{
    public class Usuario
    {
        [Key]
        [Required]
        [StringLength(36)]
        public string Id { get; set; }//segui com tipo string para os Ids, pois é o que uso no dia dia, acho que fica mais facil de trabalhar
        [Required]
        [MaxLength(100)]
        public string Nome { get; set; }
        public bool IsAtivo { get; set; }
        public int Idade { get; set; }
        public DateTime DataInclusao { get; set; }
        public DateTime DataAtualizacao { get; set; }
    }
}
