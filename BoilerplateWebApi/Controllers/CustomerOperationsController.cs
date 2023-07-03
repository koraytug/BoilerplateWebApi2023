using AutoMapper;
using BoilerplateWebApi.Entities;
using BoilerplateWebApi.Models;
using BoilerplateWebApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace BoilerplateWebApi.Controllers
{
    [Route("api/customers/{customerId}/customeroperations")]
    [ApiController]
    public class CustomerOperationsController : ControllerBase
    {
        private readonly ILogger<CustomerOperationsController> logger;
        private readonly IMailService localMailService;
        private readonly ICustomerInfoRepository customerInfoRepository;
        private readonly IMapper mapper;

        public CustomerOperationsController(ILogger<CustomerOperationsController> logger,
            IMailService localMailService,
            ICustomerInfoRepository customerInfoRepository,
            IMapper mapper)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.localMailService = localMailService ?? throw new System.ArgumentNullException(nameof(localMailService));
            this.customerInfoRepository = customerInfoRepository;
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper)); ;
            ;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerOperationsDto>>> GetCustomerOperations(int customerId)
        {
            if (await customerInfoRepository.CustomerExistsAsync(customerId))
            {
                logger.LogInformation($"Customer with Id {customerId} was not found when accessing to operation");
                return NotFound();
            }

            var customerOperationsForCustomer = await customerInfoRepository
                .GetOperationsForCustomerAsync(customerId);

            return Ok(mapper.Map<IEnumerable<CustomerOperationsDto>>(customerOperationsForCustomer));
        }

        [HttpGet("{operationId}", Name = "GetCustomerOperation")]
        public async Task<ActionResult<IEnumerable<CustomerOperationsDto>>> GetCustomerOperation(
            int customerId, int operationId)
        {
            if (await customerInfoRepository.CustomerExistsAsync(customerId))
            {
                logger.LogInformation($"Customer with Id {customerId} was not found when accessing to operation");
                return NotFound();
            }

            var customerOperation = await customerInfoRepository
                .GetOperationForCustomerAsync(customerId, operationId);

            if (customerOperation == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<CustomerOperationsDto>(customerOperation));

        }

        [HttpPost]
        public async Task<ActionResult<CustomerOperationsDto>> CreateCustomerOperation(int customerId, CustomerOperationForCreationDto operation)
        {
            if (!await customerInfoRepository.CustomerExistsAsync(customerId))
            {
                return NotFound();
            }

            var finalOperation = mapper.Map<CustomerOperation>(operation);

            await customerInfoRepository.AddCustomerOperationForCustomerAsync(customerId, finalOperation);
            await customerInfoRepository.SaveChangesAsync();

            var createdOperationToReturn = mapper.Map<CustomerOperationsDto>(finalOperation);
            return CreatedAtRoute("GetCustomerOperation", new
            {
                customerId = customerId,
                operationId = createdOperationToReturn.Id
            }, createdOperationToReturn);
        }

        [HttpPut("{operationId}")]
        public async Task<ActionResult> UpdateCustomerOperation(int customerId, int operationId,
            CustomerOperationForUpdatingDto customerOperation)
        {
            //var customer = this.customerDataStore.Customers
            //    .FirstOrDefault(x => x.Id == customerId);
            if (!await customerInfoRepository.CustomerExistsAsync(customerId))
            {
                return NotFound();
            }

            //Find operation
            var customerOperationEntitiy = await customerInfoRepository
                .GetOperationForCustomerAsync(customerId, operationId);
            if (customerOperationEntitiy == null)
            {
                return NotFound();
            }

            mapper.Map(customerOperation, customerOperationEntitiy);

            await customerInfoRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{operationId}")]
        public async Task<ActionResult> PartiallyUpdateCustomerOperation(
            int customerId, int operationId, 
            JsonPatchDocument<CustomerOperationForUpdatingDto> patchDocument)
        {
            if (!await customerInfoRepository.CustomerExistsAsync(customerId))
            {
                return NotFound();
            }

            var customerOperationEntitiy = await customerInfoRepository
               .GetOperationForCustomerAsync(customerId, operationId);
            if (customerOperationEntitiy == null)
            {
                return NotFound();
            }

            var operationToPatch = mapper.Map<CustomerOperationForUpdatingDto>(customerOperationEntitiy);

            patchDocument.ApplyTo(operationToPatch,ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (!TryValidateModel(operationToPatch))
            {
                return BadRequest();
            }

            mapper.Map(operationToPatch  ,customerOperationEntitiy);

            await customerInfoRepository.SaveChangesAsync();

            return NoContent();             
        }

        [HttpDelete("{operationId}")]
        public async Task<ActionResult> DeleteCustomerOperation(int customerId, int operationId)
        { 
            if (!await customerInfoRepository.CustomerExistsAsync(customerId))
            {
                return NotFound();
            }

            var customerOperationEntitiy = await customerInfoRepository
              .GetOperationForCustomerAsync(customerId, operationId);
            if (customerOperationEntitiy == null)
            {
                return NotFound();
            }

            customerInfoRepository.DeleteCustomerOperation(customerOperationEntitiy);
            await customerInfoRepository.SaveChangesAsync();

            localMailService.Send("Customer operation deleted.",
                $"Customer operation {customerOperationEntitiy.Name} with price {customerOperationEntitiy.Price} was deleted");

            return NoContent();
        }
    }
}
