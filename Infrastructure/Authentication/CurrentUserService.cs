using Infrastructure.Authentication.Constants;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Infrastructure.Authentication
{
    internal class CurrentUserService : ICurrentUser
    {
        private readonly ClaimsPrincipal? _user;

        public CurrentUserService(IHttpContextAccessor accessor)
        {
            _user = accessor?.HttpContext?.User;
        }

        public int? Id
        {
            get
            {
                var id = _user?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                return id == null ? null : int.Parse(id);
            }
        }

        public int? DoctorId
        {
            get
            {
                var id = _user?.FindFirst(ClaimTypesConstants.DoctorId)?.Value;
                return id == null ? null : int.Parse(id);
            }
        }

        public string? Email => _user?.FindFirst(ClaimTypes.Email)?.Value;
        public string? Name => _user?.FindFirst(ClaimTypes.GivenName)?.Value;
        public Roles? Role 
        {
            get
            {
                if (Enum.TryParse<Roles>(_user?.FindFirst(ClaimTypes.Role)?.Value,out var role))
                    return role;

                return null;
            }
        }      
    }
}
