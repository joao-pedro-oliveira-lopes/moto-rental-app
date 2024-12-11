using Moq;
using Microsoft.AspNetCore.Mvc;
using MotoRentalApp.Application.Interfaces.Services;
using MotoRentalApp.Api.Controllers;
using MotoRentalApp.Application.Common;

namespace MotoRentalApp.Tests.Controllers
{
    public class DeliveryPersonControllerTests
    {
        private readonly Mock<IDeliveryPersonService> _mockDeliveryPersonService;
        private readonly DeliveryPersonController _controller;

        public DeliveryPersonControllerTests()
        {
            _mockDeliveryPersonService = new Mock<IDeliveryPersonService>();
            _controller = new DeliveryPersonController(_mockDeliveryPersonService.Object);
        }

        [Fact]
        public async Task CheckCnpj_ReturnsBadRequest_WhenCnpjIsInvalid()
        {
            // Arrange
            string invalidCnpj = "123456789";
            var result = Result.Failure("Invalid CNPJ");
            _mockDeliveryPersonService.Setup(service => service.CheckCnpjAsync(invalidCnpj)).ReturnsAsync(result);

            // Act
            var actionResult = await _controller.CheckCnpj(invalidCnpj);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult);
            Assert.Equal("Invalid CNPJ", badRequestResult.Value);
        }
    }
}