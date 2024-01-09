using Microsoft.AspNetCore.Mvc;
using VideoTheque.Businesses.Emprunts;
using VideoTheque.ViewModels;

namespace VideoTheque.Controllers
{
    [ApiController]
    [Route("emprunts")]
    public class EmpruntsController
    {
        
        private readonly IEmpruntsBusiness _empruntsBusiness;
        
        public EmpruntsController(IEmpruntsBusiness empruntsBusiness)
        {
            _empruntsBusiness = empruntsBusiness;
        }

        [HttpPost("{idHost}/{idFilm}")]
        public async Task InsertEmprunt([FromRoute] int idHost, [FromRoute] int idFilm)
        {
            Console.WriteLine("Insert emprunt");
            await _empruntsBusiness.EmpruntFilm(idHost, idFilm);
        }

        [HttpGet("{idHost}")]
        public async Task<List<EmpruntableFilmViewModel>> GetHostEmpruntableFilms([FromRoute] int idHost)
        {
            Console.WriteLine("Get host empruntable");
            return _empruntsBusiness.GetEmpruntableFilms(idHost).Result;
        }
        
    }
}