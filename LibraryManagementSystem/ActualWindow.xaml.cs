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

        public Page OnScreenContent;
        public PagePublications P1 = new PagePublications();
        public PageUsers P2 = new PageUsers();

        private void ToPage0(object sender, RoutedEventArgs e) => NavigateTo(P1);
        private void ToPage1(object sender, RoutedEventArgs e) => NavigateTo(P2);

        public void NavigateTo(Page destinationPage)
        {
            MainView.Navigate(OnScreenContent = destinationPage);
        }

        private void ActualWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            NavigateTo(P1);
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
            if(e.Key != Key.Enter)
                return;

        }

        private void New_OnClick(object sender, RoutedEventArgs e)
        {
            var p = new NewPublication();
            p.Show();
        }
    }
}