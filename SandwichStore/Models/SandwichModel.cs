using Microsoft.EntityFrameworkCore;
namespace SandwichStore.Models
{
    public class SandwichModel
    {
        // sandwich data model
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
    }

    class SandwichDB : DbContext
    {
        public SandwichDB(DbContextOptions options) : base(options) { }
        public DbSet<SandwichModel> Sandwiches { get; set; } = null!;
    }
}
