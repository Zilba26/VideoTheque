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
        HttpClient _httpClient = new HttpClient();
        IHostsRepository _hostsRepository;
        IPersonnesRepository _personnesRepository;
        IAgeRatingsRepository _ageRatingsRepository;
        IGenresRepository _genresRepository;
        IBluRayRepository _bluRayRepository;
        
        public EmpruntsBusiness(IHostsRepository hostsRepository, IPersonnesRepository personnesRepository,
            IAgeRatingsRepository ageRatingsRepository, IGenresRepository genresRepository, 
            IBluRayRepository bluRayRepository)
        {
            _hostsRepository = hostsRepository;
            _personnesRepository = personnesRepository;
            _ageRatingsRepository = ageRatingsRepository;
            _genresRepository = genresRepository;
            _bluRayRepository = bluRayRepository;
        }

        public async void EmpruntFilm(int idHost, int idFilm)
        {
            HostDto? host = await _hostsRepository.GetHost(idHost);
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
                await _bluRayRepository.InsertBluRay(bluRayDto);
            }
            else
            {
                throw new Exception("Error while emprunting film");
            }
        }
        
        private async void VerifyAndInsertPersonneExist(PersonneViewModel personne)
        {
            PersonneDto? personneDto = await _personnesRepository.GetPersonne(personne.LastName, personne.FirstName);
            if (personneDto == null)
            {
                await _personnesRepository.InsertPersonne(personne.ToDto());
            }
            else
            {
                personne.Id = personneDto.Id;
            }
        }
        
        private async void VerifyAndInsertAgeRatingExist(AgeRatingViewModel ageRating)
        {
            AgeRatingDto? ageRatingDto = await _ageRatingsRepository.GetAgeRating(ageRating.Name);
            if (ageRatingDto == null)
            {
                await _ageRatingsRepository.InsertAgeRating(ageRating.ToDto());
            }
            else
            {
                ageRating.Id = ageRatingDto.Id;
            }
        }
        
        private async void VerifyAndInsertGenreExist(GenreViewModel genre)
        {
            GenreDto? genreDto = await _genresRepository.GetGenre(genre.Name);
            if (genreDto == null)
            {
                await _genresRepository.InsertGenre(genre.ToDto());
            }
            else
            {
                genre.Id = genreDto.Id;
            }
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