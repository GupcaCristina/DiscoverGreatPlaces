using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Places.Web.Models.ViewModels;

namespace Places.Web.Controllers
{
    public class MessageController : Controller
    {

        public IActionResult SendMessage(int placeId , string placeEmail)
        {
            ViewBag.placeId = placeId;
            ViewBag.placeEmail = placeEmail;
            return View();
        }

        [HttpPost]
        public IActionResult SendMessage(SendMessageViewModel messageViewModel)
        {
            MailMessage mail = new MailMessage(messageViewModel.From, messageViewModel.To);
            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Host = "smtp.gmail.com";
            mail.Subject = messageViewModel.Subject;
            mail.Body = messageViewModel.Body;
            client.EnableSsl = true;          
            client.Send(mail);
            return RedirectToAction("Places", "Details", new { id = messageViewModel.PlaceId });
        }
    }
}