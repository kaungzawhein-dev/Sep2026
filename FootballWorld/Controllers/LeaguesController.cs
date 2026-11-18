
using FootballWorld.Data;
using FootballWorld.Models;
using FootballWorld.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FootballWorld.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaguesController : ControllerBase
    {
        private readonly FootballDbContext _context;
        private readonly FootballService _service;

        public LeaguesController(
            FootballDbContext context,
            FootballService service)
        {
            _context = context;
            _service = service;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll(
            int pageNo = 1,
            int pageSize = 10,
            string? category = null)
        {
            var data = await _service.GetDataByPagination(
                pageNo, pageSize, category);

            return Ok(data);
        }


        [HttpGet("categories")]
        public async Task<IActionResult> GetCategories()
        {
            var data = await _service.GetCategoryList();

            return Ok(data);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _service.GetLeagueById(id);

            if (data == null)
                return NotFound("League not found.");

            return Ok(data);
        }

        // POST: api/leagues
        [HttpPost]
        public async Task<IActionResult> Create(League model)
        {
            model.Id = 0;

            _context.Leagues.Add(model);

            await _context.SaveChangesAsync();

            var result = await _service.GetLeagueById(model.Id);

            return CreatedAtAction(
                nameof(GetById),
                new { id = model.Id },
                result);
        }

        // PUT: api/leagues/1
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id, League model)
        {
            var league = await _context.Leagues.FindAsync(id);

            if (league == null)
                return NotFound("League not found.");

            league.Name = model.Name;
            league.Country = model.Country;
            league.LogoUrl = model.LogoUrl;

            await _context.SaveChangesAsync();

            return Ok(await _service.GetLeagueById(id));
        }

        // DELETE: api/leagues/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var league = await _context.Leagues.FindAsync(id);

            if (league == null)
                return NotFound("League not found.");

            _context.Leagues.Remove(league);

            await _context.SaveChangesAsync();

            return Ok("League deleted successfully.");
        }
    }
}