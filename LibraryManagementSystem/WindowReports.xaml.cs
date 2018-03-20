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
    public partial class WindowReports: Window
    {
        public WindowReports()
        {
            InitializeComponent();
        }
        public WindowReports(Window Owner): this()
        {
            this.Owner = Owner;
        }

        public HashSet<DbAuthor> Authors { get; set; } = new HashSet<DbAuthor>();

        private void This_OnLoaded(object sender, RoutedEventArgs e)
        {

        }
    }
}