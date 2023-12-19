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
            => _empruntsBusiness.EmpruntFilm(idHost, idFilm);
        
        [HttpGet("{idHost}")]
        public async Task GetHostEmpruntableFilms([FromRoute] int idHost)
            => _empruntsBusiness.GetEmpruntableFilms();
        
    }
}