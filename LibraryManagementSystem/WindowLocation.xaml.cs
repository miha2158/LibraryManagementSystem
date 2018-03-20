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
        public WindowLocation(Window Owner):this()
        {
            this.Owner = Owner;
        }

        public Publication Display = new Publication();

        public HashSet<Reader> Readers
        {
            get { return Display.Readers; }
            set { Display.Readers = value; }
        }
        public IEnumerable<BookLocation> Locations => locations.Where(r => !r.IsTaken);
        private HashSet<BookLocation> locations => Display.PhysicalLocations;
        

        private void This_OnLoaded(object sender, RoutedEventArgs e)
        {

        }

        private void Edit_OnClick(object sender, RoutedEventArgs e)
        {
            var p = new WindowEditLocation(this);
            p.DisplayItem = Display;
            p.ShowDialog();
        }
    }
}
