using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movie_Review_WebAPI.Data;
using Movie_Review_WebAPI.Models;

namespace Movie_Review_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryRepository _categoryRepository;

        public CategoryController(CategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        #region Get All Categories
        [HttpGet]
        public IActionResult GetAllCategories()
        {
            var categories = _categoryRepository.SelectAllCategories();
            ApiResponse response = null;
            if (categories == null || !categories.Any())
            {
                response = new ApiResponse("No categories found", 404);
                return NotFound(response);
            }
            response = new ApiResponse(categories, "Categories fetched successfully", 200);
            return Ok(response);
        }
        #endregion

        #region Get Category By ID
        [HttpGet("{id}")]
        public IActionResult GetCategoryById(int id)
        {
            var category = _categoryRepository.SelectByPK(id);
            ApiResponse response = null;
            if (category == null)
            {
                response = new ApiResponse("Category not found", 404);
                return NotFound(response);
            }
            response = new ApiResponse(category, "Category details retrieved successfully", 200);
            return Ok(response);
        }
        #endregion

        #region Insert Category
        [HttpPost]
        public IActionResult InsertCategory([FromBody] CategoryModel category)
        {
            ApiResponse response = null;
            if (category == null)
            {
                response = new ApiResponse("Category details are required", 400);
                return BadRequest(response);
            }

            bool isInserted = _categoryRepository.Insert(category);

            if (!isInserted)
            {
                response = new ApiResponse("Error while inserting category details", 500);
                return BadRequest(response);
            }

            response = new ApiResponse("Category inserted successfully", 200);
            return Ok(response);
        }
        #endregion

        #region Update Category
        [HttpPatch("{id}")]
        public IActionResult UpdateCategory(int id, [FromBody] CategoryModel category)
        {
            ApiResponse response = null;
            if (category == null || id != category.CategoryID)
            {
                response = new ApiResponse("Category not found or invalid details", 400);
                return BadRequest(response);
            }

            var isUpdated = _categoryRepository.Update(category);
            if (!isUpdated)
            {
                response = new ApiResponse("Error while updating category details", 500);
                return BadRequest(response);
            }

            response = new ApiResponse("Category updated successfully", 200);
            return Ok(response);
        }
        #endregion

        #region Delete Category
        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            ApiResponse response = null;

            var isDeleted = _categoryRepository.Delete(id);
            if (!isDeleted)
            {
                response = new ApiResponse("Error while deleting category", 500);
                return BadRequest(response);
            }

            response = new ApiResponse("Category deleted successfully", 200);
            return Ok(response);
        }
        #endregion
    }
}
