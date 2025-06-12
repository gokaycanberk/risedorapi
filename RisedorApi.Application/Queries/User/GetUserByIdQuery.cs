using MediatR;
using RisedorApi.Domain.Entities;

namespace RisedorApi.Application.Queries.User;

public record GetUserByIdQuery(int Id) : IRequest<Domain.Entities.User?>;
