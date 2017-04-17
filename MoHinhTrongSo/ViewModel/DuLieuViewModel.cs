using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MoHinhTrongSo.Annotations;

namespace MoHinhTrongSo.ViewModel
{
    public class DuLieuViewModel:INotifyPropertyChanged
    {
        
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private DataTable _thongSoTable;
        

        public DataTable ThongSoTable
        {
            get => _thongSoTable;
            set => _thongSoTable = value;
        }

        

        public DuLieuViewModel()
        {
            _thongSoTable = new DataTable();
            ThongSoTable.Columns.Add("Tiêu Chí", typeof(string));
            ThongSoTable.Columns.Add("Trọng Số", typeof(double));
            ThongSoTable.Columns.Add("Dự Án 1", typeof(double));
        }

        public DuLieuViewModel(int n)
        {
            _thongSoTable = new DataTable();
        }

        public DuLieuViewModel(DataTable dt)
        {
            _thongSoTable = dt;
        }

        public void Tinh()
        {
            DataRow[] xoa = ThongSoTable.Select(ThongSoTable.Columns[0].ColumnName + "='Tổng'");
            foreach (DataRow drow in xoa)
            {
                drow.Delete();
            }
            ThongSoTable.AcceptChanges();

            int n = ThongSoTable.Columns.Count - 1;
            double[] tong = new double[n];
            foreach (DataRow item in ThongSoTable.Rows)
            {
                Console.WriteLine(item.ItemArray[1]);
                var trongSo = (double) item.ItemArray[1];
                tong[0] += trongSo;
                for (int i = 1; i < n; i++)
                {
                    tong[i] += trongSo * (double) item.ItemArray[i+1];
                }
            }
            DataRow row = ThongSoTable.Rows.Add();
            row[0] = "Tổng";
            for (int i = 0; i < n; i++)
            {
                row[i+1] = tong[i];
            }
        }
    }
}
