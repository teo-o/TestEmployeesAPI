using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestHyGCasa.Shared.Entities
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Name { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int PositionId { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Email { get; set; }

        public DateTime HireDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public decimal Salary { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public Position Position { get; set; }
    }
}
