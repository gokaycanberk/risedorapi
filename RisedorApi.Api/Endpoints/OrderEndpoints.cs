using MediatR;
using Microsoft.AspNetCore.Mvc;
using RisedorApi.Application.Commands;
using RisedorApi.Application.Queries;

namespace RisedorApi.Api.Endpoints;

public static class OrderEndpoints
{
    public static void MapOrderEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/orders").WithTags("Orders").RequireAuthorization();

        // Create Order
        group
            .MapPost(
                "/",
                async ([FromBody] CreateOrderCommand command, [FromServices] IMediator mediator) =>
                {
                    var orderId = await mediator.Send(command);
                    return Results.Created($"/api/orders/{orderId}", orderId);
                }
            )
            .RequireAuthorization("Supermarket");

        // Get All Orders
        group.MapGet(
            "/",
            async ([FromServices] IMediator mediator) =>
            {
                var orders = await mediator.Send(new GetOrdersQuery());
                return Results.Ok(orders);
            }
        );

        // Get Order by Id
        group.MapGet(
            "/{id}",
            async (int id, [FromServices] IMediator mediator) =>
            {
                var order = await mediator.Send(new GetOrderByIdQuery(id));
                if (order == null)
                    return Results.NotFound();
                return Results.Ok(order);
            }
        );

        // Update Order
        group
            .MapPut(
                "/{id}",
                async (
                    int id,
                    [FromBody] UpdateOrderCommand command,
                    [FromServices] IMediator mediator
                ) =>
                {
                    if (id != command.OrderId)
                        return Results.BadRequest();

                    var success = await mediator.Send(command);
                    if (!success)
                        return Results.NotFound();

                    return Results.NoContent();
                }
            )
            .RequireAuthorization("Vendor");

        // Delete Order
        group
            .MapDelete(
                "/{id}",
                async (int id, [FromServices] IMediator mediator) =>
                {
                    var success = await mediator.Send(new DeleteOrderCommand(id));
                    if (!success)
                        return Results.NotFound();

                    return Results.NoContent();
                }
            )
            .RequireAuthorization("Staff");
    }
}
