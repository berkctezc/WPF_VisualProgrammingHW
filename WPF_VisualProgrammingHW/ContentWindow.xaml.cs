using System;
using System.Windows;

namespace WPF_VisualProgrammingHW
{
    /// <summary>
    /// Interaction logic for ContentWindow.xaml
    /// </summary>
    public partial class ContentWindow : Window
    {
        public ContentWindow()
        {
            InitializeComponent();
        }

        private Authors _authorsWindow;
        private Books _booksWindow;
        private Categories _categoriesWindow;
        private Publishers _publishersWindow;

        private void Authors_Click(object sender, RoutedEventArgs e)
        {
            _authorsWindow = new Authors();
            MyFrame.Content = null;
            MyFrame.Content = _authorsWindow;
        }

        private void Books_Click(object sender, RoutedEventArgs e)
        {
            _booksWindow = new Books();
            MyFrame.Content = null;
            MyFrame.Content = _booksWindow;
        }

        private void Categories_Click(object sender, RoutedEventArgs e)
        {
            _categoriesWindow = new Categories();
            MyFrame.Content = null;
            MyFrame.Content = _categoriesWindow;
        }

        private void Publishers_Click(object sender, RoutedEventArgs e)
        {
            _publishersWindow = new Publishers();
            MyFrame.Content = null;
            MyFrame.Content = _publishersWindow;
        }

        private void Clear_Click(object sender, RoutedEventArgs e) => MyFrame.Content = null;

        private void ContentWindow_OnClosed(object sender, EventArgs e) => Environment.Exit(0);
    }
}