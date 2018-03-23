using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LibraryManagementSystem
{
    public partial class PageAuthors: Page
    {
        public PageAuthors()
        {
            InitializeComponent();
        }

        public Window Owner;

        public new void UpdateLayout()
        {
            ((Page)this).UpdateLayout();
            using (var db = new LibraryDBContainer())
            {
                DataGrid.ItemsSource = db.DbAuthorSet1.ToList();
            }
        }

        private void This_OnLoaded(object sender, RoutedEventArgs e)
        {
            UpdateLayout();
        }

        private void ContextMenu_OnOpened(object sender, RoutedEventArgs e)
        {
            if (DataGrid.SelectedCells.Count == 0)
                (sender as ContextMenu).IsOpen = false;
        }

        private void Edit_OnClick(object sender, RoutedEventArgs e)
        {
            var item = DataGrid.SelectedItem as DbAuthor;
            var p = new WindowAddEditUserAuthor(Owner, item);
            p.ShowDialog();
            UpdateLayout();
        }

        private void Delete_OnClick(object sender, RoutedEventArgs e)
        {
            var item = DataGrid.SelectedItem as DbAuthor;

            using (var db = new LibraryDBContainer())
            {
                item = db.DbAuthorSet1.Find(item.Id);

                var p = db.Entry(item);
                p.State = EntityState.Deleted;
                
                db.DbAuthorSet1.Local.Remove(item);
                db.SaveChanges();
            }

            UpdateLayout();
        }
    }
}
