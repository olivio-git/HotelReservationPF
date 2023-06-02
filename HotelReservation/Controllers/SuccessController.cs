using HotelReservation.Models;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;

namespace HotelReservation.Controllers
{
    public class SuccessController : Controller
    {
        private readonly AppDbContext _context;

        public SuccessController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public ActionResult PaySuccess([FromQuery] string session_id)
        {
            var sessionService = new SessionService();
            Session session = sessionService.Get(session_id);
            var x = session.PaymentStatus;
            var customerService = new CustomerService();
            Persona customer = _context.Personas.Where(p => p.Id == int.Parse(session.ClientReferenceId)).FirstOrDefault();

            return Content($"Thanks for your order, {customer.Nombre} {customer.Apellido} estado: {x}!");
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
