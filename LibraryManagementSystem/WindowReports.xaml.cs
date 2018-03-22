using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using Window = System.Windows.Window;
using Microsoft.Office.Interop.Excel;
using Application = Microsoft.Office.Interop.Excel.Application;

namespace LibraryManagementSystem
{
    public partial class WindowReports: Window
    {
        public WindowReports()
        {
            InitializeComponent();
        }
        public WindowReports(System.Windows.Window Owner): this()
        {
            this.Owner = Owner;
        }

        public HashSet<DbAuthor> Authors { get; set; } = new HashSet<DbAuthor>();

        private void This_OnLoaded(object sender, RoutedEventArgs e)
        {

        }

        private void Accept_OnClick(object sender, RoutedEventArgs e)
        {

            var dialog = new SaveFileDialog
            {
                AddExtension = true,
                DefaultExt = ".xlsx",
                DereferenceLinks = true,
                Filter = @"Excel files (*.xlsx)|*.xlsx| All Files|*"
            };
            DialogResult r = dialog.ShowDialog();

            if (r == System.Windows.Forms.DialogResult.Cancel || r == System.Windows.Forms.DialogResult.None)
                return;

            var app = new Application
            {
                DisplayAlerts = false,
            };

            using (var s = new FileStream(dialog.FileName, FileMode.Create)) ;
            var wbook = app.Workbooks.Open(dialog.FileName);
            Worksheet wsheet = wbook.ActiveSheet;

            switch (ReportType.SelectedIndex)
            {
                case 1:
                {
                    break;
                }
                case 2:
                {
                    var result = DbPublication.All.AsParallel().OrderBy(d => d.Stats.Count(f => f.DateTaken > DateTime.Today.Subtract(new TimeSpan(365, 0, 0, 0)))).ToArray();

                    wsheet.Cells[0, 0] = "Название";
                    wsheet.Cells[0, 1] = "Авторы";
                    wsheet.Cells[0, 2] = "Взято раз";

                    for (int i = 0; i < result.Length; i++)
                    {
                        var el = result[i];

                        wsheet.Cells[i, 0] = el.Name;
                        wsheet.Cells[i, 1] = string.Join("\n", el.Authors.AsParallel().Select(d => d.ToString()));
                        wsheet.Cells[i, 2] = $"{el.Stats.Count(f => f.DateTaken > DateTime.Today.Subtract(new TimeSpan(365, 0, 0, 0)))}";
                    }


                    break;
                }
                case 3:
                {
                    var result = DbBookLocation.All.AsParallel().Where(d => d.IsTaken).ToArray();

                    wsheet.Cells[0, 0] = "Название";
                    wsheet.Cells[0, 1] = "Авторы";
                    wsheet.Cells[0, 2] = "Взявший";


                    for (int i = 1; i < result.Length; i++)
                    {
                        var el = result[i];

                        wsheet.Cells[i, 0] = el.Publication.Name;
                        wsheet.Cells[i, 1] = string.Join("\n", el.Publication.Authors.AsParallel().Select(d => d.ToString()));
                        wsheet.Cells[i, 2] = $"{el.Reader}, {el.Reader.Group}";
                    }

                    break;
                }
            }
        }
    }
}