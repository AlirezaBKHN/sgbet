using System.ComponentModel.DataAnnotations;
namespace sgbet.Dtos;

public class TeamDto{
    public int Id{get; set;}
    public string Name{get; set;}
}

public class CreateTeamDto{
    [Required]
    public string Name{get; set;}
}