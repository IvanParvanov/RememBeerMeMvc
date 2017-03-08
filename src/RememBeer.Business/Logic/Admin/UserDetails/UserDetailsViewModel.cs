using System.Collections.Generic;

using RememBeer.Models.Contracts;

namespace RememBeer.Business.Logic.Admin.UserDetails
{
    public class UserDetailsViewModel
    {
        public virtual IApplicationUser User { get; set; }

        public virtual IEnumerable<IBeerReview> Reviews { get; set; }
    }
}
