using Newtonsoft.Json;
using VideoTheque.Core;
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

        public async Task EmpruntFilm(int idHost, int idFilm)
        {
            HostDto? host = await _hostsDao.GetHost(idHost);
            if (host == null)
            {
                throw new NotFoundException("Host not found");
            }
            Console.WriteLine("host url + emprunts : " + host.Url + "/films/empruntables/" + idFilm);
            HttpResponseMessage response = await _httpClient.PostAsync(host.Url + "/films/empruntables/" + idFilm, new StringContent(""));
            Console.WriteLine("response : " + response);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                Console.WriteLine("content : " + content);
                EmpruntViewModel? emprunt = System.Text.Json.JsonSerializer.Deserialize<EmpruntViewModel>(content);
                if (emprunt == null)
                {
                    throw new InternalErrorException("Error while emprunting film");
                }
                Console.WriteLine("emprunt : " + emprunt);
                await VerifyAndInsertPersonneExist(emprunt.FirstActor);
                Console.WriteLine("first actor ok");
                await VerifyAndInsertPersonneExist(emprunt.Scenarist);
                Console.WriteLine("scenarist ok");
                await VerifyAndInsertPersonneExist(emprunt.Director);
                Console.WriteLine("director ok");
                await VerifyAndInsertAgeRatingExist(emprunt.AgeRating);
                Console.WriteLine("age rating ok");
                await VerifyAndInsertGenreExist(emprunt.Genre);
                Console.WriteLine("emprunt : " + emprunt);
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
                Console.WriteLine("blu ray dto : " + bluRayDto);
                await _bluRayDao.InsertBluRay(bluRayDto);
                Console.WriteLine("blu ray inserted");
            }
            else
            {
                throw new InternalErrorException("Error while emprunting film");
            }
        }
        
        private async Task VerifyAndInsertPersonneExist(PersonneViewModel personne)
        {
            Console.WriteLine("verify and insert personne exist + " + personne.FullName);
            PersonneDto? personneDto = _personnesDao.GetPersonne(personne.LastName, personne.FirstName).Result;
            Console.WriteLine("verify and insert personne exist + " + personneDto?.getFullName());
            if (personneDto == null)
            {
                Console.WriteLine("personne not found");
                await _personnesDao.InsertPersonne(personne.ToDto());
            }
            else
            {
                Console.WriteLine("personne found");
                personne.Id = personneDto.Id;
            }
        }
        
        private async Task VerifyAndInsertAgeRatingExist(AgeRatingViewModel ageRating)
        {
            AgeRatingDto? ageRatingDto = await _ageRatingsDao.GetAgeRating(ageRating.Name);
            if (ageRatingDto == null)
            {
                Console.WriteLine("age rating not found");
                Console.WriteLine(ageRating);
                Console.WriteLine(ageRating.ToDto());
                await _ageRatingsDao.InsertAgeRating(ageRating.ToDto());
            }
            else
            {
                Console.WriteLine("age rating found");
                ageRating.Id = ageRatingDto.Id;
            }
        }
        
        private async Task VerifyAndInsertGenreExist(GenreViewModel genre)
        {
            GenreDto? genreDto = await _genresDao.GetGenre(genre.Name);
            if (genreDto == null)
            {
                Console.WriteLine("genre not found");
                await _genresDao.InsertGenre(genre.ToDto());
            }
            else
            {
                Console.WriteLine("genre found");
                genre.Id = genreDto.Id;
            }
        }
        
        public async Task<EmpruntViewModel> EmpruntFilm(int idFilm)
        {
            BluRayDto? bluRayDto = _bluRayDao.GetBluRay(idFilm).Result;
            if (bluRayDto == null)
            {
                throw new NotFoundException("Film not found");
            }
            if (bluRayDto.IdOwner != null)
            {
                throw new InternalErrorException("The film is not mine");
            }
            if (!bluRayDto.IsAvailable)
            {
                throw new InternalErrorException("The film is not available");
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
            await _bluRayDao.SetAvailable(idFilm, false);
            return empruntViewModel;
        }

        public async Task DeleteEmprunt(string filmName)
        {
            BluRayDto? bluRayDto = _bluRayDao.GetBluRayByName(filmName).Result;
            if (bluRayDto == null)
            {
                throw new NotFoundException("Film {" + filmName + "} not found");
            }
            if (bluRayDto.IdOwner != null)
            {
                throw new InternalErrorException("The film {" + filmName + "} is not mine, use films/delete instead");
            }
            bluRayDto.IsAvailable = true;
            await _bluRayDao.UpdateBluRay(bluRayDto.Id, bluRayDto);
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

        public async Task<List<EmpruntableFilmViewModel>> GetEmpruntableFilms(int idHost)
        {
            HostDto? host = await _hostsDao.GetHost(idHost);
            if (host == null)
            {
                throw new NotFoundException("Host not found");
            }
            Console.WriteLine(host.Url + "/films/empruntables/");
            HttpResponseMessage response = await _httpClient.GetAsync(host.Url + "/films/empruntables/");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                List<EmpruntableFilmViewModel>? empruntableFilm = JsonConvert.DeserializeObject<List<EmpruntableFilmViewModel>>(content);
                if (empruntableFilm == null)
                {
                    throw new InternalErrorException("Error while getting empruntable films");
                }
                return empruntableFilm;

            }
            throw new InternalErrorException("Error while getting empruntable films");
        }
    }
}