using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TinhToanTaiChinh.Annotations;
using TinhToanTaiChinh.Model;

namespace TinhToanTaiChinh.ViewModel
{
    public class DuLieuViewModel:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private double[] _heSoLai;

        private Dictionary<String, Object> _chiSoDictionary;
        private ChiSo _chiSo;

        private ObservableCollection<ThongSo> _thongSoTable;
        

        private double _lai;

        public double Lai
        {
            get => _lai;
            set
            {
                if (value.Equals(_lai)) return;
                _lai = value;
                OnPropertyChanged(nameof(Lai));
            }
        }

        public ObservableCollection<ThongSo> ThongSoTable
        {
            get => _thongSoTable;
            set
            {
                if (Equals(value, _thongSoTable)) return;
                _thongSoTable = value;
                OnPropertyChanged(nameof(ThongSoTable));
            }
        }
        public Dictionary<string, object> ChiSoDictionary
        {
            get => _chiSoDictionary;
            set
            {
                if (Equals(value, _chiSoDictionary)) return;
                _chiSoDictionary = value;
                OnPropertyChanged(nameof(ChiSoDictionary));
            }
        }

        public DuLieuViewModel()
        {
            _thongSoTable = new ObservableCollection<ThongSo>();
            _chiSo = new ChiSo(0, 0, 0, 0, 0, 0, 0, 0, 0, "Thang");
            _chiSoDictionary = new Dictionary<string, object>
            {
                {"Giá trị hiện tại ròng", _chiSo.NPV},
                {"Giá trị tương lai ròng", _chiSo.NFV},
                {"Hệ số hoàn trả vốn", _chiSo.CRF},
                {"Hệ số niên kim", _chiSo.AF},
                {"Giá trị tương đương hàng năm", _chiSo.AE},
                {"Tỷ lệ hoàn vốn nội bộ", _chiSo.IRR},
                {"Tỷ lệ thu hồi vốn nội bộ thấp nhất được chấp nhận", _chiSo.MARR},
                {"Thời gian hoàn vốn", _chiSo.PB},
                {"Chỉ số hoàn vốn đầu tư", _chiSo.ROI},
                {"Hệ số thu nhập trên vốn sử dụng", _chiSo.ROCE}
            };
            
        }

        public void UpdateChiSo()
        {
            _chiSoDictionary["Giá trị hiện tại ròng"] = _chiSo.NPV;
            _chiSoDictionary["Giá trị tương lai ròng"] = _chiSo.NFV;
            _chiSoDictionary["Hệ số hoàn trả vốn"] = _chiSo.CRF;
            _chiSoDictionary["Hệ số niên kim"] = _chiSo.AF;
            _chiSoDictionary["Giá trị tương đương hàng năm"] = _chiSo.AE;
            _chiSoDictionary["Tỷ lệ hoàn vốn nội bộ"] = _chiSo.IRR;
            _chiSoDictionary["Tỷ lệ thu hồi vốn nội bộ thấp nhất được chấp nhận"] = _chiSo.MARR;
            _chiSoDictionary["Thời gian hoàn vốn"] = _chiSo.PB;
            _chiSoDictionary["Chỉ số hoàn vốn đầu tư"] = _chiSo.ROI;
            _chiSoDictionary["Hệ số thu nhập trên vốn sử dụng"] = _chiSo.ROCE;
        }

        public void TinhLai()
        {
            int n = ThongSoTable.Max(t=>t.Thang) + 1;
            double tmp = 1 + _lai;
            _heSoLai = new double[n];
            _heSoLai[0] = 1;

            for (int i = 1; i < n; i++)
            {
                _heSoLai[i] = _heSoLai[i-1] * tmp;
            }
        }

        public void TinhThongSo()
        {
            int thang = ThongSoTable.Max(t => t.Thang);
            foreach (var tmp in ThongSoTable)
            {
                tmp.KhoiLuong = tmp.Thu - tmp.Chi;
                if (_heSoLai != null)
                {
                    tmp.PV = Math.Round(tmp.KhoiLuong / _heSoLai[tmp.Thang], 2);
                    tmp.FV = Math.Round(tmp.KhoiLuong * _heSoLai[tmp.Thang], 2);
                    tmp.PB = Math.Round(tmp.KhoiLuong * _heSoLai[tmp.Thang], 2);
                }
                    
            }
        }

        public void TinhChiSo()
        {
            int thang = ThongSoTable.Max(t => t.Thang);
            double[] c = new double[thang+1];
            c[0] = 0.0;
            for (int i = 0; i < ThongSoTable.Count; i++)
            {
                c[ThongSoTable[i].Thang] = ThongSoTable[i].KhoiLuong;
                if (ThongSoTable[i].PB < 0 && ThongSoTable[i + 1].PB > 0)
                    _chiSo.PB = $"{i} - {i + 1} tháng";
            }

            _chiSo.NPVT = ThongSoTable.Sum(t => (t.Thu / _heSoLai[t.Thang]));
            _chiSo.NPVC = ThongSoTable.Sum(t => (t.Chi / _heSoLai[t.Thang]));
            _chiSo.NPV = Math.Round(ThongSoTable.Sum(t => t.PV), 2);
            _chiSo.NFV = Math.Round(ThongSoTable.Sum(t => t.FV), 2);
            _chiSo.CRF =  Math.Round(_lai * _heSoLai[thang] / (_heSoLai[thang] - 1), 3);
            _chiSo.AF = Math.Round(1 / _chiSo.CRF, 3);
            _chiSo.AE = Math.Round(_chiSo.NPV * _chiSo.CRF, 2);
            _chiSo.IRR = Math.Round(Microsoft.VisualBasic.Financial.IRR(ref c), 3);
            _chiSo.MARR = _lai;
            _chiSo.ROI = Math.Round(_chiSo.NPV / _chiSo.NPVC, 3);
            _chiSo.ROCE = Math.Round(_chiSo.NPVT / _chiSo.NPVC, 3);

            

        }

        public void Tinh()
        {
            TinhLai();
            TinhThongSo();
            TinhChiSo();
            UpdateChiSo();
        }
    }
}
