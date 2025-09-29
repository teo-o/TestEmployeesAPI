using System.ComponentModel.DataAnnotations;

namespace TestHyGCasa.Shared.DTOs.Request
{
    public class EmployeeRequestDto
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(500, ErrorMessage = "El nombre no puede tener más de {1} caracteres")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [EmailAddress(ErrorMessage = "El campo {0} debe ser un correo electrónico válido")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Range(0, double.MaxValue, ErrorMessage = "El campo {0} debe ser un número positivo")]
        public decimal Salary { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int PositionId { get; set; }
    }
}
