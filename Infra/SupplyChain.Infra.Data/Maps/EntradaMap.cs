using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SupplyChain.Domain.Entities;
using SupplyChain.Infra.Data.Maps.Base;

namespace SupplyChain.Infra.Data.Maps;

public class EntradaMap : BaseEntityMap<Entrada>
{
    public override void Configure(EntityTypeBuilder<Entrada> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Quantidade)
               .HasColumnName("Ent_Quantidade")
               .IsRequired();

        builder.Property(x => x.Local)
               .HasColumnName("Ent_Local")
               .IsRequired();

        builder.Property(x => x.DataDaEntrada)
               .HasColumnName("Ent_DataDaEntrada")
               .IsRequired();

        builder.Property(x => x.MercadoriaId)
               .HasColumnName("Mer_MercadoriaId")
               .IsRequired();

        builder.HasOne(x => x.Mercadoria)
               .WithMany(x => x.Entradas)
               .HasForeignKey(x => x.MercadoriaId);
        
        builder.ToTable("Entrada", "Inventario");
    }
}