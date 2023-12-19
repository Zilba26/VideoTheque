using VideoTheque.DTOs;
using VideoTheque.Repositories.Film;
using VideoTheque.ViewModels;

namespace VideoTheque.Businesses.Emprunts
{
    public class EmpruntsBusiness : IEmpruntsBusiness
    {

        private readonly IBluRayRepository _bluRayDao;
        
        public void EmpruntFilm(int idHost, int idFilm)
        {
            //TODO
        }

        public EmpruntViewModel EmpruntFilm(int idFilm)
        {
            return new EmpruntViewModel();
        }

        public void DeleteEmprunt(string filmName)
        {
            
        }

        public List<EmpruntableFilmViewModel> GetEmpruntableFilms()
        {
            Task<List<BluRayDto>> filmsDto = _bluRayDao.GetBluRays();
            List<EmpruntableFilmViewModel> empruntableFilmViewModels = new List<EmpruntableFilmViewModel>();
            foreach (var film in filmsDto.Result)
            {
                if (film.IdOwner == null)
                {
                    empruntableFilmViewModels.Add(new EmpruntableFilmViewModel(film.Id, film.Title));
                }
            }
            
            return empruntableFilmViewModels;
        }

        public EmpruntableFilmViewModel GetEmpruntableFilms(int idHost)
        {
            return null;
        }
    }
}