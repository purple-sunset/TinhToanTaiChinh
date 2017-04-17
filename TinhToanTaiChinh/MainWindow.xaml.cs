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
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnNhap_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnNhapExcel_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog =
                new OpenFileDialog
                {
                    Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*",
                    Title = "Chọn file nhập vào"
                };
            //openFileDialog1.InitialDirectory = @"C:\";

            if (openFileDialog.ShowDialog() == true)
            {
                string fileName = openFileDialog.FileName;
                string filePath = System.IO.Path.GetDirectoryName(fileName);
                DuLieuViewModel dl = new DuLieuViewModel();
                try
                {
                    using (XLWorkbook workBook = new XLWorkbook(fileName))
                    {
                        IXLWorksheet workSheet = workBook.Worksheet(1);

                        dl.Lai = (float) workSheet.Cell("E2").GetDouble();
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
                                temp.Thu = (long)cells.ElementAt(1).GetDouble();
                                temp.Chi = (long)cells.ElementAt(2).GetDouble();
                                dl.ThongSoTable.Add(temp);
                            }
                        }
                        DuLieu form = new DuLieu(dl);
                        form.Show();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }

            }
        }

        private void BtnXuatExcel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
