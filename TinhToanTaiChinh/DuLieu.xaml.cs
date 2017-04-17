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
using ClosedXML.Excel;
using Microsoft.Win32;
using TinhToanTaiChinh.ViewModel;

namespace TinhToanTaiChinh
{
    /// <summary>
    /// Interaction logic for DuLieu.xaml
    /// </summary>
    public partial class DuLieu : Window
    {
        private DuLieuViewModel data;
        public DuLieu()
        {
            data = new DuLieuViewModel();
            InitializeComponent();
            this.DataContext = data;
            
        }

        public DuLieu(DuLieuViewModel dl)
        {
            data = dl;
            InitializeComponent();
            this.DataContext = data;
            
            Console.WriteLine("");
        }

        private void BtnXuat_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*",
                Title = "Chọn file xuất ra",
            };
            

            if (saveFileDialog.ShowDialog() == true)
            {
                string fileName = saveFileDialog.FileName;
                

                try
                {
                    using (XLWorkbook workBook = new XLWorkbook())
                    {
                        var ws1 = workBook.Worksheets.Add("Dữ liệu");
                        var ws2 = workBook.Worksheets.Add("Kết quả");

                        var table = ws1.Cell(1, 1).InsertTable(data.ThongSoTable);
                        table.Field(0).Name = "Tháng";
                        table.Field(1).Name = "Thu được (USD)";
                        table.Field(2).Name = "Chi phí (USD)";
                        table.Field(3).Name = "Khối lượng dòng tiền mặt (USD)";
                        table.Field(4).Name = "Giá trị quy về hiện tại PV (USD)";
                        table.Field(5).Name = "Giá trị quy về tương lai FV (USD)";
                        table.Field(6).Name = "PB (USD)";
                        ws1.Cell("I1").Value = "Lãi Suất";
                        ws1.Cell("I2").Value = data.Lai;
                        ws1.Columns().AdjustToContents();

                        var list = ws2.Cell(1, 1).InsertTable(data.ChiSoDictionary);
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

        private void BtnTinh_Click(object sender, RoutedEventArgs e)
        {
            data.Tinh();
            TabControl.SelectedIndex = 1;
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
