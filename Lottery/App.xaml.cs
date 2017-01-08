using Lottery.Model;
using Lottery.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Lottery
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private LotteryViewModel _viewModel;
        private MainWindow _view;
        private LotteryModel _model;
        public App()
        {
            Startup += AppStartup;
        }

        private void AppStartup(object sender, StartupEventArgs startupEventArgs)
        {
            _model = new LotteryModel();
            _viewModel = new LotteryViewModel()
            {
                Model = _model
            };
            _viewModel.Init();   

            _view = new MainWindow() { DataContext = _viewModel };
            _view.Show();

        }
    }
}
