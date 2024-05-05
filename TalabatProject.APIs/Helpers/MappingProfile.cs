using AutoMapper;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Entities.Order_Aggregate;
using TalabatProject.APIs.DTOs;

namespace TalabatProject.APIs.Helpers
{
	public class MappingProfiles:Profile
	{

		public  MappingProfiles()
		{
			CreateMap<Product, ProductToReturnDto>()
				.ForMember(d=>d.Brand,O=>O.MapFrom(source=>source.Brand.Name))
				.ForMember(d=>d.Category,O=>O.MapFrom(source=>source.Category.Name))
				//	.ForMember(d => d.PictureUrl, O => O.MapFrom(source => $"{_configuration["ApiBaseUrl"]}/{source.PictureUrl}"))
				.ForMember(d => d.PictureUrl, O => O.MapFrom<ProductPictureUrlResolver>())
				;

            CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap<BasketItemDto, BasketItem>();
			CreateMap<Talabat.Core.Entities.Order_Aggregate.Address, AddressDto>().ReverseMap();

			CreateMap<AddressDto, Talabat.Core.Entities.Order_Aggregate.Address>();
			CreateMap<Order, OrderToReturnDto>()
				.ForMember(d => d.DeliveryMethod, O => O.MapFrom(O => O.DeliveryMethod.ShortName))
				.ForMember(d => d.DeliveryMethodCost, O => O.MapFrom(O => O.DeliveryMethod.Cost));


			CreateMap<OrderItem, OrderItemDto>()
				.ForMember(d => d.ProductName, O => O.MapFrom(O => O.Product.ProductName))
				.ForMember(d => d.PictureUrl, O => O.MapFrom(O => O.Product.PictureUrl))
				.ForMember(d => d.ProductId, O => O.MapFrom(O => O.Product.ProductId))
				.ForMember(d => d.PictureUrl, O => O.MapFrom<OrderItemPictureUrlResolver>());



		}

	}
}
