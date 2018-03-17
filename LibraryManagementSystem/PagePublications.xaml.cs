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

        public new void UpdateLayout()
        {
            ((Page)this).UpdateLayout();
            DataGrid.ItemsSource = null;
        }

        private void This_OnLoaded(object sender, RoutedEventArgs e)
        {

        }
    }
}