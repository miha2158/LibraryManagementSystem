﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Generator;

namespace LibraryManagementSystem
{
    public partial class WindowAddEditPublication: Window
    {
        public WindowAddEditPublication()
        {
            InitializeComponent();
            PublishDatePicker.DisplayDateEnd = DateTime.Today;
            Publisher.ItemsSource = DbPublication.AllPublishers;
        }
        public WindowAddEditPublication(Window Owner): this()
        {
            this.Owner = Owner;
            using (var db = new LibraryDBContainer())
            {
                AuthorList.ItemsSource = db.DbAuthorSet1.ToList().OrderBy(e => e.WriterType);
                DisciplinesList.ItemsSource = db.DbDisciplineSet.ToList();
            }
        }
        public WindowAddEditPublication(Window Owner, DbPublication pub):this(Owner)
        {
            Publication = pub;
            NameBox.Text = Publication.Name;
            PublishDatePicker.DisplayDate = Publication.DatePublished;
            Publisher.Text = Publication.Publisher;

            using (var db = new LibraryDBContainer())
            {
                var p = db.DbPublicationSet1.Find(Publication.Id)?.Course.Select(e => e.Course).ToList();

                if(p != null)
                {
                    Course1.IsChecked = p.Contains(1);
                    Course2.IsChecked = p.Contains(2);
                    Course3.IsChecked = p.Contains(3);
                    Course4.IsChecked = p.Contains(4);
                }
            }

            BookRButton.IsChecked = Publication.BookPublication == eBookPublication.Book.e();
            PublicationRButton.IsChecked = Publication.BookPublication == eBookPublication.Publication.e();

            ScientificPublication.IsChecked = Publication.PublicationType == ePublicationType.Scientific.e();
            MethodicalPublication.IsChecked = Publication.PublicationType == ePublicationType.Educational.e();

            using(var db = new LibraryDBContainer())
            {
                PubNumberChechBox.IsChecked = db.DbPublicationSet1.Find(Publication.Id)?.PhysicalLocations.Count != 0;
                PubNumber.Text = db.DbPublicationSet1.Find(Publication.Id)?.PhysicalLocations.Count.ToString();
            }

            EpubCheckBox.IsChecked = !string.IsNullOrWhiteSpace(Publication.InternetLocation);
            EpubAdress.Text = Publication.InternetLocation;

            using (var db = new LibraryDBContainer())
            {
                AuthorList.ItemsSource = db.DbAuthorSet1.ToList().OrderBy(e => e.WriterType);
                foreach (var author in db.DbPublicationSet1.Find(Publication.Id).Authors)
                    AuthorList.SelectedItems.Add(db.DbAuthorSet1.Find(author.Id));
            
                DisciplinesList.ItemsSource = db.DbDisciplineSet.ToList();
                foreach (DbDiscipline discipline in db.DbPublicationSet1.Find(Publication.Id).Discipline)
                    DisciplinesList.SelectedItems.Add(db.DbDisciplineSet.Find(discipline.Id));
            }
        }

        private void This_OnLoaded(object sender, RoutedEventArgs e)
        {

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
            Hide();
            if (Publication == null)
            {
                Publication = new DbPublication(NameBox.Text, AuthorList.SelectedItems.Cast<DbAuthor>(),
                                                BookRButton.IsChecked == true? ePublicationType.None : ScientificPublication.IsChecked == true? ePublicationType.Scientific : ePublicationType.Educational, 
                                                BookRButton.IsChecked == true? eBookPublication.Book : eBookPublication.Publication,
                                                PublishDatePicker.DisplayDate, Publisher.Text);
                if (PubNumberChechBox.IsChecked == true)
                {
                    int num = int.Parse(PubNumber.Text);
                    var p = new WindowEditLocation(this, Publication, num);
                    p.ShowDialog();
                    using (var db = new LibraryDBContainer())
                    {
                        db.DbPublicationSet1.Add(Publication);
                        db.SaveChanges();
                    }
                }
                else if (EpubCheckBox.IsChecked == true)
                {
                    Publication.InternetLocation = EpubAdress.Text;
                    using (var db = new LibraryDBContainer())
                    {
                        db.DbPublicationSet1.Add(Publication);
                        db.SaveChanges();
                    }
                }
                else
                    using (var db = new LibraryDBContainer())
                    {
                        Publication.BookPublication = eBookPublication.None.e();
                        db.DbPublicationSet1.Add(Publication);
                        db.SaveChanges();
                    }

                Close();
            }
            else
            {
                using (var db = new LibraryDBContainer())
                {
                    Publication = db.DbPublicationSet1.Find(Publication.Id);

                    Publication.Name = NameBox.Text;
                    Publication.DatePublished = PublishDatePicker.DisplayDate;
                    Publication.Publisher = Publisher.Text;

                    Publication.BookPublication = BookRButton.IsChecked == true? eBookPublication.Book.e(): eBookPublication.Publication.e();
                    Publication.PublicationType = BookRButton.IsChecked == true ? ePublicationType.None.e() : ScientificPublication.IsChecked == true ? ePublicationType.Scientific.e() : ePublicationType.Educational.e();

                    if (EpubCheckBox.IsChecked == true)
                        Publication.InternetLocation = EpubAdress.Text;

                    Publication.Authors = Publication.Authors.Union(AuthorList.SelectedItems.Cast<DbAuthor>()).ToList();
                    db.SaveChanges();
                }

                if (PubNumberChechBox.IsChecked == true)
                {
                    int num = int.Parse(PubNumber.Text);
                    var p = new WindowEditLocation(this, Publication, num);
                    p.ShowDialog();
                }
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
            var p = new WindowAddDiscipline();
            p.ShowDialog();
        }
    }
}