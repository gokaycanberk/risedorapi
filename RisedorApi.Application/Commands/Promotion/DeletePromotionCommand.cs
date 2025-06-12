using MediatR;

namespace RisedorApi.Application.Commands.Promotion;

public record DeletePromotionCommand(int Id) : IRequest<bool>;
