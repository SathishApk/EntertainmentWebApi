using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EntertainmentWebApiApplication.Infrastructure.Models
{
    public class Team : IEntity
    {
        [NotMapped]
        [JsonIgnore]
        public int Id { get; set; }
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public string Country { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDttm { get; set; }
        public DateTime? LastUpdatedDttm { get; set; }
    }
}
