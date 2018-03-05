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
    /// <summary>
    /// Interaction logic for AddPublication.xaml
    /// </summary>
    public partial class AddPublication: Page
    {
        public AddPublication()
        {
            InitializeComponent();
        }

        private void SearchBox_OnKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key != Key.Enter)
                return;

            MessageBox.Show("how are you?", "hi!", MessageBoxButton.OK, MessageBoxImage.Question);
        }

        private void AddPublication_OnLoaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
