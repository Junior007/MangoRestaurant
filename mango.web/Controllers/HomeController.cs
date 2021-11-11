using mango.web.Models;
using mango.web.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace mango.web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;
        private readonly ICartService _cartService;

        public HomeController(ILogger<HomeController> logger, IProductService productService,
            ICartService cartService)
        {
            _logger = logger;
            _cartService = cartService;
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {

            List<ProductDto> list = new();
            
            ResponseDto<List<ProductDto>> responseDto = await _productService.GetAllProductsAsync("");
            
            if (responseDto != null && responseDto.IsSuccess)
            {
                list = responseDto.Result;
            }
            else
            {
                //TODO: mostrar error

            }
            return View(list);
        }

        //[Authorize]
        public async Task<IActionResult> Details(int productId)
        {

            ResponseDto<ProductDto> responseDto = await _productService.GetProductByIdAsync(productId, "");

            ProductDto productDto = new ProductDto();

            if (responseDto != null && responseDto.IsSuccess)
            {
                productDto = responseDto.Result;
            }
            else
            {
                //TODO: mostrar error

            }


            return View(productDto);

        }

        [HttpPost]
        [ActionName("Details")]
        //[Authorize]
        public async Task<IActionResult> DetailsPost(ProductDto productDto)
        {
            CartDto cartDto = new()
            {
                CartHeader = new CartHeaderDto
                {
                    UserId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value
                }
            };

            CartDetailsDto cartDetails = new CartDetailsDto()
            {
                //Count = productDto.Count,
                ProductId = productDto.ProductId
            };

            var resp = await _productService.GetProductByIdAsync(productDto.ProductId, "");
            if (resp != null && resp.IsSuccess)
            {
                cartDetails.Product = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(resp.Result));
            }
            List<CartDetailsDto> cartDetailsDtos = new();
            cartDetailsDtos.Add(cartDetails);
            cartDto.CartDetails = cartDetailsDtos;

            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var addToCartResp = await _cartService.AddToCartAsync<ResponseDto<ProductDto>>(cartDto, accessToken);
            if (addToCartResp != null && addToCartResp.IsSuccess)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(productDto);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize]
        public async Task<IActionResult> Login()
        {
            
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Logout()
        {
            return SignOut("Cookies", "oidc");
        }

    }
}
