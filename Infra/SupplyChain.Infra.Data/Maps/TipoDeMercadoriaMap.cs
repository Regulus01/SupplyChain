using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SupplyChain.Domain.Entities;
using SupplyChain.Infra.Data.Maps.Base;

namespace SupplyChain.Infra.Data.Maps;

public class TipoDeMercadoriaMap : BaseEntityMap<TipoDeMercadoria>
{
    public override void Configure(EntityTypeBuilder<TipoDeMercadoria> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Nome)
            .HasColumnName("Tip_Nome")
            .IsRequired();
        
        builder.ToTable("TipoDeMercadoria", "Inventario");
    }
}