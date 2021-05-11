using System;

namespace Cleverbit.CodingTask.Data.DateAndTime {
    public class DefaultClock : IClock {
        public DateTime Now() {
            return DateTime.UtcNow;
        }
    }
}
