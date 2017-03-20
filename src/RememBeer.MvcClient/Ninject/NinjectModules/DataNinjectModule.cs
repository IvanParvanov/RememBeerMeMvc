using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

using Castle.Components.DictionaryAdapter.Xml;

using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

using Ninject.Extensions.Factory;
using Ninject.Modules;
using Ninject.Web.Common;

using RememBeer.Common.Cache;
using RememBeer.Common.Constants;
using RememBeer.Data.DbContexts;
using RememBeer.Data.DbContexts.Contracts;
using RememBeer.Data.Repositories;
using RememBeer.Models.Factories;

using IRequest = Ninject.Activation.IRequest;

namespace RememBeer.MvcClient.Ninject.NinjectModules
{
    [ExcludeFromCodeCoverage]
    public class DataNinjectModule : NinjectModule
    {
        private static Func<IRequest, bool> WhenSignalRHubIsRequested => (req) =>
                                                                         {
                                                                             var type = req?.ParentRequest?.ParentRequest?.Service;
                                                                             if (type == null)
                                                                             {
                                                                                 return false;
                                                                             }

                                                                             return typeof(IHub).IsAssignableFrom(type);
                                                                         };

        private static Func<IRequest, bool> AnyOtherCase => (req) =>
                                                            {
                                                                var type = req?.ParentRequest?.ParentRequest?.Service;
                                                                if (type == null)
                                                                {
                                                                    return true;
                                                                }

                                                                return !typeof(IHub).IsAssignableFrom(type);
                                                            };

        public override void Load()
        {
            //this.Rebind<IRememBeerMeDbContext>().To<RememBeerMeDbContext>().InRequestScope();
            //this.Rebind<IBeersDb>().To<RememBeerMeDbContext>().InRequestScope();
            //this.Rebind<IUsersDb>().To<RememBeerMeDbContext>().InRequestScope();
            this.Rebind<IRememBeerMeDbContext>().To<RememBeerMeDbContext>().InTransientScope();
            this.Rebind<IBeersDb>().To<RememBeerMeDbContext>().InTransientScope();
            this.Rebind<IUsersDb>().To<RememBeerMeDbContext>().InTransientScope();

            //this.Rebind<IRememBeerMeDbContext>().To<RememBeerMeDbContext>()
            //    .When(WhenSignalRHubIsRequested)
            //    .InScope(ctx =>
            //             {
            //                 var a = ctx.Request.ParentRequest.ParentRequest.Service;
            //                 return GlobalHost.ConnectionManager.GetHubContext(a.Name);
            //             });
            //this.Bind<IBeersDb>().To<RememBeerMeDbContext>()
            //    .When(WhenSignalRHubIsRequested)
            //    .InScope(ctx =>
            //             {
            //                 var a = ctx.Request.ParentRequest.ParentRequest.Service;
            //                 return GlobalHost.ConnectionManager.GetHubContext(a.Name);
            //             });
            //this.Bind<IUsersDb>().To<RememBeerMeDbContext>()
            //    .When(WhenSignalRHubIsRequested)
            //    .InScope(ctx =>
            //             {
            //                 var a = ctx.Request.ParentRequest.ParentRequest.Service;
            //                 return GlobalHost.ConnectionManager.GetHubContext(a.Name);
            //             });

            //this.Rebind<IRememBeerMeDbContext>().To<RememBeerMeDbContext>()
            //    .When(AnyOtherCase)
            //    .InRequestScope();
            //this.Bind<IBeersDb>().To<RememBeerMeDbContext>()
            //    .When(AnyOtherCase)
            //    .InRequestScope();
            //this.Bind<IUsersDb>().To<RememBeerMeDbContext>()
            //    .When(AnyOtherCase)
            //    .InRequestScope();

            this.Rebind<IModelFactory>().To<ModelFactory>().InSingletonScope();
            this.Bind<IRankFactory>().To<ModelFactory>().InSingletonScope();

            this.Bind<IDataModifiedResultFactory>().ToFactory().InSingletonScope();

            this.Rebind<ICacheManager>()
                .ToMethod(context => new CacheManager(Constants.DefaultCacheDurationInMinutes))
                .InSingletonScope();
        }
    }
}
