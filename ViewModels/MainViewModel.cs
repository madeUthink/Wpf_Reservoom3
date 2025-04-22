using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf_Reservoom3.Models;
using Wpf_Reservoom3.Stores;

namespace Wpf_Reservoom3.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        // public ViewModelBase CurrentViewModel { get;  }
        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;
      
        public MainViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }
        //~MainViewModel()
        //{ 
        //}
        //public override void Dispose()
        //{
        //    _navigationStore.CurrentViewModelChanged -= OnCurrentViewModelChanged;
        //    base.Dispose();
        //}
        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
        //public MainViewModel(Hotel hotel)
        //{
        //    //CurrentViewModel = new MakeReservationViewModel();
        //    //CurrentViewModel = new ReservationListingViewModel();
        //    // CurrentViewModel = new MakeReservationViewModel(hotel);
        //    CurrentViewModel = new ReservationListingViewModel();
        //}
    }
}
