using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GenericApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        [HttpGet("")]
        [Authorize(Policy = "UserPolicy")]
        public IActionResult GetAllProducts()
        {
            var products = new[]
            {
                new { Id = 1, Name = "Product A", Price = 100 },
                new { Id = 2, Name = "Product B", Price = 200 }
            };

            return Ok(products);
        }
    }
}
