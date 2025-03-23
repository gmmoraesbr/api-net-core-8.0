using AutoMapper;
using Base.Application.Features.Products.Responses;
using Base.Domain.Entities.Aggregates.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Application.Mappings
{
    public class ProductMapperProfile : Profile
    {
        public ProductMapperProfile()
        {
            CreateMap<Product, ProductResponse>()
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => new ProductResponse.RatingDto
                {
                    Rate = src.Rating.Rate,
                    Count = src.Rating.Count
                }));

            CreateMap<Rating, ProductResponse.RatingDto>();
        }
    }
}
