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
    public partial class WindowEditLocation: Window
    {
        public WindowEditLocation()
        {
            InitializeComponent();
        }
        public WindowEditLocation(Window Owner) : this()
        {
            this.Owner = Owner;
        }

        public Publication DisplayItem { get; set; } = new Publication();

        private void This_OnLoaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
