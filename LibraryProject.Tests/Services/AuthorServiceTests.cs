using LibraryProject.Api.Database.Entites;
using LibraryProject.Api.DTOs;
using LibraryProject.Api.Repositories;
using LibraryProject.Api.Services;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace LibraryProject.Tests.Services
{
    public class AuthorServiceTests
    {
        private readonly AuthorService _authorService;
        private readonly Mock<IAuthorRepository> _mockAuthorRepository = new();

        public AuthorServiceTests()
        {
            _authorService = new AuthorService(_mockAuthorRepository.Object);
        }

        [Fact]
        public async void GetAllAuthors_ShouldReturnListOfAuthorResponses_WhenAuthorsExists()
        {
            // Arrange
            List<Author> Authors = new();

            Authors.Add(new()
            {
                Id = 1,
                FirstName = "George",
                LastName = "Martin",
                MiddleName = "R.R.",
                BirthYear = 1948
            });

            Authors.Add(new()
            {
                Id = 2,
                FirstName = "Lewis",
                LastName = "Carol",
                MiddleName = "",
                BirthYear = 1832,
                YearOfDeath = 1898
            });

            _mockAuthorRepository
                .Setup(x => x.SelectAllAuthors())
                .ReturnsAsync(Authors);

            // Act
            var result = await _authorService.GetAllAuthors();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.IsType<List<AuthorResponse>>(result);
        }

        [Fact]
        public async void GetAllAuthors_ShouldReturnEmptyListOfAuthorResponses_WhenNoAuthorsExists()
        {
            // Arrange
            List<Author> Authors = new();

            _mockAuthorRepository
                .Setup(x => x.SelectAllAuthors())
                .ReturnsAsync(Authors);

            // Act
            var result = await _authorService.GetAllAuthors();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            Assert.IsType<List<AuthorResponse>>(result);
        }

        [Fact]
        public async void GetAuthorById_ShouldReturnAuthorResponse_WhenAuthorExists()
        {
            // Arrange

            int authorId = 1;

            Author author = new()
            {
                Id = authorId,
                FirstName = "George",
                LastName = "Martin",
                MiddleName = "R.R.",
                BirthYear = 1948
            };

            _mockAuthorRepository
                .Setup(x => x.SelectAuthorById(It.IsAny<int>()))
                .ReturnsAsync(author);

            // Act
            var result = await _authorService.GetAuthorById(authorId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<AuthorResponse>(result);
            Assert.Equal(author.Id, result.Id);
            Assert.Equal(author.FirstName, result.FirstName);
            Assert.Equal(author.LastName, result.LastName);
            Assert.Equal(author.MiddleName, result.MiddleName);
        }


        [Fact]
        public async void GetAuthorById_ShouldReturnNull_WhenAuthorDoesNotExist()
        {
            // Arrange
            int authorId = 1;

            _mockAuthorRepository
                .Setup(x => x.SelectAuthorById(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _authorService.GetAuthorById(authorId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void CreateAuthor_ShouldReturnAuthorResponse_WhenCreateIsSuccess()
        {
            // Arrange
            AuthorRequest newAuthor = new()
            {
                FirstName = "George",
                LastName = "Martin",
                MiddleName = "R.R.",
                BirthYear = 1948
            };

            int authorId = 1;

            Author createdAuthor = new()
            {
                Id = authorId,
                FirstName = "George",
                LastName = "Martin",
                MiddleName = "R.R.",
                BirthYear = 1948
            };

            _mockAuthorRepository
                .Setup(x => x.InsertNewAuthor(It.IsAny<Author>()))
                .ReturnsAsync(createdAuthor);

            // Act
            var result = await _authorService.CreateAuthor(newAuthor);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<AuthorResponse>(result);
            Assert.Equal(authorId, result.Id);
            Assert.Equal(newAuthor.FirstName, result.FirstName);
            Assert.Equal(newAuthor.LastName, result.LastName);
            Assert.Equal(newAuthor.MiddleName, result.MiddleName);
        }

        [Fact]
        public async void CreateAuthor_ShouldReturnNull_WhenRepositoryReturnsNull()
        {
            // Arrange
            AuthorRequest newAuthor = new()
            {
                FirstName = "George",
                LastName = "Martin",
                MiddleName = "R.R.",
                BirthYear = 1948
            };

            _mockAuthorRepository
                .Setup(x => x.InsertNewAuthor(It.IsAny<Author>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _authorService.CreateAuthor(newAuthor);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void UpdateAuthor_ShouldReturnAuthorResponse_WhenUpdateIsSuccess()
        {
            // NOTICE, we do not test if anything actually changed on the DB,
            // we only test that the returned values match the submitted values
            // Arrange
            AuthorRequest authorRequest = new()
            {
                FirstName = "George",
                LastName = "Martin",
                MiddleName = "R.R.",
                BirthYear = 1948
            };

            int authorId = 1;

            Author author = new()
            {
                Id = authorId,
                FirstName = "George",
                LastName = "Martin",
                MiddleName = "R.R.",
                BirthYear = 1948
            };

            _mockAuthorRepository
                .Setup(x => x.UpdateExistingAuthor(It.IsAny<int>(), It.IsAny<Author>()))
                .ReturnsAsync(author);

            // Act
            var result = await _authorService.UpdateAuthor(authorId, authorRequest);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<AuthorResponse>(result);
            Assert.Equal(authorId, result.Id);
            Assert.Equal(authorRequest.FirstName, result.FirstName);
            Assert.Equal(authorRequest.LastName, result.LastName);
            Assert.Equal(authorRequest.MiddleName, result.MiddleName);
        }


        [Fact]
        public async void UpdateAuthor_ShouldReturnNull_WhenAuthorDoesNotExist()
        {
            // Arrange
            AuthorRequest authorRequest = new()
            {
                FirstName = "George",
                LastName = "Martin",
                MiddleName = "R.R.",
                BirthYear = 1948
            };

            int authorId = 1;

            _mockAuthorRepository
                .Setup(x => x.UpdateExistingAuthor(It.IsAny<int>(), It.IsAny<Author>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _authorService.UpdateAuthor(authorId, authorRequest);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void DeleteAuthor_ShouldReturnAuthorResponse_WhenDeleteIsSuccess()
        {
            // Arrange
            int authorId = 1;

            Author deletedAuthor = new()
            {
                Id = 1,
                FirstName = "George",
                LastName = "Martin",
                MiddleName = "R.R.",
                BirthYear = 1948
            };

            _mockAuthorRepository
                .Setup(x => x.DeleteAuthorById(It.IsAny<int>()))
                .ReturnsAsync(deletedAuthor);

            // Act
            var result = await _authorService.DeleteAuthor(authorId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<AuthorResponse>(result);
            Assert.Equal(authorId, result.Id);
        }

        [Fact]
        public async void DeleteAuthor_ShouldReturnNull_WhenAuthorDoesNotExist()
        {
            // Arrange
            int authorId = 1;

            _mockAuthorRepository
                .Setup(x => x.DeleteAuthorById(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _authorService.DeleteAuthor(authorId);

            // Assert
            Assert.Null(result);
        }

    }
}
