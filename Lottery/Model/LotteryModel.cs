using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.Model
{
    public class LotteryModel
    {
		public int N { get; set; }
		public int M { get; set; }
		public int Rounds { get; set; }
        public List<int> CheckedNumbers { get; set; }

		public event EventHandler<Tuple<List<int>, List<int>>> OnLotteryDone;


        public LotteryModel()
        {

        }

		public async void AsyncStart()
        {
			for (int i = 0; i < Rounds; ++i)
            {
                List<int> chosenNumbers = new List<int>();
                var generator = new Random(DateTime.Now.GetHashCode());
				for(int j = 0; j < M; ++j)
                {
                    chosenNumbers.Add(generator.Next(1, N));
                }
				//1: guessed 2: missed
                var pair = new Tuple<List<int>, List< int >> (new List<int>(), new List<int>());
				foreach(int number in chosenNumbers)
                {
                    if (CheckedNumbers.Contains(number))
                    {
                        pair.Item1.Add(number);
                    } else
                    {
                        pair.Item2.Add(number);
                    }
                }
                OnLotteryDone.Invoke(this, pair);
                await Task.Delay(1000);
            }
        }
    }
}
