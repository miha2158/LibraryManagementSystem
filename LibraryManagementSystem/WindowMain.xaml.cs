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
    public partial class WindowMain: Window
    {
        public WindowMain()
        {
            //Ex.init();

            InitializeComponent();
            
            if (pPublications == null)
                pPublications = new PagePublications();
            if (pUsers == null)
                pUsers = new PageUsers();
            if (pAuthors == null)
            pAuthors = new PageAuthors();
        }

        public Page OnScreenContent;
        public static PagePublications pPublications;
        public static PageUsers pUsers;
        public static PageAuthors pAuthors;

        private void ToPagePublications(object sender, RoutedEventArgs e) => NavigateTo(pPublications);
        private void ToPageUsers(object sender, RoutedEventArgs e) => NavigateTo(pUsers);
        private void ToPageAuthors(object sender, RoutedEventArgs e) => NavigateTo(pAuthors);

        public void NavigateTo(Page destinationPage)
        {
            destinationPage.UpdateLayout();
            MainView.Navigate(OnScreenContent = destinationPage);
        }

        private void ActualWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            NavigateTo(pPublications);
        }
        private void SearchBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            DummySearchText.Visibility = SearchBox.Text.Length == 0 ? Visibility.Visible : Visibility.Collapsed;
        }
        private void DummySearchText_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            SearchBox.Focus();
        }

        private void SearchBox_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter)
                return;

        }

        private void NewPublication(object sender, RoutedEventArgs e)
        {
            switch (OnScreenContent)
            {
                case PageAuthors pageAuthors:
                {
                    var p0 = new WindowAddEditUserAuthor(this, false);
                    p0.ShowDialog();
                    DbAuthor.All.Add(p0.Author);
                    break;
                }
                case PagePublications pagePublications:
                {
                    var p1 = new WindowAddEditPublication(this);
                    p1.ShowDialog();
                    DbPublication.All.Add(p1.Publication);
                    break;
                }
                case PageUsers pageUsers:
                {
                    var p2 = new WindowAddEditUserAuthor(this, true);
                    p2.ShowDialog();
                    DbReader.All.Add(p2.Reader);
                    break;
                }
            }

            Ex.Lib.SaveChanges();
        }

        private void EditPublication(object sender, RoutedEventArgs e)
        {

        }

        private void Reports_OnClick(object sender, RoutedEventArgs e)
        {
            var p = new WindowReports(this);
            p.ShowDialog();
        }

        private void Filter_OnClick(object sender, RoutedEventArgs e)
        {
            var p = new WindowSorting(this);
            //var p = new WindowLocation(this);
            p.ShowDialog();
        }

        private void WindowMain_OnGotFocus(object sender, RoutedEventArgs e)
        {
            pPublications.Owner = this;
            pAuthors.Owner = this;
            pUsers.Owner = this;
        }

        private void ShowAll_OnClick(object sender, RoutedEventArgs e)
        {

        }

        private void Test_OnClick(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 5; i++)
                Ex.Lib.DbAuthorSet1.Add(DbAuthor.FillBlanks());

            for (int i = 0; i < 10; i++)
                Ex.Lib.DbReaderSet.Add(DbReader.FillBlanks());

            Ex.Lib.DbPublicationSet1.AddRange(new []
            {
                new DbPublication("Основы C", DbAuthor.All[1], ePublicationType.None, eBookPublication.Book, new DateTime(1985, 5, 1), "oldschool"){ },
                new DbPublication("Основы C++", new []{ DbAuthor.All[1], DbAuthor.All[2] }, ePublicationType.None, eBookPublication.Book, new DateTime(2001, 9, 1), "oldschool"){  },
                new DbPublication("Основы C#", new []{ DbAuthor.All[0], DbAuthor.All[1] }, ePublicationType.None, eBookPublication.Book, new DateTime(2011, 5, 1), "newschool"){  },
                new DbPublication("PHP vs C# vs Node.js", new []{ DbAuthor.All[2], DbAuthor.All[3],}, ePublicationType.Educational, eBookPublication.Publication, new DateTime(2015, 02, 01), null){ InternetLocation = "https://google.com/" },
                new DbPublication("Тестирование Не Нужно", DbAuthor.All[4], ePublicationType.Educational, eBookPublication.Publication, new DateTime(2016, 12, 01), "Прогер"){ InternetLocation = "https://proger.ru/" },
                new DbPublication("70 лайфхаков для упрощения тестирования",  DbAuthor.All[4], ePublicationType.Scientific, eBookPublication.Publication, new DateTime(2018, 02, 01), null){ InternetLocation = "https://proger.ru/" },
            });

            Ex.Lib.DbDisciplineSet.AddRange(new[]
            {
                new DbDiscipline { Name = "Программирование", Publication = new List<DbPublication>(new []{ DbPublication.All[2], DbPublication.All[5], DbPublication.All[0], DbPublication.All[1], }) },
                new DbDiscipline { Name = "Конструирование ПО", Publication = new List<DbPublication>(new []{ DbPublication.All[3], DbPublication.All[1], }) },
                new DbDiscipline { Name = "Архитектура ОС", Publication = new List<DbPublication>(new []{ DbPublication.All[4], DbPublication.All[0], }) }
            });

            Ex.Lib.DbCourseSet.AddRange(new[]
            {
                new DbCourse {Course = 1, Publication = DbPublication.All[0]},
                new DbCourse {Course = 2, Publication = DbPublication.All[0]},
                new DbCourse {Course = 3, Publication = DbPublication.All[1]},
                new DbCourse {Course = 4, Publication = DbPublication.All[1]},
                new DbCourse {Course = 1, Publication = DbPublication.All[2]},
                new DbCourse {Course = 2, Publication = DbPublication.All[3]},
                new DbCourse {Course = 3, Publication = DbPublication.All[3]},
                new DbCourse {Course = 3, Publication = DbPublication.All[4]},
                new DbCourse {Course = 2, Publication = DbPublication.All[5]},
                new DbCourse {Course = 4, Publication = DbPublication.All[5]},
            });

            Ex.Lib.DbBookLocationSet.AddRange(new []
            {
                new DbBookLocation{ Room = 307, Place = "тут", IsTaken = false, Reader = DbReader.All[0], Publication = DbPublication.All[2]},
                new DbBookLocation{ Room = 321, Place = "справа", IsTaken = false, Reader = DbReader.All[0], Publication = DbPublication.All[2]},
                new DbBookLocation{ Room = 501, Place = "на столе", IsTaken = true, Reader = DbReader.All[1], Publication = DbPublication.All[2]},

                new DbBookLocation{ Room = 321, Place = "на верхней полке шкафа", IsTaken = false, Reader = DbReader.All[0], Publication = DbPublication.All[1]},
                new DbBookLocation{ Room = 318, Place = "в левом шкуфу справа", IsTaken = false, Reader = DbReader.All[0], Publication = DbPublication.All[1]},
                new DbBookLocation{ Room = 302, Place = "под ножкой стола", IsTaken = false, Reader = DbReader.All[0], Publication = DbPublication.All[0]},
                new DbBookLocation{ Room = 323, Place = "в дырке в стене", IsTaken = false, Reader = DbReader.All[0], Publication = DbPublication.All[0]},
            });

            Ex.Lib.DbStatsSet.AddRange(new[]
            {
                new DbStats { DateTaken = new DateTime(2016, 02, 03), Publication = DbPublication.All[2]},
                new DbStats { DateTaken = new DateTime(2017, 07, 12), Publication = DbPublication.All[2]},
                new DbStats { DateTaken = new DateTime(2017, 10, 29), Publication = DbPublication.All[1]},
                new DbStats { DateTaken = new DateTime(2018, 02, 05), Publication = DbPublication.All[2]},
            });

            Ex.Lib.SaveChanges();
            OnScreenContent.UpdateLayout();
        }

        private void Search_OnClick(object sender, RoutedEventArgs e)
        {
            Ex.Lib.SaveChanges();
        }
    }
}