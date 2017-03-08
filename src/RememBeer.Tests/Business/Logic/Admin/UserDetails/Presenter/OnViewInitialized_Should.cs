using System.Collections.Generic;

using Moq;

using NUnit.Framework;

using Ploeh.AutoFixture;

using RememBeer.Business.Logic.Admin.UserDetails;
using RememBeer.Business.Logic.Admin.UserDetails.Contracts;
using RememBeer.Business.Logic.Common.EventArgs.Contracts;
using RememBeer.Models;
using RememBeer.Models.Contracts;
using RememBeer.Services.Contracts;
using RememBeer.Tests.Utils;
using RememBeer.Tests.Utils.MockedClasses;

namespace RememBeer.Tests.Business.Logic.Admin.UserDetails.Presenter
{
    [TestFixture]
   public class OnViewInitialized_Should : TestClassBase
    {
        [TestCase(null)]
        [TestCase("719f443f-6c3a-44a6-bd15-ff03bfbe54d5")]
        public void Call_UserServiceGetByIdMethodOnceWithCorrectParams(string expectedId)
        {
            var userService = new Mock<IUserService>();
            var view = new Mock<IUserDetailsView>();

            var args = new Mock<IIdentifiableEventArgs<string>>();
            args.Setup(a => a.Id)
                .Returns(expectedId);

            var presenter = new UserDetailsPresenter(userService.Object, view.Object);

            view.Raise(v => v.Initialized += null, view.Object, args.Object);

            userService.Verify(s => s.GetById(expectedId), Times.Once);
        }

        [Test]
        public void Set_ViewModelProperties_WhenUserIsFound()
        {
            var expectedId = this.Fixture.Create<string>();
            var expectedReviews = new List<BeerReview>();
            var expectedUser = new Mock<IApplicationUser>();
            expectedUser.Setup(u => u.BeerReviews)
                .Returns(expectedReviews);

            var userService = new Mock<IUserService>();
            userService.Setup(s => s.GetById(It.IsAny<string>()))
                       .Returns(expectedUser.Object);

            var viewModel = new MockedUserDetailsViewModel();
            var view = new Mock<IUserDetailsView>();
            view.Setup(v => v.Model)
                .Returns(viewModel);

            var args = new Mock<IIdentifiableEventArgs<string>>();
            args.Setup(a => a.Id)
                .Returns(expectedId);

            var presenter = new UserDetailsPresenter(userService.Object, view.Object);

            view.Raise(v => v.Initialized += null, view.Object, args.Object);

            Assert.AreSame(expectedUser.Object, viewModel.User);
            Assert.AreSame(expectedReviews, viewModel.Reviews);
        }

        [Test]
        public void Set_ViewModelProperties_WhenUserIsNotFound()
        {
            var expectedId = this.Fixture.Create<string>();
            var userService = new Mock<IUserService>();

            var view = new Mock<IUserDetailsView>();

            var args = new Mock<IIdentifiableEventArgs<string>>();
            args.Setup(a => a.Id)
                .Returns(expectedId);

            var presenter = new UserDetailsPresenter(userService.Object, view.Object);

            view.Raise(v => v.Initialized += null, view.Object, args.Object);

            view.VerifySet(v => v.ErrorMessageText = It.IsAny<string>(), Times.Once);
            view.VerifySet(v => v.ErrorMessageVisible = true, Times.Once);
        }
    }
}
