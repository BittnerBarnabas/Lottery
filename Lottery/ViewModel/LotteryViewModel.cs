using Lottery.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.ViewModel
{
    class LotteryViewModel : AbstractNotifyBase
    {
        private int _roundNumber;
        private int _rows;
        private int _columns;
        public DelegateCommand SelectedGameSize { get; set; }
        public DelegateCommand StartLottery { get; set; }
        public ObservableCollection<LotteryField> Fields { get; set; }
        public int RoundNumber {
            get { return _roundNumber; }
            set
            {
                _roundNumber = value;
                OnPropertyChanged(nameof(RoundNumber));
            }
        }
        public int Rows
        {
            get { return _rows; }
            set
            {
                _rows = value;
                OnPropertyChanged(nameof(Rows));
            }
        }

        public int Columns
        {
            get { return _columns; }
            set
            {
                _columns = value;
                OnPropertyChanged(nameof(Columns));
            }
        }
        public LotteryModel Model { get; set; }
        public List<int> CheckedFields { get; set; }

        private void checkFieldWithNumber(int Number)
        {
            if (!CheckedFields.Contains(Number))
            {
                if (CheckedFields.Count == Model.M) return;

                Fields[Number - 1].FieldStatus = LotteryFieldType.Checked;
                CheckedFields.Add(Number);
            } else
            {
                Fields[Number - 1].FieldStatus = LotteryFieldType.Normal;
                CheckedFields.Remove(Number);
            }
        }

        public LotteryViewModel()
        {
            RoundNumber = 1;
            Fields = new ObservableCollection<LotteryField>();
            CheckedFields = new List<int>();

            SelectedGameSize = new DelegateCommand(param =>
            {
                if ("90".Equals(param))
                {
                    Model.M = 5;
                    Model.N = 90;
                    Rows = 10;
                    Columns = 9;

                } else if("45".Equals(param))
                {
                    Model.M = 6;
                    Model.N = 45;
                    Rows = 5;
                    Columns = 9;
                }
                else
                {
                    Model.M = 7;
                    Model.N = 35;
                    Rows = 5;
                    Columns = 7;
                }
                UpdateTable(new Tuple<int, int>(Rows, Columns));
            });

            StartLottery = new DelegateCommand(param =>
           {
               Model.Rounds = RoundNumber;
               Model.CheckedNumbers = CheckedFields;
               Model.AsyncStart();
           });
        }

        private void UpdateTable(Tuple<int,int> rowColumnNumberTuple)
        {
            CheckedFields.Clear();
            Fields.Clear();
            int index = 1;
            for (int i = 1; i <= rowColumnNumberTuple.Item1; ++i)
            {
                for (int j = 1; j <= rowColumnNumberTuple.Item2; ++j)
                {
                    Fields.Add(new LotteryField
                    {
                        X = i,
                        Y = j,
                        Number = index.ToString(),
                        FieldStatus = LotteryFieldType.Normal,
                        ClickedCommand = new DelegateCommand(param =>
                        {
                            checkFieldWithNumber(Convert.ToInt32(param));
                        })
                    });
                    index++;
                }
            }
        }

        public void Init()
        {
            Model.OnLotteryDone += (sender, args) =>
            {
                foreach (var guessedNumber in args.Item1)
                {
                    Fields[guessedNumber - 1].FieldStatus = LotteryFieldType.Guessed;
                }
                foreach (var missedNumber in args.Item2)
                {
                    Fields[missedNumber - 1].FieldStatus = LotteryFieldType.Missed;
                }
            };
        }
    }
}
