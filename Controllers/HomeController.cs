using CoreWebSite.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CoreWebSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        // In-memory store for submitted forms (as requested: "no DB needed")
        private static readonly List<ContactForm> _submittedForms = new List<ContactForm>();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Services()
        {
            return View();
        }

        public IActionResult Projects()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Contact()
        {
            return View(new ContactForm());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Contact(ContactForm model)
        {
            if (ModelState.IsValid)
            {
                // Store in-memory and log
                _submittedForms.Add(model);
                _logger.LogInformation("New contact form submitted by {Name} ({Email}). Message: {Message}", model.Name, model.Email, model.Message);

                // Redirect to ThankYou page
                return RedirectToAction(nameof(ThankYou));
            }

            // Return with validation errors
            return View(model);
        }

        public IActionResult ThankYou()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
