﻿using System;
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

            RoomsBox.ItemsSource = DbBookLocation.Rooms;
            ListReaders.ItemsSource = DbReader.All;
        }
        public WindowEditLocation(Window Owner) : this()
        {
            this.Owner = Owner;
        }
        public WindowEditLocation(Window Owner, DbPublication pub, int number = 0): this(Owner)
        {
            EditItem = pub;

            using (var db = new LibraryDBContainer())
            {
                EditItem = db.DbPublicationSet1.Find(EditItem.Id) ?? pub;
                if (EditItem.PhysicalLocations == null || EditItem.PhysicalLocations.Count == 0)
                    for (int i = 0; i < number; i++)
                        PlacesComboBox.Items.Add(i + 1);
                else PlacesComboBox.ItemsSource = EditItem.PhysicalLocations.Select(e => e.ToString().Clone()).ToArray();
            }
        }
        
        public DbPublication EditItem { get; set; }

        private void This_OnLoaded(object sender, RoutedEventArgs e)
        {
            PlacesComboBox.SelectedIndex = 0;
        }

        private void Accept_OnClick(object sender, RoutedEventArgs e)
        {
            //PlacesComboBox.Items.Remove(PlacesComboBox.SelectedItem);
            using (var db = new LibraryDBContainer())
            {
                EditItem = db.DbPublicationSet1.Find(EditItem.Id);

                int.TryParse(RoomsBox.Text, out int num);

                DbBookLocation p;
                if (EditItem.PhysicalLocations.ElementAt(PlacesComboBox.SelectedIndex) == null)
                    p = new DbBookLocation(num, Place.Text)
                    {
                        IsTaken = ReaderRButton.IsChecked == true,
                        Reader = ListReaders.SelectedIndex != -1 ? db.DbReaderSet.Find((ListReaders.SelectedItem as DbReader)?.Id) : null,
                        Publication = EditItem
                    };
                else
                {
                    p = EditItem.PhysicalLocations.ElementAt(PlacesComboBox.SelectedIndex);
                    p.Room = num;
                    p.Place = Place.Text;
                    p.IsTaken = ReaderRButton.IsChecked == true;
                    p.Reader = ListReaders.SelectedIndex != -1 ? db.DbReaderSet.Find((ListReaders.SelectedItem as DbReader)?.Id) : null;
                }

                if (ReaderRButton.IsChecked == true)
                    db.DbStatsSet.Add(new DbStats
                    {
                        DateTaken = DateTime.Now,
                        Publication = EditItem,
                    });

                db.DbBookLocationSet.Local.Add(p);
                db.SaveChanges();
            }


            if (PlacesComboBox.SelectedIndex == PlacesComboBox.Items.Count)
                Close();
            PlacesComboBox.SelectedIndex++;
        }
        private void AcceptAll_OnClick(object sender, RoutedEventArgs e)
        {
            using (var db = new LibraryDBContainer())
            {
                EditItem = db.DbPublicationSet1.Find(EditItem.Id);
                for (int i = 0; i < PlacesComboBox.Items.Count; i++)
                {
                    db.DbBookLocationSet.Add(new DbBookLocation(int.Parse(RoomsBox.Text), Place.Text)
                    {
                        IsTaken = ReaderRButton.IsChecked == true,
                        Reader = db.DbReaderSet.Find((ListReaders.SelectedItem as DbReader).Id),
                        Publication = EditItem
                    });
                    db.SaveChanges();
                }
            }

            Close();
        }

        private void AddUser_OnClick(object sender, RoutedEventArgs e)
        {
            var p2 = new WindowAddEditUserAuthor(this, true);
            p2.ShowDialog();
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
                case Key.Tab:
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

        public bool IsReady => RoomRButton.IsChecked == true && !string.IsNullOrWhiteSpace(RoomsBox.Text) && !string.IsNullOrWhiteSpace(Place.Text) ||
                               ReaderRButton.IsChecked == true && ListReaders.SelectedIndex != -1;

        private void PlacesComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PlacesComboBox.Items.Count == 1)
                AcceptAll.Visibility = Visibility.Collapsed;

            using (var db = new LibraryDBContainer())
            {
                var t = db.DbPublicationSet1.Find(EditItem.Id).PhysicalLocations
                          .ElementAt(PlacesComboBox.SelectedIndex);
                t = db.DbBookLocationSet.Find(t.Id);
                RoomsBox.Text = t.Room.ToString();
                Place.Text = t.Place;

                try
                {
                    ListReaders.SelectedIndex = db.DbReaderSet.Select(d => d.Id).ToList().IndexOf(t.Reader.Id);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                }

                if (t.IsTaken)
                    ReaderRButton.IsChecked = true;
                else
                    RoomRButton.IsChecked = true;
            }
        }

        private void WindowEditLocation_OnMouseMove(object sender, MouseEventArgs e)
        {
            Accept.IsEnabled = IsReady;
            AcceptAll.IsEnabled = Accept.IsEnabled;
        }
    }
}