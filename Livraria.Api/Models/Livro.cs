using System.ComponentModel.DataAnnotations;

namespace Livraria.Api.Models
{
    public class Livro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Nome { get; set; }

        [MaxLength(150)]
        public string Autor { get; set; }

        [Required]
        public int Quantidade { get; set; }

        [Required]
        public int GeneroId { get; set; }

        public Genero Genero { get; set; }
    }
}
