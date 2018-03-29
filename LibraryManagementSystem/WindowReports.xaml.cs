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
            StartDate.SelectedDate = DateTime.Today.Subtract(new TimeSpan(365, 0, 0, 0));
            FinishDate.SelectedDate = DateTime.Today;
            //AuthorList.SelectAll();
            AuthorList.ItemsSource = Authors.OrderBy(d => d.WriterType);
            ReportType.SelectedIndex = 0;
        }

        private void Accept_OnClick(object sender, RoutedEventArgs e)
        {
            using (var db = new LibraryDBContainer())
            {
                //выбраны книги или публиакции
                bool bCheck = BookCheck.IsChecked == true;
                bool pCheck = PubCheck.IsChecked == true;

                var result = db.DbPublicationSet1.ToArray();
                result = result.Where(d =>
                                      {
                                          if (bCheck && pCheck)
                                              return true;
                                          if (bCheck)
                                              return d.BookPublication == eBookPublication.Book.e();
                                          if (pCheck)
                                              return d.BookPublication == eBookPublication.Publication.e();
                                          return false;
                                      }).ToArray();
                {
                    //выбор выделенных авторов
                    var a1 = AuthorList.SelectedItems.Cast<DbAuthor>().Select(g => g.Id).ToList();
                    var a2 = db.DbAuthorSet1.Where(f => a1.Contains(f.Id)).ToList();
                    result = result.Where(d => d.Authors.Any(h => a2.Any(g => g == h))).ToArray();
                }

                //открытие excel
                var app = new Application
                {
                    DisplayAlerts = true,
                    Visible = true,
                };
                var wbook = app.Workbooks.Add(1);
                Worksheet wsheet = wbook.Worksheets[1];
                wsheet.Name = "Отчёт";

                switch (ReportType.SelectedIndex)
                {
                    case 0:
                    {
                        //выбор только преподавателей
                        var res = result.Where(d => d.Authors.Any(f => f.toEnumWT == eWriterType.HseTeacher))
                                         //выбор дат публикации
                                        .Where(d => PeriodButton.IsChecked == false ||
                                                    d.DatePublished >= StartDate.SelectedDate.Value &&
                                                    d.DatePublished <= FinishDate.SelectedDate.Value.AddDays(1))
                                         //сотрировка по книгам/публикациям
                                        .OrderByDescending(d => d.BookPublication)
                                         //сортировка по дате публикации
                                        .ThenBy(d => d.DDate)
                                        .ToArray();

                        wsheet.Cells[1, 1] = "Название";
                        wsheet.Cells[1, 2] = "Авторы";
                        wsheet.Cells[1, 3] = "Опубликовано";

                        //запись в excel
                        for (int i = 0, offset = 2; i < res.Length; i++)
                        {
                            if (i == 0 || res[i].BookPublication != res[i - 1].BookPublication)
                                wsheet.Cells[i + offset++, 1] = $"{res[i].toEnumBP}s";

                            var authors = res[i].Authors
                                                .OrderBy(d => d.WriterType)
                                                .Select(d => d.ToString())
                                                .ToArray();

                            wsheet.Cells[i + offset, 1] = res[i].Name;
                            wsheet.Cells[i + offset, 2] = string.Join("\n", authors);
                            wsheet.Cells[i + offset, 3] = $"{res[i].DDate}";

                        }

                        break;
                    }
                    case 1:
                    {
                        //отчёт по популярности
                        //выбор по датам выдачи
                        var selectDate = new Func<DbStats, bool>(f => PeriodButton.IsChecked == false ||
                                                                      f.DateTaken.Date >= StartDate.SelectedDate.Value &&
                                                                      f.DateTaken <= FinishDate.SelectedDate.Value.AddDays(1));

                        //выбор выданных хоть раз
                        var res = result.Where(d => d.Stats.Count > 0)
                                         //сортировка по количеству выдач
                                        .OrderBy(d => d.Stats.Count(selectDate))
                                        .Reverse()
                                        .ToArray();

                        wsheet.Cells[1, 1] = "Название";
                        wsheet.Cells[1, 2] = "Авторы";
                        wsheet.Cells[1, 3] = "Взято раз";

                        //запись в excel
                        for (int i = 0; i < res.Length; i++)
                        {
                            var authors = res[i].Authors
                                                .OrderBy(d => d.WriterType)
                                                .Select(d => d.ToString())
                                                .ToArray();

                            wsheet.Cells[i + 2, 1] = res[i].Name;
                            wsheet.Cells[i + 2, 2] = string.Join("\n", authors);
                            wsheet.Cells[i + 2, 3] = res[i].Stats.Count(selectDate);
                        }

                        break;
                    }
                    case 2:
                    {
                        //отчёт по не возвращённым книгам
                        //выбор выданных книг
                        var res = result.SelectMany(d => d.PhysicalLocations)
                                        .Where(d => d.IsTaken)
                                         //сортировка по последней выдаче
                                        .OrderBy(d => d.Publication.Stats.Last().DateTaken.ToNiceDate())
                                        .ToArray();

                        wsheet.Cells[1, 1] = "Название";
                        wsheet.Cells[1, 2] = "Авторы";
                        wsheet.Cells[1, 3] = "Взявший";
                        wsheet.Cells[1, 4] = "Взято";

                        //запись в excel
                        for (int i = 0; i < res.Length; i++)
                        {
                            var el = res[i];

                            wsheet.Cells[i + 2, 1] = el.Publication.Name;
                            wsheet.Cells[i + 2, 2] =
                                string.Join("\n", el.Publication.Authors.Select(d => d.ToString()));
                            wsheet.Cells[i + 2, 3] = $"{el.Reader}, {el.Reader.Group}";
                            wsheet.Cells[i + 2, 4] = el.Publication.Stats.Last().DateTaken;
                        }

                        break;
                    }
                }

                        wsheet.Columns.ColumnWidth = 255;
                        wsheet.Rows.RowHeight = 255;
                        wsheet.Columns.AutoFit();
                        wsheet.Rows.AutoFit();

                app.Visible = true;
            }
        }
    }
}