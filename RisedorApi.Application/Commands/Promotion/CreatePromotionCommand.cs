using MediatR;

namespace RisedorApi.Application.Commands.Promotion;

public record CreatePromotionCommand(
    int VendorId,
    string Description,
    decimal DiscountPercentage,
    DateTime ValidFrom,
    DateTime ValidTo
) : IRequest<int>;
