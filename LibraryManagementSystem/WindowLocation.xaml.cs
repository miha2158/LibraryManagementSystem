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
    public partial class WindowLocation: Window
    {
        public WindowLocation()
        {
            InitializeComponent();
        }
        public WindowLocation(Window Owner)
        {
            this.Owner = Owner;
        }

        public HashSet<Reader> Readers { get; set; } = new HashSet<Reader>();

        private void AddReader_OnClick(object sender, RoutedEventArgs e)
        {
            var p = new WindowAddEditUserAuthor(this);
            p.ShowDialog();

            Readers.Add(p.Reader);
            ListReaders.ItemsSource = Readers;
        }

        private void This_OnLoaded(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 5; i++)
                Readers.Add(Reader.FillBlanks());

            ListReaders.ItemsSource = Readers;
        }
    }
}
