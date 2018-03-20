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
            
            if (pPublications == null)
                pPublications = new PagePublications();
            if (pUsers == null)
                pUsers = new PageUsers();
            if (pAuthors == null)
            pAuthors = new PageAuthors();
        }

        public Page OnScreenContent;
        public static PagePublications pPublications;
        public static PageUsers pUsers;
        public static PageAuthors pAuthors;

        private void ToPagePublications(object sender, RoutedEventArgs e) => NavigateTo(pPublications);
        private void ToPageUsers(object sender, RoutedEventArgs e) => NavigateTo(pUsers);
        private void ToPageAuthors(object sender, RoutedEventArgs e) => NavigateTo(pAuthors);

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
            switch (OnScreenContent)
            {
                case PageAuthors pageAuthors:
                {
                    break;
                }
                case PagePublications pagePublications:
                {
                    var p = new WindowAddEditPublication(this);
                    p.ShowDialog();
                    break;
                }
                case PageUsers pageUsers:
                {
                    break;
                }
            }
        }

        private void EditPublication(object sender, RoutedEventArgs e)
        {

        }

        private void Reports_OnClick(object sender, RoutedEventArgs e)
        {
            var p = new WindowReports(this);
            p.ShowDialog();
        }

        private void Filter_OnClick(object sender, RoutedEventArgs e)
        {
            var p = new WindowLocation(this);
            p.ShowDialog();
        }

        private void WindowMain_OnGotFocus(object sender, RoutedEventArgs e)
        {
            pPublications.Owner = this;
            pAuthors.Owner = this;
            pUsers.Owner = this;
        }
    }
}