using BoilerplateWebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace BoilerplateWebApi.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class Customers:ControllerBase
    {
        //[HttpGet("api/customer")]
        [HttpGet]
        public ActionResult<IEnumerable<CustomerDto>> GetCustomers()
        { 
            return   Ok( CustomerDataStore.Instance);
        }

        [HttpGet("{id}")]
        public ActionResult<CustomerDto> GetCustomer(int id)
        {
            var customerToReturn =  CustomerDataStore.Instance.Customers.FirstOrDefault(c=>c.Id==id);

            if (customerToReturn == null)
            {
                return NotFound();
            }
            return Ok(customerToReturn);
        }
    }

}
