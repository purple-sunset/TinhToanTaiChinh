using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TinhToanTaiChinh.Annotations;
using TinhToanTaiChinh.Model;

namespace TinhToanTaiChinh.ViewModel
{
    class ChiSoViewModel:INotifyPropertyChanged
    {
        private Dictionary<String, Object> _chiSoDictionary;
        private ChiSo _chiSo;

        public Dictionary<string, object> ChiSoDictionary
        {
            get => _chiSoDictionary;
            set
            {
                if (Equals(value, _chiSoDictionary)) return;
                _chiSoDictionary = value;
                OnPropertyChanged();
            }
        }

        public ChiSo ChiSo
        {
            get => _chiSo;
            set
            {
                if (Equals(value, _chiSo)) return;
                _chiSo = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ChiSoViewModel()
        {
            _chiSo = new ChiSo(0,0,0,0,0,0,0,0,0,"Thang");
            _chiSoDictionary = new Dictionary<string, object>();
            Update();
        }

        public void Update()
        {
            _chiSoDictionary.Add("Giá trị hiện tại ròng", _chiSo.NPV);
            _chiSoDictionary.Add("Giá trị tương lai ròng", _chiSo.NFV);
            _chiSoDictionary.Add("Hệ số hoàn trả vốn", _chiSo.CRF);
            _chiSoDictionary.Add("Hệ số niên kim", _chiSo.AF);
            _chiSoDictionary.Add("Giá trị tương đương hàng năm", _chiSo.AE);
            _chiSoDictionary.Add("Tỷ lệ hoàn vốn nội bộ", _chiSo.IRR);
            _chiSoDictionary.Add("Tỷ lệ thu hồi vốn nội bộ thấp nhất được chấp nhận", _chiSo.MARR);
            _chiSoDictionary.Add("Thời gian hoàn vốn", _chiSo.PB);
            _chiSoDictionary.Add("Chỉ số hoàn vốn đầu tư", _chiSo.ROI);
            _chiSoDictionary.Add("Hệ số thu nhập trên vốn sử dụng", _chiSo.ROCE);
        }
    }
}
