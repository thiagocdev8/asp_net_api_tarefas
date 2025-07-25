using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using asp_net_api_tarefas.Context;
using asp_net_api_tarefas.Model;

namespace asp_net_api_tarefas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefaItemsController : ControllerBase
    {
        private readonly TarefaContexto _context;

        public TarefaItemsController(TarefaContexto context)
        {
            _context = context;
        }

        // GET: api/TarefaItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TarefaItem>>> GetTarefas()
        {
          if (_context.Tarefas == null)
          {
              return NotFound();
          }
            return await _context.Tarefas.ToListAsync();
        }

        // GET: api/TarefaItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TarefaItem>> GetTarefaItem(long id)
        {
          if (_context.Tarefas == null)
          {
              return NotFound();
          }
            var tarefaItem = await _context.Tarefas.FindAsync(id);

            if (tarefaItem == null)
            {
                return NotFound();
            }

            return tarefaItem;
        }

        // PUT: api/TarefaItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTarefaItem(long id, TarefaItem tarefaItem)
        {
            if (id != tarefaItem.ID)
            {
                return BadRequest();
            }

            _context.Entry(tarefaItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TarefaItemExists(id))
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

        // POST: api/TarefaItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TarefaItem>> PostTarefaItem(TarefaItem tarefaItem)
        {
          if (_context.Tarefas == null)
          {
              return Problem("Entity set 'TarefaContexto.Tarefas'  is null.");
          }
            _context.Tarefas.Add(tarefaItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTarefaItem", new { id = tarefaItem.ID }, tarefaItem);
        }

        // DELETE: api/TarefaItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTarefaItem(long id)
        {
            if (_context.Tarefas == null)
            {
                return NotFound();
            }
            var tarefaItem = await _context.Tarefas.FindAsync(id);
            if (tarefaItem == null)
            {
                return NotFound();
            }

            _context.Tarefas.Remove(tarefaItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TarefaItemExists(long id)
        {
            return (_context.Tarefas?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
