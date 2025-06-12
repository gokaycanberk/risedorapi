using MediatR;
using RisedorApi.Domain.Entities;

namespace RisedorApi.Application.Queries.Promotion;

public record GetPromotionsQuery : IRequest<IEnumerable<Domain.Entities.Promotion>>;
