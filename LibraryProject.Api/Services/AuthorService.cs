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
        Task<AuthorResponse> GetAuthorById(int authorId);
        Task<AuthorResponse> CreateAuthor(AuthorRequest newAuthor);
        Task<AuthorResponse> UpdateAuthor(int authorId, AuthorRequest updateAuthor);
        Task<AuthorResponse> DeleteAuthor(int authorId);
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

            return authors.Select(author => MapAuthorToAuthorResponse(author)).ToList();
        }

        public async Task<AuthorResponse> GetAuthorById(int authorId)
        {
            Author author = await _authorRepository.SelectAuthorById(authorId);

            if (author != null)
            {
                return MapAuthorToAuthorResponse(author);
            }

            return null;
        }

        public async Task<AuthorResponse> CreateAuthor(AuthorRequest newAuthor)
        {
            Author author = MapAuthorRequstToAuthor(newAuthor);

            Author insertedAuthor = await _authorRepository.InsertNewAuthor(author);

            if(insertedAuthor != null)
            {
                return MapAuthorToAuthorResponse(insertedAuthor);
            }

            return null;
        }

        public async Task<AuthorResponse> UpdateAuthor(int authorId, AuthorRequest updateAuthor)
        {
            Author author = MapAuthorRequstToAuthor(updateAuthor);

            Author updatedAuthor = await _authorRepository.UpdateExistingAuthor(authorId, author);

            if (updatedAuthor != null)
            {
                return MapAuthorToAuthorResponse(updatedAuthor);
            }

            return null;
        }

        public async Task<AuthorResponse> DeleteAuthor(int authorId)
        {
            Author deletedAuthor = await _authorRepository.DeleteAuthorById(authorId);

            if (deletedAuthor != null)
            {
                return MapAuthorToAuthorResponse(deletedAuthor);
            }

            return null;
        }

        public Author MapAuthorRequstToAuthor(AuthorRequest authorRequest)
        {
            return new Author
            {
                FirstName = authorRequest.FirstName,
                LastName = authorRequest.LastName,
                MiddleName = authorRequest.MiddleName,
                BirthYear = authorRequest.BirthYear,
                YearOfDeath = authorRequest.YearOfDeath
            };
        }

        public AuthorResponse MapAuthorToAuthorResponse(Author author)
        {
            return new AuthorResponse
            {
                Id=author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName,
                MiddleName = author.MiddleName,
                BirthYear = author.BirthYear,
                YearOfDeath = author.YearOfDeath
            };
        }
    }
}
