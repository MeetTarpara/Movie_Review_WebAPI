using Microsoft.AspNetCore.Mvc;
using Movie_Review_WebAPI.Data;
using Movie_Review_WebAPI.Models;

namespace Movie_Review_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly MovieRepository _movieRepository;

        public MovieController(MovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        [HttpGet]
        public IActionResult GetAllMovies()
        {
            var movies = _movieRepository.SelectAllMovies();
            ApiResponse response;

            if (movies == null || !movies.Any())
            {
                response = new ApiResponse("Movies not found", 404);
                return NotFound(response);
            }

            response = new ApiResponse(movies, "Movies fetched successfully", 200);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetMovieById(int id)
        {
            var movie = _movieRepository.SelectByPK(id);
            ApiResponse response;

            if (movie == null)
            {
                response = new ApiResponse("Movie not found", 404);
                return NotFound(response);
            }

            response = new ApiResponse(movie, "Movie details retrieved successfully", 200);
            return Ok(response);
        }

        [HttpPost]
        public IActionResult InsertMovie([FromBody] MovieModel movie)
        {
            ApiResponse response;

            if (movie == null)
            {
                response = new ApiResponse("Movie details are required", 400);
                return BadRequest(response);
            }

            bool isInserted = _movieRepository.Insert(movie);

            if (!isInserted)
            {
                response = new ApiResponse("Error while inserting movie details", 500);
                return StatusCode(500, response);
            }

            response = new ApiResponse("Movie details inserted successfully", 200);
            return Ok(response);
        }

        [HttpPatch("{id}")]
        public IActionResult UpdateMovie(int id, [FromBody] MovieModel movie)
        {
            ApiResponse response;

            if (movie == null || id != movie.MovieID)
            {
                response = new ApiResponse("Invalid movie details", 400);
                return BadRequest(response);
            }

            bool isUpdated = _movieRepository.Update(movie);

            if (!isUpdated)
            {
                response = new ApiResponse("Error while updating movie details", 500);
                return StatusCode(500, response);
            }

            response = new ApiResponse("Movie details updated successfully", 200);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMovie(int id)
        {
            ApiResponse response;

            bool isDeleted = _movieRepository.Delete(id);

            if (!isDeleted)
            {
                response = new ApiResponse("Error while deleting movie", 500);
                return StatusCode(500, response);
            }

            response = new ApiResponse("Movie deleted successfully", 200);
            return Ok(response);
        }
    }
}