using System.ComponentModel.DataAnnotations;

namespace Livraria.Api.Models
{
    public class Genero
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Descricao { get; set; }        
    }
}
