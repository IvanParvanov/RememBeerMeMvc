using System;

namespace RememBeer.Models.Contracts
{
    public interface IBeerReview : IIdentifiable
    {
        int BeerId { get; set; }

        Beer Beer { get; set; }

        string ApplicationUserId { get; set; }

        ApplicationUser User { get; set; }

        int Overall { get; set; }

        int Look { get; set; }

        int Smell { get; set; }

        int Taste { get; set; }

        string Description { get; set; }

        DateTime CreatedAt { get; set; }

        DateTime ModifiedAt { get; set; }

        bool IsPublic { get; set; }

        bool IsDeleted { get; set; }

        string Place { get; set; }

        string ImgUrl { get; set; }
    }
}
