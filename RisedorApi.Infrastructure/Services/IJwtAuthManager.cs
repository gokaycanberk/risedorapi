using RisedorApi.Domain.Entities;

namespace RisedorApi.Infrastructure.Services;

public interface IJwtAuthManager
{
    string GenerateToken(User user);
}
