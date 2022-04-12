using System;
using System.Collections.Generic;
using System.Text;

namespace IRMS
{
    class ReservationController
    {
        private const int closingTime = 12;
        private const int numTimeSlots = 7;
        private const int numSeats = 50;
        private List<Reservation>[] reservationTable = new List<Reservation>[numTimeSlots];

        public ReservationController()
        {
            for(int i = 0; i < reservationTable.Length; i++)
            {
                reservationTable[i] = new List<Reservation>();
            }
        }

        public Reservation getReservationAt(int timeIndex, int reservationIndex)
        {
            return reservationTable[timeIndex][reservationIndex];
        }

        public void createReservation(string name, string phoneNumber, string expectedTime, int partySize)
        {
            // TODO late time is currently a placeholder with DateTime.Now
            int hour = Int32.Parse(expectedTime.Substring(0, 2));

            reservationTable[closingTime-hour].Add(new Reservation(name, phoneNumber, expectedTime, DateTime.Now.ToString(), partySize));
        }

        public void editReservation(int timeIndex,int reservationIndex, string newName, string newPhoneNumber, string newExpectedTime)
        {
            Reservation thisReservation = reservationTable[timeIndex][reservationIndex];
            thisReservation.name = newName;
            thisReservation.phoneNumber = newPhoneNumber;
            thisReservation.expectedTime = newExpectedTime;

            // TODO figure out late time
        }

        public void seatReservation(int timeIndex, int reservationIndex, bool newIsSeated)
        {
            reservationTable[timeIndex][reservationIndex].isSeated = newIsSeated;
        }

        public int getNumSeats()
        {
            return numSeats;
        }

        public int getNumTimeSlots()
        {
            return numTimeSlots;
        }
    }
}
