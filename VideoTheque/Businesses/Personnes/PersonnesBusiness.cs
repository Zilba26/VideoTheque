using VideoTheque.Core;
using VideoTheque.DTOs;
using VideoTheque.Repositories.Personnes;

namespace VideoTheque.Businesses.Personnes
{
    public class PersonnesBusiness : IPersonnesBusiness
    {
        private readonly IPersonnesRepository _personnesDao;
        
        public PersonnesBusiness(IPersonnesRepository personnesDao)
        {
            _personnesDao = personnesDao;
        }
        
        public Task<List<PersonneDto>> GetPersonnes() => _personnesDao.GetPersonnes();
        
        public PersonneDto GetPersonne(int id)
        {
            var personne = _personnesDao.GetPersonne(id).Result;
            
            if (personne == null)
            {
                throw new NotFoundException($"Personne '{id}' non trouvée");
            }
            
            return personne;
        }
        
        public PersonneDto InsertPersonne(PersonneDto personne)
        {
            if (_personnesDao.InsertPersonne(personne).IsFaulted)
            {
                throw new InternalErrorException($"Erreur lors de l'insertion de la personne {personne.LastName} {personne.FirstName}");
            }
            
            return personne;
        }
        
        public void UpdatePersonne(int id, PersonneDto personne)
        {
            if (_personnesDao.UpdatePersonne(id, personne).IsFaulted)
            {
                throw new InternalErrorException($"Erreur lors de la modification de la personne {personne.LastName} {personne.FirstName}");
            }
        }
        
        public void DeletePersonne(int id)
        {
            if (_personnesDao.DeletePersonne(id).IsFaulted)
            {
                throw new InternalErrorException($"Erreur lors de la suppression de la personne d'identifiant {id}");
            }
        }
    }
}