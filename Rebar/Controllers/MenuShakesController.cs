﻿using Microsoft.AspNetCore.Mvc;
using Rebar.Data;
using Rebar.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using Rebar.Model;


[ApiController]
[Route("api/[controller]")]
public class MenuShakeController : ControllerBase
{
    private readonly MongoDBContext _context;

    public MenuShakeController(MongoDBContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IEnumerable<MenuShake>> Get()
    {
        return await _context.MenuShakes.Find(_ => true).ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MenuShake>> Get(string id)
    {
        if (!Guid.TryParse(id, out Guid guid))
        {
            return BadRequest("Invalid ID format.");
        }
        var shake = await _context.MenuShakes.Find(p => p.ID == guid).FirstOrDefaultAsync();

        if (shake == null)
        {
            return NotFound();
        }

        return shake;
    }

    [HttpPost]
    public async Task<ActionResult<MenuShake>> Create(MenuShake shake)
    {
        await _context.MenuShakes.InsertOneAsync(shake);
        return CreatedAtRoute(new { id = shake.ID }, shake);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, MenuShake shakeIn)
    {
        if (!Guid.TryParse(id, out Guid guid))
        {
            return BadRequest("Invalid ID format.");
        }
        var shake = await _context.MenuShakes.Find(p => p.ID == guid).FirstOrDefaultAsync();

        if (shake == null)
        {
            return NotFound();
        }

        await _context.MenuShakes.ReplaceOneAsync(p => p.ID ==guid, shakeIn);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        if (!Guid.TryParse(id, out Guid guid))
        {
            return BadRequest("Invalid ID format.");
        }
        var shake = await _context.MenuShakes.Find(p => p.ID == guid).FirstOrDefaultAsync();

        if (shake == null)
        {
            return NotFound();
        }

        await _context.MenuShakes.DeleteOneAsync(p => p.ID == guid);

        return NoContent();
    }
}
