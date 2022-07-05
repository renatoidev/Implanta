using Implanta.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Implanta.Data.Mapping;

public class ProfissionalMap : IEntityTypeConfiguration<Profissional>
{
    public void Configure(EntityTypeBuilder<Profissional> builder)
    {
        // Tabela
        builder.ToTable("Profissional");

        // Chave Primária
        builder.HasKey(x => x.Id);

        // Identity - acrescentar o ID automaticamente
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();

        // Propriedades
        builder.Property(x => x.NomeCompleto)
            .IsRequired()
            .HasMaxLength(300);
            //.HasColumnName("NomeCompleto")
            //.HasColumnType("VARCHAR(300)")

        builder.Property(x => x.Cpf)
            .IsRequired();

        builder.Property(x => x.DataNascimento)
            .IsRequired();

        builder.Property(x => x.Sexo)
            .IsRequired()
            .HasMaxLength(1);

        builder.Property(x => x.Ativo)
            .IsRequired();

        builder.Property(x => x.NumeroRegistro)
            .IsRequired();

        builder.Property(x => x.Cep)
            .HasMaxLength(8);

        builder.Property(x => x.Cidade)
            .HasMaxLength(300);

        builder.Property(x => x.ValorRenda)
            .HasColumnType("Money");

        builder.Property(x => x.DataCriacao)
            .IsRequired();

        // Index
        builder.HasIndex(x => x.NumeroRegistro)
            .IsUnique();
    }
}
