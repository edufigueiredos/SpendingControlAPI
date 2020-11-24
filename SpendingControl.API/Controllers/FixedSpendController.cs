using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpendingControl.API.Data;
using SpendingControl.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpendingControl.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FixedSpendController : ControllerBase
    {
        private readonly DataContext _context;
        public FixedSpendController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<FixedSpend[]>> GetAll()
        {
            try
            {
                FixedSpend[] fixedSpends = await _context.FixedSpends.ToArrayAsync();
                if (fixedSpends.Length  > 0) return Ok(fixedSpends);
                return NoContent();
            }
            catch (Exception exception)
            {
                return InternalError(exception);
            }
        }

        [HttpPost]
        public async Task<ActionResult<FixedSpend>> Post(FixedSpend fixedSpend)
        {
            try
            {
                await _context.FixedSpends.AddAsync(fixedSpend);
                if ((await _context.SaveChangesAsync()) > 0)
                {
                    return Created($"api/fixedspend/{fixedSpend.Id}", fixedSpend);
                }
                else
                {
                    return BadRequest($"Não foi possível cadastrar gasto fixo {fixedSpend.Name}");
                }
            }
            catch (Exception exception)
            {
                return InternalError(exception);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<FixedSpend>> Put(int id, FixedSpend updatedFixedSpend)
        {
            try
            {
                FixedSpend fixedSpend = await _context.FixedSpends.AsNoTracking().FirstOrDefaultAsync(findIncome => findIncome.Id == id);
                if (fixedSpend == null) return NotFound();

                _context.Entry(updatedFixedSpend).State = EntityState.Modified;
                if ((await _context.SaveChangesAsync()) > 0)
                {
                    return Ok(updatedFixedSpend);
                }
                else
                {
                    return BadRequest($"Não foi possível atualizar o gasto fixo {updatedFixedSpend.Name}");
                }
            }
            catch (Exception exception)
            {
                return InternalError(exception);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                FixedSpend fixedSpend = await _context.FixedSpends.AsNoTracking().FirstOrDefaultAsync(findFixedSpend => findFixedSpend.Id == id);
                if (fixedSpend == null) return NotFound();

                _context.FixedSpends.Remove(fixedSpend);
                if ((await _context.SaveChangesAsync()) > 0)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest($"Não foi possível apagar o gasto fixo {fixedSpend.Name}");
                }
            }
            catch (Exception exception)
            {
                return InternalError(exception);
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public ActionResult InternalError(Exception exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Houve um erro interno: {exception.Message}");
        }
    }
}
