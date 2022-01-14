using LibraryProject.Api.Database;
using LibraryProject.Api.Database.Entites;
using LibraryProject.Api.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace LibraryProject.Tests.Repositories
{
    public class AuthorRepositoryTests
    {
        private readonly DbContextOptions<LibraryProjectContext> _options;
        private readonly LibraryProjectContext _context;
        private readonly AuthorRepository _authorRepository;

        public AuthorRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<LibraryProjectContext>()
                .UseInMemoryDatabase(databaseName: "LibraryProjectAuthors")
                .Options;

            _context = new(_options);

            _authorRepository = new(_context);
        }

        [Fact]
        public async void SelectAllAuthors_ShouldReturnListOfAuthors_WhenAuthorsExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            _context.Author.Add(new()
            {
                Id = 1,
                FirstName = "George",
                LastName = "Martin",
                MiddleName = "R.R.",
                BirthYear = 1948
            });

            _context.Author.Add(new()
            {
                Id = 2,
                FirstName = "Lewis",
                LastName = "Carol",
                MiddleName = "",
                BirthYear = 1832,
                YearOfDeath = 1898
            });

            await _context.SaveChangesAsync();

            // Act
            var result = await _authorRepository.SelectAllAuthors();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Author>>(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async void SelectAllAuthors_ShouldReturnEmptyListOfAuthors_WhenNoAuthorsExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            // Act
            var result = await _authorRepository.SelectAllAuthors();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Author>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public async void SelectAuthorById_ShouldReturnAuthor_WhenAuthorExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            int authorId = 1;

            _context.Author.Add(new()
            {
                Id = authorId,
                FirstName = "George",
                LastName = "Martin",
                MiddleName = "R.R.",
                BirthYear = 1948
            });

            await _context.SaveChangesAsync();

            // Act
            var result = await _authorRepository.SelectAuthorById(authorId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Author>(result);
            Assert.Equal(authorId, result.Id);
        }

        [Fact]
        public async void SelectAuthorById_ShouldReturnNull_WhenAuthorDoesNotExist()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            // Act
            var result = await _authorRepository.SelectAuthorById(1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void InsertNewAuthor_ShouldAddnewIdToAuthor_WhenSavingToDatabase()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            int expectedNewId = 1;

            Author author = new()
            {
                FirstName = "George",
                LastName = "Martin",
                MiddleName = "R.R.",
                BirthYear = 1948
            };

            // Act
            var result = await _authorRepository.InsertNewAuthor(author);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Author>(result);
            Assert.Equal(expectedNewId, result.Id);
        }

        [Fact]
        public async void InsertNewAuthor_ShouldFailToAddNewAuthor_WhenAuthorIdAlreadyExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            Author author = new()
            {
                Id = 1,
                FirstName = "George",
                LastName = "Martin",
                MiddleName = "R.R.",
                BirthYear = 1948
            };

            _context.Author.Add(author);
            await _context.SaveChangesAsync();

            // Act
            async Task action() => await _authorRepository.InsertNewAuthor(author);

            // Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(action);
                            // be careful, and make sure to type everything correctly! 
            Assert.Contains("An item with the same key has already been added.", ex.Message);
        }

        [Fact]
        public async void UpdateExistingAuthor_ShouldChangeValuesOnAuthor_WhenAuthorExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            int authorId = 1;

            Author newAuthor = new()
            {
                Id = authorId,
                FirstName = "George",
                LastName = "Martin",
                MiddleName = "R.R.",
                BirthYear = 1948
            };

            _context.Author.Add(newAuthor);
            await _context.SaveChangesAsync();

            Author updateAuthor = new()
            {
                Id = authorId,
                FirstName = "updated George",
                LastName = "updated Martin",
                MiddleName = "updated R.R.",
                BirthYear = 1948
            };

            // Act
            var result = await _authorRepository.UpdateExistingAuthor(authorId, updateAuthor);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Author>(result);
            Assert.Equal(authorId, result.Id);
            Assert.Equal(updateAuthor.FirstName, result.FirstName);
            Assert.Equal(updateAuthor.LastName, result.LastName);
            Assert.Equal(updateAuthor.MiddleName, result.MiddleName);
        }

        [Fact]
        public async void UpdateExistingAuthor_ShouldReturnNull_WhenAuthorDoesNotExist()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            int authorId = 1;

            Author updateAuthor = new()
            {
                Id = authorId,
                FirstName = "George",
                LastName = "Martin",
                MiddleName = "R.R.",
                BirthYear = 1948
            };

            // Act
            var result = await _authorRepository.UpdateExistingAuthor(authorId, updateAuthor);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void DeleteAuthorById_ShouldReturnDeletedAuthor_WhenAuthorIsDeleted()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            int authorId = 1;

            Author newAuthor = new()
            {
                Id = authorId,
                FirstName = "George",
                LastName = "Martin",
                MiddleName = "R.R.",
                BirthYear = 1948
            };

            _context.Author.Add(newAuthor);
            await _context.SaveChangesAsync();

            // Act
            var result = await _authorRepository.DeleteAuthorById(authorId);
            var author = await _authorRepository.SelectAuthorById(authorId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Author>(result);
            Assert.Equal(authorId, result.Id);
            Assert.Null(author);
        }

        [Fact]
        public async void DeleteAuthorById_ShouldReturnNull_WhenAuthorDoesNotExist()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            // Act
            var result = await _authorRepository.DeleteAuthorById(1);

            // Assert
            Assert.Null(result);
        }
    }
}
