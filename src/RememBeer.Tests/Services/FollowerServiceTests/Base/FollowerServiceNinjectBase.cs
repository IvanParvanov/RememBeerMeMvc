using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

using Moq;

using Ninject.MockingKernel;

using RememBeer.Data.DbContexts.Contracts;
using RememBeer.Data.Repositories;
using RememBeer.Models;
using RememBeer.Services;
using RememBeer.Tests.MvcClient.Controllers.Ninject.Base;
using RememBeer.Tests.Utils.MockedClasses.IDbSetMocks;

namespace RememBeer.Tests.Services.FollowerServiceTests.Base
{
    public class FollowerServiceNinjectBase : NinjectTestBase
    {
        public override void Init()
        {
            this.MockingKernel.Bind<FollowerService>().ToSelf();

            this.MockingKernel.Bind<IUsersDb>().ToMock().InSingletonScope();
            this.MockingKernel.Bind<IDbSet<ApplicationUser>>().ToMock().InSingletonScope();
            this.MockingKernel.Bind<IDataModifiedResultFactory>().ToMock().InSingletonScope();
        }

        protected Mock<IDbSet<T>> GetMockAsyncDbSet<T>(IQueryable<T> data) where T : class
        {
            var mockSet = new Mock<IDbSet<T>>();
            mockSet.As<IDbAsyncEnumerable<T>>()
                   .Setup(m => m.GetAsyncEnumerator())
                   .Returns(new TestDbAsyncEnumerator<T>(data.GetEnumerator()));

            mockSet.As<IQueryable<T>>()
                   .Setup(m => m.Provider)
                   .Returns(new TestDbAsyncQueryProvider<T>(data.Provider));

            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            return mockSet;
        }

        protected Mock<IDbSet<T>> GetMockDbSet<T>(IQueryable<T> data) where T : class
        {
            var mockSet = new Mock<IDbSet<T>>();
            mockSet.As<IQueryable<T>>()
                   .Setup(m => m.Provider)
                   .Returns(data.Provider);

            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            return mockSet;
        }
    }
}
