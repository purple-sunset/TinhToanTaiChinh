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
    public class ThongSoViewModel:INotifyPropertyChanged
    {
        private ObservableCollection<ThongSo> _thongSoTable;
        private ICommand _command;

        public ObservableCollection<ThongSo> ThongSoTable
        {
            get => _thongSoTable;
            set
            {
                if (Equals(value, _thongSoTable)) return;
                _thongSoTable = value;
                OnPropertyChanged();
            }
        }

        public ThongSoViewModel()
        {
            _thongSoTable = new ObservableCollection<ThongSo>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand RemoveCommand
        {
            get
            {
                if (_command == null)
                {
                    _command = new DelegateCommand(CanExecute, Execute);
                }
                return _command;
            }
        }

        private void Execute(object parameter)
        {
            int index = ThongSoTable.IndexOf(parameter as ThongSo);
            if (index > -1 && index < ThongSoTable.Count)
            {
                ThongSoTable.RemoveAt(index);
            }
        }

        private bool CanExecute(object parameter)
        {
            return true;
        }
    }
}
