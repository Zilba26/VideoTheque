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
    }
}