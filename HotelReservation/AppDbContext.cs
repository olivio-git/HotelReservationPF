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
        public DbSet<Promocion> promociones { get; set; }
        public DbSet<PromocionReserva> PromocionReservas { get; set; }
        public DbSet<Recibo> Recibos { get; set; }
        public DbSet<Reserva> Reservas  { get; set; }
        public DbSet<Servicio> Servicios { get; set; }
        public DbSet<ServicioHabitacion> servicioHabitaciones { get; set; }

    }
}
