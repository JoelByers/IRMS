/*  SE 306
 *  Prestige Worldwide
 *  
 *  Description: Reservation Class, for creating and managing reservations
 */

using System;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace IRMS
{
    class ReservationController
    {
        private const int numTimeSlots = 7;
        private const int numSeats = 100;
        private ObservableCollection<Reservation>[] reservationTable = new ObservableCollection<Reservation>[numTimeSlots];

        public ReservationController()
        {
            for(int i = 0; i < reservationTable.Length; i++)
            {
                reservationTable[i] = new ObservableCollection<Reservation>();
            }
        }

        public string getLateTime(Reservation reservation, CustomerProfiles customerProfiles)
        {
            Customer thisCustomer = customerProfiles.getCustomer(reservation.name);
            Rank thisRank;
            int thisTimeHour;
            int thisTimeMin = Int32.Parse(reservation.expectedTime.Substring(reservation.expectedTime.Length - 2, 2));

            if (reservation.expectedTime.ToCharArray()[1] == ':')
            {
                thisTimeHour = Int32.Parse(reservation.expectedTime.Substring(0, 1));
            }
            else
            {
                thisTimeHour = Int32.Parse(reservation.expectedTime.Substring(0, 2));
            }

            if (thisCustomer == null)
            {
                thisRank = Rank.BRONZE;
            }
            else
            {
                thisRank = thisCustomer.getRank();
            }

            switch (thisRank)
            {
                case Rank.SILVER:
                    thisTimeMin += 5;
                    break;
                case Rank.GOLD:
                    thisTimeMin += 15;
                    break;
                case Rank.PREMIUM:
                    thisTimeMin += 20;
                    break;
            }

            if (thisTimeMin >= 60)
            {
                thisTimeMin -= 60;
                thisTimeHour++;
            }

            return thisTimeHour.ToString() + ":" + thisTimeMin.ToString("00");
        }

        public Reservation getReservationAt(int timeIndex, int reservationIndex)
        {
            return reservationTable[timeIndex][reservationIndex];
        }

        public void createReservation(Reservation newRes)
        {
            createReservation(newRes.name, newRes.phoneNumber, newRes.expectedTime, newRes.lateTime, newRes.partySize);
        }

        public void createReservation(string name, string phoneNumber, string expectedTime, string lateTime,int partySize)
        {
            // TODO late time is currently a placeholder with DateTime.Now

            reservationTable[timeToIndex(expectedTime)].Add(new Reservation(name, phoneNumber, expectedTime, lateTime, partySize));
        }

        public void removeReservation(Reservation removeReservation)
        {
            foreach(ObservableCollection<Reservation> timeSlot in reservationTable)
            {
                if(timeSlot.Contains(removeReservation))
                {
                    timeSlot.Remove(removeReservation);
                    break;
                }
            }
        }

        public void fixReservationTime(Reservation reservation, string newTime)
        {

            traceReservations();
            for(int i = 0; i < reservationTable.Length; i++)
            {
                foreach(Reservation searchReservation in reservationTable[i])
                {
                    if(reservation == searchReservation)
                    {
                        Trace.WriteLine("Found " + reservation.expectedTime + " " + newTime);
                        if(reservation.expectedTime != newTime)
                        {
                            Trace.WriteLine("Fix");
                            //reservationTable[i].Remove(reservation);
                            //reservationTable[timeToIndex(reservation.expectedTime)].Add(reservation);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }

            // TODO figure out late time
        }

        public void traceReservations()
        {
            Trace.WriteLine("Trace:");
            foreach(ObservableCollection<Reservation> c in reservationTable)
            {
                foreach(Reservation r in c)
                {
                    Trace.WriteLine(r.expectedTime);
                }
            }
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

        public ref ObservableCollection<Reservation> getReservationsAtTime(int timeIndex)
        {
            return ref reservationTable[timeIndex];
        }

        public int timeToIndex(string time)
        {
            string hourString = time.Substring(0, 2);
            if (hourString.ToCharArray()[1] == ':')
            {
                hourString = hourString.Substring(0, 1);
            }

            int hour = Int32.Parse(hourString);

            return hour - numTimeSlots + 1;
        }
    }
}
