using CrossCutting.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    public abstract class BaseController
    {
        [NonAction]
        public IActionResult CustomReponse<T>(Result<T> result)
        {
            if (result.IsSuccess)
            {
                return new OkObjectResult(result.Data);
            }

            if (result.Errors == null || result.Errors.Length == 0)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            var firstError = result.Errors.First();

            var messageError = new
            {
                firstError.Message,
            };

            return firstError.Type switch
            {
                ErrorTypeEnum.NotFound => new NotFoundObjectResult(messageError),
                ErrorTypeEnum.BadRequest => new BadRequestObjectResult(messageError),
                ErrorTypeEnum.Validation => new UnprocessableEntityObjectResult(messageError),
                ErrorTypeEnum.InternalServerError => new ObjectResult(messageError)
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                },
                _ => new ObjectResult(messageError)
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                }
            };

        }

    }
}
