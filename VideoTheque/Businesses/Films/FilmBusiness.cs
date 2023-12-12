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
            PersonneDto? director = _personneDao.GetPersonne(film.Director.LastName, film.Director.FirstName).Result;
            PersonneDto? scenarist = _personneDao.GetPersonne(film.Scenarist.LastName, film.Scenarist.FirstName).Result;
            PersonneDto? firstActor = _personneDao.GetPersonne(film.FirstActor.LastName, film.FirstActor.FirstName).Result;
            AgeRatingDto? ageRating = _ageRatingDao.GetAgeRating(film.AgeRating.Name).Result;
            GenreDto? genre = _genreDao.GetGenre(film.Genre.Name).Result;
            if (director == null)
            {
                throw new NotFoundException($"Réalisateur '{film.Director.LastName} {film.Director.FirstName}' non trouvé");
            }
            if (scenarist == null)
            {
                throw new NotFoundException($"Scénariste '{film.Scenarist.LastName} {film.Scenarist.FirstName}' non trouvé");
            }
            if (firstActor == null)
            {
                throw new NotFoundException($"Acteur '{film.FirstActor.LastName} {film.FirstActor.FirstName}' non trouvé");
            }
            if (ageRating == null)
            {
                throw new NotFoundException($"Classification '{film.AgeRating.Name}' non trouvée");
            }
            if (genre == null)
            {
                throw new NotFoundException($"Genre '{film.Genre.Name}' non trouvé");
            }
            BluRayDto blueRay = new BluRayDto
            {
                Title = film.Title,
                Duration = film.Duration,
                IdDirector = director.Id,
                IdScenarist = scenarist.Id,
                IdFirstActor = firstActor.Id,
                IdAgeRating = ageRating.Id,
                IdGenre = genre.Id,
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

            if (!bluRay.IsAvailable)
            {
                throw new InternalErrorException($"Le film '{id}' n'est pas disponible");
            }
            PersonneDto? director = _personneDao.GetPersonne(film.Director.LastName, film.Director.FirstName).Result;
            PersonneDto? scenarist = _personneDao.GetPersonne(film.Scenarist.LastName, film.Scenarist.FirstName).Result;
            PersonneDto? firstActor = _personneDao.GetPersonne(film.FirstActor.LastName, film.FirstActor.FirstName).Result;
            AgeRatingDto? ageRating = _ageRatingDao.GetAgeRating(film.AgeRating.Name).Result;
            GenreDto? genre = _genreDao.GetGenre(film.Genre.Name).Result;
            if (director == null)
            {
                throw new NotFoundException($"Réalisateur '{film.Director.LastName} {film.Director.FirstName}' non trouvé");
            }
            if (scenarist == null)
            {
                throw new NotFoundException($"Scénariste '{film.Scenarist.LastName} {film.Scenarist.FirstName}' non trouvé");
            }
            if (firstActor == null)
            {
                throw new NotFoundException($"Acteur '{film.FirstActor.LastName} {film.FirstActor.FirstName}' non trouvé");
            }
            if (ageRating == null)
            {
                throw new NotFoundException($"Classification '{film.AgeRating.Name}' non trouvée");
            }
            if (genre == null)
            {
                throw new NotFoundException($"Genre '{film.Genre.Name}' non trouvé");
            }
            bluRay.Title = film.Title;
            bluRay.Duration = film.Duration;
            bluRay.IdDirector = director.Id;
            bluRay.IdScenarist = scenarist.Id;
            bluRay.IdFirstActor = firstActor.Id;
            bluRay.IdAgeRating = ageRating.Id;
            bluRay.IdGenre = genre.Id;
            if (_bluRayDao.UpdateBluRay(id, bluRay).IsFaulted)
            {
                throw new InternalErrorException($"Erreur lors de la modification du film {film.Title}");
            }
        }

        public void DeleteFilm(int id)
        {
            BluRayDto bluRay = _bluRayDao.GetBluRay(id).Result;
            if (bluRay == null)
            {
                throw new NotFoundException($"Film '{id}' non trouvé");
            }
            if (!bluRay.IsAvailable)
            {
                throw new InternalErrorException($"Le film '{id}' n'est pas disponible");
            }
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