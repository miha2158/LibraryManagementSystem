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
                    pAuthors.UpdateLayout();
                    break;
                }
                case PagePublications pagePublications:
                {
                    var p1 = new WindowAddEditPublication(this);
                    p1.ShowDialog();
                    pPublications.UpdateLayout();
                    break;
                }
                case PageUsers pageUsers:
                {
                    var p2 = new WindowAddEditUserAuthor(this, true);
                    p2.ShowDialog();
                    pUsers.UpdateLayout();
                    break;
                }
            }
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
            pPublications.UpdateLayout();
            pUsers.UpdateLayout();
            pAuthors.UpdateLayout();
        }

        private void Test_OnClick(object sender, RoutedEventArgs e)
        {
            using (var db = new LibraryDBContainer())
            {

                for (int i = 0; i < 6; i++)
                    db.DbAuthorSet1.Add(DbAuthor.FillBlanks());

                for (int i = 0; i < 10; i++)
                    db.DbReaderSet.Add(DbReader.FillBlanks());

                var courses = new[]
                {
                    new DbCourse { Id = 1, Course = 1 },
                    new DbCourse { Id = 2, Course = 2 },
                    new DbCourse { Id = 3, Course = 3 },
                    new DbCourse { Id = 4, Course = 4 },
                    new DbCourse { Id = 5, Course = 1 },
                    new DbCourse { Id = 6, Course = 2 },
                    new DbCourse { Id = 7, Course = 3 },
                    new DbCourse { Id = 8, Course = 3 },
                    new DbCourse { Id = 9, Course = 2 },
                    new DbCourse { Id = 10, Course = 4 },
                };
                for (var i = 0; i < courses.Length; i++)
                    db.DbCourseSet.Add(courses[i]);

                var disciplines = new[]
                {
                    new DbDiscipline
                    {
                        Id = 1,
                        Name = "Программирование",
                    },
                    new DbDiscipline
                    {
                        Id = 2,
                        Name = "Конструирование ПО",
                    },
                    new DbDiscipline
                    {
                        Id = 3,
                        Name = "Архитектура ОС",
                    }
                };
                for (var i = 0; i < disciplines.Length; i++)
                    db.DbDisciplineSet.Add(disciplines[i]);

                var publications = new[]
                {
                    new DbPublication("Основы C",
                                      db.DbAuthorSet1.Local[2],
                                      ePublicationType.None,
                                      eBookPublication.Book,
                                      new DateTime(1985, 5, 1),
                                      "oldschool")
                    {
                        Id = 1,
                        Course = new []
                        {
                            courses[0],
                            courses[1]
                        },
                        Discipline = new []
                        {
                            disciplines[0],
                            disciplines[2]
                        }
                    },
                    new DbPublication("Основы C++",
                                      new[]
                                      {
                                          db.DbAuthorSet1.Local[2],
                                          db.DbAuthorSet1.Local[3]
                                      },
                                      ePublicationType.None,
                                      eBookPublication.Book,
                                      new DateTime(2001, 9, 1),
                                      "oldschool")
                    {
                        Id = 2,
                        Course = new []
                        {
                            courses[2],
                            courses[3]
                        },
                        Discipline = new []
                        {
                            disciplines[0],
                            disciplines[1]
                        }
                    },
                    new DbPublication("Основы C#",
                                      new[]
                                      {
                                          db.DbAuthorSet1.Local[1],
                                          db.DbAuthorSet1.Local[2]
                                      },
                                      ePublicationType.None,
                                      eBookPublication.Book,
                                      new DateTime(2011, 5, 1),
                                      "newschool")
                    {
                        Id = 3,
                        Course = new []
                        {
                            courses[4],
                        },
                        Discipline = new []
                        {
                            disciplines[0],
                        }
                    },
                    new DbPublication("PHP vs C# vs Node.js",
                                      new[]
                                      {
                                          db.DbAuthorSet1.Local[3],
                                          db.DbAuthorSet1.Local[4],
                                      },
                                      ePublicationType.Educational,
                                      eBookPublication.Publication,
                                      new DateTime(2015, 02, 01), null)
                    {
                        Id = 4,
                        InternetLocation = "https://google.com/",
                        Course = new []
                        {
                            courses[5],
                            courses[6]
                        },
                        Discipline = new []
                        {
                            disciplines[1],
                        }
                    },
                    new DbPublication("Тестирование Не Нужно",
                                      db.DbAuthorSet1.Local[5],
                                      ePublicationType.Educational,
                                      eBookPublication.Publication,
                                      new DateTime(2016, 12, 01),
                                      "Прогер")
                    {
                        Id = 5,
                        InternetLocation = "https://proger.ru/",
                        Course = new []
                        {
                            courses[7],
                        },
                        Discipline = new []
                        {
                            disciplines[2],
                        }
                    },
                    new DbPublication("70 лайфхаков для упрощения тестирования",
                                      db.DbAuthorSet1.Local[5],
                                      ePublicationType.Scientific,
                                      eBookPublication.Publication,
                                      new DateTime(2018, 02, 01),
                                      null)
                    {
                        Id = 6,
                        InternetLocation = "https://proger.ru/",
                        Course = new []
                        {
                            courses[8],
                            courses[9]
                        },
                        Discipline = new []
                        {
                            disciplines[0],
                        }
                    },
                };
                for (var i = 0; i < publications.Length; i++)
                    db.DbPublicationSet1.Add(publications[i]);

                var locations = new[]
                {
                    new DbBookLocation
                    {
                        Id = 1,
                        Room = 307,
                        Place = "тут",
                        IsTaken = false,
                        Reader = db.DbReaderSet.Local[0],
                        Publication = db.DbPublicationSet1.Local[2]
                    },
                    new DbBookLocation
                    {
                        Id = 2,
                        Room = 321,
                        Place = "справа",
                        IsTaken = false,
                        Reader = db.DbReaderSet.Local[0],
                        Publication = db.DbPublicationSet1.Local[2]
                    },
                    new DbBookLocation
                    {
                        Id = 3,
                        Room = 501,
                        Place = "на столе",
                        IsTaken = true,
                        Reader = db.DbReaderSet.Local[1],
                        Publication = db.DbPublicationSet1.Local[2]
                    },

                    new DbBookLocation
                    {
                        Id = 4,
                        Room = 321,
                        Place = "на верхней полке шкафа",
                        IsTaken = false,
                        Reader = db.DbReaderSet.Local[0],
                        Publication = db.DbPublicationSet1.Local[1]
                    },
                    new DbBookLocation
                    {
                        Id = 5,
                        Room = 318,
                        Place = "в левом шкуфу справа",
                        IsTaken = false,
                        Reader = db.DbReaderSet.Local[0],
                        Publication = db.DbPublicationSet1.Local[1]
                    },
                    new DbBookLocation
                    {
                        Id = 6,
                        Room = 302,
                        Place = "под ножкой стола",
                        IsTaken = false,
                        Reader = db.DbReaderSet.Local[0],
                        Publication = db.DbPublicationSet1.Local[0]
                    },
                    new DbBookLocation
                    {
                        Id = 7,
                        Room = 323,
                        Place = "в дырке в стене",
                        IsTaken = false,
                        Reader = db.DbReaderSet.Local[0],
                        Publication = db.DbPublicationSet1.Local[0]
                    },
                };
                for (var i = 0; i < locations.Length; i++)
                    db.DbBookLocationSet.Add(locations[i]);

                var stats = new[]
                {
                    new DbStats {Id = 1, DateTaken = new DateTime(2016, 02, 03), Publication = db.DbPublicationSet1.Local[2]},
                    new DbStats {Id = 2, DateTaken = new DateTime(2017, 07, 12), Publication = db.DbPublicationSet1.Local[2]},
                    new DbStats {Id = 3, DateTaken = new DateTime(2017, 10, 29), Publication = db.DbPublicationSet1.Local[1]},
                    new DbStats {Id = 4, DateTaken = new DateTime(2018, 02, 05), Publication = db.DbPublicationSet1.Local[2]},
                };
                for (var i = 0; i < stats.Length; i++)
                    db.DbStatsSet.Add(stats[i]);

                db.SaveChanges();
            }

            pPublications.UpdateLayout();
            pUsers.UpdateLayout();
            pAuthors.UpdateLayout();
        }

        private void Search_OnClick(object sender, RoutedEventArgs e)
        {

        }
    }
}