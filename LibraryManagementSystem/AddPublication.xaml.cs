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
using Generator;

namespace LibraryManagementSystem
{
    public partial class AddPublication : Window
    {
        public AddPublication()
        {
            InitializeComponent();
        }

        List<Reader> Items1 = new List<Reader>(0);

        public void Click1(object sender, RoutedEventArgs e)
        {
            Items1.Add(Reader.FillBlanks((Gender)NewValue.Int(2)));
        }

        private void AddPublication_OnLoaded(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 10; i++)
                Items1.Add(Reader.FillBlanks((Gender)NewValue.Int(2)));

            List.ItemsSource = Items1;
        }

        private void Refresh_OnClick(object sender, RoutedEventArgs e)
        {
            List.ItemsSource = Items1;
        }
    }
}