using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Wpf_Reservoom3.Exceptions;
using Wpf_Reservoom3.Services.ReservationConflictValidators;
using Wpf_Reservoom3.Services.ReservationCreators;
using Wpf_Reservoom3.Services.ReservationProviders;

namespace Wpf_Reservoom3.Models
{
    public class ReservationBook
    {
        private readonly IReservationProvider _reservationProvider;
        private readonly IReservationCreator _reservationCreator;
        private readonly IReservationConflictValidator  _reservationConflictValidator;

       

        private readonly List<Reservation> _reservations;
        public ReservationBook()
        {
            _reservations = new List<Reservation>();
        }

        public ReservationBook(IReservationProvider reservationProvider, IReservationCreator reservationCreator, IReservationConflictValidator reservationConflictValidator)
        {
            _reservationProvider = reservationProvider;
            _reservationCreator = reservationCreator;
            _reservationConflictValidator = reservationConflictValidator;
        }

        /// <summary>
        /// Get reservations for a user.
        /// </summary>
        /// <param name="username"></param>
        /// <returns>All reservations for a user.</returns>
        public IEnumerable<Reservation> GetReservationsForUser(string username)
        {
            return _reservations.Where(r => r.Username == username);
        }

        /// <summary>
        /// Get all reservations.
        /// </summary>
        /// <returns>Get all reservations in the reservation book.</returns>
        //public IEnumerable<Reservation> GetAllReservations()
        //{
        //    return _reservations;
        //}
        public async Task<IEnumerable<Reservation>> GetAllReservations()
        {
            return await _reservationProvider.GetAllReservations();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reservation"></param>
        /// <exception cref="ReservationConflictException"></exception>
        //public void AddReservation(Reservation reservation)
        //{
        //    foreach (Reservation existingReservation in _reservations)
        //    {
        //        if (existingReservation.Conflicts(reservation))
        //        {
        //            throw new ReservationConflictException(existingReservation, reservation);
        //        }
        //    }
        //    _reservations.Add(reservation);
        //}
        public async Task AddReservation(Reservation reservation)
        {
            Reservation conflictingReservation = await _reservationConflictValidator.GetConflictingReservation(reservation);

            if (conflictingReservation != null)
            {
                throw new ReservationConflictException(conflictingReservation, reservation);
            }

            await _reservationCreator.CreateReservation(reservation);
        }
        //public async Task AddReservation(Reservation reservation)
        //{
        //    Reservation conflictingReservation = await _reservationConflictValidator.GetConflictingReservation(reservation);

        //    if (conflictingReservation != null)
        //    {
        //        throw new ReservationConflictException(conflictingReservation, reservation);
        //    }

        //    await _reservationCreator.CreateReservation(reservation);
        //}
    }
    //public static class ExtendClass 
    //{ 
    //    public static TaskAwaiter GetAwaiter(this AsyncOperation asyncOp) 
    //    { 
    //        var tcs = new TaskCompletionSource(); 
    //        asyncOp.completed += obj => { tcs.SetResult(null); }; 
    //        return ((Task)tcs.Task).GetAwaiter(); 
    //    } 
    //}
}
