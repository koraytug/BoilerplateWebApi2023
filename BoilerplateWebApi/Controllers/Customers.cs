using BoilerplateWebApi.Models;
using BoilerplateWebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BoilerplateWebApi.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class Customers:ControllerBase
    {
        private readonly CustomerDataStore customerDataStore;
                 
        public Customers(CustomerDataStore customerDataStore)
        {
            this.customerDataStore = customerDataStore ?? throw new System.ArgumentNullException(nameof(customerDataStore));
        }
        //[HttpGet("api/customer")]
        [HttpGet]
        public ActionResult<IEnumerable<CustomerDto>> GetCustomers()
        { 
            return   Ok(this.customerDataStore);
        }

        [HttpGet("{id}")]
        public ActionResult<CustomerDto> GetCustomer(int id)
        {
            var customerToReturn =  this.customerDataStore.Customers.FirstOrDefault(c=>c.Id==id);

            if (customerToReturn == null)
            {
                return NotFound();
            }
            return Ok(customerToReturn);
        }
    }

}
