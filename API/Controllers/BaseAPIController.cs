using Application.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")] //api/activities
    public class BaseAPIController : ControllerBase
    {
        private IMediator _mediator;

        // ??= null coelsence 
        // if _mediator is null, whatever it on the right of ??= we assign as the value of Mediator
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
        

        protected ActionResult HandleResult<T>(Result<T> result)
        {
        if(result == null) return NotFound();
         if(result.IsSuccess && result.Value != null)
        return Ok(result.Value);
        if(result.IsSuccess && result.Value == null)
         return NotFound();

         return BadRequest(result.Error);
        }
    }
}