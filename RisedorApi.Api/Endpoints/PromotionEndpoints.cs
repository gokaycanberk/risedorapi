using MediatR;
using Microsoft.AspNetCore.Mvc;
using RisedorApi.Application.Commands.Promotion;
using RisedorApi.Application.Queries.Promotion;

namespace RisedorApi.Api.Endpoints;

public static class PromotionEndpoints
{
    public static void MapPromotionEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/promotions").WithTags("Promotions").RequireAuthorization();

        group
            .MapPost(
                "/",
                async (
                    [FromBody] CreatePromotionCommand command,
                    [FromServices] IMediator mediator
                ) =>
                {
                    var promotionId = await mediator.Send(command);
                    return Results.Created($"/api/promotions/{promotionId}", promotionId);
                }
            )
            .RequireAuthorization("Vendor");

        group.MapGet(
            "/",
            async ([FromServices] IMediator mediator) =>
            {
                var promotions = await mediator.Send(new GetPromotionsQuery());
                return Results.Ok(promotions);
            }
        );

        group.MapGet(
            "/{id}",
            async (int id, [FromServices] IMediator mediator) =>
            {
                var promotion = await mediator.Send(new GetPromotionByIdQuery(id));
                if (promotion == null)
                    return Results.NotFound();
                return Results.Ok(promotion);
            }
        );

        group
            .MapPut(
                "/{id}",
                async (
                    int id,
                    [FromBody] UpdatePromotionCommand command,
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
            )
            .RequireAuthorization("Vendor");

        group
            .MapDelete(
                "/{id}",
                async (int id, [FromServices] IMediator mediator) =>
                {
                    var success = await mediator.Send(new DeletePromotionCommand(id));
                    if (!success)
                        return Results.NotFound();

                    return Results.NoContent();
                }
            )
            .RequireAuthorization("Vendor");
    }
}
