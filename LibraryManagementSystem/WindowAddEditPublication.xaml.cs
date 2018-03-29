using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Generator;

namespace LibraryManagementSystem
{
    public partial class WindowAddEditPublication : Window
    {
        public WindowAddEditPublication()
        {
            InitializeComponent();
            PublishDatePicker.DisplayDateEnd = DateTime.Today;
            Publisher.ItemsSource = DbPublication.AllPublishers;
        }
        public WindowAddEditPublication(Window Owner) : this()
        {
            this.Owner = Owner;
            using (var db = new LibraryDBContainer())
            {
                AuthorList.ItemsSource = db.DbAuthorSet1.ToList().OrderBy(e => e.WriterType);
                DisciplinesList.ItemsSource = db.DbDisciplineSet.Where(d => d != null).ToList();
            }
        }
        public WindowAddEditPublication(Window Owner, DbPublication pub) : this(Owner)
        {
            Publication = pub;
            NameBox.Text = Publication.Name;
            PublishDatePicker.SelectedDate = Publication.DatePublished;
            Publisher.Text = Publication.Publisher;

            BookRButton.IsChecked = Publication.BookPublication == eBookPublication.Book.e();
            PublicationRButton.IsChecked = Publication.BookPublication == eBookPublication.Publication.e();

            ScientificPublication.IsChecked = Publication.PublicationType == ePublicationType.Scientific.e();
            MethodicalPublication.IsChecked = Publication.PublicationType == ePublicationType.Educational.e();

            EpubCheckBox.IsChecked = !string.IsNullOrWhiteSpace(Publication.InternetLocation);
            EpubAdress.Text = Publication.InternetLocation;

            using (var db = new LibraryDBContainer())
            {
                Publication = db.DbPublicationSet1.Find(Publication.Id);
                var p = Publication?.Course.Select(e => e.Course).ToList();
                if (p != null)
                {
                    Course1.IsChecked = p.Contains(1);
                    Course2.IsChecked = p.Contains(2);
                    Course3.IsChecked = p.Contains(3);
                    Course4.IsChecked = p.Contains(4);
                }

                PubNumberChechBox.IsChecked = Publication?.PhysicalLocations.Count != 0;
                PubNumber.Text = Publication?.PhysicalLocations.Count.ToString();

                AuthorList.ItemsSource = db.DbAuthorSet1.ToList().OrderBy(e => e.WriterType);
                foreach (var author in db.DbAuthorSet1.Where(d => d.Publications.Any(e => e.Id == Publication.Id)))
                    AuthorList.SelectedItems.Add(author);

                DisciplinesList.ItemsSource = db.DbDisciplineSet.Where(d => d != null).ToList();
                foreach (DbDiscipline discipline in db.DbDisciplineSet.Where(
                    d => d.Publication.Any(e => e.Id == Publication.Id)))
                    DisciplinesList.SelectedItems.Add(discipline);
            }
        }

        private void This_OnLoaded(object sender, RoutedEventArgs e)
        {
            MouseMove_OnHandler(null, null);
        }
        private void AddAuthor_OnClick(object sender, RoutedEventArgs e)
        {
            var p = new WindowAddEditUserAuthor(this, false);
            p.ShowDialog();
            DbAuthor.All.Add(p.Author);
        }

