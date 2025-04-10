using crud_curotec.Domain.Entities;

namespace crud_curotec.Controllers
{
    using global::crud_curotec.Domain.Interfaces.Services;
    using Microsoft.AspNetCore.Mvc;

    namespace crud_curotec.API.Controllers
    {
        [ApiController]
        [Route("api/[controller]")]
        public class ProductsController : ControllerBase
        {
            private readonly IProductService _productService;

            public ProductsController(IProductService productService)
            {
                _productService = productService;
            }

            /// <summary>
            /// Endpoint to list All Products
            /// </summary>
            /// <returns>List of products</returns>
            [HttpGet]

            public async Task<ActionResult<IEnumerable<Product>>> GetAll()
            {
                var products = await _productService.GetAllAsync();
                
                if(!products.Any())
                    return NotFound();

                return Ok(products);
            }

            /// <summary>
            /// Endpoint to find an especifc item
            /// </summary>
            /// <param name="id">id to search</param>
            /// <returns>One product</returns>
            [HttpGet("{id}")]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            [ProducesResponseType(StatusCodes.Status200OK)]
            public async Task<ActionResult<Product>> GetById(int id)
            {
                var product = await _productService.GetByIdAsync(id);
                if (product == null)
                    return NotFound();

                return Ok(product);
            }

            /// <summary>
            /// endointo to create a new product
            /// </summary>
            /// <param name="product">data to save</param>
            /// <returns>Product registered</returns>
            [HttpPost]
            public async Task<ActionResult<Product>> Create(Product product)
            {
                var created = await _productService.CreateAsync(product);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }

            /// <summary>
            /// endpoint for updates
            /// </summary>
            /// <param name="id">Id of the product</param>
            /// <param name="product">New data to save</param>
            /// <returns>updated product</returns>
            [HttpPut("{id}")]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            [ProducesResponseType(StatusCodes.Status200OK)]
            public async Task<IActionResult> Update(int id, Product product)
            {
                if (id != product.Id)
                    return BadRequest("Product ID mismatch.");

                var success = await _productService.UpdateAsync(product);
                if (!success)
                    return NotFound();

                return Ok(product);
            }

            /// <summary>
            /// Endpoint for remove a product
            /// </summary>
            /// <param name="id">id to remove</param>
            /// <returns>NoFound when deleted</returns>
            [HttpDelete("{id}")]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            [ProducesResponseType(StatusCodes.Status204NoContent)]
            public async Task<IActionResult> Delete(int id)
            {
                var success = await _productService.DeleteAsync(id);
                if (!success)
                    return NotFound();

                return NoContent();
            }

            /// <summary>
            /// Endpoint for insert a list of products in parallell
            /// </summary>
            /// <param name="products">list of products to insert</param>
            /// <returns></returns>
            [HttpPost("batch")]

            public async Task<ActionResult> CreateBatchOptimized([FromBody] List<Product> products)
            {

                return Ok();
            }
        }
    }
}
