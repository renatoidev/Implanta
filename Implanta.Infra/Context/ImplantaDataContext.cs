using Implanta.Data.Mapping;
using Implanta.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Implanta.Data.Context;

public class ImplantaDataContext : DbContext
{
    public ImplantaDataContext(DbContextOptions<ImplantaDataContext> options)
        : base(options)
    {
    }

    public DbSet<Profissional> Profissionais { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProfissionalMap());
    }
}
