using MediatR;
using RisedorApi.Domain.Entities;

namespace RisedorApi.Application.Queries.User;

public record GetUsersQuery : IRequest<IEnumerable<Domain.Entities.User>>;
