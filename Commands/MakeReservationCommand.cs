using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Wpf_Reservoom3.Exceptions;
using Wpf_Reservoom3.Models;
using Wpf_Reservoom3.Services;
using Wpf_Reservoom3.Stores;
using Wpf_Reservoom3.ViewModels;

namespace Wpf_Reservoom3.Commands
{
    //public class MakeReservationCommand : CommandBase
    //{
    //    private readonly MakeReservationViewModel _makeReservationViewModel;
    //    private readonly Hotel _hotel;
    //    private readonly NavigationService _reservationViewNavigationService;

    //    public MakeReservationCommand(MakeReservationViewModel makeReservationViewModel, Hotel hotel, NavigationService reservationViewNavigationService)
    //    {
    //        _makeReservationViewModel = makeReservationViewModel;
    //        _hotel = hotel;
    //        _reservationViewNavigationService = reservationViewNavigationService;
    //        _makeReservationViewModel.PropertyChanged += OnViewModelPropertyChanged;
    //    }

    //    public override bool CanExecute(object parameter)
    //    {
    //        return !string.IsNullOrEmpty(_makeReservationViewModel.Username) &&
    //            _makeReservationViewModel.FloorNumber > 0 &&
    //            base.CanExecute(parameter);
    //    }

    //    public override void Execute(object parameter)
    //    {
    //        Reservation reservation = new Reservation(
    //            new RoomID(_makeReservationViewModel.FloorNumber, _makeReservationViewModel.RoomNumber),
    //            _makeReservationViewModel.Username,
    //            _makeReservationViewModel.StartDate,
    //            _makeReservationViewModel.EndDate);

    //        try
    //        {
    //            _hotel.MakeReservation(reservation);

    //            MessageBox.Show("Successfully reserved room.", "Success",
    //                MessageBoxButton.OK, MessageBoxImage.Information);

    //            _reservationViewNavigationService.Navigate();
    //        }
    //        catch (ReservationConflictException)
    //        {
    //            MessageBox.Show("This room is already taken.", "Error",
    //                MessageBoxButton.OK, MessageBoxImage.Error);
    //        }
    //    }

    //    private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
    //    {
    //        if (e.PropertyName == nameof(MakeReservationViewModel.Username) ||
    //            e.PropertyName == nameof(MakeReservationViewModel.FloorNumber))
    //        {
    //            OnCanExecutedChanged();
    //        }
    //    }
    public class MakeReservationCommand : AsyncCommandBase
    {
        private readonly MakeReservationViewModel _makeReservationViewModel;
        private readonly HotelStore _hotelStore;
        private readonly NavigationService<ReservationListingViewModel> _reservationViewNavigationService;

        public MakeReservationCommand(MakeReservationViewModel makeReservationViewModel,
            HotelStore hotelStore,
            NavigationService<ReservationListingViewModel> reservationViewNavigationService)
        {
            _makeReservationViewModel = makeReservationViewModel;
            _hotelStore = hotelStore;
            _reservationViewNavigationService = reservationViewNavigationService;

            _makeReservationViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }
        public override bool CanExecute(object parameter)
        {
            return !string.IsNullOrEmpty(_makeReservationViewModel.Username) &&
                _makeReservationViewModel.FloorNumber > 0 &&
                base.CanExecute(parameter); // submit button is disabled as long as username is empty
        }
        public override async Task ExecuteAsync(object parameter)
        {
            Reservation reservation = new Reservation(
                new RoomID(_makeReservationViewModel.FloorNumber, _makeReservationViewModel.RoomNumber),
                _makeReservationViewModel.Username,
                _makeReservationViewModel.StartDate,
                _makeReservationViewModel.EndDate);

            try
            {
                //await _hotel.MakeReservation(reservation);
                await _hotelStore.MakeReservation(reservation);

                MessageBox.Show("Successfully reserved room.", "Success",
                    MessageBoxButton.OK, MessageBoxImage.Information);

                _reservationViewNavigationService.Navigate();
            }
            catch (ReservationConflictException)
            {
                MessageBox.Show("This room is already taken.", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to make reservation.", "Error",
                   MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MakeReservationViewModel.Username) ||
                e.PropertyName == nameof(MakeReservationViewModel.FloorNumber))
            {
                OnCanExecutedChanged();
            }
        }

    }
}
