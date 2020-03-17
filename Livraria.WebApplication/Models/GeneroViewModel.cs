using System.ComponentModel.DataAnnotations;

namespace Livraria.WebApplication.Models
{
    public class GeneroViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo Descrição é obrigatório.", AllowEmptyStrings = false)]
        [MaxLength(150)]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
    }
}
