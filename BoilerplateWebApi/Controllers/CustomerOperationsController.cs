using BoilerplateWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
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
        [HttpGet("{operationId}", Name = "GetCustomerOperation")]
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
                .FirstOrDefault(o => o.Id == operationId);

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
                .FirstOrDefault(x => x.Id == customerId);
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
                customerId = customerId,
                operationId = finalOperation.Id
            }, finalOperation);
        }

        [HttpPut("{operationId}")]
        public ActionResult UpdateCustomerOperation(int customerId, int operationId, CustomerOperationForUpdatingDto customerOperation)
        {
            var customer = CustomerDataStore.Instance.Customers
                .FirstOrDefault(x => x.Id == customerId);
            if (customer == null)
            {
                return NotFound();
            }

            //Find operation
            var operationFromStore = customer.CustomerOperations
                .FirstOrDefault(x => x.Id == operationId);
            if (operationFromStore == null)
            {
                return NotFound();
            }

            operationFromStore.Name = customerOperation.Name;
            operationFromStore.Price = customerOperation.Price;

            return NoContent();
        }

        [HttpPatch("{operationId}")]
        public ActionResult PartiallyUpdateCustomerOperation(int customerId, int operationId, JsonPatchDocument<CustomerOperationForUpdatingDto> patchDocument)
        {
            var customer = CustomerDataStore.Instance.Customers
                .FirstOrDefault(x => x.Id == customerId);
            if (customer == null)
            {
                return NotFound();
            }

            var operationFromStore = customer.CustomerOperations
               .FirstOrDefault(x => x.Id == operationId);
            if (operationFromStore == null)
            {
                return NotFound();
            }

            var customerOperationToPatch = new CustomerOperationForUpdatingDto
            {
                Name = operationFromStore.Name,
                Price = operationFromStore.Price
            };

            patchDocument.ApplyTo(customerOperationToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (!TryValidateModel(customerOperationToPatch))
            {
                return BadRequest();
            }

            operationFromStore.Name = customerOperationToPatch.Name;
            operationFromStore.Price = customerOperationToPatch.Price;

            return NoContent();
        }

        [HttpDelete("{operationId}")]
        public ActionResult DeleteCustomerOperation(int customerId,int operationId)
        {
            var customer = CustomerDataStore.Instance.Customers
             .FirstOrDefault(x => x.Id == customerId);
            if (customer == null)
            {
                return NotFound();
            }

            var operationFromStore = customer.CustomerOperations
               .FirstOrDefault(x => x.Id == operationId);
            if (operationFromStore == null)
            {
                return NotFound();
            }

            customer.CustomerOperations.Remove(operationFromStore);

            return NoContent();
        }
    }
}
