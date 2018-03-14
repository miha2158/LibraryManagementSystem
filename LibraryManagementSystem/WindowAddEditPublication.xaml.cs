using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
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

        public Reader[] Readers { get; set; } = new Reader[0];

        private void AddEditPublication_OnLoaded(object sender, RoutedEventArgs e)
        {
            Readers = new Reader[5];

             for (int i = 0; i < Readers.Length; i++)
                Readers[i] = Reader.FillBlanks();

            List1.ItemsSource = Readers;
        }

        private void Add_OnClick(object sender, RoutedEventArgs e)
        {
            Readers = Readers.Add(Reader.FillBlanks());
            List1.ItemsSource = Readers;
        }
        
        private void Refresh_OnClick(object sender, RoutedEventArgs e)
        {
        }
    }
}