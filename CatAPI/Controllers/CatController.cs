using CatAPI.BC.Models;
using CatAPI.BW.Interfaces.BW;
using CatAPI.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CatAPI.Controllers
{
    // The ApiController attribute automatically validates request data and binds incoming data to action parameters
    [ApiController]
    // Define the base route for the controller, here it's mapped to "api/cat"
    [Route("api/[controller]")]
    public class CatController : Controller
    {
        // Private readonly field to access business logic layer methods for managing cat breeds
        private readonly IManageCatBW _manageCatBW;

        // Constructor to inject the IManageCatBW service into the controller
        public CatController(IManageCatBW manageCatBW)
        {
            _manageCatBW = manageCatBW;
        }

        // GET: api/cat/getAll
        // Action method to retrieve a paginated list of cat breeds
        [HttpGet("getAll")]
        public async Task<IActionResult> GetCatBreeds([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            // Validate the page number to ensure it's greater than 0
            if (page < 1)
                return BadRequest("Page number must be greater than 0");

            // Validate the page size to ensure it's between 1 and 10
            if (pageSize < 1 || pageSize > 10)
                return BadRequest("Page size must be between 1 and 10");

            try
            {
                // Get the cat breeds from the business layer using pagination
                var result = await _manageCatBW.GetCatBreedsAsync(page, pageSize);

                // Return a response with the data and pagination information
                return Ok(new
                {
                    Data = result.Items,
                    Pagination = new
                    {
                        CurrentPage = page,
                        PageSize = pageSize,
                        TotalItems = result.TotalCount,
                        TotalPages = (int)Math.Ceiling((double)result.TotalCount / pageSize) // Calculate the total pages
                    }
                });
            }
            catch (Exception ex)
            {
                // Return a 500 status code in case of an error with exception details
                return StatusCode(500, ex);
            }
        }

        // GET: api/cat/search/{id}
        // Action method to retrieve a cat breed by its ID
        [HttpGet("search/{id}")]
        public async Task<IActionResult> GetCatBreedById(int id)
        {
            // Retrieve the cat breed from the business layer by ID
            var breed = await _manageCatBW.GetCatBreedByIdAsync(id);

            // Return the breed data
            return Ok(breed);
        }

        // POST: api/cat/register
        // Action method to register a new cat breed, requires authentication
        [HttpPost("register")]
        [Authorize] // Ensures that only authenticated users can access this method
        public async Task<IActionResult> RegisterCatBreed(CatBreed catBreed)
        {
            // Call the business layer to register the cat breed
            ResponseModel response = await _manageCatBW.RegisterCatBreedAsync(catBreed);

            // Return the response from the registration process
            return Ok(response);
        }

        // PUT: api/cat/update
        // Action method to update an existing cat breed, requires authentication
        [HttpPut("update")]
        [Authorize] // Ensures that only authenticated users can access this method
        public async Task<IActionResult> UpdateCatBreed(CatBreed catBreed)
        {
            // Call the business layer to update the cat breed
            ResponseModel response = await _manageCatBW.UpdateCatBreedAsync(catBreed);

            // Return the response from the update process
            return Ok(response);
        }

        // DELETE: api/cat/delete/{id}
        // Action method to delete a cat breed by its ID, requires authentication
        [HttpDelete("delete/{id}")]
        [Authorize] // Ensures that only authenticated users can access this method
        public async Task<IActionResult> DeleteCatBreed(int id)
        {
            // Call the business layer to delete the cat breed by its ID
            ResponseModel response = await _manageCatBW.DeleteCatBreedAsync(id);

            // Return the response from the deletion process
            return Ok(response);
        }
    }
}
