using AutoMapper;
using BoilerplateWebApi.Models;
using BoilerplateWebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BoilerplateWebApi.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomersController : ControllerBase
    {
        private readonly IMapper mapper;

        public ICustomerInfoRepository customerInfoRepository { get; }

        public CustomersController(ICustomerInfoRepository customerInfoRepository,
            IMapper mapper)
        {
            this.customerInfoRepository = customerInfoRepository ?? throw new System.ArgumentNullException(nameof(customerInfoRepository));
            this.mapper = mapper ?? throw new System.ArgumentNullException(nameof(mapper)); ;
        }
        //[HttpGet("api/customer")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerWithoutOperationDto>>> GetCustomers()
        {
            var customerEntities = await customerInfoRepository.GetCustomersAsync();

            return Ok(mapper.Map<IEnumerable<CustomerWithoutOperationDto>>(customerEntities));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer(int id, bool includeOperations = false)
        {
            var customer = await customerInfoRepository.GetCustomerAsync(id, includeOperations);

            if(customer== null)
            {
                return NotFound();
            }

            if(includeOperations)
            {
                return Ok(mapper.Map<CustomerDto>(customer));
            }

            return Ok(mapper.Map<CustomerWithoutOperationDto>(customer));
        }
    }

}
