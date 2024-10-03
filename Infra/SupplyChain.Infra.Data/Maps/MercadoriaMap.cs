using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SupplyChain.Domain.Entities;
using SupplyChain.Infra.Data.Maps.Base;

namespace SupplyChain.Infra.Data.Maps;

public class MercadoriaMap : BaseEntityMap<Mercadoria>
{
    public override void Configure(EntityTypeBuilder<Mercadoria> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.NumeroDeRegistro)
               .HasColumnName("Mer_NumeroDeRegistro")
               .IsRequired();

        builder.Property(x => x.Nome)
               .HasColumnName("Mer_Nome")
               .IsRequired();

        builder.Property(x => x.Fabricante)
               .HasColumnName("Mer_Fabricante")
               .IsRequired();

        builder.Property(x => x.Descricao)
               .HasColumnName("Mer_Descricao")
               .IsRequired();

        builder.Property(x => x.TipoMercadoriaId)
               .HasColumnName("Tip_TipoMercadoriaId")
               .IsRequired();

        builder.HasOne(x => x.TipoDeMercadoria)
               .WithMany(x => x.Mercadorias)
               .HasForeignKey(x => x.TipoMercadoriaId);

        builder.ToTable("Mercadoria", "Inventario");
    }
}