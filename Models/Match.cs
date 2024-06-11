// Models/Match.cs
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sgbet.Models
{
    public class Match
    {
        public int Id { get; set; }

        [Required]
        public int HostId { get; set; }

        [Required]
        public int AwayId { get; set; }

        [ForeignKey("HostId")]
        public Team Host { get; set; }

        [ForeignKey("AwayId")]
        public Team Away { get; set; }

        [Required]
        public DateTime DateAndTime { get; set; }

        [Required]
        public bool IsEnded { get; set; }

        public int? ResultHome { get; set; }

        public int? ResultAway { get; set; }
    }
}
