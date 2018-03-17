using System;
using System.Collections.Generic;
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

        public HashSet<Author> Authors { get; set; } = new HashSet<Author>();

        private void This_OnLoaded(object sender, RoutedEventArgs e)
        {

        }

        private void AddAuthor_OnClick(object sender, RoutedEventArgs e)
        {
            var p = new WindowAddEditUserAuthor(this, false);
            p.ShowDialog();
            Authors.Add(p.Author);
        }
    }
}