        private void PublicationRButton_OnUnchecked(object sender, RoutedEventArgs e)
        {
            PublicationSpecific.Visibility = Visibility.Collapsed;
        }
        private void PublicationRButton_OnChecked(object sender, RoutedEventArgs e)
        {
            PublicationSpecific.Visibility = Visibility.Visible;
        }
        private void PubNumberTextBlock_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            PubNumber.Focus();
        }
        private void PubNumber_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            PubNumberTextBlock.Visibility = PubNumber.Text.Length == 0 ? Visibility.Visible : Visibility.Collapsed;
        }
        private void EpubAdressTextBlock_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            EpubAdress.Focus();
        }
        private void EpubAdress_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            EpubAdressTextBlock.Visibility = EpubAdress.Text.Length == 0 ? Visibility.Visible : Visibility.Collapsed;
        }

        private void LocationButton_OnClick(object sender, RoutedEventArgs e)
        {
            int num = 0;
            int.TryParse(PubNumber.Text, out num);

            Hide();
            if (Publication == null)
            {
                using (var db = new LibraryDBContainer())
                {
                    Publication = new DbPublication(NameBox.Text, new List<DbAuthor>(),
                                                    BookRButton.IsChecked == true
                                                        ? ePublicationType.None
                                                        : ScientificPublication.IsChecked == true
                                                            ? ePublicationType.Scientific
                                                            : ePublicationType.Educational,
                                                    BookRButton.IsChecked == true
                                                        ? eBookPublication.Book
                                                        : eBookPublication.Publication,
                                                    PublishDatePicker.SelectedDate.Value, Publisher.Text);

                    if (Course1.IsChecked == true)
                        db.DbCourseSet.Find(1).Publication.Add(Publication);
                    if (Course2.IsChecked == true)
                        db.DbCourseSet.Find(2).Publication.Add(Publication);
                    if (Course3.IsChecked == true)
                        db.DbCourseSet.Find(3).Publication.Add(Publication);
                    if (Course4.IsChecked == true)
                        db.DbCourseSet.Find(4).Publication.Add(Publication);

                    var selectedIds = AuthorList
                                     .SelectedItems.Cast<DbAuthor>()
                                     .Select(g => g.Id);
                    Publication.Authors = db.DbAuthorSet1.Where(d => selectedIds.Contains(d.Id)).ToList();

                    db.DbPublicationSet1.Add(Publication);

                    var selectedDisciplines = db.DbDisciplineSet
                                                .ToArray()
                                                .Where(d =>
                                                       {
                                                           return DisciplinesList
                                                                 .SelectedItems
                                                                 .Cast<DbDiscipline>()
                                                                 .Select(f => f.Id)
                                                                 .Contains(d.Id);
                                                       });
                    foreach (var disc in selectedDisciplines)
                        disc.Publication.Add(Publication);

                    if (EpubCheckBox.IsChecked == true)
                        Publication.InternetLocation = EpubAdress.Text;

                    if (EpubCheckBox.IsChecked != true && PubNumberChechBox.IsChecked != true)
                        Publication.BookPublication = eBookPublication.None.e();

                    Publication.PhysicalLocations = new List<DbBookLocation>(num);

                    db.SaveChanges();
                }

                if (PubNumberChechBox.IsChecked == true)
                {
                    var p = new WindowEditLocation(this, Publication, num);
                    p.ShowDialog();
                }

                Close();
            }
            else
            {
                using (var db = new LibraryDBContainer())
                {
                    Publication = db.DbPublicationSet1.Find(Publication.Id);

                    Publication.Name = NameBox.Text;
                    Publication.DatePublished = PublishDatePicker.SelectedDate.Value;
                    Publication.Publisher = Publisher.Text;

                    Publication.BookPublication = BookRButton.IsChecked == true
                        ? eBookPublication.Book.e()
                        : eBookPublication.Publication.e();
                    Publication.PublicationType = BookRButton.IsChecked == true
                        ? ePublicationType.None.e()
                        : ScientificPublication.IsChecked == true
                            ? ePublicationType.Scientific.e()
                            : ePublicationType.Educational.e();
                    
                    Publication.Course.Clear();
                    if (Course1.IsChecked == true)
                        db.DbCourseSet.Find(1).Publication.Add(Publication);
                    if (Course2.IsChecked == true)
                        db.DbCourseSet.Find(2).Publication.Add(Publication);
                    if (Course3.IsChecked == true)
                        db.DbCourseSet.Find(3).Publication.Add(Publication);
                    if (Course4.IsChecked == true)
                        db.DbCourseSet.Find(4).Publication.Add(Publication);
                    
                    Publication.Discipline.Clear();
                    foreach (var disc in DisciplinesList
                                        .SelectedItems.Cast<DbDiscipline>().Select(d => db.DbDisciplineSet.Find(d.Id)))
                        Publication.Discipline.Add(disc);

                    if (EpubCheckBox.IsChecked == true)
                        Publication.InternetLocation = EpubAdress.Text;

                    Publication.Authors.Clear();
                    Publication.Authors = AuthorList.SelectedItems.Cast<DbAuthor>()
                                                    .Select(d => db.DbAuthorSet1.Find(d.Id)).ToList();
                    Thread.Sleep(10);
                    db.SaveChanges();
                }

                if (PubNumberChechBox.IsChecked == true)
                {
                    var p = new WindowEditLocation(this, Publication, num);
                    p.ShowDialog();
                }

                Close();
            }
        }

        public DbPublication Publication;

        private void PubNumber_OnKeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.D0:
                case Key.NumPad0:
                case Key.D1:
                case Key.NumPad1:
                case Key.D2:
                case Key.NumPad2:
                case Key.D3:
                case Key.NumPad3:
                case Key.D4:
                case Key.NumPad4:
                case Key.D5:
                case Key.NumPad5:
                case Key.D6:
                case Key.NumPad6:
                case Key.D7:
                case Key.NumPad7:
                case Key.D8:
                case Key.NumPad8:
                case Key.D9:
                case Key.NumPad9:
                case Key.Home:
                case Key.Delete:
                case Key.End:
                case Key.Right:
                case Key.Left:
                    break;
                default:
                    e.Handled = true;
                    return;
            }
        }
        private void PubNumber_OnTextInput(object sender, TextCompositionEventArgs e)
        {
            var c = new string(e.Text.Where(char.IsDigit).ToArray());
            int sel = PubNumber.SelectionStart;
            PubNumber.SelectedText = c;
            PubNumber.SelectionStart = sel + c.Length;
            PubNumber.SelectionLength = 0;
            e.Handled = true;
        }

        private void AddDiscipline_OnClick(object sender, RoutedEventArgs e)
        {
            var p = new WindowAddDiscipline(this);
            p.ShowDialog();
            DisciplinesList.ItemsSource = DbPublication.AllDisciplines;
        }

        public bool IsReady => true;

        private void MouseMove_OnHandler(object sender, MouseEventArgs e)
        {
            LocationButton.IsEnabled = !string.IsNullOrWhiteSpace(NameBox.Text) &&
                                       !string.IsNullOrWhiteSpace(Publisher.Text) &&
                                       (EpubCheckBox.IsChecked == true && !string.IsNullOrWhiteSpace(EpubAdress.Text) || PubNumberChechBox.IsChecked == true && !string.IsNullOrWhiteSpace(PubNumber.Text)) &&
                                       (Course1.IsChecked == true || Course2.IsChecked == true || Course3.IsChecked == true || Course4.IsChecked == true) &&
                                       AuthorList.SelectedIndex != -1 &&
                                       DisciplinesList.SelectedIndex != -1 &&
                                       PublishDatePicker.SelectedDate.HasValue;
        }
    }
}