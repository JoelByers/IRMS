using System;
using System.Collections.Generic;
using System.Text;

namespace IRMS
{
    class Reservation
    {
        public string name { get; set; }
        public string phoneNumber { get; set; }
        public string expectedTime { get; set; }
        public string lateTime { get; set; }
        public int partySize { get; set; }
        public bool isSeated { get; set; }

        public Reservation(string name, string phoneNumber, string expectedTime, string lateTime, int partySize)
        {
            this.name = name;
            this.phoneNumber = phoneNumber;
            this.expectedTime = expectedTime;
            this.lateTime = lateTime;
            this.partySize = partySize;
            isSeated = false;
        }
    }
}
