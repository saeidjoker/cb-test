using System;

namespace Cleverbit.CodingTask.Data.Models {
    /// <summary>
    /// History of <see cref="Match"/>s that <see cref="User"/>s have played
    /// </summary>
    public class Play {

        public Guid Id { get; set; }

        /// <summary>
        /// Id of the <see cref="Match"/>
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Id of the <see cref="Match"/>
        /// </summary>
        public Guid MatchId { get; set; }

        /// <summary>
        /// User's score in the match
        /// </summary>
        public uint Score { get; set; }

        /// <summary>
        /// The date at which the user has submitted a score (Seconds since EPOCH)
        /// </summary>
        public long DateTimestamp { get; set; }
    }
}
