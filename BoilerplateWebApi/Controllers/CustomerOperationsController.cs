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
        [HttpGet("{operationId}",Name = "GetCustomerOperation")]
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

        [HttpPost]
        public ActionResult<CustomerOperationsDto> CreateCustomerOperation(int customerId, CustomerOperationForCreationDto operation)
        {
            var customer = CustomerDataStore.Instance.Customers
                .FirstOrDefault (x => x.Id == customerId);
            if (customer == null)
            {
                return NotFound();
            }

            var maxOpeatarionId = CustomerDataStore.Instance
                .Customers.SelectMany(
                c => c.CustomerOperations).Max(p => p.Id);

            var finalOperation = new CustomerOperationsDto
            {
                Id = ++maxOpeatarionId,
                Name = operation.Name,
                Price = operation.Price
            };

            customer.CustomerOperations.Add(finalOperation);

            return CreatedAtRoute("GetCustomerOperation", new
            {
                customerId ,
                customerOperationId = finalOperation.Id
            }, finalOperation);
        }
    }
}
