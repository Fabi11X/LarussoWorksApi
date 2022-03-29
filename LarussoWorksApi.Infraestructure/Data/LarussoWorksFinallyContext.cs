using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using LarussoWorksApi.Domain.entities;

#nullable disable

namespace LarussoWorksApi.Infraestructure.Data
{
    public partial class LarussoWorksFinallyContext : DbContext
    {
        public LarussoWorksFinallyContext()
        {
        }

        public LarussoWorksFinallyContext(DbContextOptions<LarussoWorksFinallyContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Administrador> Administradors { get; set; }
        public virtual DbSet<Categoria> Categorias { get; set; }
        public virtual DbSet<Contactano> Contactanos { get; set; }
        public virtual DbSet<Empleo> Empleos { get; set; }
        public virtual DbSet<Empresa> Empresas { get; set; }
        public virtual DbSet<Mensaje> Mensajes { get; set; }
        public virtual DbSet<MensajesEmpresa> MensajesEmpresas { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=LarussoWorksFinally.mssql.somee.com;Initial Catalog=LarussoWorksFinally;Persist Security Info=False;User ID=LarussoWork_SQLLogin_1;Password=bejqmdqb4e");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Administrador>(entity =>
            {
                entity.HasKey(e => e.IdAdmin)
                    .HasName("PK_Administradores");

                entity.ToTable("Administrador");

                entity.Property(e => e.ApellidoAdmin)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ContrasenaAdmin)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CorreoAdmin)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.DireccionAdmin)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.FechanacAdmin)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FotografiaAdmin).IsRequired();

                entity.Property(e => e.NivelAdmin)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.NombreAdmin)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.SexoAdmin)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TelefonoAdmin)
                    .IsRequired()
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.HasKey(e => e.IdCategoria);

                entity.Property(e => e.NombreCategoria)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Contactano>(entity =>
            {
                entity.HasKey(e => e.IdContacto);

                entity.Property(e => e.AsuntoContacto).IsRequired();

                entity.Property(e => e.CorreoContacto)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.MensajeContacto).IsRequired();
            });

            modelBuilder.Entity<Empleo>(entity =>
            {
                entity.HasKey(e => e.IdEmpleo);

                entity.Property(e => e.DescripcionEmpleo).IsRequired();

                entity.Property(e => e.ImagenEmpleo).IsRequired();

                entity.Property(e => e.NombreEmpleo)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PrestacionesEmpleo).IsRequired();

                entity.Property(e => e.RequisitosEmpleo).IsRequired();

                entity.Property(e => e.UbicacionEmpleo)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.IdCategoriaNavigation)
                    .WithMany(p => p.Empleos)
                    .HasForeignKey(d => d.IdCategoria)
                    .HasConstraintName("FK_Empleos_Categorias");

                entity.HasOne(d => d.IdEmpresaNavigation)
                    .WithMany(p => p.Empleos)
                    .HasForeignKey(d => d.IdEmpresa)
                    .HasConstraintName("FK_Empleos_Empresas");
            });

            modelBuilder.Entity<Empresa>(entity =>
            {
                entity.HasKey(e => e.IdEmpresa);

                entity.Property(e => e.ContrasenaEmpresa)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CorreoEmpresa)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.DescripcionEmpresa).IsRequired();

                entity.Property(e => e.LogoEmpresa).IsRequired();

                entity.Property(e => e.NivelEmpresa)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.NombreEmpresa)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.TelefonoEmpresa)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.UbicacionEmpresa)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Mensaje>(entity =>
            {
                entity.HasKey(e => e.IdMensaje);

                entity.Property(e => e.BuzonMensaje).IsRequired();

                entity.HasOne(d => d.IdEmpleoNavigation)
                    .WithMany(p => p.Mensajes)
                    .HasForeignKey(d => d.IdEmpleo)
                    .HasConstraintName("FK_Mensajes_Empleos");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Mensajes)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK_Mensajes_Usuarios");
            });

            modelBuilder.Entity<MensajesEmpresa>(entity =>
            {
                entity.HasKey(e => e.IdMensajeEmpresa)
                    .HasName("PK_MensajeEmpresa");

                entity.Property(e => e.MensajeEmpresa).IsRequired();

                entity.HasOne(d => d.IdEmpresaNavigation)
                    .WithMany(p => p.MensajesEmpresas)
                    .HasForeignKey(d => d.IdEmpresa)
                    .HasConstraintName("FK_MensajesEmpresas_Empresas");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.MensajesEmpresas)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK_MensajesEmpresas_Usuarios");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario);

                entity.Property(e => e.ApellidoUsuario)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ContrasenaUsuario)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CorreoUsuario)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.EducacionUsuario).IsRequired();

                entity.Property(e => e.ExperienciaUsuario).IsRequired();

                entity.Property(e => e.FechanacUsuario)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FotografiaUsuario).IsRequired();

                entity.Property(e => e.HabilidadUsuario).IsRequired();

                entity.Property(e => e.NivelUsuario)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.NombreUsuario)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ObjProUsuario).IsRequired();

                entity.Property(e => e.SexoUsuario)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TelefonoUsuario)
                    .IsRequired()
                    .HasMaxLength(10);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
