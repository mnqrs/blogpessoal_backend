using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog_Pessoal.Model
{
    public class Postagem : Auditable
    {
        [Key] // Primari Key (Id)
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Identity (1,1)
        public long Id { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(100)]
        public string Titulo { get; set;} = string.Empty;

        [Column(TypeName = "varchar")]
        [StringLength(1000)]
        public string Texto { get; set; } = string.Empty;
    }
}
