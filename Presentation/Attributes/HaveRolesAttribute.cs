using Domain.Enums;
using Microsoft.AspNetCore.Authorization;

namespace Presentation.Attributes
{
    public class HaveRolesAttribute: AuthorizeAttribute
    {
        public HaveRolesAttribute(params Roles[] roles)
        {
            Roles = string.Join(",", roles);
        }
    }
}
