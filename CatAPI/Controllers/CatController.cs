using CatAPI.BC.Models;
using CatAPI.BW.Interfaces.BW;
using CatAPI.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CatAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CatController : Controller
    {
        private readonly IManageCatBW _manageCatBW;

        public CatController(IManageCatBW manageCatBW)
        {
            _manageCatBW = manageCatBW;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetCatBreeds([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if (page < 1)
                return BadRequest("Page number must be greater than 0");

            if (pageSize < 1 || pageSize > 10)
                return BadRequest("Page size must be between 1 and 10");
            try
            {
                var result = await _manageCatBW.GetCatBreedsAsync(page, pageSize);

                return Ok(new
                {
                    Data = result.Items,
                    Pagination = new
                    {
                        CurrentPage = page,
                        PageSize = pageSize,
                        TotalItems = result.TotalCount,
                        TotalPages = (int)Math.Ceiling((double)result.TotalCount / pageSize)
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request");
            }
        }
   

        [HttpGet("search/{id}")]
        public async Task<IActionResult> GetCatBreedById(int id)
        {
            var breed = await _manageCatBW.GetCatBreedByIdAsync(id);
            return Ok(breed);
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterCatBreed(CatBreed catBreed)
        {
            ResponseModel response = await _manageCatBW.RegisterCatBreedAsync(catBreed);
            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }   
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateCatBreed(CatBreed catBreed)
        {
            ResponseModel response = await _manageCatBW.UpdateCatBreedAsync(catBreed);
            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCatBreed(int id)
        {
            ResponseModel response = await _manageCatBW.DeleteCatBreedAsync(id);
            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }
    }
}
