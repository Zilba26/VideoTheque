using VideoTheque.DTOs;

namespace VideoTheque.Businesses.Genres
{
    public interface IAgeRatingBusiness
    {
        Task<List<GenreDto>> GetAgesRating();

        GenreDto GetAgeRating(int id);

        GenreDto InsertAgeRating(AgeRatingDto genre);

        void UpdateAgeRating(int id, AgeRatingDto genre);

        void DeleteAgeRating(int id);
        
    }
}