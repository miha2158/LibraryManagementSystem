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
        private WindowAddEditUserAuthor(Window Owner): this()
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

        public WindowAddEditUserAuthor(Window Owner, DbReader reader) : this(Owner)
        {
            Reader = reader;

            isReader = true;
            UserRole.SelectedIndex = Reader.AccessLevel;
            Last.Text = Reader.Last;
            First.Text = Reader.First;
            Patronimic.Text = Reader.Patronimic;
            GroupBox.Text = Reader.Group;
        }
        public WindowAddEditUserAuthor(Window Owner, DbAuthor author)
        {
            Author = author;

            isReader = false;
            Last.Text = Reader.Last;
            First.Text = Reader.First;
            Patronimic.Text = Reader.Patronimic;
            AuthorRole.SelectedIndex = Author.WriterType;

        }

        public bool IsStudent => isReader && UserRole.SelectedIndex == 0;
        private bool isReader;

        public DbReader Reader = new DbReader();
        public DbAuthor Author = new DbAuthor();

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
                Reader = IsStudent ? new DbReader(First.Text, Last.Text, Patronimic.Text, GroupBox.Text) : new DbReader(First.Text, Last.Text, Patronimic.Text);
            else
                Author = new DbAuthor(First.Text, Last.Text, Patronimic.Text, (eWriterType)AuthorRole.SelectedIndex);
            Close();
        }
    }
}