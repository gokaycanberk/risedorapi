using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RisedorApi.Api.Models;
using RisedorApi.Application.Commands.User;
using RisedorApi.Application.Queries.User;
using RisedorApi.Domain.Entities;
using RisedorApi.Infrastructure.Services;

namespace RisedorApi.Api.Endpoints;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this WebApplication app)
    {
        app.MapPost(
            "/api/auth/login",
            async ([FromBody] LoginRequest request, [FromServices] IMediator mediator) =>
            {
                try
                {
                    var result = await mediator.Send(
                        new LoginUserCommand(request.Email, request.Password)
                    );
                    return Results.Ok(result);
                }
                catch (FluentValidation.ValidationException ex)
                {
                    return Results.BadRequest(new { Message = ex.Message });
                }
            }
        );

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

public record LoginRequest(string Email, string Password);
