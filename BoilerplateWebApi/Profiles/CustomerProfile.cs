using AutoMapper;
using BoilerplateWebApi.Entities;
using BoilerplateWebApi.Models;

namespace BoilerplateWebApi.Profiles
{
    public class CustomerProfile:Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer,CustomerWithoutOperationDto>();
            CreateMap<Customer,CustomerDto>();
        }
    }
}
