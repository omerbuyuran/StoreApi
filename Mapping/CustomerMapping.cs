using AutoMapper;
using StoreApi.Domain.Model;
using StoreApi.Domain.Request;

namespace StoreApi.Mapping
{
    public class CustomerMapping:Profile
    {
        public CustomerMapping()
        {
            CreateMap<CustomerRequest, Customer>();
            CreateMap<Customer, CustomerRequest>();
        }
    }
}
