using VideoTheque.DTOs;

namespace VideoTheque.Repositories.AgeRating
{
    public interface IAgeRatingsRepository
    {
        Task<List<AgeRatingDto>> GetAgesRating();
        
        ValueTask<AgeRatingDto?> GetAgeRating(int id);
        
        Task InsertAgeRating(AgeRatingDto ageRating);
        
        Task UpdateAgeRating(int id, AgeRatingDto ageRating);
        
        Task DeleteAgeRating(int id);
    }
}