using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movie_Review_WebAPI.Data;
using Movie_Review_WebAPI.Models;

namespace Movie_Review_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieStarController : ControllerBase
    {

        private readonly MovieStarRepository _movieStarRepository;

        public MovieStarController(MovieStarRepository movieCategoryRepository)
        {
            _movieStarRepository = movieCategoryRepository;
        }

        [HttpGet("movie/{movieid}")]
        public IActionResult GetAllStarByMovieId(int movieid)
        {
            var category = _movieStarRepository.GetAllStarByMovieID(movieid);
            ApiResponse response;

            if (category == null)
            {
                response = new ApiResponse("Category not found for this Movie", 404);
                return NotFound(response);
            }

            response = new ApiResponse(category, "Movie Category retrieved successfully", 200);
            return Ok(response);
        }



        [HttpGet("star/{starid}")]
        public IActionResult GetAllMovieByCategoryId(int starid)
        {
            var category = _movieStarRepository.GetAllMovieByStarId(starid);
            ApiResponse response;

            if (category == null)
            {
                response = new ApiResponse("Movie not found for this Category", 404);
                return NotFound(response);
            }

            response = new ApiResponse(category, "Movie Category retrieved successfully", 200);
            return Ok(response);
        }


        [HttpPost("{movieid}/{starid}")]
        public IActionResult InsertMovieWithCategory(int movieid, int starid)
        {
            ApiResponse response;

            if (movieid == null || starid == null)
            {
                response = new ApiResponse("Movieid or categoryid required", 400);
                return BadRequest(response);
            }

            bool isInserted = _movieStarRepository.Insert(movieID: movieid, starID: starid);

            if (!isInserted)
            {
                response = new ApiResponse("Error while inserting movie details", 500);
                return StatusCode(500, response);
            }

            response = new ApiResponse("Movie details inserted successfully", 200);
            return Ok(response);
        }



        [HttpDelete("movie/{movieid}/{starid}")]
        public IActionResult DeleteMovieCategory(int movieid, int starid)
        {
            ApiResponse response;

            bool isDeleted = _movieStarRepository.Delete(movieID: movieid, starID: starid);

            if (!isDeleted)
            {
                response = new ApiResponse("Error while deleting movie category", 500);
                return StatusCode(500, response);
            }

            response = new ApiResponse("Movie category deleted successfully", 200);
            return Ok(response);
        }

    }
}
