using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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

        public List<DbAuthor> Authors { get; set; } = DbAuthor.All;

        private void This_OnLoaded(object sender, RoutedEventArgs e)
        {
            StartDate.SelectedDate = DateTime.MinValue;
            FinishDate.SelectedDate = DateTime.Today;
            AuthorList.SelectAll();
            AuthorList.ItemsSource = Authors.Where(d => d.WriterType == eWriterType.HseTeacher.e());
            ReportType.SelectedIndex = 0;
        }

        private void Accept_OnClick(object sender, RoutedEventArgs e)
        {

            //var dialog = new SaveFileDialog
            //{
            //    AddExtension = true,
            //    DefaultExt = ".xlsx",
            //    DereferenceLinks = true,
            //    Filter = @"Excel files (*.xlsx)|*.xlsx| All Files|*"
            //};
            //DialogResult r = dialog.ShowDialog();

            //if (r == System.Windows.Forms.DialogResult.Cancel || r == System.Windows.Forms.DialogResult.None)
            //    return;

            var app = new Application
            {
                DisplayAlerts = true,
                Visible = true,
            };
            var wbook = app.Workbooks.Add(1);
            Worksheet wsheet = wbook.Worksheets[1];
            wsheet.Name = "Отчёт";

            using (var db = new LibraryDBContainer())
            {


                var result = db.DbPublicationSet1
                               .ToArray()
                               .Where((d) =>
                                      {
                                          bool a = BookCheck.IsChecked == true;
                                          bool b = PubCheck.IsChecked == true;

                                          if (a && b)
                                              return true;
                                          if (a)
                                              return d.BookPublication == eBookPublication.Book.e();
                                          if (b)
                                              return d.BookPublication == eBookPublication.Publication.e();
                                          return false;
                                      })
                               .ToArray();

                switch (ReportType.SelectedIndex)
                {
                    case 0:
                    {
                        break;
                    }
                    case 1:
                    {
                        var t = new Func<DbStats, bool>(f => (f.DateTaken.Date >= StartDate.SelectedDate.Value &&
                                                              f.DateTaken <= FinishDate.SelectedDate.Value) ||
                                                             PeriodButton.IsChecked == false);

                        var res = result.Where(d => d.Stats.Count > 0)
                                        .OrderBy(d => d.Stats.Count(t))
                                        .Reverse()
                                        .ToArray();

                        wsheet.Cells[1, 1] = "Название";
                        wsheet.Cells[1, 2] = "Авторы";
                        wsheet.Cells[1, 3] = "Взято раз";

                        for (int i = 0; i < res.Length; i++)
                        {
                            var el = res[i];

                            wsheet.Cells[i + 2, 1] = el.Name;
                            wsheet.Cells[i + 2, 2] =
                                string.Join("\n", el.Authors.Select(d => d.ToString()));
                            wsheet.Cells[i + 2, 3] = $"{el.Stats.Count(t)}";
                        }

                        wsheet.Columns.ColumnWidth = 255;
                        wsheet.Rows.RowHeight = 255;
                        wsheet.Columns.AutoFit();
                        wsheet.Rows.AutoFit();

                        break;
                    }
                    case 2:
                    {
                        var res = result.SelectMany(d => d.PhysicalLocations)
                                        .Where(d => d.IsTaken)
                                        .ToArray();

                        wsheet.Cells[1, 1] = "Название";
                        wsheet.Cells[1, 2] = "Авторы";
                        wsheet.Cells[1, 3] = "Взявший";


                        for (int i = 0; i < res.Length; i++)
                        {
                            var el = res[i];

                            wsheet.Cells[i + 2, 1] = el.Publication.Name;
                            wsheet.Cells[i + 2, 2] =
                                string.Join("\n", el.Publication.Authors.Select(d => d.ToString()));
                            wsheet.Cells[i + 2, 3] = $"{el.Reader}, {el.Reader.Group}";
                        }

                        wsheet.Columns.ColumnWidth = 255;
                        wsheet.Rows.RowHeight = 255;
                        wsheet.Columns.AutoFit();
                        wsheet.Rows.AutoFit();

                        break;
                    }
                }
            }

            //wbook.Save();
            //wbook.Close();
            //app.Quit();
            //Marshal.ReleaseComObject(wsheet);
            //Marshal.ReleaseComObject(wbook);
            //Marshal.ReleaseComObject(app);
        }
    }
}