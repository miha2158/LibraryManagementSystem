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
        public WindowEditLocation(Window Owner, DbBookLocation location)
        {
            EditItem = location;

            PlacesComboBox.SelectedItem = EditItem;
            RoomsBox.SelectedItem = EditItem.Room;
            Place.Text = EditItem.Place;
            ListReaders.SelectedItem = EditItem.Reader;
        }

        public IEnumerable<int> Rooms => DbBookLocation.Rooms;
        
        public DbBookLocation EditItem { get; set; }

        private void This_OnLoaded(object sender, RoutedEventArgs e)
        {

        }

        private void Accept_OnClick(object sender, RoutedEventArgs e)
        {
            EditItem = new DbBookLocation(int.Parse(RoomsBox.Text), Place.Text){IsTaken = ReaderRButton.IsChecked == true, Reader = ListReaders.SelectedItem as DbReader ?? DbReader.All[0]};
        }

        private void AcceptAll_OnClick(object sender, RoutedEventArgs e)
        {

        }

        private void AddUser_OnClick(object sender, RoutedEventArgs e)
        {
            var p2 = new WindowAddEditUserAuthor(this, true);
            p2.ShowDialog();
            DbReader.All.Add(p2.Reader);
            Ex.Lib.SaveChanges();
        }

        private void RoomsBox_OnKeyDown(object sender, KeyEventArgs e)
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

        private void RoomsBox_OnTextInput(object sender, TextCompositionEventArgs e)
        {
            var c = e.Text.Where(char.IsNumber);
            string s = c.Aggregate(string.Empty, (current, d) => current + d);
            e.Handled = true;
            RoomsBox.Text += s;
        }
    }
}