using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestFullApi.Authentication;
using RestFullApi.DataBase;
using RestFullApi.Models;
using RestFullApi.Services;
using System.Diagnostics;

namespace RestFullApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderServices _service;
        public OrderController(IOrderServices service)
        {
            _service= service;

        }

        [HttpGet]
        public async Task<ActionResult> AllOrders() 
        {
            var customer = await _service.AllOrders();

            if(customer.Any())
            {
                return Ok(customer);
            }
            return NotFound();
           
        }


        [HttpGet("{id}")]
        public async Task<ActionResult> SpecficOrder(int id)
        {

            var customer = await _service.SpecficOrder(id);
            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);

        }

        [HttpPost]

        public async Task<ActionResult> PlaceOrder(Order order)
        {
            var customer = await _service.PlaceOrder(order);
            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        [HttpPut]

        public async Task<ActionResult> UpdateOrder(int id, Order order)
        {
            var customer = await _service.UpdateOrder(id, order);
            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);

        }



        [HttpDelete]
        [ServiceFilter(typeof(ApiKeyAuthFilter))]
        public async Task<ActionResult> DeleteOrder(int id)
        {
            var customer = await _service.DeleteOrder(id);
            if (customer == null)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
