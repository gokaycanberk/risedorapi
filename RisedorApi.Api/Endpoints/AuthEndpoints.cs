using Microsoft.AspNetCore.Mvc;
using RisedorApi.Api.Models;
using RisedorApi.Domain.Entities;
using RisedorApi.Domain.Enums;
using RisedorApi.Infrastructure.Persistence;
using RisedorApi.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace RisedorApi.Api.Endpoints;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost(
                "/auth/login",
                async (
                    [FromBody] LoginRequest request,
                    [FromServices] ApplicationDbContext dbContext,
                    [FromServices] JwtAuthManager jwtAuthManager
                ) =>
                {
                    var user = await dbContext.Users.FirstOrDefaultAsync(
                        u => u.Email == request.Email
                    );

                    if (user == null)
                    {
                        return Results.Unauthorized();
                    }

                    // In a real application, you would hash the password and compare with the stored hash
                    // This is just for demonstration
                    if (request.Password != user.PasswordHash)
                    {
                        return Results.Unauthorized();
                    }

                    var token = jwtAuthManager.GenerateToken(user);
                    return Results.Ok(new { Token = token });
                }
            )
            .AllowAnonymous()
            .WithName("Login")
            .WithOpenApi();

        // Example protected endpoint
        app.MapGet(
                "/auth/test",
                (ClaimsPrincipal user) =>
                {
                    return Results.Ok(
                        new { Message = $"Hello {user.Identity?.Name}! You are authenticated." }
                    );
                }
            )
            .RequireAuthorization()
            .WithName("TestAuth")
            .WithOpenApi();
    }
}
