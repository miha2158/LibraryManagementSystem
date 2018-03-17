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
    public partial class WindowMain: Window
    {
        public WindowMain()
        {
            InitializeComponent();
        }

        public Page OnScreenContent;
        public static PagePublications pPublications = new PagePublications();
        public static PageUsers pUsers = new PageUsers();

        private void ToPagePublications(object sender, RoutedEventArgs e) => NavigateTo(pPublications);
        private void ToPageUsers(object sender, RoutedEventArgs e) => NavigateTo(pUsers);

        public void NavigateTo(Page destinationPage)
        {
            destinationPage.UpdateLayout();
            MainView.Navigate(OnScreenContent = destinationPage);
        }

        private void ActualWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            NavigateTo(pPublications);
        }

        private void SearchBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            DummySearchText.Visibility = SearchBox.Text.Length == 0 ? Visibility.Visible : Visibility.Collapsed;
        }

        private void DummySearchText_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            SearchBox.Focus();
        }

        private void SearchBox_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter)
                return;

        }

        private void NewPublication(object sender, RoutedEventArgs e)
        {
            var p = new WindowAddEditPublication(this);
            p.ShowDialog();
        }

        private void EditPublication(object sender, RoutedEventArgs e)
        {

        }
    }
}