namespace TestHyGCasa.API.DTOs.Response
{
    public class EmployeeResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;

        public DateTime HireDate { get; set; } = DateTime.Now;
        public decimal Salary { get; set; }
        public string PositionName { get; set; } = null!;
    }
}
