using MediatR;
using Microsoft.AspNetCore.Mvc;
using RisedorApi.Application.Commands.User;
using RisedorApi.Application.Queries.User;

namespace RisedorApi.Api.Endpoints;

public static class UserEndpoints
{
    public static void MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/users").WithTags("Users");

        group
            .MapPost(
                "/",
                async ([FromBody] CreateUserCommand command, [FromServices] IMediator mediator) =>
                {
                    var userId = await mediator.Send(command);
                    return Results.Created($"/api/users/{userId}", userId);
                }
            )
            .AllowAnonymous();

        group
            .MapPost(
                "/login",
                async ([FromBody] LoginUserCommand command, [FromServices] IMediator mediator) =>
                {
                    var response = await mediator.Send(command);
                    return Results.Ok(response);
                }
            )
            .AllowAnonymous();

        group.MapGet(
            "/",
            async ([FromServices] IMediator mediator) =>
            {
                var users = await mediator.Send(new GetUsersQuery());
                return Results.Ok(users);
            }
        );

        group.MapGet(
            "/{id}",
            async (int id, [FromServices] IMediator mediator) =>
            {
                var user = await mediator.Send(new GetUserByIdQuery(id));
                if (user == null)
                    return Results.NotFound();
                return Results.Ok(user);
            }
        );

        group.MapPut(
            "/{id}",
            async (
                int id,
                [FromBody] UpdateUserCommand command,
                [FromServices] IMediator mediator
            ) =>
            {
                if (id != command.Id)
                    return Results.BadRequest();

                var success = await mediator.Send(command);
                if (!success)
                    return Results.NotFound();

                return Results.NoContent();
            }
        );

        group.MapDelete(
            "/{id}",
            async (int id, [FromServices] IMediator mediator) =>
            {
                var success = await mediator.Send(new DeleteUserCommand(id));
                if (!success)
                    return Results.NotFound();

                return Results.NoContent();
            }
        );
    }
}
