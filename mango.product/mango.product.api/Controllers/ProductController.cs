using mango.product.application.Interfaces;
using mango.product.application.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace mango.product.api.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductsService _productsService;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductsService productsService, ILogger<ProductController> logger)
        {
            _productsService = productsService ?? throw new ArgumentNullException(nameof(productsService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        // GET: api/<ProductController>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> Get()
        {
            var products = await _productsService.Get();
            if (products == null || !products.Any())
            {
                return NotFound();
            }
            return Ok(products);
        }

        // GET api/<ProductController>/5
        [HttpGet("{id:length(24)}")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Get(int id)
        {
            try
            {
                var product = await _productsService.Get(id);
                if (product == null)
                {
                    return NotFound();
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("Error in {0}: stack trace{1}", ex.Message, ex.StackTrace));
                return BadRequest();
            }
        }
        //
        [HttpGet("[action]/{name}")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetByName(string name)
        {
            IEnumerable<Product> products = await _productsService.GetProductsByName(name);
            if (products == null)
            {
                return NotFound();
            }
            return Ok(products);
        }
        //
        [HttpGet("[action]/{category}")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.NotFound)]

        public async Task<ActionResult> GetByCategory(string category)
        {
            IEnumerable<Product> products = await _productsService.GetProductsByCategory(category);
            if (products == null)
            {
                return NotFound();
            }
            return Ok(products);
        }
        // POST api/<ProductController>
        [HttpPost]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<Product>> Post([FromBody] Product product)
        {
            //await _productsService.Create(product);

            //return CreatedAtRoute("Get", new { id = product.Id }, product);
            //var prod = await _productsService.Get(product.Id);
            return Created("", await _productsService.Create(product));
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id:length(24)}")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(int id, [FromBody] Product product)
        {
            if (id == product.ProductId)
            {
                return Ok(await _productsService.Update(product));
            }
            return BadRequest("Diferent id");

        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id:length(24)}")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(int id, [FromBody] Product product)
        {
            if (id == product.ProductId)
            {
                return Ok(await _productsService.Delete(id));
            }
            return BadRequest("Diferent id");
        }
    }
}
