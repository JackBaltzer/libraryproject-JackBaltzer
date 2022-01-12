using LibraryProject.Api.Controllers;
using LibraryProject.Api.DTOs;
using LibraryProject.Api.Services;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace LibraryProject.Tests.Controllers
{
    public class AuthorControllerTests
    {
        private readonly AuthorController _authorController;
        private readonly Mock<IAuthorService> _mockAuthorService = new();

        public AuthorControllerTests()
        {
            _authorController = new(_mockAuthorService.Object);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode200_WhenAuthorsExists()
        {
            // Arrange
            List<AuthorResponse> authors = new();

            authors.Add(new()
            {
                Id = 1,
                FirstName = "George",
                LastName = "Martin",
                MiddleName = "R.R.",
                BirthYear = 1948
            });

            authors.Add(new()
            {
                Id = 2,
                FirstName = "Lewis",
                LastName = "Carol",
                MiddleName = "",
                BirthYear = 1832,
                YearOfDeath = 1898
            });

            _mockAuthorService
                .Setup(x => x.GetAllAuthors())
                .ReturnsAsync(authors);

            // Act
            var result = await _authorController.GetAll();

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode204_WhenNoAuthorsExists()
        {
            // Arrange
            List<AuthorResponse> authors = new();

            _mockAuthorService
                .Setup(x => x.GetAllAuthors())
                .ReturnsAsync(authors);

            // Act
            var result = await _authorController.GetAll();

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(204, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode500_WhenNullIsReturnedFromService()
        {
            // Arrange
            _mockAuthorService
                .Setup(x => x.GetAllAuthors())
                .ReturnsAsync(() => null);

            // Act
            var result = await _authorController.GetAll();

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            _mockAuthorService
                .Setup(x => x.GetAllAuthors())
                .ReturnsAsync(() => throw new System.Exception("This is an exception"));

            // Act
            var result = await _authorController.GetAll();

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }
    }
}
