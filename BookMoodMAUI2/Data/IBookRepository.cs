
namespace BookMoodMAUI2.Data
{
    public interface IBookRepository
    {
        Task<List<Book>> LoadAsync(CancellationToken ct = default);
        Task SaveAsync(List<Book> books, CancellationToken ct = default);
    }
}
