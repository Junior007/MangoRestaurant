using mango.product.application.Interfaces;
using mango.product.application.Models;
using DALModels = mango.product.DAL.Models;
using mango.product.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.AspNetCore.Authorization;

namespace mango.product.api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductsService _productsService;
        private readonly IProductsServiceDal _productsServiceDAL;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductsService productsService, IProductsServiceDal productsServiceDAL, ILogger<ProductController> logger)
        {
            _productsService = productsService ?? throw new ArgumentNullException(nameof(productsService));
            _productsServiceDAL = productsServiceDAL ?? throw new ArgumentNullException(nameof(productsServiceDAL));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        // GET: api/<ProductController>
        [HttpGet]
        //[Authorize]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> Get()
        {
            try
            {
                var products = await _productsServiceDAL.Get();
                if (products == null || !products.Any())
                {
                    return NotFound();
                }
                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("Error in {0}: stack trace{1}", ex.Message, ex.StackTrace));
                return BadRequest(ex.Message);
            }
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Get(int id)
        {
            try
            {
                var product = await _productsServiceDAL.Get(id);
                if (product == null)
                {
                    return NotFound();
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("Error in {0}: stack trace{1}", ex.Message, ex.StackTrace));
                return BadRequest(ex.Message);
            }
        }
        //
        [HttpGet("[action]/{name}")]
        [Authorize]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetByName(string name)
        {
            try
            {
                IEnumerable<DALModels.Product> products = await _productsServiceDAL.GetProductsByName(name);
                if (products == null)
                {
                    return NotFound();
                }
                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("Error in {0}: stack trace{1}", ex.Message, ex.StackTrace));
                return BadRequest(ex.Message);
            }
        }
        //
        [HttpGet("[action]/{category}")]
        [Authorize]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetByCategory(string category)
        {
            try
            {
                IEnumerable<DALModels.Product> products = await _productsServiceDAL.GetProductsByCategory(category);
                if (products == null)
                {
                    return NotFound();
                }
                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("Error in {0}: stack trace{1}", ex.Message, ex.StackTrace));
                return BadRequest(ex.Message);
            }
        }
        // POST api/<ProductController>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<Product>> Post([FromBody] Product product)
        {
            try
            {
                return Created("", await _productsService.Create(product));
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("Error in {0}: stack trace{1}", ex.Message, ex.StackTrace));
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(int id, [FromBody] Product product)
        {
            try
            {
                if (id == product.ProductId)
                {
                    return Created("",await _productsService.Update(product));
                }
                return BadRequest("Diferent id");
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("Error in {0}: stack trace{1}", ex.Message, ex.StackTrace));
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(int id, [FromBody] Product product)
        {
            try
            {


                if (id == product.ProductId)
                {
                    return Ok(await _productsService.Delete(id));
                }
                return BadRequest("Diferent id");
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("Error in {0}: stack trace{1}", ex.Message, ex.StackTrace));
                return BadRequest(ex.Message);
            }
        }
    }
}
