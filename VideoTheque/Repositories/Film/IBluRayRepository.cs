using VideoTheque.DTOs;

namespace VideoTheque.Repositories.Film
{
    public interface IBluRayRepository
    {
        Task<List<BluRayDto>> GetBluRays();
        
        ValueTask<BluRayDto?> GetBluRay(int id);
        
        Task InsertBluRay(BluRayDto film);
        
        Task UpdateBluRay(int id, BluRayDto film);
        
        Task DeleteBluRay(int id);
        
    }
}