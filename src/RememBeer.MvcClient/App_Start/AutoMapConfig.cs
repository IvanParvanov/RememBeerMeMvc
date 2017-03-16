using System.Diagnostics.CodeAnalysis;

using AutoMapper;

using RememBeer.Models;
using RememBeer.Models.Contracts;
using RememBeer.Models.Dtos;
using RememBeer.MvcClient.Areas.Admin.Models;
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
                                  cfg.CreateMap<IBrewery, CreateBeerBindingModel>();
                                  cfg.CreateMap<IBrewery, EditBreweryBindingModel>();

                                  var breweryDetailsMap = cfg.CreateMap<IBrewery, BreweryDetailsViewModel>();
                                  breweryDetailsMap.ForMember(model => model.EditModel, opt => opt.MapFrom(x => x));
                                  breweryDetailsMap.ForMember(model => model.CreateModel, opt => opt.MapFrom(x => x));
                              });

            Mapper.AssertConfigurationIsValid();
        }
    }
}
