using mango.web.Models;
using mango.web.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mango.web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductController> _logger;
        private async Task<string> _accesToken() { return await HttpContext.GetTokenAsync("access_token") ?? throw new ArgumentNullException("access_token"); }


        public ProductController(IProductService productService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService)); ;

        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ProductIndex()
        {
            var responseDto = await _productService.GetAllProductsAsync(await _accesToken());

            List<ProductDto> products = new List<ProductDto>();

            if ( responseDto.IsSuccess)
            {
                products = responseDto.Result;
            }

            return View(products);

        }

        public IActionResult ProductCreate()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ProductCreate(ProductDto model)
        {

            model.RowVersion = new byte[8];

            if (ModelState.IsValid)
            {

                var responseDto = await _productService.CreateProductAsync(model, await _accesToken());
                if (responseDto.IsSuccess)
                {
                    return RedirectToAction(nameof(ProductIndex));
                }

            }
            return View(model);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ProductEdit(int productId)
        {

            var responsDto = await _productService.GetProductByIdAsync(productId, await _accesToken());
            if (responsDto.IsSuccess)
            {
                ProductDto products = responsDto.Result;
                return View(products);
            }

            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ProductEdit(ProductDto model)
        {
            if (ModelState.IsValid)
            {


                var responsDto = await _productService.UpdateProductAsync(model, await _accesToken());

                if (responsDto.IsSuccess)
                {
                    return RedirectToAction(nameof(ProductIndex));
                }

            }
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ProductDelete(int productId)
        {
            var responsDto = await _productService.GetProductByIdAsync(productId, await _accesToken());

            if (responsDto.IsSuccess)
            {
                ProductDto model = responsDto.Result;
                return View(model);
            }

            return NotFound();

        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductDelete(ProductDto model)
        {
            if (ModelState.IsValid)
            {

                var responsDto = await _productService.DeleteProductAsync(model, await _accesToken());
                if (responsDto.IsSuccess)
                {
                    return RedirectToAction(nameof(ProductIndex));
                }

            }
            return View(model);
        }
    }
}
