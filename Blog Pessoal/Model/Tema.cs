using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Blog_Pessoal.Model
{
    
            public class Tema 
        {
            [Key] // Primari Key (Id)
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Identity (1,1)
            public long Id { get; set; }

            [Column(TypeName = "varchar")]
            [StringLength(1000)]
            public string Descricao { get; set; } = string.Empty;

            [InverseProperty("Tema")]
            public virtual ICollection<Postagem>? Postagem { get; set; }
        }
    }
