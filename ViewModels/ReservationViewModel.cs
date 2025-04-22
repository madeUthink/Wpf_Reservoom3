using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf_Reservoom3.Models;

namespace Wpf_Reservoom3.ViewModels
{
    public class ReservationViewModel: ViewModelBase
    {
        private readonly Reservation _reservation;

        // calculated values
        public string RoomID => _reservation.RoomID?.ToString();
        public string Username => _reservation.Username;
        public string StartDate => _reservation.StartDate.ToString("d");
        public string EndDate => _reservation.EndDate.ToString("d");
        public ReservationViewModel(Reservation reservation)
        {
            _reservation = reservation;
        }
    }
}
