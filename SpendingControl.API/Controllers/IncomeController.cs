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
    public class IncomeController : ControllerBase
    {
        private readonly DataContext _context;
        public IncomeController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<Income[]>> GetAll()
        {
            try
            {
                Income[] incomes = await _context.Incomes.ToArrayAsync();
                if (incomes.Length > 0) return Ok(incomes);
                return NoContent();
            }
            catch (Exception exception)
            {
                return InternalError(exception);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Income>> Post(Income income)
        {
            try
            {
                await _context.Incomes.AddAsync(income);
                if ((await _context.SaveChangesAsync()) > 0) 
                {
                    return Created($"api/income/{income.Id}", income);
                }  else
                {
                    return BadRequest($"Não foi possível cadastrar a renda {income.Name}");
                }
            }
            catch (Exception exception)
            {
                return InternalError(exception);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Income>> Put(int id, Income updatedIncome)
        {
            try
            {
                Income income = await _context.Incomes.AsNoTracking().FirstOrDefaultAsync(findIncome => findIncome.Id == id);
                if (income == null) return NotFound();

                _context.Entry(updatedIncome).State = EntityState.Modified;
                if ((await _context.SaveChangesAsync()) > 0)
                {
                    return Ok(updatedIncome);
                } else
                {
                    return BadRequest($"Não foi possível atualizar a renda {updatedIncome.Name}");
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
                Income income = await _context.Incomes.AsNoTracking().FirstOrDefaultAsync(findIncome => findIncome.Id == id);
                if (income == null) return NotFound();

                _context.Incomes.Remove(income);
                if ((await _context.SaveChangesAsync()) > 0)
                {
                    return Ok();
                } else
                {
                    return BadRequest($"Não foi possível apagar a renda {income.Name}");
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
