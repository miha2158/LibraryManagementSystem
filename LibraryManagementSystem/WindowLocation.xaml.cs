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
            Display = item;
        }

        public DbPublication Display;

        private void This_OnLoaded(object sender, RoutedEventArgs e)
        {
            using (var db = new LibraryDBContainer())
            {
                Display = db.DbPublicationSet1.Find(Display.Id);

                UriBox.Text = Display.InternetLocation;
                ListPlaces.ItemsSource = Display.PhysicalLocations.Where(d => !d.IsTaken);
                var t = Display.PhysicalLocations.Where(d => d.IsTaken).Select(d => d.Reader);
                ListReaders.ItemsSource = t;
            }
        }

        private void Edit_OnClick(object sender, RoutedEventArgs e)
        {
            var p = new WindowEditLocation(Owner, Display, Display.DCount);
            p.ShowDialog();
            This_OnLoaded(null, null);
        }
    }
}
