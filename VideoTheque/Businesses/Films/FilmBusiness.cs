using VideoTheque.Core;
using VideoTheque.DTOs;
using VideoTheque.Repositories.AgeRating;
using VideoTheque.Repositories.Film;
using VideoTheque.Repositories.Genres;
using VideoTheque.Repositories.Personnes;
using VideoTheque.ViewModels;

namespace VideoTheque.Businesses.Films
{
    public class FilmBusiness : IFilmsBusiness
    {
        private readonly IBluRayRepository _bluRayDao;
        private readonly IPersonnesRepository _personneDao;
        private readonly IAgeRatingsRepository _ageRatingDao;
        private readonly IGenresRepository _genreDao;
        
        public FilmBusiness(IBluRayRepository blueRayDao, IPersonnesRepository personneDao,
            IAgeRatingsRepository ageRatingDao, IGenresRepository genreDao)
        {
            _bluRayDao = blueRayDao;
            _personneDao = personneDao;
            _ageRatingDao = ageRatingDao;
            _genreDao = genreDao;
        }
        
        public List<FilmDto> GetFilms()
        {
            List<FilmDto> films = new List<FilmDto>();
            films.AddRange(GetBlueRays());
            return films;
        }

        private List<FilmDto> GetBlueRays()
        {
            List<BluRayDto> blueRays = _bluRayDao.GetBluRays().Result;
            List<FilmDto> films = new List<FilmDto>();
            foreach (BluRayDto blueRay in blueRays)
            {
                FilmDto film = ParseBluRayDtoToFilmDto(blueRay);
                films.Add(film);
            }
            return films;
        }

        public FilmDto GetFilm(int id)
        {
            BluRayDto blueRay = _bluRayDao.GetBluRay(id).Result;
            if (blueRay == null)
            {
                throw new NotFoundException($"Film '{id}' non trouvé");
            }

            return ParseBluRayDtoToFilmDto(blueRay);
        }
        
        public FilmDto InsertFilm(FilmDto film)
        {
            switch (film.Support)
            {
                case Support.BluRay:
                    InsertBlueRay(film);
                    return film;
                default:
                    return null;
            }
        }
        
        private void InsertBlueRay(FilmDto film)
        {
            BluRayDto blueRay = new BluRayDto
            {
                Title = film.Title,
                Duration = film.Duration,
                IdDirector = film.Director.Id,
                IdScenarist = film.Scenarist.Id,
                IdFirstActor = film.FirstActor.Id,
                IdAgeRating = film.AgeRating.Id,
                IdGenre = film.Genre.Id,
                IsAvailable = true,
                IdOwner = null
            };
            if (_bluRayDao.InsertBluRay(blueRay).IsFaulted)
            {
                throw new InternalErrorException($"Erreur lors de l'insertion du film {film.Title}");
            }
        }

        public void UpdateFilm(int id, FilmDto film)
        {
            BluRayDto bluRay = _bluRayDao.GetBluRay(id).Result;
            if (bluRay == null)
            {
                throw new NotFoundException($"Film '{id}' non trouvé");
            }
            bluRay.Title = film.Title;
            bluRay.Duration = film.Duration;
            bluRay.IdDirector = film.Director.Id;
            bluRay.IdScenarist = film.Scenarist.Id;
            bluRay.IdFirstActor = film.FirstActor.Id;
            bluRay.IdAgeRating = film.AgeRating.Id;
            bluRay.IdGenre = film.Genre.Id;
            if (_bluRayDao.UpdateBluRay(id, bluRay).IsFaulted)
            {
                throw new InternalErrorException($"Erreur lors de la modification du film {film.Title}");
            }
        }

        public void DeleteFilm(int id)
        {
            if (_bluRayDao.DeleteBluRay(id).IsFaulted)
            {
                throw new InternalErrorException($"Erreur lors de la suppression du film d'identifiant {id}");
            }
        }

        private FilmDto ParseBluRayDtoToFilmDto(BluRayDto blueRay)
        {
            PersonneDto director = _personneDao.GetPersonne(blueRay.IdDirector).Result;
            PersonneDto scenarist = _personneDao.GetPersonne(blueRay.IdScenarist).Result;
            PersonneDto firstActor = _personneDao.GetPersonne(blueRay.IdFirstActor).Result;
            AgeRatingDto ageRating = _ageRatingDao.GetAgeRating(blueRay.IdAgeRating).Result;
            GenreDto genre = _genreDao.GetGenre(blueRay.IdGenre).Result;
            Support support = Support.BluRay;
            FilmDto film = new FilmDto
            {
                Id = blueRay.Id,
                Title = blueRay.Title,
                Duration = blueRay.Duration,
                Director = director,
                Scenarist = scenarist,
                Support = support,
                Genre = genre,
                FirstActor = firstActor,
                AgeRating = ageRating
            };
            return film;
        }
    }
}