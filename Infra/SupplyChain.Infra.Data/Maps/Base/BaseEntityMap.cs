using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SupplyChain.Domain.Entities.Base;

namespace SupplyChain.Infra.Data.Maps.Base;

public class BaseEntityMap<TDomain> : IEntityTypeConfiguration<TDomain> where TDomain : BaseEntity
{
    public virtual void Configure(EntityTypeBuilder<TDomain> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id)
            .HasColumnName("Id")
            .IsRequired()
            .ValueGeneratedOnAdd();
        
        builder.Property(x => x.DataDeCriacao)
            .HasColumnName("DataDeCriacao")
            .IsRequired();

        builder.Property(x => x.DataDeAlteracao)
            .HasColumnName("DataDeAlteracao")
            .IsRequired();
    }
}