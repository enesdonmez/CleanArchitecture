using CleanArchitecture.Application.Features.CarFeatures.Commands.CreateCar;
using CleanArchitecture.Application.Features.CarFeatures.Queries.GetAllCar;
using CleanArchitecture.Domain.Dtos;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Models;
using CleanArchitecture.Infrastructure.Authorization;
using CleanArchitecture.Presentation.Abstraction;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Presentation.Controllers
{
    public class CarsController : ApiController
    {
        public CarsController(IMediator mediator) : base(mediator) { }

        [RoleFilter("Create")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Create(CreateCarCommand request, CancellationToken cancellationToken)
        {
            MessageResponse message = await _mediator.Send(request, cancellationToken);
            return Ok(message);
        }

        [RoleFilter("Admin")]
        [HttpPost("[action]")]
        public async Task<IActionResult> GetAll(GetAllCarQuery request, CancellationToken cancellationToken)
        {
            PaginationResult<Car> response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }
    }
}
