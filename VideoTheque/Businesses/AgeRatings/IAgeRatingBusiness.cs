using VideoTheque.DTOs;

namespace VideoTheque.Businesses.AgeRatings
{
    public interface IAgeRatingBusiness
    {
        Task<List<AgeRatingDto>> GetAgeRatings();

        AgeRatingDto GetAgeRating(int id);

        AgeRatingDto InsertAgeRating(AgeRatingDto genre);

        void UpdateAgeRating(int id, AgeRatingDto genre);

        void DeleteAgeRating(int id);
        
    }
}