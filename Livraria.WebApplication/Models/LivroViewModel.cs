using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Livraria.WebApplication.Models
{
    public class LivroViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório.", AllowEmptyStrings = false)]
        [MaxLength(150)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo Autor(a) é obrigatório.", AllowEmptyStrings = false)]
        [MaxLength(150)]
        [Display(Name = "Autor(a)")]
        public string Autor { get; set; }

        [Required(ErrorMessage = "O campo Quantidade é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantidade informada deve ser maior que 0.")]
        public int Quantidade { get; set; }

        [Required(ErrorMessage = "O campo Gênero é obrigatório.")]
        [Display(Name = "Gênero")]
        public int GeneroId { get; set; }

        [Display(Name = "Gênero")]
        public GeneroViewModel Genero { get; set; }

        public SelectList Generos { get; set; }
    }
}
