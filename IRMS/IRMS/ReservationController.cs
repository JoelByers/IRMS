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

        public void createReservation(string name, string phoneNumber, DateTime expectedTime, int partySize)
        {
            // TODO late time is currently a placeholder with DateTime.Now
            reservationTable.Add(new Reservation(name, phoneNumber, expectedTime, DateTime.Now, partySize));
        }

        public void editReservation(int reservationIndex, string newName, string newPhoneNumber, DateTime newExpectedTime)
        {
            Reservation thisReservation = reservationTable[reservationIndex];
            thisReservation.setName(newName);
            thisReservation.setPhoneNumber(newPhoneNumber);
            thisReservation.setExpectedTime(newExpectedTime);

            // TODO figure out late time
        }

        public void seatReservation(int reservationIndex, bool newIsSeated)
        {
            reservationTable[reservationIndex].setIsSeated(newIsSeated);
        }
    }
}
