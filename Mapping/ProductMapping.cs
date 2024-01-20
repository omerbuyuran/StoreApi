using AutoMapper;
using StoreApi.Domain.Model;
using StoreApi.Domain.Request;

namespace StoreApi.Mapping
{
    public class ProductMapping : Profile
    {
        public ProductMapping()
        {
            CreateMap<ProductRequest,Product>();
            CreateMap<Product,ProductRequest>();
        }
    }
}
