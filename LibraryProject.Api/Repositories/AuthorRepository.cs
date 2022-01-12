using LibraryProject.Api.Database;
using LibraryProject.Api.Database.Entites;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryProject.Api.Repositories
{
    public interface IAuthorRepository
    {
        Task<List<Author>> SelectAllAuthors();
        Task<Author> SelectAuthorById(int authorId);
        Task<Author> InsertNewAuthor(Author author);
        Task<Author> UpdateExistingAuthor(int authorId, Author author);
        Task<Author> DeleteAuthorById(int authorId);
    }

    public class AuthorRepository : IAuthorRepository
    {
        private readonly LibraryProjectContext _context;

        public AuthorRepository(LibraryProjectContext context)
        {
            _context = context;
        }

        public async Task<List<Author>> SelectAllAuthors()
        {
            return await _context.Author.ToListAsync();
        }

        public async Task<Author> SelectAuthorById(int authorId)
        {
            return await _context.Author
                .FirstOrDefaultAsync(author => author.Id == authorId);
        }

        public async Task<Author> InsertNewAuthor(Author author)
        {
           _context.Author.Add(author);
            await _context.SaveChangesAsync();
            return author;
        }

        public async Task<Author> UpdateExistingAuthor(int authorId, Author author)
        {
            Author updateAuthor = await _context.Author.FirstOrDefaultAsync(author => author.Id == authorId);
            if(updateAuthor != null)
            {
                updateAuthor.FirstName = author.FirstName;
                updateAuthor.LastName = author.LastName;
                updateAuthor.MiddleName = author.MiddleName;
                updateAuthor.BirthYear = author.BirthYear;
                updateAuthor.YearOfDeath = author.YearOfDeath;

                await _context.SaveChangesAsync();   
            }
            return updateAuthor;
        }

        public async Task<Author> DeleteAuthorById(int authorId)
        {
            Author deleteAuthor = await _context.Author.FirstOrDefaultAsync(author => author.Id == authorId);
            if(deleteAuthor != null)
            {
                _context.Author.Remove(deleteAuthor);
                await _context.SaveChangesAsync();
            }
            return deleteAuthor;
        }
    }
}
