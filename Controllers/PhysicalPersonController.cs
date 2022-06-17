using AIRLINE.API.Data;
using AIRLINE.API.Models;
using AIRLINE.API.Models.Providers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AIRLINE.API.ViewModel;


namespace AIRLINE.API.Controllers;

    [Controller]
    [Route("v1")]
    public class PhysicalPersonController : ControllerBase
{
    [HttpGet("physicalperson")]
    public async Task<IActionResult> Get(
        [FromServices] DataContext context)
    {
        try
        {
             var todos = await context
            .Physical
            .AsNoTracking()
            .ToListAsync();
             
             return Ok(new ResultViewModel<List<PhysicalPerson>>(todos));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<List<PhysicalPerson>>("05X04 - Falha interna no servidor"));
        }
    }

    private List<PhysicalPerson> GetPerson(DataContext context)
    {
        return context.Physical.ToList();
    }
    

    [HttpGet("physicalperson/{id}")]
    public async Task<IActionResult> GetById(
        [FromServices] DataContext context,
        [FromRoute] int id)
    {
        try
        {
            var list = await context
                .Physical
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (list == null)
                return NotFound();

            return Ok(list);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ResultViewModel<PhysicalPerson>("05X05 - Falha interna no servidor"));
        }
    }

    [HttpPost("physicalperson")]
    public async Task<IActionResult> Post(
        [FromBody] PhysicalPersonViewModel model,
        [FromServices] DataContext context)
    {
        if (!ModelState.IsValid)
            return BadRequest();
        
        var client = new PhysicalPerson
            {
                Name = model.Name,
                CPF = model.CPF,
                Telephone = model.Telephone,
                PostedAt = DateTime.Now,
                RG = model.RG,
                BirthDate = model.BirthDate
            }; 
        try 
        {
            await context.Physical.AddAsync(client);
            await context.SaveChangesAsync();

            return Created($"v1/physicalperson/{client.Id}", new ResultViewModel<PhysicalPerson>(client));
        }
        catch (DbUpdateException ex)
        {

            return StatusCode(500,
                new ResultViewModel<PhysicalPerson>("05XE9 - Não foi possivel incluir o seu cadastro"));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<PhysicalPerson>("05X10 - Falha interna no servidor"));
        }
    }

    [HttpPut("physicalperson/{id:int}")]
    public async Task<IActionResult> Put(
        [FromRoute] int id,
        [FromBody] PhysicalPersonViewModel model,
        [FromServices] DataContext context)
    {
        try
        {
            var category = await context
                .Physical
                .FirstOrDefaultAsync(x => x.Id == id);

            if (category == null)
                return NotFound(new ResultViewModel<PhysicalPerson>("05x06 - Conteúdo não encontrado"));

            category.Name = model.Name;
            category.CPF = model.CPF;
            category.Telephone = model.Telephone;
            category.PostedAt = DateTime.Now;
            category.RG = model.RG;
            category.BirthDate = model.BirthDate;

            context.Physical.Update(category);
            await context.SaveChangesAsync();

            return Ok(new ResultViewModel<PhysicalPerson>(category));
        }
        catch (DbUpdateException ex)
        {
            return StatusCode(500,
                new ResultViewModel<PhysicalPerson>("05XE8 - Não foi possivel alterar o seu cadastro"));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ResultViewModel<PhysicalPerson>("05X11 - Falha interna no servidor"));
        }
    }


    [HttpDelete("physicalperson/{id:int}")]
    public async Task<IActionResult> Delete(
        [FromRoute] int id,
        [FromServices] DataContext context)
    {
        try
        {
            var category = await context
                .Physical
                .FirstOrDefaultAsync(x => x.Id == id);

            if (category == null)
                return NotFound(new ResultViewModel<PhysicalPerson>("05x06 - Conteúdo não encontrado"));

            context.Physical.Remove(category);
            await context.SaveChangesAsync();

            return Ok(new ResultViewModel<PhysicalPerson>(category));
        }
        catch (DbUpdateException ex)
        {
            return StatusCode(500, new ResultViewModel<PhysicalPerson>("05XE7 - Não foi possivel excluir o seu cadastro"));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ResultViewModel<PhysicalPerson>("05X12 - Falha interna no servidor"));
        }
    }
}