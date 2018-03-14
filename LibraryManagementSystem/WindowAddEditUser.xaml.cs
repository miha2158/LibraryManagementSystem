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
    public partial class WindowAddEditUser: Window
    {
        public WindowAddEditUser()
        {
            InitializeComponent();
        }
        public WindowAddEditUser(Window Owner): this()
        {
            this.Owner = Owner;
        }

        private void AddEditUser_OnLoaded(object sender, RoutedEventArgs e)
        {

        }

        private void EventSetter_OnHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                ((TextBox) sender).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
        }
    }
}