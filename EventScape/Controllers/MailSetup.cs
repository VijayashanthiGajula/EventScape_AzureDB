using Microsoft.AspNetCore.Mvc;
using EventScape.Models;
using System.Net;
using System.Net.Mail;


namespace EventScape.Controllers
{
    public class MailSetup : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(EventScape.Models.Mail model)
        {
            MailMessage mm = new MailMessage("TestPractice321@gmail.com", model.To); 
            mm.Subject = model.Subject;
            mm.Body = model.Body;
            mm.IsBodyHtml = false;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;

            NetworkCredential nc = new NetworkCredential("TestPractice321@gmail.com", "!PASSword123");
            smtp.UseDefaultCredentials = true; 
            smtp.Credentials = nc;
            smtp.Send(mm);
            ViewBag.Message = "Mail Has Been Sent Successfully";


            return View();
        }
    }
}
