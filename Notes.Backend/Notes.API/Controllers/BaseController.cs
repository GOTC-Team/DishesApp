using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Notes.API.Controllers;


// [ApiVersionNeutral] // контролер может быть вызван из любой из версий API (версия не указана)
[ApiController]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
public abstract class BaseController : ControllerBase
{
    private IMediator? _mediator;
    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<IMediator>();

    internal Guid UserId => User.Identity.IsAuthenticated
                            ? Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier))
                            : Guid.Empty;
}
