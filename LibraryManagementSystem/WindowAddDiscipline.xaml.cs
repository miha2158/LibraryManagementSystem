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
    public partial class WindowAddDiscipline: Window
    {
        public WindowAddDiscipline()
        {
            InitializeComponent();
        }

        public WindowAddDiscipline(Window Owner) : this()
        {
            this.Owner = Owner;
        }

        private void This_OnLoaded(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            using (var db = new LibraryDBContainer())
            {
                db.DbDisciplineSet.Add(new DbDiscipline {Name = NewDiscipline.Text});
                db.SaveChanges();
            }
            Close();
        }
    }
}