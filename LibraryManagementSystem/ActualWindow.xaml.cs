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
    public partial class ActualWindow : Window
    {
        public ActualWindow()
        {
            InitializeComponent();
        }

        public TestPage TestPage = new TestPage();
        public AddPublication AddPublication = new AddPublication();

        private void ToPage0(object sender, RoutedEventArgs e) => NavigateTo(TestPage);
        private void ToPage1(object sender, RoutedEventArgs e) => NavigateTo(AddPublication);

        public Page OnScreenContent;
        public void NavigateTo(Page destinationPage)
        {
            MainView.Navigate(OnScreenContent = destinationPage);
        }

        private void ActualWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            NavigateTo(AddPublication);
        }

    }
}
