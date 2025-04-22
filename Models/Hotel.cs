using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_Reservoom3.Models
{
    public class Hotel
    {
        private readonly ReservationBook _reservationBook;
        public string Name { get; }
        //public Hotel(string name)
        //{
        //    Name = name;
        //    _reservationBook = new ReservationBook();
        //}
        public Hotel(string name, ReservationBook reservationBook)
        {
            Name = name;
            _reservationBook = reservationBook;
        }
        /// <summary>
        /// Get all reservations.
        /// </summary>
        /// <param name="username"></param>
        /// <returns>All reservations in the hotel reservation book.</returns>
        //public IEnumerable<Reservation> GetAllReservations()
        //{
        //    return _reservationBook.GetAllReservations();
        //}

        public async Task<IEnumerable<Reservation>> GetAllReservations()
        {
            return await _reservationBook.GetAllReservations();
        }

        //public void MakeReservation(Reservation reservation)
        //{
        //    _reservationBook.AddReservation(reservation);
        //}
        /// <summary>
        /// Make a reservation.
        /// </summary>
        /// <param name="reservation">The incoming reservation.</param>
        /// <exception cref="ReservationConflictException">Thrown if incoming reservation conflicts with existing reservation.</exception>
        public async Task MakeReservation(Reservation reservation)
        {
            await _reservationBook.AddReservation(reservation);
        }
    }
}
