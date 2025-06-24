using MediatR;
using Microsoft.AspNetCore.Mvc;
using RisedorApi.Application.Commands;
using RisedorApi.Application.Queries;
using RisedorApi.Domain.Entities;
using RisedorApi.Domain.Enums;

namespace RisedorApi.Api.Endpoints;

public static class OrderEndpoints
{
    public static void MapOrderEndpoints(this WebApplication app)
    {
        app.MapGet(
            "/api/orders",
            async ([FromServices] IMediator mediator) =>
            {
                var orders = await mediator.Send(new GetOrdersQuery());
                return Results.Ok(orders);
            }
        );

        app.MapGet(
            "/api/orders/{id}",
            async (int id, [FromServices] IMediator mediator) =>
            {
                var order = await mediator.Send(new GetOrderByIdQuery(id));
                if (order == null)
                    return Results.NotFound();

                return Results.Ok(order);
            }
        );

        app.MapPost(
            "/api/orders",
            async ([FromBody] CreateOrderRequest request, [FromServices] IMediator mediator) =>
            {
                var items = request.Items
                    .Select(i => new OrderItemDto(i.ProductId, i.Quantity))
                    .ToList();
                var command = new CreateOrderCommand(
                    request.SupermarketId,
                    request.VendorId,
                    items
                );
                var orderId = await mediator.Send(command);
                return Results.Created($"/api/orders/{orderId}", orderId);
            }
        );

        app.MapPut(
            "/api/orders/{id}/status",
            async (
                int id,
                [FromBody] UpdateOrderStatusRequest request,
                [FromServices] IMediator mediator
            ) =>
            {
                var command = new UpdateOrderCommand(id, request.Status);
                var success = await mediator.Send(command);
                if (!success)
                    return Results.NotFound();

                return Results.NoContent();
            }
        );

        app.MapDelete(
            "/api/orders/{id}",
            async (int id, [FromServices] IMediator mediator) =>
            {
                var success = await mediator.Send(new DeleteOrderCommand(id));
                if (!success)
                    return Results.NotFound();

                return Results.NoContent();
            }
        );
    }
}

public record CreateOrderRequest(int SupermarketId, int VendorId, List<OrderItemRequest> Items);

public record OrderItemRequest(int ProductId, int Quantity);

public record UpdateOrderStatusRequest(OrderStatus Status);
