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
        private async Task<string> _accesToken() { return await HttpContext.GetTokenAsync("access_token"); }
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ProductIndex()
        {
            var response = await _productService.GetAllProductsAsync(await _accesToken());

            List<ProductDto> products = new List<ProductDto>();

            if (response != null && response.IsSuccess)
            {
                products = response.Result;
            }

            return View(products);

        }

        public async Task<IActionResult> ProductCreate()
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
                //var accessToken = await HttpContext.GetTokenAsync("access_token");
                /*var response = await _productService.CreateProductAsync<ResponseDto>(model, accessToken);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(ProductIndex));
                }*/

                var responseDto = await _productService.CreateProductAsync(model, await _accesToken());
                return RedirectToAction(nameof(ProductIndex));


            }
            return View(model);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ProductEdit(int productId)
        {
            /*var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _productService.GetProductByIdAsync<ResponseDto>(productId, accessToken);
            if (response != null && response.IsSuccess)
            {
                ProductDto model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
                return View(model);
            }
            return NotFound();*/

            var response = await _productService.GetProductByIdAsync(productId, await _accesToken());
            if(response != null && response.IsSuccess)
            {
                ProductDto products = response.Result;
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


                var response = await _productService.UpdateProductAsync(model, await _accesToken());

                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(ProductIndex));
                }

            }
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ProductDelete(int productId)
        {
            var response = await _productService.GetProductByIdAsync(productId, await _accesToken());

            if (response != null && response.IsSuccess)
            {
                ProductDto model =response.Result;
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

                var response = await _productService.DeleteProductAsync(model, await _accesToken());
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(ProductIndex));
                }

            }
            return View(model);
        }
    }
}
