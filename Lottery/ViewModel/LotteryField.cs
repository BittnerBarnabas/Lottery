using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.ViewModel
{
    class LotteryField : AbstractNotifyBase
    {
        private string _number;
        private LotteryFieldType _status;
        public int X { get; set; }
        public int Y { get; set; }
        public string Number {
            get
            {
                return _number;
            }
            set
            {
                _number = value;
                OnPropertyChanged(nameof(Number));
            }
        }

        public LotteryFieldType FieldStatus
        {
            get { return _status; }
            set
            {
                _status = value;
                OnPropertyChanged(nameof(FieldStatus));
            }
        }

        public DelegateCommand ClickedCommand { get; set; }

    }
}
