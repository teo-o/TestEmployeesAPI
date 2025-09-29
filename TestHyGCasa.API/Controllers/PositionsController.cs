using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestHyGCasa.API.Data;
using TestHyGCasa.API.DTOs.Request;
using TestHyGCasa.API.DTOs.Response;
using TestHyGCasa.Shared.Entities;

namespace TestHyGCasa.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionsController : ControllerBase
    {
        private readonly DataContext _context;

        public PositionsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Positions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PositionResponseDto>>> GetPositions()
        {
            return await _context.Positions.Select(p => new PositionResponseDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description
            }).ToListAsync();
        }

        // GET: api/Positions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PositionResponseDto>> GetPosition(int id)
        {
            var position = await _context.Positions
                .Where(e => e.Id == id)
                .Select(e => new PositionResponseDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    Description = e.Description
                })
                .FirstOrDefaultAsync();

            if (position == null)
            {
                return NotFound();
            }

            return position;

        }

        // PUT: api/Positions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPosition(int id, PositionRequestDto dto)
        {
            var position = await _context.Positions.FindAsync(id);
            if (position == null)
            {
                return NotFound();
            }

            position.Name = dto.Name;
            position.Description = dto.Description;
            _context.Entry(position).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PositionExists(id))
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

        // POST: api/Positions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PositionResponseDto>> PostPosition(PositionRequestDto dto)
        {
            var position = new Position
            {
                Name = dto.Name,
                Description = dto.Description
            };

            _context.Positions.Add(position);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPosition), new { id = position.Id }, position);
        }

        // DELETE: api/Positions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePosition(int id)
        {
            var position = await _context.Positions.FindAsync(id);
            if (position == null)
            {
                return NotFound();
            }

            _context.Positions.Remove(position);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PositionExists(int id)
        {
            return _context.Positions.Any(e => e.Id == id);
        }
    }
}
