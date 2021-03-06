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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LibraryManagementSystem
{
    public partial class WindowLogin: Window
    {
        public WindowLogin()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            var window = new WindowMain();
            window.Show();
            Close();
        }

        private void LoginKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter)
                return;
            
            if (Login.Text != string.Empty && Password.Password != string.Empty)
                Login_Click(null, null);

            ((Control)sender).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            Login.Focus();
        }
    }
}