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
            GroupBox.ItemsSource = Groups;
        }
        public WindowAddEditUserAuthor(Window Owner, bool isReader) : this(Owner)
        {
            this.isReader = isReader;

            if (isReader)
                return;
            UserRole.Visibility = Visibility.Collapsed;
            GroupBox.IsEnabled = false;
            AuthorRole.Visibility = Visibility.Visible;
            GroupBox.Visibility = Visibility.Collapsed;
        }

        public WindowAddEditUserAuthor(Window Owner, DbReader reader) : this(Owner, true)
        {
            Reader = reader;

            isReader = true;
            UserRole.SelectedIndex = Reader.AccessLevel;
            Last.Text = Reader.Last;
            First.Text = Reader.First;
            Patronimic.Text = Reader.Patronimic;
            GroupBox.Text = Reader.Group;
        }
        public WindowAddEditUserAuthor(Window Owner, DbAuthor author): this(Owner, false)
        {
            GroupBox.Visibility = Visibility.Collapsed;
            Author = author;

            isReader = false;
            Last.Text = Author.Last;
            First.Text = Author.First;
            Patronimic.Text = Author.Patronimic;
            AuthorRole.SelectedIndex = Author.WriterType;

        }

        public bool IsStudent => isReader && UserRole.SelectedIndex == 0;
        private bool isReader;

        public IEnumerable<string> Groups => DbReader.Groups;

        public DbReader Reader;
        public DbAuthor Author;

        public bool IsReady => !string.IsNullOrWhiteSpace(Last.Text) && !string.IsNullOrWhiteSpace(First.Text) && !string.IsNullOrWhiteSpace(Patronimic.Text) &&
                               (Reader != null && (UserRole.SelectedIndex == 0 && !string.IsNullOrWhiteSpace(GroupBox.Text) || UserRole.SelectedIndex != -1) ||
                                Author != null && AuthorRole.SelectedIndex != -1);

        private void This_OnLoaded(object sender, RoutedEventArgs e)
        {
            Title += isReader? "Читателя": "Автора";
        }
        private void EventSetter_OnHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Space)
                ((FrameworkElement) sender).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
        }

        private void Accept_OnClick(object sender, RoutedEventArgs e)
        {
            Hide();
            if (Reader == null && Author == null)
            {
                using (var db = new LibraryDBContainer())
                {
                    if (isReader)
                    {
                        Reader = IsStudent
                            ? new DbReader(First.Text, Last.Text, Patronimic.Text, GroupBox.Text)
                            : new DbReader(First.Text, Last.Text, Patronimic.Text);
                        db.DbReaderSet.Add(Reader);
                    }
                    else
                    {
                        Author = new DbAuthor(First.Text, Last.Text, Patronimic.Text,
                                              (eWriterType) AuthorRole.SelectedIndex);
                        db.DbAuthorSet1.Add(Author);
                    }

                    db.SaveChanges();
                }
            }
            else
            {
                using (var db = new LibraryDBContainer())
                {
                    if (isReader)
                    {
                        Reader = db.DbReaderSet.Find(Reader.Id);

                        Reader.AccessLevel = (byte) UserRole.SelectedIndex;
                        Reader.Last = Last.Text;
                        Reader.First = First.Text;
                        Reader.Patronimic = Patronimic.Text;
                        Reader.Group = GroupBox.Text;
                    }
                    else
                    {
                        Author = db.DbAuthorSet1.Find(Author.Id);

                        Author.Last = Last.Text;
                        Author.First = First.Text;
                        Author.Patronimic = Patronimic.Text;
                        Author.WriterType = (byte) AuthorRole.SelectedIndex;
                    }

                    db.SaveChanges();
                }
            }
            Close();
        }
    }
}