using Microsoft.EntityFrameworkCore;

namespace cSharpBelt{

public class MyContext : DbContext
{

    public MyContext(DbContextOptions<MyContext>options): base(options){}

    public DbSet<User> users {get;set;}

    public DbSet<Activities> activities {get;set;}

    public DbSet<Intrsts> intrsts {get;set;}
}



}