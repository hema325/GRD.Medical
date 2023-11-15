using Domain.Entities;

namespace Infrastructure.Authentication.JWT.JWTManager
{
    internal interface IJWTManager
    {
        JWTToken GenerateToken(User user);
    }
}
