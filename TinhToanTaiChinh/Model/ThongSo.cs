using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TinhToanTaiChinh.Annotations;

namespace TinhToanTaiChinh.Model
{
    public class ThongSo:INotifyPropertyChanged
    {
        private int _thang;
        private long _thu;
        private long _chi;
        private long _khoiLuong;
        private float _pv;
        private float _fv;
        private float _pb;

        public int Thang
        {
            get => _thang;
            set
            {
                if (value == _thang) return;
                _thang = value;
                OnPropertyChanged(nameof(Thang));
            }
        }

        public long Thu
        {
            get => _thu;
            set
            {
                if (value == _thu) return;
                _thu = value;
                OnPropertyChanged(nameof(Thu));
            }
        }

        public long Chi
        {
            get => _chi;
            set
            {
                if (value == _chi) return;
                _chi = value;
                OnPropertyChanged(nameof(Chi));
            }
        }

        public long KhoiLuong
        {
            get => _khoiLuong;
            set
            {
                if (value == _khoiLuong) return;
                _khoiLuong = value;
                OnPropertyChanged(nameof(KhoiLuong));
            }
        }

        public float PV
        {
            get => _pv;
            set
            {
                if (value.Equals(_pv)) return;
                _pv = value;
                OnPropertyChanged(nameof(PV));
            }
        }

        public float FV
        {
            get => _fv;
            set
            {
                if (value.Equals(_fv)) return;
                _fv = value;
                OnPropertyChanged(nameof(FV));
            }
        }

        public float PB
        {
            get => _pb;
            set
            {
                if (value.Equals(_pb)) return;
                _pb = value;
                OnPropertyChanged(nameof(PB));
            }
        }


        public ThongSo()
        {
            
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
