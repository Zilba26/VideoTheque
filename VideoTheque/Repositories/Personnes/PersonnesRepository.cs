using Microsoft.EntityFrameworkCore;
using VideoTheque.Context;
using VideoTheque.DTOs;

namespace VideoTheque.Repositories.Personnes
{
    public class PersonnesRepository : IPersonnesRepository
    {
        private readonly VideothequeDb _db;
        
        public PersonnesRepository(VideothequeDb db)
        {
            _db = db;
        }
        
        public Task<List<PersonneDto>> GetPersonnes() => _db.Personnes.ToListAsync();
        
        public ValueTask<PersonneDto?> GetPersonne(int id) => _db.Personnes.FindAsync(id);
        
        public Task InsertPersonne(PersonneDto personne)
        {
            _db.Personnes.AddAsync(personne);
            return _db.SaveChangesAsync();
        }

        public Task UpdatePersonne(int id, PersonneDto personne)
        {
            var personneToUpdate = _db.Personnes.FirstAsync(p => p.Id == id).Result;
            
            if (personneToUpdate is null)
            {
                throw new KeyNotFoundException($"Personne '{personne.Id}' non trouvée");
            }
            
            personneToUpdate.FirstName = personne.FirstName;
            personneToUpdate.LastName = personne.LastName;
            personneToUpdate.BirthDay = personne.BirthDay;
            personneToUpdate.Nationality = personne.Nationality;
            return _db.SaveChangesAsync();
        }
        
        public Task DeletePersonne(int id)
        {
            var personneToDelete = _db.Personnes.FirstOrDefaultAsync(p => p.Id == id).Result;
            
            if (personneToDelete is null)
            {
                throw new KeyNotFoundException($"Personne '{id}' non trouvée");
            }
            
            _db.Personnes.Remove(personneToDelete);
            return _db.SaveChangesAsync();
        }
    }
}