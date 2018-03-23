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
    public partial class PagePublications: Page
    {
        public PagePublications()
        {
            InitializeComponent();
        }

        public Window Owner;

        public new void UpdateLayout()
        {
            ((Page)this).UpdateLayout();
            DataGrid.ItemsSource = DbPublication.All;
        }

        private void This_OnLoaded(object sender, RoutedEventArgs e)
        {
            UpdateLayout();
        }

        private void Delete_OnClick(object sender, RoutedEventArgs e)
        {
            DbPublication item = DataGrid.SelectedItem as DbPublication;

            using (var db = new LibraryDBContainer())
            {
                db.DbPublicationSet1.Remove(db.DbPublicationSet1.Find(item.Id));
                db.SaveChanges();
            }

            UpdateLayout();
        }

        private void Edit_OnClick(object sender, RoutedEventArgs e)
        {
            var item = DataGrid.SelectedItem as DbPublication;
            var p = new WindowAddEditPublication(Owner, item);
            p.ShowDialog();
            UpdateLayout();
        }

        private void ContextMenu_OnOpened(object sender, RoutedEventArgs e)
        {
            if (DataGrid.SelectedCells.Count == 0)
                (sender as ContextMenu).IsOpen = false;
        }

        private void Location_OnClick(object sender, RoutedEventArgs e)
        {
            var item = DataGrid.SelectedItem as DbPublication;
            var p = new WindowLocation(Owner, item);
            p.ShowDialog();
            UpdateLayout();
        }
    }
}