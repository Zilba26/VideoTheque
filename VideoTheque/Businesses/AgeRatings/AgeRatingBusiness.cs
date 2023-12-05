using VideoTheque.Core;
using VideoTheque.DTOs;
using VideoTheque.Repositories.AgeRating;

namespace VideoTheque.Businesses.AgeRatings
{
    public class AgeRatingBusiness : IAgeRatingBusiness
    {
        
        private readonly IAgeRatingsRepository _ageRatingDao;

        public AgeRatingBusiness(IAgeRatingsRepository ageRatingDao)
        {
            _ageRatingDao = ageRatingDao;
        }

        public Task<List<AgeRatingDto>> GetAgeRatings() => _ageRatingDao.GetAgesRating();

        public AgeRatingDto GetAgeRating(int id)
        {
            var ar = _ageRatingDao.GetAgeRating(id).Result;

            if (ar == null)
            {
                throw new NotFoundException($"Age Rating '{id}' non trouvé");
            }

            return ar;
        }

        public AgeRatingDto InsertAgeRating(AgeRatingDto arDTO)
        {
            if (_ageRatingDao.InsertAgeRating(arDTO).IsFaulted)
            {
                throw new InternalErrorException($"Erreur lors de l'insertion du genre {arDTO.Name}");
            }

            return arDTO;
        }

        public void UpdateAgeRating(int id, AgeRatingDto arDTO)
        {
            if (_ageRatingDao.UpdateAgeRating(id, arDTO).IsFaulted)
            {
                throw new InternalErrorException($"Erreur lors de la modification du genre {arDTO.Name}");
            }
        }
                

        public void DeleteAgeRating(int id)
        {
            if (_ageRatingDao.DeleteAgeRating(id).IsFaulted)
            {
                throw new InternalErrorException($"Erreur lors de la suppression du genre d'identifiant {id}");
            }
        }
    }
}
