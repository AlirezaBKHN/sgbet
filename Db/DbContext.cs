using sgbet.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace sgbet.Db;

public class AppDbContext : IdentityDbContext<User>{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){
    }

    public DbSet<Team> Teams{get;set;}
    public DbSet<Match> Matches {get; set;}
}