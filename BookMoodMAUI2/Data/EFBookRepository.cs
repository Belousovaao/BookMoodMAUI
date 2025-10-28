using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BookMoodMAUI2.Data
{
    public class EFBookRepository : IBookRepository
    {
        private readonly BookDbContext _context;
        public EFBookRepository(BookDbContext context)
        {
            _context = context;
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "books.db");
            Debug.WriteLine($"Database location: {dbPath}");

        }

        public async Task<List<Book>> LoadAsync(CancellationToken ct = default)
        {
            return await _context.Books.ToListAsync(ct);
        }

        public async Task SaveAsync(List<Book> books, CancellationToken ct = default)
        {
            List<Book> existingBooks = await _context.Books.ToListAsync(ct);
            List<Guid> existingIds = existingBooks.Select(b => b.Id).ToList();

            foreach (Book book in books)
            {
                if (book.Id == Guid.Empty || !existingIds.Contains(book.Id))
                {
                    _context.Books.Add(book);
                }
                else
                {
                    _context.Books.Update(book);
                }
            }

            List<Book> booksToRemove = existingBooks.Where(b => !books.Any(nb => nb.Id == b.Id)).ToList();

            _context.Books.RemoveRange(booksToRemove);

            await _context.SaveChangesAsync();
        }
    }
}
