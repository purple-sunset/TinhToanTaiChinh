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

        }

        private void BtnTinh_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
