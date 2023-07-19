using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeliculasAPI.Entities;
using PeliculasAPI.Entities.DTOs;

namespace PeliculasAPI.Controllers
{
    [ApiController]
    [Route("api/genres")]
    public class GenreController : ControllerBase
    {
        private readonly ApplicationDBContext context;
        private readonly IMapper mapper;

        public GenreController(ApplicationDBContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<GenreDTOResponse>>> GetAll()
        {
            var genres = await context.Genres.ToListAsync();
            var dtos = mapper.Map<List<GenreDTOResponse>>(genres);
            return dtos;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<GenreDTOResponse>> GetById(int id)
        {
            var genre = await context.Genres.FirstOrDefaultAsync(g => g.Id == id);
            if (genre == null)
            {
                return NotFound($"The genre with id:{id} not exists");
            }

            var dto = mapper.Map<GenreDTOResponse>(genre);
            return dto;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] GenreDTORequest dto)
        {
            var exists = await context.Genres.AnyAsync(g => g.Name == dto.Name);
            if (exists)
            {
                return BadRequest($"The genre {dto.Name} already exists");
            }

            var genre = mapper.Map<Genre>(dto);

            context.Genres.Add(genre);
            await context.SaveChangesAsync();

            return Ok(genre);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] GenreDTORequest dto)
        {
            var exists = context.Genres.AnyAsync(g => g.Id == id);
            if (!exists.Result)
            {
                return NotFound($"The genre with id:{id} not exists");
            }

            var genre = mapper.Map<Genre>(dto);
            genre.Id = id;

            context.Genres.Entry(genre).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exists = context.Genres.AnyAsync(g => g.Id == id);
            if (!exists.Result)
            {
                return NotFound($"The genre with id:{id} not exists");
            }

            context.Genres.Remove( new Genre
            {
                Id = id
            });
            await context.SaveChangesAsync();
            return NoContent();
        }

    }
}
