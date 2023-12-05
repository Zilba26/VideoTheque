using Microsoft.AspNetCore.Mvc;
using VideoTheque.ViewModels;

namespace VideoTheque.Controllers
{
    [ApiController]
    [Route("supports")]
    public class SupportsController : ControllerBase
    {

        [HttpGet]
        public ActionResult<IEnumerable<object>> GetSupports()
        {
            var supports = Enum.GetValues(typeof(Support))
                .Cast<Support>()
                .Select(s => new { id = (int)s, nom = s.ToString() })
                .ToList();

            return Ok(supports);
        }
    }
}