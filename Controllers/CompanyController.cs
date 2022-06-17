using AIRLINE.API.Data;
using AIRLINE.API.Models;
using AIRLINE.API.Models.Company;
using AIRLINE.API.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AIRLINE.API.Controllers;

    [Controller]
    [Route("v1")]
    public class CompanyController : ControllerBase
{
    [HttpGet("company")]
    public async Task<IActionResult> Get(
        [FromServices] DataContext context)
    {
        try
        {
            var todos = await context
                .Companies
                .AsNoTracking()
                .ToListAsync();

            return Ok(new ResultViewModel<List<Company>>(todos));
        }
        catch (Exception e)
        {
            return StatusCode(500, new ResultViewModel<List<Company>>("05X04 - Falha interna no servidor"));
        }
    }

    [HttpGet("company/{id}")]
    public async Task<IActionResult> GetById(
        [FromServices] DataContext context,
        [FromRoute] int id)

    {
        try
        {
            var list = await context
                .Companies
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (list == null)
                return NotFound();

            return Ok(list);
        }
        catch (Exception e)
        {
            return StatusCode(500, new ResultViewModel<List<Company>>("05X04 - Falha interna no servidor"));
        }
    }
    
    [HttpPost("company")]
    public async Task<IActionResult> Post(
        [FromBody] CompanyViewModel model,
        [FromServices] DataContext context)
    {
        if (!ModelState.IsValid)
            return BadRequest();
        
        var client = new Company
            {
                Name = model.Name,
                CNPJ = model.CNPJ,
                UF = model.UF
            }; 
        try 
        {
            await context.Companies.AddAsync(client);
            await context.SaveChangesAsync();
            return Created($"v1/company/{client.Id}", new ResultViewModel<Company>(client));
        }
        catch (DbUpdateException ex)
        {
            return StatusCode(500,
                new ResultViewModel<Company>("05XE9 - Não foi possivel incluir o seu cadastro"));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Company>("05X10 - Falha interna no servidor"));
        }
    }

    [HttpPut("company/{id:int}")]
    public async Task<IActionResult> Put(
        [FromRoute] int id,
        [FromBody] CompanyViewModel model,
        [FromServices] DataContext context)
    {
        try
        {
            var category = await context
                .Companies
                .FirstOrDefaultAsync(x => x.Id == id);

            if (category == null)
                return NotFound(new ResultViewModel<Company>("05x06 - Conteúdo não encontrado"));

            category.Name = model.Name;
            category.CNPJ = model.CNPJ;
            category.UF = model.UF;

            context.Companies.Update(category);
            await context.SaveChangesAsync();

            return Ok(new ResultViewModel<Company>(category));
        }
        catch (DbUpdateException ex)
        {
            return StatusCode(500,
                new ResultViewModel<Company>("05XE8 - Não foi possivel alterar o seu cadastro"));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ResultViewModel<Company>("05X11 - Falha interna no servidor"));
        }
    }


    [HttpDelete("company/{id:int}")]
    public async Task<IActionResult> Delete(
        [FromRoute] int id,
        [FromServices] DataContext context)
    {
        try
        {
            var category = await context
                .Companies
                .FirstOrDefaultAsync(x => x.Id == id);

            if (category == null)
                return NotFound(new ResultViewModel<Company>("05x06 - Conteúdo não encontrado"));

            context.Companies.Remove(category);
            await context.SaveChangesAsync();

            return Ok(new ResultViewModel<Company>(category));
        }
        catch (DbUpdateException ex)
        {
            return StatusCode(500, new ResultViewModel<Company>("05XE7 - Não foi possivel excluir o seu cadastro"));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ResultViewModel<Company>("05X12 - Falha interna no servidor"));
        }
    }
}
