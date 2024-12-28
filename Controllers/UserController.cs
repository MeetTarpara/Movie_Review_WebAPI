using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movie_Review_WebAPI.Data;
using Movie_Review_WebAPI.Models;

namespace Movie_Review_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly UserRepository _userRepository;

        public UserController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult GetAllUser() { 
            var user = _userRepository.SelectAllUser();
            ApiResponse response = null;
            if (user==null)
            {
                response = new ApiResponse("User not found", 404);
                return NotFound(response);

            }
            response = new ApiResponse(user, "User Featched", 200);
            return Ok(response);

        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id) 
        {
            var user = _userRepository.SelectByPK(id);
            ApiResponse response = null;
            if (user == null)
            {
                response = new ApiResponse("User Details are required", 400);
                return BadRequest(response);
            }
            response = new ApiResponse(user,"User Detail Retrived Succesfully", 200);
            return Ok(response);

        }


        [HttpPost]
        public IActionResult InsertUser([FromBody] UserModel user) {
            ApiResponse response = null;
            if (user == null)
            {
                response = new ApiResponse("User Details are required", 400);
                return BadRequest(response);
            }

            bool isInserted = _userRepository.Insert(user);

            if (!isInserted)
            {
                response = new ApiResponse("Error while inserting User detail", 500);
                return BadRequest(response);
            }

            response = new ApiResponse("User Detail Insert Succesfully", 200);
            return Ok(response);

        }

        [HttpPatch("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] UserModel user) 
        {
            ApiResponse response = null;
            if (user == null || id != user.UserID)
            {
                response = new ApiResponse("User Not exists", 400);
                return BadRequest(response);
            }

            var isUpdated = _userRepository.UpdateAll(user);
            if (!isUpdated)
            {
                response = new ApiResponse("Error while Updating User detail", 500);
                return BadRequest(response);
            }
            response = new ApiResponse("User Detail Updated Succesfully", 200);
            return Ok(response);
        }

    }
}
