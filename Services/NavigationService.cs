using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf_Reservoom3.Stores;
using Wpf_Reservoom3.ViewModels;

namespace Wpf_Reservoom3.Services
{
    //public class NavigationService
    public class NavigationService<TViewModel> where TViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        //private readonly Func<ViewModelBase> _createViewModel;
        private readonly Func<TViewModel> _createViewModel;

        //public NavigationService(NavigationStore navigationStore, Func<ViewModelBase> createViewModel)
        public NavigationService(NavigationStore navigationStore, Func<TViewModel> createViewModel)
        {
            _navigationStore = navigationStore;
            _createViewModel = createViewModel;
        }

        public void Navigate()
        {
            _navigationStore.CurrentViewModel = _createViewModel();
        }
    }
}
