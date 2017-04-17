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
using TinhToanTaiChinh.Model;
using TinhToanTaiChinh.ViewModel;

namespace TinhToanTaiChinh
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
            check = form.ShowDialog()??false;
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
                duLieu = new DuLieuViewModel();
                try
                {
                    using (XLWorkbook workBook = new XLWorkbook(fileName))
                    {
                        IXLWorksheet workSheet = workBook.Worksheet(1);

                        duLieu.Lai = workSheet.Cell("E2").GetDouble();
                        //Loop through the Worksheet rows.
                        bool firstRow = true;
                        foreach (IXLRow row in workSheet.Rows())
                        {
                            //Use the first row to add columns to DataTable.
                            if (firstRow)
                            {
                                foreach (IXLCell cell in row.Cells("1:3"))
                                {
                                    //dl.ThuChi.Columns.Add(cell.Value.ToString());
                                }
                                firstRow = false;
                            }
                            else
                            {
                                //Add rows to DataTable.
                                ThongSo temp = new ThongSo();
                                
                                IXLCells cells = row.Cells("1:3");
                                temp.Thang = (int) cells.ElementAt(0).GetDouble();
                                temp.Thu = (long) cells.ElementAt(1).GetDouble();
                                temp.Chi = (long) cells.ElementAt(2).GetDouble();
                                duLieu.ThongSoTable.Add(temp);
                            }
                        }
                        duLieu.Tinh();
                        form = new DuLieu(duLieu);
                        check = form.ShowDialog()??false;
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
                        var ws2 = workBook.Worksheets.Add("Kết quả");
                        
                        var table = ws1.Cell(1,1).InsertTable(duLieu.ThongSoTable);
                        table.Field(0).Name = "Tháng";
                        table.Field(1).Name = "Thu được (USD)";
                        table.Field(2).Name = "Chi phí (USD)";
                        table.Field(3).Name = "Khối lượng dòng tiền mặt (USD)";
                        table.Field(4).Name = "Giá trị quy về hiện tại PV (USD)";
                        table.Field(5).Name = "Giá trị quy về tương lai FV (USD)";
                        table.Field(6).Name = "PB (USD)";
                        ws1.Cell("I1").Value = "Lãi Suất";
                        ws1.Cell("I2").Value = duLieu.Lai;
                        ws1.Columns().AdjustToContents();

                        var list = ws2.Cell(1, 1).InsertTable(duLieu.ChiSoDictionary);
                        list.Field(0).Name = "Chỉ số";
                        list.Field(1).Name = "Giá trị";
                        ws2.Columns().AdjustToContents();

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
            if(check)
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
