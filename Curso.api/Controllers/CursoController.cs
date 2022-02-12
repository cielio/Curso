using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Curso.api.Controllers
{
    [Route("api/v1/cursos")]
    [ApiController]
    public class CursoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
