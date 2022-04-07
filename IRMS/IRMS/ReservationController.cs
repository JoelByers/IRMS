using System;
using System.Collections.Generic;
using System.Text;

namespace IRMS
{
    class ReservationController
    {
        private List<Reservation> reservationTable = new List<Reservation>();

        public Reservation getReservationAt(int index)
        {
            return reservationTable[index];
        }

        public void createReservation(string name, string phoneNumber, string expectedTime, int partySize)
        {
            // TODO late time is currently a placeholder with DateTime.Now
            reservationTable.Add(new Reservation(name, phoneNumber, expectedTime, DateTime.Now.ToString(), partySize));
        }

        public void editReservation(int reservationIndex, string newName, string newPhoneNumber, string newExpectedTime)
        {
            Reservation thisReservation = reservationTable[reservationIndex];
            thisReservation.name = newName;
            thisReservation.phoneNumber = newPhoneNumber;
            thisReservation.expectedTime = newExpectedTime;

            // TODO figure out late time
        }

        public void seatReservation(int reservationIndex, bool newIsSeated)
        {
            reservationTable[reservationIndex].isSeated = newIsSeated;
        }
    }
}
