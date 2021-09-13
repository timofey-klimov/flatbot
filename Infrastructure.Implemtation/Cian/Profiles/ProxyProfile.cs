using AutoMapper;
using Entities.Models;
using Infrastructure.Interfaces.Cian.Dto;

namespace Infrastructure.Implemtation.Cian.Profiles
{
    public class ProxyProfile : Profile
    {
        public ProxyProfile()
        {
            CreateMap<Proxy, ProxyDto>();
        }
    }
}
