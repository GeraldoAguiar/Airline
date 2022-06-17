
using System.Runtime.CompilerServices;
using AIRLINE.API.Data;
using AIRLINE.API.Models;
using AIRLINE.API.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace AIRLINE.API.Controllers;

[Controller]
[Route("v1")]
public class LegalPersonController : ControllerBase
{
    [HttpGet("legalperson")]
    public async Task<IActionResult> Get(
        [FromServices] DataContext context)
    {
        try
        {
            var todos = await context
                .Legal
                .AsNoTracking()
                .ToListAsync();

            return Ok(new ResultViewModel<List<LegalPerson>>(todos));
        }
        catch (Exception e)
        {
            return StatusCode(500, new ResultViewModel<List<LegalPerson>>("05X04 - Falha interna no servidor"));
        }
    }

    private List<LegalPerson> GetLegalPerson(DataContext context)
        {
            return context.Legal.ToList();
        }
    

    [HttpGet("legalperson/{id}")]
    public async Task<IActionResult> GetById(
        [FromServices] DataContext context,
        [FromRoute] int id)

    {
        try
        {
            var list = await context
                .Legal
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (list == null)
                return NotFound();

            return Ok(list);
        }
        catch (Exception e)
        {
            return StatusCode(500, new ResultViewModel<List<LegalPerson>>("05X04 - Falha interna no servidor"));
        }
    }
    
    [HttpPost("legalperson")]
    public async Task<IActionResult> PostAsync(
        [FromBody] LegalPersonViewModels model,
        [FromServices] DataContext context)
    {
        if (!ModelState.IsValid)
            return BadRequest();
        
        var client = new LegalPerson
            {
                Id = 0,
                Name = model.Name,
                CNPJ = model.CNPJ,
                Telephone = model.Telephone,
                PostedAt = DateTime.Now
            };

         try
        {
            await context.Legal.AddAsync(client);
            await context.SaveChangesAsync();

            return Created($"v1/legalperson/{client.Id}", new ResultViewModel<LegalPerson>(client));
        }
        catch (DbUpdateException ex)
        {

            return StatusCode(500,
                new ResultViewModel<LegalPerson>("05XE9 - Não foi possivel incluir o seu cadastro"));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<LegalPerson>("05X10 - Falha interna no servidor"));
        }
    }

    [HttpPut("legalperson/{id:int}")]
    public async Task<IActionResult> Put(
        [FromRoute] int id,
        [FromBody] LegalPersonViewModels model,
        [FromServices] DataContext context)
    {
        try
        {
            var category = await context
                .Legal
                .FirstOrDefaultAsync(x => x.Id == id);

            if (category == null)
                return NotFound(new ResultViewModel<LegalPerson>("05x06 - Conteúdo não encontrado"));

            category.Name = model.Name;
            category.CNPJ = model.CNPJ;
            category.Telephone = model.Telephone;
            category.PostedAt = DateTime.Now;

            context.Legal.Update(category);
            await context.SaveChangesAsync();

            return Ok(new ResultViewModel<LegalPerson>(category));
        }
        catch (DbUpdateException ex)
        {
            return StatusCode(500,
                new ResultViewModel<LegalPerson>("05XE8 - Não foi possivel alterar o seu cadastro"));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ResultViewModel<LegalPerson>("05X11 - Falha interna no servidor"));
        }
    }


    [HttpDelete("legalperson/{id:int}")]
    public async Task<IActionResult> DeleteAsync(
        [FromRoute] int id,
        [FromServices] DataContext context)
    {
        try
        {
            var category = await context
                .Legal
                .FirstOrDefaultAsync(x => x.Id == id);

            if (category == null)
                return NotFound(new ResultViewModel<LegalPerson>("05x06 - Conteúdo não encontrado"));

            context.Legal.Remove(category);
            await context.SaveChangesAsync();

            return Ok(new ResultViewModel<LegalPerson>(category));
        }
        catch (DbUpdateException ex)
        {
            return StatusCode(500, new ResultViewModel<LegalPerson>("05XE7 - Não foi possivel excluir o seu cadastro"));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ResultViewModel<LegalPerson>("05X12 - Falha interna no servidor"));
        }
    }
}
    
