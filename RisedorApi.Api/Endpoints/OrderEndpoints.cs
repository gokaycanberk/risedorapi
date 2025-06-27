using MediatR;
using Microsoft.AspNetCore.Mvc;
using RisedorApi.Application.Commands.Order;
using RisedorApi.Application.Queries;
using RisedorApi.Application.Queries.Order;
using RisedorApi.Domain.Enums;

namespace RisedorApi.Api.Endpoints;

public static class OrderEndpoints
{
    public static void MapOrderEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/orders").WithTags("Orders").RequireAuthorization();

        group.MapGet(
            "/",
            async ([FromServices] IMediator mediator) =>
            {
                var orders = await mediator.Send(new GetOrdersQuery());
                return Results.Ok(orders);
            }
        );

        group.MapGet(
            "/salesrep/{userId}",
            async (int userId, [FromServices] IMediator mediator) =>
            {
                var orders = await mediator.Send(new GetOrdersBySalesRepQuery(userId));
                return Results.Ok(orders);
            }
        );

        group.MapGet(
            "/supermarket/{userId}",
            async (int userId, [FromServices] IMediator mediator) =>
            {
                var orders = await mediator.Send(new GetOrdersBySupermarketQuery(userId));
                return Results.Ok(orders);
            }
        );

        group.MapGet(
            "/vendor/{vendorId}",
            async (int vendorId, [FromServices] IMediator mediator) =>
            {
                var orders = await mediator.Send(new GetOrdersByVendorQuery(vendorId));
                return Results.Ok(orders);
            }
        );

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

        group.MapPost(
            "/",
            async ([FromBody] CreateOrderRequest request, [FromServices] IMediator mediator) =>
            {
                var command = new CreateOrderCommand
                {
                    SalesRepUserId = request.SalesRepUserId,
                    SupermarketUserId = request.SupermarketUserId,
                    BuyerInfo = request.BuyerInfo,
                    Items = request.Items
                        .Select(
                            i =>
                                new CreateOrderItemDto
                                {
                                    ProductItemCode = i.ProductItemCode,
                                    Quantity = i.Quantity,
                                    VendorId = i.VendorId
                                }
                        )
                        .ToList()
                };

                var orderId = await mediator.Send(command);
                return Results.Created($"/api/orders/{orderId}", orderId);
            }
        );

        group.MapPut(
            "/{id}",
            async (
                int id,
                [FromBody] UpdateOrderRequest request,
                [FromServices] IMediator mediator
            ) =>
            {
                var command = new UpdateOrderCommand
                {
                    Id = id,
                    BuyerInfo = request.BuyerInfo,
                    Items = request.Items
                        .Select(
                            i =>
                                new UpdateOrderItemDto
                                {
                                    ProductItemCode = i.ProductItemCode,
                                    Quantity = i.Quantity,
                                    VendorId = i.VendorId
                                }
                        )
                        .ToList()
                };

                var result = await mediator.Send(command);
                if (!result)
                    return Results.NotFound();

                return Results.NoContent();
            }
        );

        group.MapPut(
            "/{id}/status",
            async (
                int id,
                [FromBody] UpdateOrderStatusRequest request,
                [FromServices] IMediator mediator
            ) =>
            {
                var command = new UpdateOrderStatusCommand(id, request.Status);
                var result = await mediator.Send(command);
                if (!result)
                    return Results.NotFound();

                return Results.NoContent();
            }
        );
    }
}

public class CreateOrderRequest
{
    public int SalesRepUserId { get; set; }
    public int SupermarketUserId { get; set; }
    public string BuyerInfo { get; set; } = string.Empty;
    public List<CreateOrderItemRequest> Items { get; set; } = new();
}

public class CreateOrderItemRequest
{
    public string ProductItemCode { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public int VendorId { get; set; }
}

public class UpdateOrderRequest
{
    public string BuyerInfo { get; set; } = string.Empty;
    public List<UpdateOrderItemRequest> Items { get; set; } = new();
}

public class UpdateOrderItemRequest
{
    public string ProductItemCode { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public int VendorId { get; set; }
}

public record UpdateOrderStatusRequest(OrderStatus Status);
