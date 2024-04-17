using AutoMapper;
using Talabat.Core.Entities;
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
		}

	}
}
