//using System.Data.Entity;

//using Microsoft.AspNet.Identity;
//using Microsoft.AspNet.Identity.EntityFramework;
//using Microsoft.AspNetCore.Http.Authentication;
//using Microsoft.AspNetCore.Http.Authentication.Internal;
//using Microsoft.Owin.Security;

//using Moq;

//using Ninject;

//using NUnit.Framework;

//using RememBeer.Data.DbContexts.Contracts;
//using RememBeer.Models;
//using RememBeer.Models.Identity;
//using RememBeer.Models.Identity.Contracts;
//using RememBeer.MvcClient.App_Start;

//namespace RememBeer.IntegrationTests
//{
//    [SetUpFixture]
//    public class TestStartup
//    {
//        private static IKernel kernel;

//        [OneTimeSetUp]
//        public void Init()
//        {
//            NinjectWebCommon.Start();
//            kernel = NinjectWebCommon.Kernel;

//            kernel.Rebind<IRememBeerMeDbContext>()
//                  .ToMethod(ctx =>
//                            {
//                                var context = new Mock<IRememBeerMeDbContext>();
//                                context.Setup(c => c.BeerTypes)
//                                       .Returns(new Mock<IDbSet<BeerType>>().Object);
//                                return context.Object;
//                            });

//            kernel.Rebind<IUsersDb>()
//                  .ToMethod(ctx =>
//                  {
//                      var context = new Mock<IUsersDb>();
//                      context.Setup(c => c.Users)
//                             .Returns(new Mock<IDbSet<ApplicationUser>>().Object);
//                      return context.Object;
//                  });

//            kernel.Rebind<IBeersDb>()
//                  .ToMethod(ctx =>
//                  {
//                      var context = new Mock<IBeersDb>();
//                      context.Setup(c => c.Beers)
//                             .Returns(new Mock<IDbSet<Beer>>().Object);
//                      return context.Object;
//                  });

//            kernel.Bind<IUserStore<ApplicationUser>>()
//                  .ToMethod(ctx =>
//                            {
//                                var a = ctx.Kernel.Get<IRememBeerMeDbContext>();
//                                var db = new UserStore<ApplicationUser>(a);
//                                return db;
//                            });

//            kernel.Rebind<IApplicationUserManager>()
//                  .ToMethod(ctx =>
//                            {
//                                var userStore = ctx.Kernel.Get<IUserStore<ApplicationUser>>();
//                                var manager = new ApplicationUserManager(userStore);

//                                return manager;
//                            });

//            kernel.Rebind<IApplicationSignInManager>()
//                  .ToMethod(ctx =>
//                  {
//                      var a = new AuthenticationManager();
//                      var userManager = (ApplicationUserManager)ctx.Kernel.Get<IApplicationUserManager>();
//                      var manager = new ApplicationSignInManager(userManager, a);

//                      return manager;
//                  });
//        }

//        public static IKernel Kernel => kernel;
//    }
//}


