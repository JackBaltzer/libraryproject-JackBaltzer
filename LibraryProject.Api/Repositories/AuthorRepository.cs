using LibraryProject.Api.Database;
using LibraryProject.Api.Database.Entites;
using System.Collections.Generic;

namespace LibraryProject.Api.Repositories
{
    public interface IAuthorRepository
    {
        List<Author> SelectAllAuthors();
    }

    public class AuthorRepository : IAuthorRepository
    {
        public List<Author> SelectAllAuthors()
        {
            throw new System.NotImplementedException();
        }
    }
}
