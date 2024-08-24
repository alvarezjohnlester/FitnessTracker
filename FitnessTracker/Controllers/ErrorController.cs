using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FitnessTracker.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	[ApiExplorerSettings(IgnoreApi = true)]
	public class ErrorController : ControllerBase
	{
		[Route("/error")]
		public IActionResult HandleError()
		{
			var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
			var exception = context?.Error;

			var response = new
			{
				StatusCode = (int)HttpStatusCode.InternalServerError,
				Message = "An unexpected error occurred.",
				Detailed = exception?.Message
			};

			return StatusCode((int)HttpStatusCode.InternalServerError, response);
		}

	}
}
