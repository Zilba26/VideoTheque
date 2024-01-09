using VideoTheque.DTOs;

namespace VideoTheque.Businesses.Films
{
    public interface IFilmsBusiness
    {
        List<FilmDto> GetFilms();
        
        FilmDto GetFilm(int id);
        
        FilmDto InsertFilm(FilmDto film);
        
        void UpdateFilm(int id, FilmDto film);
        
        Task DeleteFilm(int id);
    }
}