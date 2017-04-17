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
        
        private double _npv;
        private double _npvt;
        private double _npvc;
        private double _nfv;
        private double _crf;
        private double _af;
        private double _ae;
        private double _irr;
        private double _marr;
        private double _roi;
        private double _roce;
        private string _pb;


        public ChiSo(double npv, double nfv, double crf, double af, double ae, double irr, double marr, double roi, double roce, string pb)
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

        public double NPV
        {
            get => _npv;
            set
            {
                if (value.Equals(_npv)) return;
                _npv = value;
                OnPropertyChanged(nameof(NPV));
            }
        }

        public double NPVT
        {
            get => _npvt;
            set
            {
                if (value.Equals(_npvt)) return;
                _npvt = value;
                OnPropertyChanged(nameof(NPVT));
            }
        }

        public double NPVC
        {
            get => _npvc;
            set
            {
                if (value.Equals(_npvc)) return;
                _npvc = value;
                OnPropertyChanged(nameof(NPVC));
            }
        }

        public double NFV
        {
            get => _nfv;
            set
            {
                if (value.Equals(_nfv)) return;
                _nfv = value;
                OnPropertyChanged(nameof(NFV));
            }
        }

        public double CRF
        {
            get => _crf;
            set
            {
                if (value.Equals(_crf)) return;
                _crf = value;
                OnPropertyChanged(nameof(CRF));
            }
        }

        public double AF
        {
            get => _af;
            set
            {
                if (value.Equals(_af)) return;
                _af = value;
                OnPropertyChanged(nameof(AF));
            }
        }

        public double AE
        {
            get => _ae;
            set
            {
                if (value.Equals(_ae)) return;
                _ae = value;
                OnPropertyChanged(nameof(AE));
            }
        }

        public double IRR
        {
            get => _irr;
            set
            {
                if (value.Equals(_irr)) return;
                _irr = value;
                OnPropertyChanged(nameof(IRR));
            }
        }

        public double MARR
        {
            get => _marr;
            set
            {
                if (value.Equals(_marr)) return;
                _marr = value;
                OnPropertyChanged(nameof(MARR));
            }
        }

        public double ROI
        {
            get => _roi;
            set
            {
                if (value.Equals(_roi)) return;
                _roi = value;
                OnPropertyChanged(nameof(ROI));
            }
        }

        public double ROCE
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
