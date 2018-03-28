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
    public partial class WindowLocation: Window
    {
        public WindowLocation()
        {
            InitializeComponent();
        }
        protected WindowLocation(Window Owner):this()
        {
            this.Owner = Owner;
        }
        public WindowLocation(Window Owner, DbPublication item): this(Owner)
        {
            using (var db = new LibraryDBContainer())
            {
                Display = db.DbPublicationSet1.Find(item.Id);
                ListPlaces.ItemsSource = Display.PhysicalLocations.Where(e => !e.IsTaken);
                ListReaders.ItemsSource = Display.PhysicalLocations.Where(e => e.IsTaken).Select(e => e.Reader);
                UriBox.Text = Display.InternetLocation;
            }
        }

        public DbPublication Display;

        private void This_OnLoaded(object sender, RoutedEventArgs e)
        {
        }

        private void Edit_OnClick(object sender, RoutedEventArgs e)
        {
            var p = new WindowEditLocation(Owner, Display, Display.DCount);
            p.ShowDialog();
        }
    }
}
