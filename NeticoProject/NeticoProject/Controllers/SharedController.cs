using Infrastructure.Authorize;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;
using Service.OrderService;

namespace NeticoProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SharedController : ControllerBase
    {
        private readonly CategoryService _categoryService;
        private readonly CustomerService _customerService;
        private readonly ProductService _productService;
        private readonly ILogger<SharedController> _logger;
        public SharedController(CategoryService categoryService, CustomerService customerService, ProductService productService, ILogger<SharedController> logger)
        {
            _categoryService = categoryService;
            _customerService = customerService;
            _productService = productService;
            _logger = logger;
        }

        [Authorize]
        [HttpGet]
        [Route("get-all-customer")]
        public IActionResult GetAllCustomer()
        {
            try{
                var response = _customerService.GetAll();
                return Ok(response);
            }catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }


        [Authorize]
        [HttpGet]
        [Route("get-all-product")]
        public IActionResult GetAllProduct()
        {
            try
            {
                var response = _productService.GetAll();
                return Ok(response);
            }catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }


        [Authorize]
        [HttpGet]
        [Route("get-all-category")]
        public IActionResult GetAllCategory()
        {
            try
            {
                var response = _categoryService.GetAll();
                return Ok(response);
            }catch( Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
