using BookMoodMAUI2.Helpers;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace BookMoodMAUI2.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly IBookRepository _bookRepository;
        private readonly IBookFactory _bookFactory;

        private BookItemViewModel _selectedBook;
        private bool _isExpanded;
        public BookItemViewModel SelectedBook
        {
            get => _selectedBook;
            set
            {
                if (_selectedBook != null)
                    _selectedBook.IsSelected = false;   
                SetProperty(ref _selectedBook, value);
                if (_selectedBook != null)
                    _selectedBook.IsSelected = true;
            }
        }

        public bool IsExpanded
        {
            get => _isExpanded;
            set
            {
                SetProperty(ref _isExpanded, value);
            }

        }

        public ObservableCollection<BookItemViewModel> Books { get; set; } = new();
        public AsyncRelayCommand LoadCommand { get; }
        public AsyncRelayCommand SaveCommand { get; }
        public RelayCommand AddCommand { get; }
        public RelayCommand DeleteCommand { get; }
        public RelayCommand ToggleExpandCommand { get; }

        public MainViewModel(IBookRepository bookRepository, IBookFactory bookFactory)
        {
            _bookRepository = bookRepository;
            _bookFactory = bookFactory;
            LoadCommand = new AsyncRelayCommand(LoadBooksAsync);
            SaveCommand = new AsyncRelayCommand(SaveBooksAsync);
            AddCommand = new RelayCommand(AddBook);
            DeleteCommand = new RelayCommand(DeleteBook);
            ToggleExpandCommand = new RelayCommand(Expand);

            _ = LoadBooksAsync();
        }

        private async Task LoadBooksAsync()
        {
            List<Book> books = await _bookRepository.LoadAsync();
            Books.Clear();
            Color[] colors = BookColorsProvider.Colors;
            Random rand = new Random();
            foreach (Book b in books) 
            {
                b.CoverColor = colors[rand.Next(colors.Length)];
                Debug.WriteLine($"{b.Title}: {b.CoverColor}");
                Books.Add(new BookItemViewModel(b)); 
            }
        }

        private async Task SaveBooksAsync()
        {
            List<Book> booksToSave = Books.Select(bvm => bvm.Book).ToList();
            await _bookRepository.SaveAsync(booksToSave);
        }

        private void AddBook()
        {
            Book newBook = _bookFactory.CreateNew();
            Color[] colors = BookColorsProvider.Colors;
            newBook.CoverColor = colors[new Random().Next(colors.Length)];
            var itemVm = new BookItemViewModel(newBook);
            Books.Add(itemVm);
            SelectedBook = itemVm;
        }

        private void DeleteBook()
        {
            if (SelectedBook != null)
            {
                int currentIndex = Books.IndexOf(SelectedBook);
                Books.Remove(SelectedBook);

                if (Books.Count == 0)
                {
                    SelectedBook = null;
                    return;
                }

                else if (currentIndex >= Books.Count)
                    SelectedBook = Books[currentIndex - 1];

                else
                {
                    SelectedBook = Books[currentIndex];
                }
            }
        }

        private void Expand()
        {
            IsExpanded = !IsExpanded;
        }

    }
}
