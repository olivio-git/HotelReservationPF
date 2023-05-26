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

    }
}
