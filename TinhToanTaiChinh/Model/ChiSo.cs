using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TinhToanTaiChinh.Annotations;

namespace TinhToanTaiChinh.Model
{
    class ChiSo:INotifyPropertyChanged
    {
        
        private float _npv;
        private float _npvt;
        private float _npvc;
        private float _nfv;
        private float _crf;
        private float _af;
        private float _ae;
        private float _irr;
        private float _marr;
        private float _roi;
        private float _roce;
        private string _pb;


        public ChiSo(float npv, float nfv, float crf, float af, float ae, float irr, float marr, float roi, float roce, string pb)
        {
            _npvc = 0F;
            _npvt = 0F;
            _npv = npv;
            _nfv = nfv;
            _crf = crf;
            _af = af;
            _ae = ae;
            _irr = irr;
            _marr = marr;
            _roi = roi;
            _roce = roce;
            _pb = pb;
        }

        public float NPV
        {
            get => _npv;
            set
            {
                if (value.Equals(_npv)) return;
                _npv = value;
                OnPropertyChanged(nameof(NPV));
            }
        }

        public float NPVT
        {
            get => _npvt;
            set
            {
                if (value.Equals(_npvt)) return;
                _npvt = value;
                OnPropertyChanged(nameof(NPVT));
            }
        }

        public float NPVC
        {
            get => _npvc;
            set
            {
                if (value.Equals(_npvc)) return;
                _npvc = value;
                OnPropertyChanged(nameof(NPVC));
            }
        }

        public float NFV
        {
            get => _nfv;
            set
            {
                if (value.Equals(_nfv)) return;
                _nfv = value;
                OnPropertyChanged(nameof(NFV));
            }
        }

        public float CRF
        {
            get => _crf;
            set
            {
                if (value.Equals(_crf)) return;
                _crf = value;
                OnPropertyChanged(nameof(CRF));
            }
        }

        public float AF
        {
            get => _af;
            set
            {
                if (value.Equals(_af)) return;
                _af = value;
                OnPropertyChanged(nameof(AF));
            }
        }

        public float AE
        {
            get => _ae;
            set
            {
                if (value.Equals(_ae)) return;
                _ae = value;
                OnPropertyChanged(nameof(AE));
            }
        }

        public float IRR
        {
            get => _irr;
            set
            {
                if (value.Equals(_irr)) return;
                _irr = value;
                OnPropertyChanged(nameof(IRR));
            }
        }

        public float MARR
        {
            get => _marr;
            set
            {
                if (value.Equals(_marr)) return;
                _marr = value;
                OnPropertyChanged(nameof(MARR));
            }
        }

        public float ROI
        {
            get => _roi;
            set
            {
                if (value.Equals(_roi)) return;
                _roi = value;
                OnPropertyChanged(nameof(ROI));
            }
        }

        public float ROCE
        {
            get => _roce;
            set
            {
                if (value.Equals(_roce)) return;
                _roce = value;
                OnPropertyChanged(nameof(ROCE));
            }
        }

        public string PB
        {
            get => _pb;
            set
            {
                if (value == _pb) return;
                _pb = value;
                OnPropertyChanged(nameof(PB));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        
    }
}
