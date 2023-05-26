using HotelReservation.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelReservation
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Habitacion> Habitaciones { get; set; }
        public DbSet<Tipo> Tipos { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Persona> Personas { get; set; }

    }
}
