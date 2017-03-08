using AutoMapper;

using RememBeer.Models.Contracts;
using RememBeer.MvcClient.Models.Reviews;

namespace RememBeer.MvcClient
{
    public static class AutoMapConfig
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(cfg =>
                              {
                                  cfg.CreateMap<IBeerReview, SingleReviewViewModel>();
                              });
        }
    }
}
