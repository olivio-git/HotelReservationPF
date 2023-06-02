using HotelReservation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe;
using Stripe.Checkout;
using System.Web;

namespace HotelReservation.Controllers
{
    public class PaymentsController : Controller
    {
        private readonly AppDbContext _context;

        public PaymentsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public ActionResult CreateCheckoutSession()
        {
            IEnumerable<Reserva> ReservasClient = _context.Reservas.Where(r => r.IdUsuario == GlobalVariables.usuario.Id)
                .Include(r => r.Habitacion).Include(r => r.Habitacion.tipo).ToList();
            var options = new SessionCreateOptions
            {
                SubmitType = "book",
                PaymentMethodTypes = new List<string>
                {
                    "card",
                },
                LineItems = new List<SessionLineItemOptions>
                {
                    //new SessionLineItemOptions
                    //{
                    //    //PriceData = new SessionLineItemPriceDataOptions
                    //    //{
                    //    //    UnitAmount = 2000,
                    //    //    Currency = "usd",
                    //    //    ProductData = new SessionLineItemPriceDataProductDataOptions
                    //    //    {
                    //    //        Name = "T-shirt",
                    //    //        Description = GlobalVariables.usuario.Correo,
                    //    //        Images = new List<string>
                    //    //        {
                    //    //            HttpUtility.UrlPathEncode("https://www.thefourtienda.com.bo/wp-content/uploads/2021/03/Polera-ploma.png")
                    //    //        }
                    //    //    },
                    //    //},
                    //    //Quantity = 1,
                    //}, 
                },
                ClientReferenceId = GlobalVariables.usuario.Idpersona.ToString(),
                Mode = "payment",
                SuccessUrl = "https://localhost:7065/Success/PaySuccess?session_id={CHECKOUT_SESSION_ID}",
                CancelUrl = "http://localhost:4242/cancel",
            };
            foreach (var item in ReservasClient)
            {
                var itemsReserva = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long?)item.Subtotal * 100,
                        Currency = "bob",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Habitacion.Codigo,
                            Description = item.Habitacion.tipo.Nombre,
                            Images = new List<string>
                            {
                                HttpUtility.UrlPathEncode("~/imagenes/"+item.Habitacion.Imagen)
                            }
                        },
                    },
                    Quantity = 1,
                };
                options.LineItems.Add(itemsReserva);
                IEnumerable<ServicioHabitacion> ServiciosHab = _context.ServicioHabitaciones
                    .Where(sh => sh.Idtipo == item.Habitacion.Idtipo)
                    .Include(sh => sh.Servicio).ToList();
                if (ServiciosHab != null)
                {
                    foreach (var servicios in ServiciosHab)
                    {
                        var itemServicios = new SessionLineItemOptions
                        {
                            PriceData = new SessionLineItemPriceDataOptions
                            {
                                UnitAmount = (long?)servicios.Servicio.Precio * 100,
                                Currency = "bob",
                                ProductData = new SessionLineItemPriceDataProductDataOptions
                                {
                                    Name = servicios.Servicio.Nombre,
                                    Description = servicios.Servicio.Descripcion
                                },
                            },
                            Quantity = 1,
                        };
                        options.LineItems.Add(itemServicios);
                    }
                }
            }

            var service = new SessionService();
            Session session = service.Create(options);

            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
