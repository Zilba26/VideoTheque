using Microsoft.AspNetCore.Mvc;
using VideoTheque.Businesses.Films;
using VideoTheque.ViewModels;

namespace VideoTheque.Controllers
{
    [ApiController]
    [Route("films")]
    public class FilmsController
    {
        private readonly IFilmsBusiness _filmsBusiness;

        public FilmsController(IFilmsBusiness filmsBusiness)
        {
            _filmsBusiness = filmsBusiness;
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

    }
}