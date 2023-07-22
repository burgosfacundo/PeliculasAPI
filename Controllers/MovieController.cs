using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeliculasAPI.Entities;
using PeliculasAPI.Entities.DTOs;
using PeliculasAPI.Helpers;
using PeliculasAPI.Services;
using System.ComponentModel;

namespace PeliculasAPI.Controllers
{
    [ApiController]
    [Route("api/movies")]
    public class MovieController : ControllerBase
    {
        private readonly ApplicationDBContext context;
        private readonly IMapper mapper;
        private readonly IStoreFile storeFile;
        private readonly string container = "movies";

        public MovieController(ApplicationDBContext context,IMapper mapper,IStoreFile storeFile)
        {
            this.context = context;
            this.mapper = mapper;
            this.storeFile = storeFile;
        }

        [HttpGet]
        public async Task<ActionResult<List<MovieDTOResponse>>> GetAll([FromQuery] PaginationDTO paginationDTO)
        {
            var queryable = context.Movies.AsQueryable();
            await HttpContext.InsertParamsPagination(queryable, paginationDTO.RecordsPerPage);

            var movies = await queryable.Paginate(paginationDTO).ToListAsync();
            return mapper.Map<List<MovieDTOResponse>>(movies);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<MovieDTOResponse>> GetById(int id) 
        {
            var movie = await context.Movies.FirstOrDefaultAsync(m => m.Id == id);
            if(movie == null)
            {
                return NotFound($"The movie with id:{id} doesn't exist");
            }

            return mapper.Map<MovieDTOResponse>(movie);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] MovieDTORequest dto)
        {
            var exists = await context.Movies.AnyAsync(m => m.Title == dto.Title);
            if (exists)
            {
                return BadRequest($"A movie with the title {dto.Title} already exists");
            }

            var movie = mapper.Map<Movie>(dto);

            if (dto.Image != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await dto.Image.CopyToAsync(memoryStream);
                    var content = memoryStream.ToArray();
                    var extension = Path.GetExtension(dto.Image.FileName);
                    movie.Image = await storeFile.SaveFile(content, extension, container, dto.Image.ContentType);
                }
            }

            context.Movies.Add(movie);
            await context.SaveChangesAsync();

            return Ok(movie);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromForm] MovieDTORequest dto)
        {
            var moviesDB = await context.Movies.FirstOrDefaultAsync(m => m.Id == id);
            if (moviesDB == null)
            {
                return NotFound($"The movie with id:{id} doesn't exist");
            }

            moviesDB = mapper.Map(dto, moviesDB);

            if (dto.Image != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await dto.Image.CopyToAsync(memoryStream);
                    var content = memoryStream.ToArray();
                    var extension = Path.GetExtension(dto.Image.FileName);
                    moviesDB.Image = await storeFile.EditFile(content, extension, container, moviesDB.Image,
                                                                    dto.Image.ContentType);
                }
            }

            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<MovieDTOPatch> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }

            var movie = await context.Movies.FirstOrDefaultAsync(m => m.Id == id);

            if (movie == null)
            {
                return NotFound($"The movie with id:{id} doesn't exist");
            }

            var dto = mapper.Map<MovieDTOPatch>(movie);

            patchDocument.ApplyTo(dto, ModelState);
            var valid = TryValidateModel(dto);

            if (!valid)
            {
                return BadRequest(ModelState);
            }

            mapper.Map(dto, movie);
            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exists = context.Movies.AnyAsync(m => m.Id == id);
            if (!exists.Result)
            {
                return NotFound($"The movie with id:{id} doesn't exist");
            }

            context.Movies.Remove(new Movie
            {
                Id = id
            });
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
