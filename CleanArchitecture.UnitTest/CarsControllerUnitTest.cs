using CleanArchitecture.Application.Features.CarFeatures.Commands.CreateCar;
using CleanArchitecture.Domain.Dtos;
using CleanArchitecture.Presentation.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CleanArchitecture.UnitTest;

public class CarsControllerUnitTest
{
    [Fact]
    public async void Create_ReturnsOkResult_WhenRequestIsValid()
    {
        //arrange

        var mediatrMock = new Mock<IMediator>();
        CreateCarCommand createCarCommand = new("BMW", "ix1", 500);
        MessageResponse messageResponse = new("Araç kaydý baþarýlý");
        CancellationToken cancellationToken = new();
        mediatrMock.Setup(m => m.Send(createCarCommand, cancellationToken)).ReturnsAsync(messageResponse);
        CarsController carsController = new(mediatrMock.Object);


        //act
        
        var result = await carsController.Create(createCarCommand, cancellationToken);

        //assert

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<MessageResponse>(okResult.Value);

        Assert.Equal(messageResponse, returnValue);
        mediatrMock.Verify(m => m.Send(createCarCommand, cancellationToken), Times.Once);
    }
}