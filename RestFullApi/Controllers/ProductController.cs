using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using RestFullApi.Authentication;
using RestFullApi.DataBase;
using RestFullApi.Models;
using RestFullApi.Services;
using System.Diagnostics;

namespace RestFullApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly IProductServices _service;

        public ProductController(IProductServices service)
        {
            _service = service; 
        }

        [HttpGet]
        public async Task<ActionResult> AllProducts()
        {
           var customer = await _service.AllProducts();

            if (customer.Any())
            {
                return Ok(customer);
            }

            return NotFound();  
         
        }


        [HttpGet("{id}")]
        public async Task<ActionResult> SpecficProduct(int id)
        {
           
            var customer = await _service.SpecficProduct(id);
             if (customer == null)
             {
                return NotFound();
             }

            return Ok(customer);
           
        }

        [HttpPost]

        public async Task<ActionResult> CreateProduct(Product product)
        {
           var customer = await _service.CreateProduct(product);
            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        [HttpPut]

        public async Task<ActionResult> UpdateProduct(int id, Product product) 
        {
            var customer = await _service.UpdateProduct(id, product);
            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);

        }



        [HttpDelete]
        [ServiceFilter(typeof(ApiKeyAuthFilter))]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var customer = await _service.DeleteProduct(id);
            if (customer == null)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
