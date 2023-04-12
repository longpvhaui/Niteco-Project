using Domain;
using Domain.ViewModel;
using Infrastructure.Authorize;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.OrderService;

namespace NeticoProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ILogger<OrderController> _logger;
        public OrderController(IOrderService orderService, ILogger<OrderController> logger )
        {
            _orderService = orderService;
            _logger = logger;
        }

        [Authorize]
        [HttpGet]
        [Route("get-all")]
        public IActionResult GetAll(int pageIndex, int pageSize)
        {
            try
            {
                var response = _orderService.GetPagging(pageIndex, pageSize);
                return Ok(response);
            }catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPost]
        [Route("get-search")]
        public IActionResult GetPagging(SearchModel model)
        {
            try
            {
                var response = _orderService.GetOrderSearch(model);
                return Ok(response);
            }catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPost]
        [Route("create")]
        public IActionResult CreateOrder(OrderManage order)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                // order.CreatedDate = DateTime.Now;
                if (order == null)
                {
                    return BadRequest();
                }
                var result = _orderService.CreateOrder(order);
                if (result.Success)
                {
                    return Ok(result);
                }
                else return BadRequest(result);
            }catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
