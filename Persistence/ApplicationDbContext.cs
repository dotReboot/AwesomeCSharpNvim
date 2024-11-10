using Microsoft.EntityFrameworkCore;
using AwesomeCSharpNvim.Models;

namespace AwesomeCSharpNvim.Persistence;
internal partial class ApplicationDbContext : DbContext
{
	public DbSet<Plugin> Plugins { get; set; }
	
	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseNpgsql("Host=db;Port=5433;Username=user;Password=password;Database=AwesomeCSharpNvimDB");
	}
}