using VideoTheque.DTOs;
using VideoTheque.ViewModels;

namespace VideoTheque.Businesses.Emprunts
{
    public interface IEmpruntsBusiness
    {
        //Empruntes un film à un hôte
        Task EmpruntFilm(int idHost, int idFilm);
        
        //Passes un film en emprunté et retourne la copie du film
        Task<EmpruntViewModel> EmpruntFilm(int idFilm);

        //Supprime un emprunt
        Task DeleteEmprunt(string filmName);
        
        //Récupère nos films empruntables
        List<EmpruntableFilmViewModel> GetEmpruntableFilms();
        
        //Recupère les films empruntables de l'hôte
        Task<List<EmpruntableFilmViewModel>> GetEmpruntableFilms(int idHost);
    }
}