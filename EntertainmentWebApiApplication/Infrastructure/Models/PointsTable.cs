using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntertainmentWebApiApplication.Infrastructure.Models
{
    public class PointsTable
    {
        public long RowId { get; set; }
        public string TeamName { get; set; }
        public string Country { get; set; }
        public string Owner { get; set; }
        public int MatchesPlayed { get; set; }
        public int MatchesWon { get; set; }
        public int MatchesDraw { get; set; }
        public int MatchesLost { get; set; }
        public int Points { get; set; }
    }
}
