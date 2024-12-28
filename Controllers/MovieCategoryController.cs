using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movie_Review_WebAPI.Data;
using Movie_Review_WebAPI.Models;

namespace Movie_Review_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieCategoryController : ControllerBase
    {
        private readonly MovieCategoryRepository _movieCategoryRepository;

        public MovieCategoryController(MovieCategoryRepository movieCategoryRepository)
        {
            _movieCategoryRepository = movieCategoryRepository;
        }


        [HttpGet("movie/{movieid}")]
        public IActionResult GetAllCategoryByMovieId(int movieid) 
        {
            var category = _movieCategoryRepository.GetAllCategoryByMovieID(movieid);
            ApiResponse response;

            if (category == null)
            {
                response = new ApiResponse("Category not found for this Movie", 404);
                return NotFound(response);
            }

            response = new ApiResponse(category, "Movie Category retrieved successfully", 200);
            return Ok(response);
        }


        [HttpGet("category/{categoryid}")]
        public IActionResult GetAllMovieByCategoryId(int categoryid)
        {
            var category = _movieCategoryRepository.GetAllMovieByCategoryId(categoryid);
            ApiResponse response;

            if (category == null)
            {
                response = new ApiResponse("Movie not found for this Category", 404);
                return NotFound(response);
            }

            response = new ApiResponse(category, "Movie Category retrieved successfully", 200);
            return Ok(response);
        }


        [HttpPost("{movieid}/{categoryid}")]
        public IActionResult InsertMovieWithCategory(int movieid,int categoryid)
        {
            ApiResponse response;

            if (movieid == null || categoryid == null)
            {
                response = new ApiResponse("Movieid or categoryid required", 400);
                return BadRequest(response);
            }

            bool isInserted = _movieCategoryRepository.Insert(movieID: movieid, categoryID: categoryid);

            if (!isInserted)
            {
                response = new ApiResponse("Error while inserting movie details", 500);
                return StatusCode(500, response);
            }

            response = new ApiResponse("Movie details inserted successfully", 200);
            return Ok(response);
        }


        [HttpDelete("movie/{movieid}/{categoryid}")]
        public IActionResult DeleteMovieCategory(int movieid, int categoryid)
        {
            ApiResponse response;

            bool isDeleted = _movieCategoryRepository.Delete(movieID:movieid,categoryID:categoryid);

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
