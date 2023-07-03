using AutoMapper;
using BoilerplateWebApi.Entities;
using BoilerplateWebApi.Models;

namespace BoilerplateWebApi.Profiles
{
    public class CustomerOperationProfile: Profile
    {
        public CustomerOperationProfile()
        {
            CreateMap<CustomerOperation, CustomerOperationsDto>();
            CreateMap<CustomerOperationForCreationDto, CustomerOperation>();
            CreateMap<CustomerOperationForUpdatingDto, CustomerOperation>();
            CreateMap<CustomerOperation,CustomerOperationForUpdatingDto>();
        }
    }
}
