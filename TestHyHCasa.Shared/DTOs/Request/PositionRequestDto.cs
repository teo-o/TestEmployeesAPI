using System.ComponentModel.DataAnnotations;

namespace TestHyGCasa.Shared.DTOs.Request
{
    public class PositionRequestDto
    {

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(100, ErrorMessage = "El nombre no puede tener más de {1} caracteres")]
        public string Name { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(500, ErrorMessage = "La descripción no puede tener más de {1} caracteres")]
        public string Description { get; set; }
    }
}
