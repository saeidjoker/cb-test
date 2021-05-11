using System;

namespace Cleverbit.CodingTask.Data.Models {

    /// <summary>
    /// Represents a match in the system
    /// </summary>
    public class Match {
        public Guid Id { get; set; }

        /// <summary>
        /// Expire date of the match (Seconds since EPOCH)
        /// </summary>
        public long ExpiresTimestamp { get; set; }
    }

}