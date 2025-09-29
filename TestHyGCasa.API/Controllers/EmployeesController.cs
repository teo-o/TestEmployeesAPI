using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestHyGCasa.API.Data;
using TestHyGCasa.Shared.Entities;
using TestHyGCasa.API.DTOs.Request;
using TestHyGCasa.API.DTOs.Response;

namespace TestHyGCasa.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly DataContext _context;

        public EmployeesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeResponseDto>>> GetEmployees()
        {
            return await _context.Employees
                .Include(e => e.Position)
                .Select(e => new EmployeeResponseDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    Email = e.Email,
                    Salary = e.Salary,
                    PositionName = e.Position!.Name
                })
                .ToListAsync();
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeResponseDto>> GetEmployee(int id)
        {
            var employee = await _context.Employees
                .Include(e => e.Position)
                .Where(e => e.Id == id)
                .Select(e => new EmployeeResponseDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    Email = e.Email,
                    Salary = e.Salary,
                    PositionName = e.Position!.Name
                })
                .FirstOrDefaultAsync();

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        // PUT: api/Employees/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, EmployeeRequestDto dto)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            var position = await _context.Positions.FindAsync(dto.PositionId);
            if (position == null)
            {
                return BadRequest("El cargo especificado no existe.");
            }

            employee.Name = dto.Name;
            employee.Email = dto.Email;
            employee.PositionId = dto.PositionId;
            employee.Salary = dto.Salary;

            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Employees
        [HttpPost]
        public async Task<ActionResult<EmployeeResponseDto>> PostEmployee(EmployeeRequestDto dto)
        {
            var position = await _context.Positions.FindAsync(dto.PositionId);
            if (position == null)
            {
                return BadRequest("El cargo especificado no existe.");
            }

            var employee = new Employee
            {
                Name = dto.Name,
                Email = dto.Email,
                Salary = dto.Salary,
                PositionId = dto.PositionId
            };

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            var response = new EmployeeResponseDto
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Salary = employee.Salary,
                PositionName = position.Name
            };

            return CreatedAtAction(nameof(GetEmployee), new { id = employee.Id }, response);
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }
    }
}
