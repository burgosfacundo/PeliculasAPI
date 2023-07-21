using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeliculasAPI.Entities;
using PeliculasAPI.Entities.DTOs;
using PeliculasAPI.Services;

namespace PeliculasAPI.Controllers
{
    [ApiController]
    [Route("api/actors")]
    public class ActorController : ControllerBase
    {
        private readonly ApplicationDBContext context;
        private readonly IMapper mapper;
        private readonly IStoreFile storeFile;
        private readonly string container = "actors";

        public ActorController(ApplicationDBContext context, IMapper mapper, IStoreFile storeFile)
        {
            this.context = context;
            this.mapper = mapper;
            this.storeFile = storeFile;
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

            if (dto.Image != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await dto.Image.CopyToAsync(memoryStream);
                    var content = memoryStream.ToArray();
                    var extension = Path.GetExtension(dto.Image.FileName);
                    actor.Image = await storeFile.SaveFile(content, extension, container, dto.Image.ContentType);
                }
            }

            context.Actors.Add(actor);
            await context.SaveChangesAsync();

            return Ok(actor);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromForm] ActorDTORequest dto) 
        {
            var actorDB = await context.Actors.FirstOrDefaultAsync(a => a.Id == id);
            if (actorDB == null)
            {
                return NotFound($"The actor with id:{id} not exists");
            }

            actorDB = mapper.Map(dto,actorDB);

            if (dto.Image != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await dto.Image.CopyToAsync(memoryStream);
                    var content = memoryStream.ToArray();
                    var extension = Path.GetExtension(dto.Image.FileName);
                    actorDB.Image = await storeFile.EditFile(content, extension, container,actorDB.Image,
                                                                    dto.Image.ContentType);
                }
            }

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
