using AutoMapper;
using BoilerplateWebApi.Models;
using BoilerplateWebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace BoilerplateWebApi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/customers")]
    public class CustomersController : ControllerBase
    {
        private readonly IMapper mapper;
        const int maxCustomerSize = 20;
        public ICustomerInfoRepository customerInfoRepository { get; }

        public CustomersController(ICustomerInfoRepository customerInfoRepository,
            IMapper mapper)
        {
            this.customerInfoRepository = customerInfoRepository ?? throw new System.ArgumentNullException(nameof(customerInfoRepository));
            this.mapper = mapper ?? throw new System.ArgumentNullException(nameof(mapper)); ;
        }
        //[HttpGet("api/customer")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerWithoutOperationDto>>> GetCustomers([FromQuery] string? name, string? searchQuery,
            int pageNumber = 1, int pageSize = 10)
        {
            if (pageSize > maxCustomerSize)
            {
                pageSize = maxCustomerSize;
            }

            var (customerEntities, paginationMetaData) = await customerInfoRepository.GetCustomersAsync(name, searchQuery, pageNumber, pageSize);

            Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(paginationMetaData));

            return Ok(mapper.Map<IEnumerable<CustomerWithoutOperationDto>>(customerEntities));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer(int id, bool includeOperations = false)
        {
            var customer = await customerInfoRepository.GetCustomerAsync(id, includeOperations);

            if (customer == null)
            {
                return NotFound();
            }

            if (includeOperations)
            {
                return Ok(mapper.Map<CustomerDto>(customer));
            }

            return Ok(mapper.Map<CustomerWithoutOperationDto>(customer));
        }
    }

}
