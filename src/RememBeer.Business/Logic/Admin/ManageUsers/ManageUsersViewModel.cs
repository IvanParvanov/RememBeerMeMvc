using System.Collections.Generic;

using RememBeer.Models.Contracts;

namespace RememBeer.Business.Logic.Admin.ManageUsers
{
    public class ManageUsersViewModel
    {
        public virtual IEnumerable<IApplicationUser> Users { get; set; }
    }
}
