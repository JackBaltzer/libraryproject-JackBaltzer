using LibraryProject.Api.Database.Entites;
using LibraryProject.Api.DTOs;
using LibraryProject.Api.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryProject.Api.Services
{
    public interface IAuthorService
    {
        Task<List<AuthorResponse>> GetAllAuthors();
    }

    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<List<AuthorResponse>> GetAllAuthors()
        {
            List<Author> authors = await _authorRepository.SelectAllAuthors();

            return authors.Select(author => new AuthorResponse
            {
                Id = author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName,
                MiddleName = author.MiddleName,
                BirthYear = author.BirthYear,
                YearOfDeath = author.YearOfDeath
            }).ToList();
        }
    }
}
