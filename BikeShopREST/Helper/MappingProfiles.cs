using AutoMapper;
using BikeShopREST.Dto;
using BikeShopREST.Models;

namespace BikeShopREST.Helper
{
	public class MappingProfiles:Profile
	{
        public MappingProfiles()
        {
			CreateMap<Category, CategoryDto>();
			CreateMap<CategoryDto, Category>();

			CreateMap<Bike, BikeDto>();
			CreateMap<BikeDto, Bike>();

			CreateMap<Address,AddressDto>();
			CreateMap<AddressDto,Address>();

			CreateMap<User, UserDto>();
			CreateMap<UserDto,User>();

			CreateMap<Review, ReviewDto>();
			CreateMap<ReviewDto,Review>();

			CreateMap<Contact, ContactDto>();
			CreateMap<ContactDto, Contact>();

			CreateMap<Auth, AuthDto>();
			CreateMap<AuthDto,Auth>();
		}      
	}
}
