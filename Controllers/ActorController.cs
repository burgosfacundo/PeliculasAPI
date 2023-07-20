using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeliculasAPI.Entities;
using PeliculasAPI.Entities.DTOs;

namespace PeliculasAPI.Controllers
{
    [ApiController]
    [Route("api/actors")]
    public class ActorController : ControllerBase
    {
        private readonly ApplicationDBContext context;
        private readonly IMapper mapper;

        public ActorController(ApplicationDBContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ActorDTOResponse>>> GetAll()
        {
            var actors = await context.Actors.ToListAsync();
            return mapper.Map<List<ActorDTOResponse>>(actors);   
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ActorDTOResponse>> GetById(int id)
        {
            var actor = await context.Actors.FirstOrDefaultAsync(a => a.Id == id);
            if (actor == null)
            {
                return NotFound($"The actor with id:{id} not exists");
            }
            return mapper.Map<ActorDTOResponse>(actor);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] ActorDTORequest dto)
        {
            var exists = await context.Actors.AnyAsync(a => a.Name == dto.Name);
            if (exists)
            {
                return BadRequest($"An actor with the name {dto.Name} already exists");
            }

            var actor = mapper.Map<Actor>(dto);

            context.Actors.Add(actor);
            await context.SaveChangesAsync();

            return Ok(actor);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromForm] ActorDTORequest dto) 
        {
            var exists = context.Actors.AnyAsync(a => a.Id == id);
            if (!exists.Result)
            {
                return NotFound($"The actor with id:{id} not exists");
            }

            var actor = mapper.Map<Actor>(dto);
            actor.Id = id;

            context.Actors.Entry(actor).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exists = context.Actors.AnyAsync(a => a.Id == id);
            if (!exists.Result)
            {
                return NotFound($"The actor with id:{id} not exists");
            }

            context.Actors.Remove(new Actor
            {
                Id = id
            });
            await context.SaveChangesAsync();
            return NoContent();
        }

    }
}
