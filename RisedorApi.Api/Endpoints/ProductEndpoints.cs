using MediatR;
using Microsoft.AspNetCore.Mvc;
using RisedorApi.Application.Commands.Product;
using RisedorApi.Application.Queries.Product;

namespace RisedorApi.Api.Endpoints;

public static class ProductEndpoints
{
    public static void MapProductEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/products").WithTags("Products").RequireAuthorization();

        group
            .MapPost(
                "/",
                async (
                    [FromBody] CreateProductCommand command,
                    [FromServices] IMediator mediator
                ) =>
                {
                    var productId = await mediator.Send(command);
                    return Results.Created($"/api/products/{productId}", productId);
                }
            )
            .RequireAuthorization("Vendor");

        group.MapGet(
            "/",
            async ([FromServices] IMediator mediator) =>
            {
                var products = await mediator.Send(new GetProductsQuery());
                return Results.Ok(products);
            }
        );

        group.MapGet(
            "/{id}",
            async (int id, [FromServices] IMediator mediator) =>
            {
                var product = await mediator.Send(new GetProductByIdQuery(id));
                if (product == null)
                    return Results.NotFound();
                return Results.Ok(product);
            }
        );

        group
            .MapPut(
                "/{id}",
                async (
                    int id,
                    [FromBody] UpdateProductCommand command,
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
                    var success = await mediator.Send(new DeleteProductCommand(id));
                    if (!success)
                        return Results.NotFound();

                    return Results.NoContent();
                }
            )
            .RequireAuthorization("Vendor");
    }
}
