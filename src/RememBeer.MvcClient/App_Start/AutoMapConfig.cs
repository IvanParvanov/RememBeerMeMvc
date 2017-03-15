using System.Diagnostics.CodeAnalysis;

using AutoMapper;

using RememBeer.Models;
using RememBeer.Models.Contracts;
using RememBeer.Models.Dtos;
using RememBeer.MvcClient.Models.Reviews;

namespace RememBeer.MvcClient
{
    [ExcludeFromCodeCoverage]
    public static class AutoMapConfig
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(cfg =>
                              {
                                  cfg.CreateMap<IBeerReview, SingleReviewViewModel>();
                                  cfg.CreateMap<SingleReviewViewModel, IBeerReview>();
                                  cfg.CreateMap<EditReviewBindingModel, IBeerReview>();
                                  cfg.CreateMap<CreateReviewBindingModel, IBeerReview>();
                                  cfg.CreateMap<BeerReview, CreateReviewBindingModel>();
                                  cfg.CreateMap<IBeer, BeerDto>();
                                  cfg.CreateMap<IBeerType, BeerTypeDto>();
                              });
        }
    }
}
