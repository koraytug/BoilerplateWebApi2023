using BoilerplateWebApi.Models;

namespace BoilerplateWebApi
{
    public class CustomerDataStore
    {
        public List<CustomerDto> Customers { get; set; }

        //public static CustomerDataStore Instance { get; set; } = new CustomerDataStore();

        public CustomerDataStore()
        {
            Customers = new List<CustomerDto>() {
                new CustomerDto()
                {
                    Id = 1,
                    Address= "Address 1",
                    Email= "customer1@email.com",
                    Name= "Customer1",
                    Phone= "012345649879",  
                    CustomerOperations = new List<CustomerOperationsDto>()
                    {
                        new CustomerOperationsDto()
                        {
                            Id= 1,
                            Name = "operation 1",
                            Price = 10
                        },
                        new CustomerOperationsDto()
                        {
                            Id= 2,
                            Name = "operation 2",
                            Price = 20
                        }
                    }
                },
                new CustomerDto()
                {
                    Id = 2,
                    Address= "Address 2",
                    Email= "customer2@email.com",
                    Name= "Customer2",
                    Phone= "012345649879",
                    CustomerOperations = new List<CustomerOperationsDto>()
                    {
                        new CustomerOperationsDto()
                        {
                            Id= 3,
                            Name = "operation 3",
                            Price = 30
                        },
                        new CustomerOperationsDto()
                        {
                            Id= 4,
                            Name = "operation 4",
                            Price = 40
                        }
                    }
                },
                new CustomerDto()
                {
                    Id = 3,
                    Address= "Address 3",
                    Email= "customer1@email.com",
                    Name= "Customer3",
                    Phone= "012345649879",
                    CustomerOperations = new List<CustomerOperationsDto>()
                    {
                        new CustomerOperationsDto()
                        {
                            Id= 5,
                            Name = "operation 5",
                            Price = 50
                        },
                        new CustomerOperationsDto()
                        {
                            Id= 6,
                            Name = "operation 6",
                            Price = 60
                        }
                    }
                }
            };
        }
    }

}
