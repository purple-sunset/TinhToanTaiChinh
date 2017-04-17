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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ClosedXML.Excel;
using Microsoft.Win32;
using MoHinhTrongSo.Model;
using MoHinhTrongSo.ViewModel;


namespace MoHinhTrongSo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DuLieuViewModel duLieu;
        private DuLieu form;
        private bool check;
        private string filePath = "";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnNhap_Click(object sender, RoutedEventArgs e)
        {
            duLieu = new DuLieuViewModel();
            form = new DuLieu(duLieu);
            check = form.ShowDialog() ?? false;
        }

        private void BtnNhapExcel_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog =
                new OpenFileDialog
                {
                    Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*",
                    Title = "Chọn file nhập vào",

                };
            if (filePath.Length > 0)
                openFileDialog.InitialDirectory = filePath;

            if (openFileDialog.ShowDialog() == true)
            {
                string fileName = openFileDialog.FileName;
                filePath = System.IO.Path.GetDirectoryName(fileName);
                duLieu = new DuLieuViewModel(0);
                try
                {
                    using (XLWorkbook workBook = new XLWorkbook(fileName))
                    {
                        IXLWorksheet workSheet = workBook.Worksheet(1);

                        
                        //Loop through the Worksheet rows.
                        bool firstRow = true;
                        foreach (IXLRow row in workSheet.Rows())
                        {
                            //Use the first row to add columns to DataTable.
                            if (firstRow)
                            {
                                int j = 0;
                                foreach (IXLCell cell in row.Cells())
                                {
                                    switch (j)
                                    {
                                        case 0:
                                            duLieu.ThongSoTable.Columns.Add(cell.Value.ToString(), typeof(string));
                                            break;

                                        case 1:
                                            duLieu.ThongSoTable.Columns.Add(cell.Value.ToString(), typeof(double));
                                            break;

                                        default:
                                            duLieu.ThongSoTable.Columns.Add(cell.Value.ToString(), typeof(double));
                                            break;
                                    }
                                    
                                    j++;
                                }
                                firstRow = false;
                            }
                            else
                            {
                                //Add rows to DataTable.
                                duLieu.ThongSoTable.Rows.Add();
                                int i = 0;
                                foreach (IXLCell cell in row.Cells())
                                {
                                    switch (i)
                                    {
                                        case 0:
                                            duLieu.ThongSoTable.Rows[duLieu.ThongSoTable.Rows.Count - 1][i] = cell.Value.ToString();
                                            break;

                                        case 1:
                                            duLieu.ThongSoTable.Rows[duLieu.ThongSoTable.Rows.Count - 1][i] = cell.GetDouble();
                                            break;

                                        default:
                                            duLieu.ThongSoTable.Rows[duLieu.ThongSoTable.Rows.Count - 1][i] = cell.GetDouble();
                                            break;
                                    }
                                    
                                    i++;
                                }
                            }
                        }
                        duLieu.Tinh();
                        form = new DuLieu(duLieu);
                        check = form.ShowDialog() ?? false;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    ShowFail();
                }

            }
        }

        private void BtnXuatExcel_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*",
                Title = "Chọn file xuất ra",
            };
            if (filePath.Length > 0)
                saveFileDialog.InitialDirectory = filePath;

            if (saveFileDialog.ShowDialog() == true)
            {
                string fileName = saveFileDialog.FileName;
                filePath = System.IO.Path.GetDirectoryName(fileName);

                try
                {
                    using (XLWorkbook workBook = new XLWorkbook())
                    {
                        var ws1 = workBook.Worksheets.Add("Dữ liệu");
                        

                        var table = ws1.Cell(1, 1).InsertTable(duLieu.ThongSoTable);

                        table.Column(2).Style.NumberFormat.NumberFormatId = 9;
                        ws1.Columns().AdjustToContents();

                        

                        workBook.SaveAs(fileName);
                        ShowOk();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    ShowFail();
                }

            }
        }

        private void MainWindow_OnClosed(object sender, EventArgs e)
        {
            if (check)
                try
                {
                    form.Close();
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    throw;
                }
        }

        private void ShowOk()
        {
            string messageBoxText = "Thành công";
            string caption = "Thành công";
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Information;
            MessageBox.Show(messageBoxText, caption, button, icon);
        }

        private void ShowFail()
        {
            string messageBoxText = "Thất bại";
            string caption = "Thất bại";
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Error;
            MessageBox.Show(messageBoxText, caption, button, icon);
        }
    }
}
