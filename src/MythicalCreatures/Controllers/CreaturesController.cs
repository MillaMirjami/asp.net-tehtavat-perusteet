using Microsoft.AspNetCore.Mvc;
using MythicalCreatures.Models;
using MythicalCreatures.Data;

namespace MythicalCreatures.Controllers;

[ApiController]
[Route("[controller]")]
public class CreaturesController : ControllerBase
{
    private readonly CreatureRepository _repository;

    public CreaturesController()
    {
        _repository = new CreatureRepository();
    }

    [HttpGet]
    public ActionResult<IEnumerable<Creature>> GetAll()
    {
        var creatures = _repository.LoadCreatures();
        return Ok(creatures);
    }

    [HttpGet("{id}")]
    public ActionResult<Creature> Get(int id)
    {
        var creatures = _repository.LoadCreatures();
        var creature = creatures.FirstOrDefault(c => c.Id == id);
        return creature is null ? NotFound() : Ok(creature);
    }

    [HttpPost]
    public ActionResult Add(Creature creature)
    {
        var creatures = _repository.LoadCreatures();
        creature.Id = creatures.Any() ? creatures.Max(c => c.Id) + 1 : 1;
        creatures.Add(creature);
        _repository.SaveCreatures(creatures);
        return CreatedAtAction(nameof(Get), new { id = creature.Id }, creature);
    }

    [HttpPut("{id}")]
    public ActionResult Update(int id, Creature updatedCreature)
    {
        var creatures = _repository.LoadCreatures();
        var index = creatures.FindIndex(c => c.Id == id);
        if (index == -1) return NotFound();

        creatures[index] = updatedCreature;
        _repository.SaveCreatures(creatures);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        var creatures = _repository.LoadCreatures();
        var creature = creatures.FirstOrDefault(c => c.Id == id);
        if (creature is null) return NotFound();

        creatures.Remove(creature);
        _repository.SaveCreatures(creatures);
        return NoContent();
    }
}