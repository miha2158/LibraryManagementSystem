using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Generator;

namespace LibraryManagementSystem
{
    public partial class WindowAddEditPublication: Window
    {
        public WindowAddEditPublication()
        {
            InitializeComponent();
        }
        public WindowAddEditPublication(Window Owner): this()
        {
            this.Owner = Owner;
        }

        public HashSet<DbAuthor> Authors { get; set; } = new HashSet<DbAuthor>();

        private void This_OnLoaded(object sender, RoutedEventArgs e)
        {

        }
        private void AddAuthor_OnClick(object sender, RoutedEventArgs e)
        {
            var p = new WindowAddEditUserAuthor(this, false);
            p.ShowDialog();
            Authors.Add(p.Author);
        }

        private void PublicationRButton_OnUnchecked(object sender, RoutedEventArgs e)
        {
            PublicationSpecific.Visibility = Visibility.Collapsed;
        }
        private void PublicationRButton_OnChecked(object sender, RoutedEventArgs e)
        {
            PublicationSpecific.Visibility = Visibility.Visible;
        }

        private void PubNumberTextBlock_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            PubNumber.Focus();
        }
        private void PubNumber_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            PubNumberTextBlock.Visibility = PubNumber.Text.Length == 0 ? Visibility.Visible : Visibility.Collapsed;
        }
        private void EpubAdressTextBlock_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            EpubAdress.Focus();
        }
        private void EpubAdress_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            EpubAdressTextBlock.Visibility = EpubAdress.Text.Length == 0 ? Visibility.Visible : Visibility.Collapsed;
        }

        private void LocationButton_OnClick(object sender, RoutedEventArgs e)
        {
            var p = new WindowEditLocation(this);
            p.ShowDialog();
        }

        private void PubNumber_OnKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.D0:
                case Key.D1:
                case Key.D2:
                case Key.D3:
                case Key.D4:
                case Key.D5:
                case Key.D6:
                case Key.D7:
                case Key.D8:
                case Key.D9:
                case Key.NumPad0:
                case Key.NumPad1:
                case Key.NumPad2:
                case Key.NumPad3:
                case Key.NumPad4:
                case Key.NumPad5:
                case Key.NumPad6:
                case Key.NumPad7:
                case Key.NumPad8:
                case Key.NumPad9:
                case Key.Home:
                case Key.Delete:
                case Key.End:
                case Key.Right:
                case Key.Left:
                    break;
                default:
                    e.Handled = true;
                    return;
            }
        }
        private void PubNumber_OnTextInput(object sender, TextCompositionEventArgs e)
        {
            var c = e.Text.Where(char.IsNumber);
            var s = c.Aggregate(string.Empty, (current, d) => current + d);
            e.Handled = true;
            PubNumber.Text += s;
        }

    }
}