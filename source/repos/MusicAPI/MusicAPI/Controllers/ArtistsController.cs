using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicAPI.Data;
using MusicAPI.Helper;
using MusicAPI.Models;

namespace MusicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController : ControllerBase
    {
        private ApiDBContext _dbContext;

        public ArtistsController(ApiDBContext dbcontext)
        {
            _dbContext = dbcontext;
        }


        // POST api/<ArtistsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] Artist artist)
        {


            artist.ImageUrl = await FileHelper.UploadFile(artist.Image);

            await _dbContext.AddAsync(artist);
            await _dbContext.SaveChangesAsync();

            return StatusCode(StatusCodes.Status201Created);



        }

        [HttpGet]
        public async Task<IActionResult> ArtistsGet(int? pageNumber, int? pageSize)
        {
            var currentPageNumber = pageNumber ?? 1;
            var currentPageSize = pageSize ?? 5;
            var artistlist = await (from artists in _dbContext.artists
                          select new
                          {
                              Id = artists.Id,
                              Name = artists.Name,
                              ImageUrl = artists.ImageUrl
                          }).ToListAsync();
            return Ok(artistlist.Skip((currentPageNumber - 1) * currentPageSize).Take(currentPageSize));
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> ArtistDetails(int artistId)
        {
            var artists = await _dbContext.artists.Where(a => a.Id == artistId).Include(a => a.Songs).Include(a => a.Albums).ToListAsync();
            return Ok(artists);
        }
    }
}
