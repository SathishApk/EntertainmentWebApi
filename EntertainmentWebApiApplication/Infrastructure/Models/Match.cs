using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EntertainmentWebApiApplication.Infrastructure.Models
{
    public class Match
    {
        public int MatchId { get; set; }
        public string MatchLocation { get; set; }

        [ForeignKey("TeamA_Obj")]
        public int? TeamA_Id  { get; set; }
        public int TeamA_Score  { get; set; }
        [ForeignKey("TeamB_Obj")]
        public int? TeamB_Id  { get; set; }
        public int TeamB_Score  { get; set; }
        [ForeignKey("Winner_Obj")]
        public int? Winner_Id { get; set; }
        public DateTime RecordCreatedDttm { get; set; }
        public DateTime? LastUpdatedDttm { get; set; }

        [JsonIgnore]
        public Team TeamA_Obj { get; set; }
        [JsonIgnore]
        public Team TeamB_Obj { get; set; }
        [JsonIgnore]
        public Team Winner_Obj { get; set; }
    }
}
