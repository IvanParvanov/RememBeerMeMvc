using System.Diagnostics.CodeAnalysis;

using AutoMapper;

using RememBeer.Models.Contracts;
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
                              });
        }
    }
}
