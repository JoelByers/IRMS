using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Text;

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
                reservationTable[i].CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(tableChangeCallback);
            }
        }

        private void tableChangeCallback(object sender, NotifyCollectionChangedEventArgs e)
        {
            for(int i = 0; i < e.NewItems.Count; i++)
            {
                Reservation res = (Reservation)e.NewItems[i];

                if (timeToIndex(res.expectedTime) != i)
                {
                    reservationTable[timeToIndex(res.expectedTime)].Add(new Reservation(res));
                    reservationTable[i].Remove(res);
                }
            }
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
                            reservationTable[i].Remove(reservation);
                            reservationTable[timeToIndex(reservation.expectedTime)].Add(reservation);
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
