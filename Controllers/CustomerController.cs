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
using System.Security.Claims;

namespace StoreApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService customerService;
        private readonly IMapper mapper;
        public CustomerController(ICustomerService customerService, IMapper mapper)
        {
            this.customerService = customerService;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            CustomerListResponse customerListResponse = await customerService.ListAsync();
            if (customerListResponse.Success)
            {
                return Ok(customerListResponse.CustomerList);
            }
            else
            {
                return BadRequest(customerListResponse.Message);
            }
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetCustomer() 
        {
            IEnumerable<Claim> claims = User.Claims;

            string customerId= claims.Where(x => x.Type == ClaimTypes.NameIdentifier).First().Value;

            CustomerResponse customerResponse = customerService.GetCustomerById(Convert.ToInt32(customerId));

            if(customerResponse.Success)
            {
                return Ok(customerResponse.Customer);
            }
            else
            {
                return BadRequest(customerResponse.Message);
            }
        }


        [AllowAnonymous]
        [HttpPost]
        public IActionResult AddCustomer(CustomerRequest customerRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }
            else
            {
                Customer customer = mapper.Map<CustomerRequest, Customer>(customerRequest);
                CustomerResponse customerResponse = customerService.AddCustomer(customer);
                if (customerResponse.Success)
                {
                    return Ok(customerResponse.Customer);
                }
                else
                {
                    return BadRequest(customerResponse.Message);
                }
            }
        }
    }
}
