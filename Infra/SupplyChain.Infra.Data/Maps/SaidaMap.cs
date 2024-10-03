using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SupplyChain.Domain.Entities;
using SupplyChain.Infra.Data.Maps.Base;

namespace SupplyChain.Infra.Data.Maps;

public class SaidaMap : BaseEntityMap<Saida>
{
    public override void Configure(EntityTypeBuilder<Saida> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Quantidade)
            .HasColumnName("Sai_Quantidade")
            .IsRequired();

        builder.Property(x => x.Local)
            .HasColumnName("Sai_Local")
            .IsRequired();

        builder.Property(x => x.DataDaSaida)
            .HasColumnName("Sai_DataDaSaida")
            .IsRequired();

        builder.Property(x => x.MercadoriaId)
            .HasColumnName("Mer_MercadoriaId")
            .IsRequired();

        builder.HasOne(x => x.Mercadoria)
            .WithMany(x => x.Saidas)
            .HasForeignKey(x => x.MercadoriaId);
        
        builder.ToTable("Saida", "Inventario");

    }
}