using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Wpf_Reservoom3.Models;
using Wpf_Reservoom3.Stores;
using Wpf_Reservoom3.ViewModels;

namespace Wpf_Reservoom3.Commands
{
    public class LoadReservationsCommand : AsyncCommandBase
    {
        // without HotelStore:
        //private readonly ReservationListingViewModel _viewModel;
        //private readonly Hotel _hotel;

        //public LoadReservationsCommand(ReservationListingViewModel viewModel, Hotel hotel)
        //{
        //    _viewModel = viewModel;
        //    _hotel = hotel;
        //}

        //public override async Task ExecuteAsync(object parameter)
        //{
        //    try
        //    {
        //        IEnumerable<Reservation> reservations = await _hotel.GetAllReservations();

        //        _viewModel.UpdateReservations(reservations);
        //    }
        //    catch (Exception)
        //    {
        //        MessageBox.Show("Failed to load reservations.", "Error",
        //           MessageBoxButton.OK, MessageBoxImage.Error);
        //    }
        //}

        private readonly ReservationListingViewModel _viewModel;
        private readonly HotelStore _hotelStore;

        public LoadReservationsCommand(ReservationListingViewModel viewModel, HotelStore hotelStore)
        {
            _viewModel = viewModel;
            _hotelStore = hotelStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.ErrorMessage = string.Empty;
            _viewModel.IsLoading = true;
            try
            {
                await _hotelStore.Load();
                //throw new Exception();

                _viewModel.UpdateReservations(_hotelStore.Reservations);
            }
            catch (Exception)
            {
                //MessageBox.Show("Failed to load reservations.", "Error",
                //   MessageBoxButton.OK, MessageBoxImage.Error);
                _viewModel.ErrorMessage = "Failed to load reservations.";
            }
            finally
            {
                _viewModel.IsLoading = false;
            }
        }
    }
}
