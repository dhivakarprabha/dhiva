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
    public class SongsController : ControllerBase
    {
        private ApiDBContext _dbContext;

        public SongsController (ApiDBContext dbcontext)
        {
            _dbContext = dbcontext;
        }

        // POST api/<SongsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] Song song)
        {


            song.ImageUrl = await FileHelper.UploadFile(song.Image);
            song.AudioUrl = await FileHelper.UploadAudio(song.AudioFile);
            song.UplodatedDate = DateTime.Now;

            await _dbContext.AddAsync(song);
            await _dbContext.SaveChangesAsync();

            return StatusCode(StatusCodes.Status201Created);



        }

        [HttpGet]
        public async Task<IActionResult> GetAllSongs(int? pageNumber, int? pageSize)
        {
            var currentPageNumber = pageNumber ?? 1;
            var currentPageSize = pageSize ?? 5;
            
            var songlist = await (from song in _dbContext.songs
                                  select new
                                  {
                                      Id = song.Id,
                                      Title = song.Title,
                                      Duration = song.Duration,
                                      ImageUrl = song.ImageUrl,
                                      AudioUrl = song.AudioUrl
                                  }).ToListAsync();

            return Ok(songlist.Skip((currentPageNumber - 1)* currentPageSize).Take(currentPageSize));
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> FeaturedSongs()
        {
            var songlist = await (from song in _dbContext.songs
                                  where song.IsFeatured == true
                                  select new
                                  {
                                      Id = song.Id,
                                      Title = song.Title,
                                      Duration = song.Duration,
                                      ImageUrl = song.ImageUrl,
                                      AudioUrl = song.AudioUrl
                                  }).ToListAsync();

            return Ok(songlist);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> NewSongs()
        {
            var songlist = await (from song in _dbContext.songs
                                  orderby song.UplodatedDate descending
                                  select new
                                  {
                                      Id = song.Id,
                                      Title = song.Title,
                                      Duration = song.Duration,
                                      ImageUrl = song.ImageUrl,
                                      AudioUrl = song.AudioUrl
                                  }).ToListAsync();

            return Ok(songlist);
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> SearchSongs(string query)
        {
            var songlist = await (from song in _dbContext.songs
                                  where song.Title.StartsWith(query)
                                  select new
                                  {
                                      Id = song.Id,
                                      Title = song.Title,
                                      Duration = song.Duration,
                                      ImageUrl = song.ImageUrl,
                                      AudioUrl = song.AudioUrl
                                  }).ToListAsync();

            return Ok(songlist);
        }

    }
}
