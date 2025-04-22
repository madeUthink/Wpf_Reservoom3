
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_Reservoom3.Models
{
    public class Reservation
    {
        public RoomID RoomID { get; }
        public string Username { get; }
        public DateTime StartDate { get; } 
        public DateTime EndDate { get; }
        public TimeSpan Length => EndDate.Subtract(StartDate);
        public Reservation(RoomID roomID, string username, DateTime startDate, DateTime endDate)
        {
            RoomID = roomID;
            Username = username;
            StartDate = startDate;
            EndDate = endDate;
        }

        //public bool Conflicts(Reservation reservation)
        //{
        //    //if (reservation.RoomID == RoomID)
        //    if (reservation.RoomID != RoomID)
        //    {
        //        return false;
        //    }
        //    return reservation.StartDate < EndDate && reservation.EndDate > StartDate;
        //}
    }
}
