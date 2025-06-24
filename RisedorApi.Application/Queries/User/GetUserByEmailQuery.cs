using MediatR;
using RisedorApi.Domain.Entities;

namespace RisedorApi.Application.Queries.User;

public record GetUserByEmailQuery(string Email) : IRequest<Domain.Entities.User>;
