using Microsoft.AspNetCore.Mvc;
using VideoTheque.Businesses.Emprunts;
using VideoTheque.Businesses.Films;
using VideoTheque.ViewModels;

namespace VideoTheque.Controllers
{
    [ApiController]
    [Route("films")]
    public class FilmsController
    {
        private readonly IFilmsBusiness _filmsBusiness;
        private readonly IEmpruntsBusiness _empruntsBusiness;

        public FilmsController(IFilmsBusiness filmsBusiness, IEmpruntsBusiness empruntsBusiness)
        {
            _filmsBusiness = filmsBusiness;
            _empruntsBusiness = empruntsBusiness;
        }

        [HttpGet]
        public async Task<List<FilmViewModel>> GetFilms() =>
            (_filmsBusiness.GetFilms()).Select(FilmViewModel.ToModel).ToList();

        [HttpGet("{id}")]
        public async Task<FilmViewModel> GetFilm([FromRoute] int id) =>
            FilmViewModel.ToModel(_filmsBusiness.GetFilm(id));

        [HttpPost]
        public async Task<FilmViewModel> InsertFilm([FromBody] FilmViewModel filmVM) => FilmViewModel.ToModel(_filmsBusiness.InsertFilm(filmVM.ToDto()));

        [HttpPut("{id}")]
        public async Task<IResult> UpdateFilm([FromRoute] int id, [FromBody] FilmViewModel filmVM)
        {
            _filmsBusiness.UpdateFilm(id, filmVM.ToDto());
            return Results.Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IResult> DeleteGenre([FromRoute] int id)
        {
            _filmsBusiness.DeleteFilm(id);
            return Results.Ok();
        }

        [HttpPost("empruntables/{id}")]
        public async Task<EmpruntViewModel> InsertEmpruntableFilm([FromRoute] int id)
            => _empruntsBusiness.EmpruntFilm(id).Result;
        
        [HttpDelete("empruntables/{name}")]
        public async Task<IResult> DeleteEmpruntableFilm([FromRoute] string name)
        {
            _empruntsBusiness.DeleteEmprunt(name);
            return Results.Ok();
        }
        
        [HttpGet("empruntables")]
        public async Task GetEmpruntableFilms()
            => _empruntsBusiness.GetEmpruntableFilms();

    }
}