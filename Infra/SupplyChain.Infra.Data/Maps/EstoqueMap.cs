using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SupplyChain.Domain.Entities;
using SupplyChain.Infra.Data.Maps.Base;

namespace SupplyChain.Infra.Data.Maps;

public class EstoqueMap : BaseEntityMap<Estoque>
{
    public override void Configure(EntityTypeBuilder<Estoque> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Quantidade)
               .HasColumnName("Est_Estoque")
               .IsRequired();

        builder.Property(x => x.Local)
               .HasColumnName("Est_Local")
               .IsRequired();

        builder.Property(x => x.MercadoriaId)
               .HasColumnName("Merc_MercadoriaId")
               .IsRequired();

        builder.HasOne(x => x.Mercadoria)
               .WithMany(x => x.Estoque)
               .HasForeignKey(x => x.MercadoriaId);

        builder.ToTable("Estoque", "Inventario");
    }
}