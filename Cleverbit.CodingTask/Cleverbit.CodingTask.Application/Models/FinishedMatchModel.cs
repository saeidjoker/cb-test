using System;

namespace Cleverbit.CodingTask.Application.Models {

    public class FinishedMatchModel {
        public Guid MatchId { get; set; }
        public long ExpiresTimestamp { get; set; }
        public string WinnerUserName { get; set; }
        public uint WinnerScore { get; set; }
    }

}