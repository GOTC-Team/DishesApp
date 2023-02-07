using Notes.Application.Common;
using System.Security.Claims;

namespace Notes.API.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _accessor;

        public string? UserId => _accessor.HttpContext.User?.FindFirstValue(ClaimTypes.NameIdentifier);

        public CurrentUserService(IHttpContextAccessor accessor)
        {
            this._accessor = accessor;
        }
    }
}
