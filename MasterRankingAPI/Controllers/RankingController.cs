using MasterRankingAPI.APIModels;
using MasterRankingAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace MasterRankingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RankingController : Controller
    {

        private readonly AppDbContext _context;

        public RankingController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("courses")]
        public async Task<ActionResult<List<Courses>>> GetCourses(Appdata appdata)
        {
            var loggedin = await Functions.IsUserLoggedIn(_context, appdata);
            if (!loggedin)
                return BadRequest("User has been logged out");
            var courses = await _context.TextReviews.GroupBy(n => n.Course).Select(n => new Courses { ID = n.Key, Description = $"Course: {Functions.GetCourseName(n.Key, _context)} ID: {n.Key}, Count: {n.Count()}" }).ToListAsync();
            return courses;
        }

        [HttpPost("reviews")]
        public async Task<ActionResult<string>> GetReviews(Appdata appdata)
        {
            var loggedin = await Functions.IsUserLoggedIn(_context, appdata);
            if (!loggedin)
                return BadRequest("User has been logged out");

            var reviews = await _context.TextReviews.Where(n => n.Course == appdata.SelectedCourse).ToArrayAsync();
            if (reviews.Length == 0)
                return BadRequest("Non-existing course has been selected");

            var revs = Functions.LoadReviews(reviews, appdata.UserName);
            var test = JsonConvert.SerializeObject(revs);
            return test;
        }

        [HttpPost("comparisons")]
        public async Task<ActionResult> SaveComparisons(RankingData rankings)
        {
            var loggedin = await Functions.IsUserLoggedIn(_context, rankings.appdata);
            if (!loggedin)
                return BadRequest("User has been logged out");
            Functions.SaveComparisons(rankings.rankingInfos.ToArray(), rankings.appdata.UserName);
            return Ok();
        }

    }
}
