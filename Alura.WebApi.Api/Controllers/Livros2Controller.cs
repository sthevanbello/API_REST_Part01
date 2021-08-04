using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Alura.WebApi.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v2.0/[controller]")]
    public class Livros2Controller : ControllerBase
    {

    }
}
