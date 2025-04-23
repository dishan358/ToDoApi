using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoApi.Data;
using ToDoApi.Models;

namespace ToDoApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ToDoController : ControllerBase
{
    private readonly ToDoContext _context;

    public ToDoController(ToDoContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ToDoItem>>> GetAll()
    {
        return await _context.ToDoItems.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ToDoItem>> Get(int id)
    {
        var item = await _context.ToDoItems.FindAsync(id);
        if (item == null) return NotFound();
        return item;
    }

    [HttpPost]
    public async Task<ActionResult<ToDoItem>> Post(ToDoItem item)
    {
        _context.ToDoItems.Add(item);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, ToDoItem item)
    {
        if (id != item.Id) return BadRequest();
        _context.Entry(item).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _context.ToDoItems.FindAsync(id);
        if (item == null) return NotFound();
        _context.ToDoItems.Remove(item);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}