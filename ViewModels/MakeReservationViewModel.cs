﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;
using Wpf_Reservoom3.Commands;
using Wpf_Reservoom3.Models;
using Wpf_Reservoom3.Services;
using Wpf_Reservoom3.Stores;

namespace Wpf_Reservoom3.ViewModels
{
    public class MakeReservationViewModel : ViewModelBase, INotifyDataErrorInfo
    {
		private string _username;
		public string Username
		{
			get { 
				return _username; 
			}
			set {
                _username = value; 
				OnPropertyChanged(nameof(Username));
			}
		}
		private int _roomNumber;

		public int RoomNumber
		{
			get { return _roomNumber; }
			set { 
				_roomNumber = value;
                OnPropertyChanged(nameof(RoomNumber));
            }
		}
        private int _floorNumber;

        public int FloorNumber
        {
            get { return _floorNumber; }
            set
            {
                _floorNumber = value;
                OnPropertyChanged(nameof(FloorNumber));
            }
        }
        private DateTime _startDate = new DateTime(2025,1,1);

        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                _startDate = value;
                OnPropertyChanged(nameof(StartDate));

                ClearErrors(nameof(StartDate));
                ClearErrors(nameof(EndDate));

                if (EndDate < StartDate)
                {
                    AddError("The start date cannot be after the end date.", nameof(StartDate));
                }
            }
        }
        private DateTime _endDate = new DateTime(2025, 1, 1);

       

        public DateTime EndDate
        {
            get { return _endDate; }
            set
            {
                _endDate = value;
                OnPropertyChanged(nameof(EndDate));

                ClearErrors(nameof(StartDate));
                ClearErrors(nameof(EndDate));

                if (EndDate < StartDate)
                {
                    AddError("The end date cannot be before the start date.", nameof(EndDate));
                }

            }
        }
        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }




        //public MakeReservationViewModel(Hotel hotel, NavigationStore navigationStore, Func<ReservationListingViewModel> createReservationViewModel)
        //public MakeReservationViewModel(Hotel hotel, NavigationService reservationViewNavigationService)
        //{
        //    SubmitCommand = new MakeReservationCommand(this, hotel, reservationViewNavigationService);
        //    //CancelCommand = new NavigateCommand(navigationStore, createReservationViewModel);
        //    CancelCommand = new NavigateCommand(reservationViewNavigationService);

        //}
        //public MakeReservationViewModel(HotelStore hotelStore, NavigationService reservationViewNavigationService)
        //{
        //    SubmitCommand = new MakeReservationCommand(this, hotelStore, reservationViewNavigationService);
        //    CancelCommand = new NavigateCommand(reservationViewNavigationService);
        //}
        public readonly Dictionary<string, List<string>> _propertyNameToErrorsDictionary;
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
        public bool HasErrors => _propertyNameToErrorsDictionary.Any();

        public IEnumerable GetErrors(string? propertyName)
        {
            return _propertyNameToErrorsDictionary.GetValueOrDefault(propertyName, new List<string>());
        }
        private void ClearErrors(string propertyName)
        {
            _propertyNameToErrorsDictionary.Remove(propertyName);

            OnErrorsChanged(propertyName);
        }

        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }
        private void AddError(string errorMessage, string propertyName)
        {
            if (!_propertyNameToErrorsDictionary.ContainsKey(propertyName))
            {
                _propertyNameToErrorsDictionary.Add(propertyName, new List<string>());
            }

            _propertyNameToErrorsDictionary[propertyName].Add(errorMessage);

            OnErrorsChanged(propertyName);
        }

        //public MakeReservationViewModel(HotelStore hotelStore, NavigationService reservationViewNavigationService)
        //{
        //    SubmitCommand = new MakeReservationCommand(this, hotelStore, reservationViewNavigationService);
        //    CancelCommand = new NavigateCommand(reservationViewNavigationService);
        //    _propertyNameToErrorsDictionary = new Dictionary<string, List<string>>();
        //}
        public MakeReservationViewModel(HotelStore hotelStore, NavigationService<ReservationListingViewModel> reservationViewNavigationService)
        {
            SubmitCommand = new MakeReservationCommand(this, hotelStore, reservationViewNavigationService);
            CancelCommand = new NavigateCommand<ReservationListingViewModel>(reservationViewNavigationService);
            _propertyNameToErrorsDictionary = new Dictionary<string, List<string>>();
        }

    }
}
