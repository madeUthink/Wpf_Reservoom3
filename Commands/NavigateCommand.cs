using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using Wpf_Reservoom3.Stores;
using Wpf_Reservoom3.ViewModels;
using Wpf_Reservoom3.Services;


namespace Wpf_Reservoom3.Commands
{
    //public class NavigateCommand: CommandBase
    public class NavigateCommand<TViewModel> : CommandBase where TViewModel : ViewModelBase
    {
       //private readonly NavigationStore _navigationStore;
        //private readonly Func<ViewModelBase> _createViewModel;

        //public NavigateCommand(NavigationStore navigationStore, Func<ViewModelBase> createViewModel)
        //{
        //    _navigationStore = navigationStore;
        //    _createViewModel = createViewModel;
        //}

        //public override void Execute(object parameter)
        //{
        //    //_navigationStore.CurrentViewModel = new MakeReservationViewModel(new Models.Hotel(""));
        //    _navigationStore.CurrentViewModel = _createViewModel();
        //}
        //private readonly NavigationService _navigationService;

        //public NavigateCommand(NavigationService navigationService)
        //{
        //    _navigationService = navigationService;
        //}
        private readonly NavigationService<TViewModel> _navigationService;

        public NavigateCommand(NavigationService<TViewModel> navigationService)
        {
            _navigationService = navigationService;
        }

        public override void Execute(object parameter)
        {
            _navigationService.Navigate();
        }
        
    }
}
