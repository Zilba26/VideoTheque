using Microsoft.EntityFrameworkCore;
using VideoTheque.Context;
using VideoTheque.DTOs;

namespace VideoTheque.Repositories.Film
{
    public class BluRayRepository : IBluRayRepository
    {

        private readonly VideothequeDb _db;

        public BluRayRepository(VideothequeDb db)
        {
            _db = db;
        }

        public Task<List<BluRayDto>> GetBluRays() => _db.BluRays.ToListAsync();

        public ValueTask<BluRayDto?> GetBluRay(int id) => _db.BluRays.FindAsync(id);

        public Task InsertBluRay(BluRayDto film)
        {
            _db.BluRays.AddAsync(film);
            return _db.SaveChangesAsync();
        }

        public Task UpdateBluRay(int id, BluRayDto film)
        {
            var filmToUpdate = _db.BluRays.FindAsync(id).Result;

            if (filmToUpdate is null)
            {
                throw new KeyNotFoundException($"Film '{id}' non trouvé");
            }

            filmToUpdate.Title = film.Title;
            // TODO
            return _db.SaveChangesAsync();
        }

        public Task DeleteBluRay(int id)
        {
            var filmToDelete = _db.BluRays.FindAsync(id).Result;

            if (filmToDelete is null)
            {
                throw new KeyNotFoundException($"Film '{id}' non trouvé");
            }

            _db.BluRays.Remove(filmToDelete);
            return _db.SaveChangesAsync();
        }
        
        public Task SetAvailable(int id, bool available)
        {
            var filmToUpdate = _db.BluRays.FindAsync(id).Result;

            if (filmToUpdate is null)
            {
                throw new KeyNotFoundException($"Film '{id}' non trouvé");
            }

            filmToUpdate.IsAvailable = available;
            return _db.SaveChangesAsync();
        }
    }
}