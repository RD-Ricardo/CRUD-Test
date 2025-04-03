using Application.Addresses.Commands.CreateAddress;
using Application.Addresses.Commands.UpdateAddress;
using Application.Addresses.Models;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappers
{
    public class AddressProfile : Profile
    {
        public AddressProfile()
        {
            CreateMap<CreateAddressCommand, Address>()
                .ForMember(dest => dest.CustomerId, opt => opt.Ignore())
                .ForMember(dest => dest.Customer, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<UpdateAddressCommand, Address>();

            CreateMap<Address, AddressReponse>()
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.Name));
        }
    }
}
