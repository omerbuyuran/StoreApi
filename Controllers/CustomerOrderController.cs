using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreApi.Domain.Interfaces;
using StoreApi.Domain.Model;
using StoreApi.Domain.Request;
using StoreApi.Domain.Responses;
using StoreApi.Extensions;
using StoreApi.Services;

namespace StoreApi.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomerOrderController : ControllerBase
    {
        private readonly ICustomerOrderService customerOrderService;
        private readonly IMapper mapper;
        public CustomerOrderController(ICustomerOrderService customerOrderService, IMapper mapper)
        {
            this.customerOrderService = customerOrderService;
            this.mapper = mapper;
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOrderList(int id)
        {
            CustomerOrderResponse orderListResponse = await customerOrderService.GetOrderAsync(id);
            if (orderListResponse.Success)
            {
                return Ok(orderListResponse);
            }
            else
            {
                return BadRequest(orderListResponse.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> SaveOrderAsync(List<CustomerOrderRequest> request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }
            else
            {
                List<CustomerOrder> customerOrders = new List<CustomerOrder>();
                foreach (var item in request)
                {
                    CustomerOrder customerOrder = mapper.Map<CustomerOrderRequest, CustomerOrder>(item);
                    customerOrders.Add(customerOrder);
                }
                CustomerOrderResponse customerOrderResponse = await customerOrderService.SaveOrderAsync(customerOrders);
                if (customerOrderResponse.Success)
                {
                    return Ok(customerOrderResponse);
                }
                else
                {
                    return BadRequest(customerOrderResponse.Message);
                }
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOrderAsync(int customerOrderId, CustomerOrderRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }
            else
            {
                CustomerOrder customerOrder = mapper.Map<CustomerOrderRequest, CustomerOrder>(request);
                await customerOrderService.UpdateOrderAsync(customerOrderId, customerOrder);
                return Ok(request);
                
            }
        }
        [HttpDelete("{id:int}")]
        public IActionResult RemoveOrder(int id)
        {
            customerOrderService.RemoveOrder(id);
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public IActionResult RemoveAllOrder(int id)
        {
            customerOrderService.RemoveAllOrder(id);
            return Ok();
        }
    }
}
