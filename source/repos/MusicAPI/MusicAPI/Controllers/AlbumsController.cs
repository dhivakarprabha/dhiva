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
    public class AlbumsController : ControllerBase
    {
        private ApiDBContext _dbContext;

        public AlbumsController(ApiDBContext dbcontext)
        {
            _dbContext = dbcontext;
        }


        // POST api/<AlbumsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] Album album)
        {


            album.ImageUrl = await FileHelper.UploadFile(album.Image);

            await _dbContext.AddAsync(album);
            await _dbContext.SaveChangesAsync();

            return StatusCode(StatusCodes.Status201Created);



        }

        [HttpGet]
        public async Task<IActionResult> AlbumsGet(int? pageNumber, int? pageSize)
        {
            var currentPageNumber = pageNumber ?? 1;
            var currentPageSize = pageSize ?? 5;
            var albumlist = await (from albums in _dbContext.albums
                                    select new
                                    {
                                        Id = albums.Id,
                                        Name = albums.Name,
                                        ImageUrl = albums.ImageUrl
                                    }).ToListAsync();
            return Ok(albumlist.Skip((currentPageNumber - 1) * currentPageSize).Take(currentPageSize));
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> AlbumDetails(int albumId)
        {
            var albums = await _dbContext.albums.Where(a => a.Id == albumId).Include(a => a.Songs).ToListAsync();
            return Ok(albums);
        }
    }
}
