using CommunityToolkit.Mvvm.ComponentModel;

namespace BookMoodMAUI2.ViewModel
{
    public class BookItemViewModel : ObservableObject
    {
        public Book Book { get; }
        private bool _isSelected;

        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }

        public BookItemViewModel(Book book)
        {
            Book = book;
        }

        public string Title
        {
            get => Book.Title;
            set
            {
                if (Book.Title != value)
                {
                    Book.Title = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Author
        {
            get => Book.Author;
            set
            {
                if (Book.Author != value)
                {
                    Book.Author = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Description
        {
            get => Book.Description;
            set
            {
                if (Book.Description != value)
                {
                    Book.Description = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Notes
        {
            get => Book.Notes;
            set
            {
                if (Book.Notes != value)
                {
                    Book.Notes = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Mood
        {
            get => Book.Mood;
            set
            {
                if (Book.Mood != value)
                {
                    Book.Mood = value;
                    OnPropertyChanged();
                }
            }
        }

        public Color CoverColor
        {
            get => Book.CoverColor;
            set
            {
                if (Book.CoverColor != value)
                {
                    Book.CoverColor = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime DateAdded
        {
            get => Book.DateAdded;
            set
            {
                if (Book.DateAdded != value)
                {
                    Book.DateAdded = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
