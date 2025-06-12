using MediatR;

namespace RisedorApi.Application.Commands.Promotion;

public record UpdatePromotionCommand(
    int Id,
    string Description,
    decimal DiscountPercentage,
    DateTime ValidFrom,
    DateTime ValidTo
) : IRequest<bool>;
