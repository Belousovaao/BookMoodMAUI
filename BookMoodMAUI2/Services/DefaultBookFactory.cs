namespace BookMoodMAUI2.Services
{
    public class DefaultBookFactory : IBookFactory
    {
        public Book CreateNew()
        {
            return new Book
            {
                Id = Guid.NewGuid(),
                DateAdded = DateTime.Now,
                Title = "New Book"
            };
        }
    }
}