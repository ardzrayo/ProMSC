using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProMSC.Areas.Clientes.Models;
using ProMSC.Areas.Servidores.Models;

namespace ProMSC.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext()
        {
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<Servidor> Servidor { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.ToTable("Cliente");
                entity.HasKey(e => e.idcliente);
                entity.Property(e => e.idcliente).HasColumnName("idcliente");

                entity.Property(e => e.razonsocial)
                .HasColumnName("razonsocial")
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

                entity.Property(e => e.ubicacion)
                .HasColumnName("ubicacion")
                .HasMaxLength(255)
                .IsUnicode(false);

                entity.Property(e => e.contacto)
                .HasColumnName("contacto")
                .HasMaxLength(100);

                entity.Property(e => e.email)
                .HasColumnName("email")
                .HasMaxLength(255);

                entity.Property(e => e.telefono)
                .HasColumnName("telefono")
                .HasMaxLength(20);

                entity.Property(e => e.estado)
                .HasColumnName("estado")
                .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Servidor>(entity =>
            {
                entity.ToTable("Servidor");
                entity.HasKey(e => e.idvps);
                entity.Property(e => e.idvps).HasColumnName("idvps");

                entity.Property(e => e.idcliente).HasColumnName("idcliente");

                entity.Property(e => e.nombrevps)
                .HasColumnName("nombrevps")
                .HasMaxLength(50)
                .IsUnicode(false);

                entity.Property(e => e.cpu)
                .HasColumnName("cpu");

                entity.Property(e => e.ram)
                .HasColumnName("ram");

                entity.Property(e => e.discoduro)
                .HasColumnName("discoduro");

                entity.Property(e => e.ips)
                .HasColumnName("ips");

                entity.Property(e => e.osystem)
                .HasColumnName("osystem")
                .HasMaxLength(255);

                entity.Property(e => e.database)
                .HasColumnName("database");

                entity.Property(e => e.desktop)
                .HasColumnName("dasktop");

                entity.Property(e => e.anchobanda)
                .HasColumnName("anchobanda");

                entity.Property(e => e.ip_publica)
                .HasColumnName("ip_publica")
                .HasMaxLength(255);

                entity.Property(e => e.ip_privada)
                .HasColumnName("ip_privada")
                .HasMaxLength(255);

                entity.Property(e => e.descripcion)
                .HasColumnName("descripcion")
                .HasMaxLength(255)
                .IsUnicode(false);

                entity.Property(e => e.estado)
                .HasColumnName("estado")
                .HasDefaultValueSql("((1))");

                entity.HasOne(p => p.cliente)
                .WithMany(c => c.servidores)
                .HasForeignKey(p => p.idcliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_servidor_cliente");
            });
        }
    }
}
