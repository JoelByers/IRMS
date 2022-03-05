using System;
using System.Collections.Generic;
using System.Text;

namespace IRMS
{
    class Reservation
    {
        string name;
        string phoneNumber;
        DateTime expectedTime;
        DateTime lateTime;
        int partySize;
        bool isSeated;

        public Reservation(string name, string phoneNumber, DateTime expectedTime, DateTime lateTime, int partySize)
        {
            this.name = name;
            this.phoneNumber = phoneNumber;
            this.expectedTime = expectedTime;
            this.lateTime = lateTime;
            this.partySize = partySize;
            isSeated = false;
        }

        public void setName(string newName)
        {
            name = newName;
        }

        public void setPhoneNumber(string newPhoneNumber)
        {
            phoneNumber = newPhoneNumber;
        }

        public void setExpectedTime(DateTime newExpectedTime)
        {
            expectedTime = newExpectedTime;
        }

        public void setPartySize(int newPartySize)
        {
            partySize = newPartySize;
        }

        public void setIsSeated(bool newIsSeated)
        {
            isSeated = newIsSeated;
        }
    }
}
