using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Models;
using MediatR;

namespace CleanArchitecture.Application.Features.CarFeatures.Queries.GetAllCar;

public sealed record GetAllCarQuery(int PageNumber = 1, int PageSize = 10, string Search = "") : IRequest<PaginationResult<Car>>;

