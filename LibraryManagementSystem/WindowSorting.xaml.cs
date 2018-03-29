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
    public partial class WindowSorting: Window
    {
        public WindowSorting()
        {
            InitializeComponent();
        }
        public WindowSorting(Window Owner): this()
        {
            this.Owner = Owner;
            ListAuthor.ItemsSource = DbAuthor.All;
            ListDisciplines.ItemsSource = DbPublication.AllDisciplines;
            ListPublishers.ItemsSource = DbPublication.AllPublishers;
        }
        private void This_OnLoaded(object sender, RoutedEventArgs e)
        {
            StartDate.SelectedDate = DateTime.MinValue;
            FinishDate.SelectedDate = DateTime.Today;
        }

        private void Search_OnClick(object sender, RoutedEventArgs e)
        {
            (Owner as WindowMain)?.NavigateTo(WindowMain.pPublications);
            Hide();
            using (var db = new LibraryDBContainer())
            {
                var p1 =  db.DbPublicationSet1.ToList();
                
                p1 = p1.Where(d => BookBox.IsChecked == true && d.toEnumBP == eBookPublication.Book || PublicationBox.IsChecked == true && d.toEnumBP == eBookPublication.Publication).ToList();
                p1 = p1.Where(d => d.toEnumPT == ePublicationType.None || SciBox.IsChecked == true && d.toEnumPT == ePublicationType.Scientific || EduBox.IsChecked == true && d.toEnumPT == ePublicationType.Educational).ToList();
                p1 = p1.Where(d => PhysicalBox.IsChecked == true && d.PhysicalLocations.Count != 0 || EpubBox.IsChecked == true && !string.IsNullOrWhiteSpace(d.InternetLocation)).ToList();

                if (Course1.IsEnabled)
                    p1 = p1.Where(d => Course1.IsChecked == true && d.Course.Contains(db.DbCourseSet.Find(1)) ||
                                       Course2.IsChecked == true && d.Course.Contains(db.DbCourseSet.Find(2)) ||
                                       Course3.IsChecked == true && d.Course.Contains(db.DbCourseSet.Find(3)) ||
                                       Course4.IsChecked == true && d.Course.Contains(db.DbCourseSet.Find(4))).ToList();

                if (NameBox.IsEnabled)
                    p1 = p1.Where(d => d.Name.ToLower().Contains(NameBox.Text.ToLower())).ToList();

                if (ListAuthor.IsEnabled)
                {
                    var a1 = ListAuthor.SelectedItems.Cast<DbAuthor>().Select(d => d.Id).ToList();
                    var a2 = db.DbAuthorSet1.Where(f => a1.Contains(f.Id)).ToList();
                    p1 = p1.Where(d => d.Authors.Any(h => a2.Any(g => g == h))).ToList();
                }

                if (StartDate.IsEnabled)
                    p1 = p1.Where(d => d.DatePublished >= StartDate.SelectedDate.Value && d.DatePublished <= FinishDate.SelectedDate.Value).ToList();

                if (ListPublishers.IsEnabled)
                {
                    var a1 = ListPublishers.SelectedItems.Cast<string>().ToList();
                    p1 = p1.Where(d => a1.Contains(d.Publisher)).ToList();
                }

                if (ListDisciplines.IsEnabled)
                {
                    var a1 = ListDisciplines.SelectedItems.Cast<DbDiscipline>().Select(d => d.Id).ToList();
                    var a2 = db.DbDisciplineSet.Where(f => a1.Contains(f.Id)).ToList();
                    p1 = p1.Where(d => d.Discipline.Any(h => a2.Contains(h))).ToList();
                }

                
                WindowMain.pPublications.DataGrid.ItemsSource = p1;
            }

            MessageBox.Show("Поиск завершён", "Готово", MessageBoxButton.OK, MessageBoxImage.Information);
            Close();
        }
    }
}
