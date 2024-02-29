using AutoMapper;
using DAL.Entities;
using Domain.Models;

namespace DAL.AutoMapper
{
    public class AutomapperProfiles : Profile
    {
        public AutomapperProfiles()
        {
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<OrderImportDto, Order>().ReverseMap();
        }
    }
}
