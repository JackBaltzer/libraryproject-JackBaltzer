using LibraryProject.Api.DTOs;
using System.Collections.Generic;

namespace LibraryProject.Api.Services
{
    public interface IAuthorService
    {
        List<AuthorResponse> GetAllAuthors();
    }

    public class AuthorService : IAuthorService
    {
        public List<AuthorResponse> GetAllAuthors()
        {
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

            return authors;
        }
    }
}
