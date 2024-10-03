﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SupplyChain.Infra.Data.Context;

#nullable disable

namespace SupplyChain.Infra.Data.Migrations
{
    [DbContext(typeof(MercadoriaDbContext))]
    [Migration("20241003162910_initial")]
    partial class initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SupplyChain.Domain.Entities.Entrada", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("Id");

                    b.Property<DateTimeOffset>("DataDaEntrada")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("Ent_DataDaEntrada");

                    b.Property<DateTimeOffset>("DataDeAlteracao")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("DataDeAlteracao");

                    b.Property<DateTimeOffset>("DataDeCriacao")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("DataDeCriacao");

                    b.Property<string>("Local")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Ent_Local");

                    b.Property<Guid>("MercadoriaId")
                        .HasColumnType("uuid")
                        .HasColumnName("Mer_MercadoriaId");

                    b.Property<int>("Quantidade")
                        .HasColumnType("integer")
                        .HasColumnName("Ent_Quantidade");

                    b.HasKey("Id");

                    b.HasIndex("MercadoriaId");

                    b.ToTable("Entrada", "Inventario");
                });

            modelBuilder.Entity("SupplyChain.Domain.Entities.Mercadoria", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("Id");

                    b.Property<DateTimeOffset>("DataDeAlteracao")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("DataDeAlteracao");

                    b.Property<DateTimeOffset>("DataDeCriacao")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("DataDeCriacao");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Mer_Descricao");

                    b.Property<string>("Fabricante")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Mer_Fabricante");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Mer_Nome");

                    b.Property<string>("NumeroDeRegistro")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Mer_NumeroDeRegistro");

                    b.Property<Guid>("TipoMercadoriaId")
                        .HasColumnType("uuid")
                        .HasColumnName("Tip_TipoMercadoriaId");

                    b.HasKey("Id");

                    b.HasIndex("TipoMercadoriaId");

                    b.ToTable("Mercadoria", "Inventario");
                });

            modelBuilder.Entity("SupplyChain.Domain.Entities.Saida", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("Id");

                    b.Property<DateTimeOffset>("DataDaSaida")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("Sai_DataDaSaida");

                    b.Property<DateTimeOffset>("DataDeAlteracao")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("DataDeAlteracao");

                    b.Property<DateTimeOffset>("DataDeCriacao")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("DataDeCriacao");

                    b.Property<string>("Local")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Sai_Local");

                    b.Property<Guid>("MercadoriaId")
                        .HasColumnType("uuid")
                        .HasColumnName("Mer_MercadoriaId");

                    b.Property<int>("Quantidade")
                        .HasColumnType("integer")
                        .HasColumnName("Sai_Quantidade");

                    b.HasKey("Id");

                    b.HasIndex("MercadoriaId");

                    b.ToTable("Saida", "Inventario");
                });

            modelBuilder.Entity("SupplyChain.Domain.Entities.TipoDeMercadoria", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("Id");

                    b.Property<DateTimeOffset>("DataDeAlteracao")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("DataDeAlteracao");

                    b.Property<DateTimeOffset>("DataDeCriacao")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("DataDeCriacao");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Tip_Nome");

                    b.HasKey("Id");

                    b.ToTable("TipoDeMercadoria", "Inventario");
                });

            modelBuilder.Entity("SupplyChain.Domain.Entities.Entrada", b =>
                {
                    b.HasOne("SupplyChain.Domain.Entities.Mercadoria", "Mercadoria")
                        .WithMany("Entradas")
                        .HasForeignKey("MercadoriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Mercadoria");
                });

            modelBuilder.Entity("SupplyChain.Domain.Entities.Mercadoria", b =>
                {
                    b.HasOne("SupplyChain.Domain.Entities.TipoDeMercadoria", "TipoDeMercadoria")
                        .WithMany("Mercadorias")
                        .HasForeignKey("TipoMercadoriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TipoDeMercadoria");
                });

            modelBuilder.Entity("SupplyChain.Domain.Entities.Saida", b =>
                {
                    b.HasOne("SupplyChain.Domain.Entities.Mercadoria", "Mercadoria")
                        .WithMany("Saidas")
                        .HasForeignKey("MercadoriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Mercadoria");
                });

            modelBuilder.Entity("SupplyChain.Domain.Entities.Mercadoria", b =>
                {
                    b.Navigation("Entradas");

                    b.Navigation("Saidas");
                });

            modelBuilder.Entity("SupplyChain.Domain.Entities.TipoDeMercadoria", b =>
                {
                    b.Navigation("Mercadorias");
                });
#pragma warning restore 612, 618
        }
    }
}
