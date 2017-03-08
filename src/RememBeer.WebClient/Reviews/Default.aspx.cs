using System;

using RememBeer.Business.Logic.Common.EventArgs.Contracts;
using RememBeer.Business.Logic.Reviews.Common.ViewModels;
using RememBeer.Business.Logic.Reviews.Default;
using RememBeer.Business.Logic.Reviews.Default.Contracts;
using RememBeer.WebClient.BaseClasses;

using WebFormsMvp;

namespace RememBeer.WebClient.Reviews
{
    [PresenterBinding(typeof(DefaultPresenter))]
    public partial class Default : BaseMvpPage<BeerReviewViewModel>, IReviewDetailsView
    {
        public event EventHandler<IIdentifiableEventArgs<int>> OnInitialise;

        public bool NotFoundVisible
        {
            get
            {
                return this.NotFound.Visible;
            }

            set
            {
                if (value)
                {
                    this.ReviewPlaceholder.Controls.RemoveAt(0);
                }

                this.NotFound.Visible = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var id = -1;
            var strId = this.Request.QueryString["id"];
            var idIsValid = int.TryParse(strId, out id);
            if (idIsValid)
            {
                var args = this.EventArgsFactory.CreateIdentifiableEventArgs(id);
                this.OnInitialise?.Invoke(this, args);
            }
            else
            {
                this.NotFoundVisible = true;
            }
        }
    }
}
