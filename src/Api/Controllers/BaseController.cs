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

            return firstError.Type switch
            {
                ErrorTypeEnum.NotFound => new NotFoundObjectResult(firstError.Message),
                ErrorTypeEnum.BadRequest => new BadRequestObjectResult(firstError.Message),
                ErrorTypeEnum.Validation => new UnprocessableEntityObjectResult(firstError.Message),
                ErrorTypeEnum.InternalServerError => new ObjectResult(firstError.Message)
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                },
                _ => new ObjectResult(firstError.Message)
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                }
            };

        }

    }
}
