using MediatR;
using RisedorApi.Domain.Entities;

namespace RisedorApi.Application.Queries.Promotion;

public record GetPromotionByIdQuery(int Id) : IRequest<Domain.Entities.Promotion?>;
