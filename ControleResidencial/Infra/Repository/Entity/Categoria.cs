using ControleResidencial.Helper.Enums;
using System.ComponentModel.DataAnnotations;

namespace ControleResidencial.Infra.Repository.Entity
{
    public class Categoria
    {
        [Key]
        [Required]
        [StringLength(36)]
        public string Id { get; set; }
        [Required]
        [MaxLength(400)]
        public string Descricao { get; set; }
        public FinalidadeEnum Finalidade { get; set; } // aqui eu uso um enum para organizar o tipo de finalidade da categoria
    }
}
