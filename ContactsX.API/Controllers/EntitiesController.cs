using Microsoft.AspNetCore.Mvc;
using ContactsX.Application.Interfaces.Services;
using ContactsX.Application.DTOs.Entity;

[ApiController]
[Route("api/entities")]
public class EntitiesController : ControllerBase
{
    private readonly IEntityService _entityService;

    public EntitiesController(IEntityService entityService)
    {
        _entityService = entityService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _entityService.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var entity = await _entityService.GetByIdAsync(id);

        if (entity == null)
            return NotFound();

        return Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateEntityDto dto)
    {
        var entity = await _entityService.CreateAsync(dto);

        return Ok(entity);
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateEntityDto dto)
    {
        var entity = await _entityService.UpdateAsync(dto);

        if (entity == null)
            return NotFound();

        return Ok(entity);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _entityService.DeleteAsync(id);

        if (!result)
            return NotFound();

        return Ok();
    }
}