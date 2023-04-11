using Domain;
using Domain.ViewModel;
using Microsoft.AspNetCore.Authorization;
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

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [Authorize]
        [HttpGet]
        [Route("get-all")]
        public IActionResult GetAll(int pageIndex, int pageSize)
        {
            var response = _orderService.GetPagging(pageIndex, pageSize);
            return Ok(response);
        }

        [Authorize]
        [HttpPost]
        [Route("get-search")]
        public IActionResult GetPagging(SearchModel model)
        {

            var response = _orderService.GetOrderSearch(model);
            return Ok(response);
        }

        [Authorize]
        [HttpPost]
        [Route("create")]
        public IActionResult CreateOrder(OrderManage order)
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
        }
    }
}
