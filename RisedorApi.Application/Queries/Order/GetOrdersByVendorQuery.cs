using MediatR;
using RisedorApi.Application.DTOs;

namespace RisedorApi.Application.Queries.Order;

public record GetOrdersByVendorQuery(int VendorId) : IRequest<List<OrderDto>>;
