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

        private Dictionary<String, Object> _chiSoDictionary;
        private ChiSo _chiSo;

        private ObservableCollection<ThongSo> _thongSoTable;
        

        private float _lai;

        public float Lai
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
            UpdateChiSo();
        }

        public void UpdateChiSo()
        {
            
        }
        
    }
}
