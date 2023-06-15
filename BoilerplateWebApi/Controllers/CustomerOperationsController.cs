using BoilerplateWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BoilerplateWebApi.Controllers
{
    [Route("api/customers/{customerId}/customeroperations")]
    [ApiController]
    public class CustomerOperationsController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<CustomerOperationsDto>> GetCustomerOperations(int customerId)
        {
            var customer = CustomerDataStore.Instance.Customers.FirstOrDefault(
                x => x.Id == customerId);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer.CustomerOperations);
        }
        [HttpGet("{operationId}")]
        public ActionResult<IEnumerable<CustomerOperationsDto>> GetCustomerOperation(
            int customerId, int operationId)
        {
            var customer = CustomerDataStore.Instance.Customers
                .FirstOrDefault(x => x.Id == customerId);

            if (customer == null)
            {
                return NotFound();
            }

            var customerOperation = customer.CustomerOperations
                .FirstOrDefault(o=> o.Id == operationId);

            if (customerOperation == null)
            {
                return NotFound();
            }

            return Ok(customerOperation);
        }
    }
}
