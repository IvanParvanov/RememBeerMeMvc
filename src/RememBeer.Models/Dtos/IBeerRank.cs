using RememBeer.Models.Contracts;

namespace RememBeer.Models.Dtos
{
    public interface IBeerRank
    {
        IBeer Beer { get; set; }

        decimal CompositeScore { get; set; }

        decimal OverallScore { get; set; }

        decimal TasteScore { get; set; }

        decimal LookScore { get; set; }

        decimal SmellScore { get; set; }

        int TotalReviews { get; set; }
    }
}
