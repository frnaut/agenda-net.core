using Agenda.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agenda.EtityFramework
{
    public class Context : DbContext
    {
        public Context(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Persona> Personas { get; set; }
        public DbSet<Direcciones> Direcciones { get; set; }
        public DbSet<Contactos> Contactos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Persona>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Persona>()
                .HasMany(x => x.Direcciones)
                .WithOne(x => x.Persona)
                .HasForeignKey(x => x.PersonaId);

            modelBuilder.Entity<Persona>()
                .HasMany(x => x.Contactos)
                .WithOne(x => x.Persona)
                .HasForeignKey(x => x.PersonaId);

            modelBuilder.Entity<Direcciones>()
               .HasKey(x => x.Id);

            modelBuilder.Entity<Contactos>()
                .HasKey(x => x.Id);
                
        }
    }
}
