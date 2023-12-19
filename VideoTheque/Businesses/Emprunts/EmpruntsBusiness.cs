using VideoTheque.DTOs;
using VideoTheque.ViewModels;

namespace VideoTheque.Businesses.Emprunts
{
    public class EmpruntsBusiness : IEmpruntsBusiness
    {

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

        public EmpruntableFilmViewModel GetEmpruntableFilms()
        {
            return new EmpruntableFilmViewModel();
        }

        public EmpruntableFilmViewModel GetEmpruntableFilms(int idHost)
        {
            return new EmpruntableFilmViewModel();
        }
    }
}