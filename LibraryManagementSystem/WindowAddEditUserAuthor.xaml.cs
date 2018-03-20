using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LibraryManagementSystem
{
    public partial class WindowAddEditUserAuthor: Window
    {
        public WindowAddEditUserAuthor()
        {
            InitializeComponent();
        }
        public WindowAddEditUserAuthor(Window Owner): this()
        {
            this.Owner = Owner;
        }
        public WindowAddEditUserAuthor(Window Owner, bool isReader) : this(Owner)
        {
            this.isReader = isReader;

            if (isReader)
                return;
            UserRole.Visibility = Visibility.Collapsed;
            GroupBox.IsEnabled = false;
            AuthorRole.Visibility = Visibility.Visible;
        }

        public bool IsStudent => isReader && UserRole.SelectedIndex == 0;
        private bool isReader;

        public Reader Reader = new Reader();
        public Author Author = new Author();

        private void This_OnLoaded(object sender, RoutedEventArgs e)
        {
            Title += isReader? "Читателя": "Автора";
        }

        private void EventSetter_OnHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Space)
                ((TextBox) sender).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
        }

        private void Accept_OnClick(object sender, RoutedEventArgs e)
        {
            Hide();
            if (isReader)
                Reader = IsStudent ? new Reader(First.Text, Last.Text, Patronimic.Text, GroupBox.Text) : new Reader(First.Text, Last.Text, Patronimic.Text);
            else
                Author = new Author(First.Text, Last.Text, Patronimic.Text, (WriterType)AuthorRole.SelectedIndex);
            Close();
        }
    }
}