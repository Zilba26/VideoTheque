using Newtonsoft.Json;
using VideoTheque.DTOs;
using VideoTheque.Repositories.AgeRating;
using VideoTheque.Repositories.Film;
using VideoTheque.Repositories.Genres;
using VideoTheque.Repositories.Hosts;
using VideoTheque.Repositories.Personnes;
using VideoTheque.ViewModels;

namespace VideoTheque.Businesses.Emprunts
{
    public class EmpruntsBusiness : IEmpruntsBusiness
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly IHostsRepository _hostsDao;
        private readonly IPersonnesRepository _personnesDao;
        private readonly IAgeRatingsRepository _ageRatingsDao;
        private readonly IGenresRepository _genresDao;
        private readonly IBluRayRepository _bluRayDao;
        
        public EmpruntsBusiness(IHostsRepository hostsRepository, IPersonnesRepository personnesRepository,
            IAgeRatingsRepository ageRatingsRepository, IGenresRepository genresRepository, 
            IBluRayRepository bluRayRepository)
        {
            _hostsDao = hostsRepository;
            _personnesDao = personnesRepository;
            _ageRatingsDao = ageRatingsRepository;
            _genresDao = genresRepository;
            _bluRayDao = bluRayRepository;
        }

        public async void EmpruntFilm(int idHost, int idFilm)
        {
            HostDto? host = await _hostsDao.GetHost(idHost);
            if (host == null)
            {
                throw new Exception("Host not found");
            }
            HttpResponseMessage response = await _httpClient.GetAsync(host.Name + "/emprunts/" + idFilm);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                EmpruntViewModel? emprunt = JsonConvert.DeserializeObject<EmpruntViewModel>(content);
                if (emprunt == null)
                {
                    throw new Exception("Error while emprunting film");
                }
                VerifyAndInsertPersonneExist(emprunt.FirstActor);
                VerifyAndInsertPersonneExist(emprunt.Scenarist);
                VerifyAndInsertPersonneExist(emprunt.Director);
                VerifyAndInsertAgeRatingExist(emprunt.AgeRating);
                VerifyAndInsertGenreExist(emprunt.Genre);
                BluRayDto bluRayDto = new BluRayDto
                {
                    Duration = emprunt.Duration,
                    IdAgeRating = emprunt.AgeRating.Id,
                    IdDirector = emprunt.Director.Id,
                    IdFirstActor = emprunt.FirstActor.Id,
                    IdGenre = emprunt.Genre.Id,
                    IdScenarist = emprunt.Scenarist.Id,
                    Title = emprunt.Title,
                    IdOwner = idHost
                };
                await _bluRayDao.InsertBluRay(bluRayDto);
            }
            else
            {
                throw new Exception("Error while emprunting film");
            }
        }
        
        private async void VerifyAndInsertPersonneExist(PersonneViewModel personne)
        {
            PersonneDto? personneDto = await _personnesDao.GetPersonne(personne.LastName, personne.FirstName);
            if (personneDto == null)
            {
                await _personnesDao.InsertPersonne(personne.ToDto());
            }
            else
            {
                personne.Id = personneDto.Id;
            }
        }
        
        private async void VerifyAndInsertAgeRatingExist(AgeRatingViewModel ageRating)
        {
            AgeRatingDto? ageRatingDto = await _ageRatingsDao.GetAgeRating(ageRating.Name);
            if (ageRatingDto == null)
            {
                await _ageRatingsDao.InsertAgeRating(ageRating.ToDto());
            }
            else
            {
                ageRating.Id = ageRatingDto.Id;
            }
        }
        
        private async void VerifyAndInsertGenreExist(GenreViewModel genre)
        {
            GenreDto? genreDto = await _genresDao.GetGenre(genre.Name);
            if (genreDto == null)
            {
                await _genresDao.InsertGenre(genre.ToDto());
            }
            else
            {
                genre.Id = genreDto.Id;
            }
        }

        public EmpruntViewModel EmpruntFilm(int idFilm)
        {
            BluRayDto? bluRayDto = _bluRayDao.GetBluRay(idFilm).Result;
            if (bluRayDto == null)
            {
                throw new Exception("Film not found");
            }
            PersonneDto? firstActor = _personnesDao.GetPersonne(bluRayDto.IdFirstActor).Result;
            PersonneDto? director = _personnesDao.GetPersonne(bluRayDto.IdDirector).Result;
            PersonneDto? scenarist = _personnesDao.GetPersonne(bluRayDto.IdScenarist).Result;
            AgeRatingDto? ageRating = _ageRatingsDao.GetAgeRating(bluRayDto.IdAgeRating).Result;
            GenreDto? genre = _genresDao.GetGenre(bluRayDto.IdGenre).Result;
            EmpruntViewModel empruntViewModel = new EmpruntViewModel
            {
                Title = bluRayDto.Title,
                Duration = bluRayDto.Duration,
                Support = Support.BluRay.ToString(),
                FirstActor = PersonneViewModel.FromDto(firstActor),
                Director = PersonneViewModel.FromDto(director),
                Scenarist = PersonneViewModel.FromDto(scenarist),
                AgeRating = AgeRatingViewModel.FromDto(ageRating),
                Genre = GenreViewModel.FromDto(genre)
            };
            return empruntViewModel;
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