using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movie_Review_WebAPI.Data;
using Movie_Review_WebAPI.Models;

namespace Movie_Review_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StarController : ControllerBase
    {
        private readonly StarRepository _starRepository;

        public StarController(StarRepository starRepository)
        {
            _starRepository = starRepository;
        }

        #region Get All Stars
        [HttpGet]
        public IActionResult GetAllStars()
        {
            var stars = _starRepository.SelectAllStars();
            ApiResponse response = null;

            if (stars == null || !stars.Any())
            {
                response = new ApiResponse("No stars found", 404);
                return NotFound(response);
            }

            response = new ApiResponse(stars, "Stars fetched successfully", 200);
            return Ok(response);
        }
        #endregion

        #region Get Star By ID
        [HttpGet("{id}")]
        public IActionResult GetStarById(int id)
        {
            var star = _starRepository.SelectByPK(id);
            ApiResponse response = null;

            if (star == null)
            {
                response = new ApiResponse("Star not found", 404);
                return NotFound(response);
            }

            response = new ApiResponse(star, "Star details retrieved successfully", 200);
            return Ok(response);
        }
        #endregion

        #region Insert Star
        [HttpPost]
        public IActionResult InsertStar([FromBody] StarModel star)
        {
            ApiResponse response = null;

            if (star == null)
            {
                response = new ApiResponse("Star details are required", 400);
                return BadRequest(response);
            }

            bool isInserted = _starRepository.Insert(star);

            if (!isInserted)
            {
                response = new ApiResponse("Error while inserting star details", 500);
                return BadRequest(response);
            }

            response = new ApiResponse("Star inserted successfully", 200);
            return Ok(response);
        }
        #endregion

        #region Update Star
        [HttpPatch("{id}")]
        public IActionResult UpdateStar(int id, [FromBody] StarModel star)
        {
            ApiResponse response = null;

            if (star == null || id != star.StarId)
            {
                response = new ApiResponse("Star not found or invalid details", 400);
                return BadRequest(response);
            }

            var isUpdated = _starRepository.Update(star);

            if (!isUpdated)
            {
                response = new ApiResponse("Error while updating star details", 500);
                return BadRequest(response);
            }

            response = new ApiResponse("Star updated successfully", 200);
            return Ok(response);
        }
        #endregion

        #region Delete Star
        [HttpDelete("{id}")]
        public IActionResult DeleteStar(int id)
        {
            ApiResponse response = null;

            var isDeleted = _starRepository.Delete(id);

            if (!isDeleted)
            {
                response = new ApiResponse("Error while deleting star", 500);
                return BadRequest(response);
            }

            response = new ApiResponse("Star deleted successfully", 200);
            return Ok(response);
        }
        #endregion
    }
}
