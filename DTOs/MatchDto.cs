using System;

namespace sgbet.Dtos
{
    public class MatchDto
    {
        public int Id { get; set; }
        public int HostId { get; set; }
        public int AwayId { get; set; }
        public DateTime DateAndTime { get; set; }
        public bool IsEnded { get; set; }
        public int? ResultHome { get; set; }
        public int? ResultAway { get; set; }
    }

    public class CreateMatchDto
    {
        public int HostId { get; set; }
        public int AwayId { get; set; }
        public DateTime DateAndTime { get; set; }
    }
    public class EndMatchDto
    {
        public int Id { get; set; }
        public int HomeScore { get; set; }
        public int AwayScore { get; set; }
    }
}