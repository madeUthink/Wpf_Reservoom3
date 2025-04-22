using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Wpf_Reservoom3.Commands;
using Wpf_Reservoom3.Models;
using Wpf_Reservoom3.Services;
using Wpf_Reservoom3.Stores;

namespace Wpf_Reservoom3.ViewModels
{
    public class ReservationListingViewModel : ViewModelBase
    {
        private readonly HotelStore _hotelStore;

        // ObservableCollection for ListView :: Change ObservableCollection<Reservation> into ObservableCollection<ReservationViewModel>
        // But binding to an object that does not implement INotifyPropertyChanged could result in memory leaks
        private readonly ObservableCollection<ReservationViewModel> _reservations;
        //private static Hotel _hotel;

        public IEnumerable<ReservationViewModel> Reservations => _reservations;

        public MakeReservationViewModel MakeReservationViewModel { get; } // for reactivating of HotelStore
        private string _errorMessage;
        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));

                OnPropertyChanged(nameof(HasErrorMessage));
            }
        }
        public bool HasErrorMessage => !string.IsNullOrEmpty(ErrorMessage);

        private bool _isLoading;

        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }

        public ICommand LoadReservationsCommand { get; }
        public ICommand MakeReservationCommand { get; }

        //public ReservationListingViewModel(Hotel hotel, NavigationService makeReservationNavigationService)
        //public ReservationListingViewModel(HotelStore hotelStore,  NavigationService makeReservationNavigationService)
        //{
        //    //_hotel = hotel;
        //    _reservations = new ObservableCollection<ReservationViewModel>();

        //    //LoadReservationsCommand = new LoadReservationsCommand(this, hotel);
        //    LoadReservationsCommand = new LoadReservationsCommand(this, hotelStore);
        //    MakeReservationCommand = new NavigateCommand(makeReservationNavigationService);
        //   // UpdateReservations();
        //}
      
        public ReservationListingViewModel(HotelStore hotelStore, NavigationService<MakeReservationViewModel> makeReservationNavigationService)
        {
            _hotelStore = hotelStore;
            _reservations = new ObservableCollection<ReservationViewModel>();
            

            LoadReservationsCommand = new LoadReservationsCommand(this, hotelStore);
            MakeReservationCommand = new NavigateCommand<MakeReservationViewModel>(makeReservationNavigationService);

            _hotelStore.ReservationMade += OnReservationMode;
        }
        //~ReservationListingViewModel()
        //{ // put here a breakpoint ..after a while you will get here

        //}
        public override void Dispose()
        {
            _hotelStore.ReservationMade -= OnReservationMode;
            base.Dispose();
        }
        private void OnReservationMode(Reservation reservation)
        {
            ReservationViewModel reservationViewModel = new ReservationViewModel(reservation);
            _reservations.Add(reservationViewModel);
        }
        public static ReservationListingViewModel LoadViewModel(HotelStore hotelStore,NavigationService<MakeReservationViewModel> makeReservationNavigationService)
        {
    
            ReservationListingViewModel viewModel = new ReservationListingViewModel(hotelStore, makeReservationNavigationService);

            viewModel.LoadReservationsCommand.Execute(null);

            return viewModel;
        }
        public void UpdateReservations(IEnumerable<Reservation> reservations)
        {
            _reservations.Clear();

            foreach (Reservation reservation in reservations)
            {
                ReservationViewModel reservationViewModel = new ReservationViewModel(reservation);
                _reservations.Add(reservationViewModel);
            }
        }
        //public void UpdateReservations()
        //{
        //    _reservations.Clear();

        //    foreach (Reservation reservation in _hotel.GetAllReservations())
        //    {
        //        ReservationViewModel reservationViewModel = new ReservationViewModel(reservation);
        //        _reservations.Add(reservationViewModel);
        //    }
        //}
        //public void UpdateReservations(IEnumerable<Reservation> reservations)
        //{
        //    _reservations.Clear();

        //    foreach (Reservation reservation in reservations)
        //    {
        //        ReservationViewModel reservationViewModel = new ReservationViewModel(reservation);
        //        _reservations.Add(reservationViewModel);
        //    }
        //}

    }
}
