using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using E_Shop.Models;
using E_Shop.Classes;
using E_Shop.Business.Interfaces;
using E_Shop.Extensions;

namespace E_Shop.Controllers
{
    [PassCartStateFilter]
    public class HomeController : Controller
    {
        IEmailSender emailSender;
        
        public HomeController(IEmailSender emailSender)
        {
            this.emailSender = emailSender;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            string confirmationBody = "<p>Dobrý deň,</p>"  +
                                      "<p>týmto Vás informujeme, že sme dostali Váš dotaz. <br> Vašu žiadosť sa budeme snažiť vybaviť čo najskôr.</p>" +
                                      "<p>S pozdravom <br><br> SparkyShop</p><br><br>" +
                                      "<p style=\"color:red;\">Tento e-mail bol vygenerovaný automaticky. Neodpovedajte naň!“</p>" +
                                      "<h3>Váš dotaz:</h3>" +
                                      $"<p>{model.EmailBody}</p>";
            emailSender.ReceiveEmail(model.Subject, model.EmailBody, model.SenderEmail);
            emailSender.SendEmail(model.SenderEmail, "Potvrdenie dotazu - " + model.Subject, confirmationBody);
            this.AddFlashMessage("Vaša správa bola úspešne odoslaná. Na Vami zadaný e-mail Vám bolo zaslané potvrdenie o prijatí Vašej žiadosti. Vašu žiadosť sa budeme snažiť vybaviť čo najskôr.", FlashMessageType.Success);
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
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
