﻿using Kutse_App.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace Kutse_App.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
           
            int hour = DateTime.Now.Hour;
            if (hour >= 5 && hour < 12) 
            {
                ViewBag.Greeting = "Tere hommikust";
            }
            else if (hour >= 12 && hour < 19) 
            {
                ViewBag.Greeting = "Tere päevast";
            }
            else if (hour >= 19 && hour < 23) 
            {
                ViewBag.Greeting = "Tere õhtust";
            }
            else
            {
                ViewBag.Greeting = "Head ööd";
            }

            int month = DateTime.Now.Month;
            if (month == 1)
            {
                ViewBag.Message = "Happy New Year!";
                ViewBag.ImagePath = "~/Images/new_year.jpg";
            }
            else if (month == 2)
            {
                ViewBag.Message = "Head sõbrapäeva";
                ViewBag.ImagePath = "~/Images/soberpaev.jpg";
            }
            else if (month == 4)
            {
                ViewBag.Message = "Lihavõttepühade 1. päev";
                ViewBag.ImagePath = "~/Images/haid_puhi.jpg";
            }
            else
            {
                ViewBag.Message = "Ootan sind minu peole! Palun tule!!!";
                ViewBag.ImagePath = "~/Images/kutse.jpg";
            }
            return View();
        }

        [HttpGet]
        public ActionResult Ankeet()
        {
            var puhads = db.Puhads.ToList();
            ViewBag.Holidays = new SelectList(puhads, "Id", "Puhkuse_nimi");

            var guest = new Guest();
            if (!User.IsInRole("Admin"))
            {
                guest.WillAttend = false;
            }

            return View(guest);
        }

        [HttpPost]
        public ViewResult Ankeet(Guest guest)
        {
            if (ModelState.IsValid)
            {
                E_mail(guest);
                if (ModelState.IsValid)
                {
                    db.Guests.Add(guest);
                    db.SaveChanges();
                    return View("Thanks", guest);
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return View(guest);
            }
        }

        public void E_mail(Guest guest)
        {
            try
            {
                WebMail.SmtpServer = "smtp.gmail.com";
                WebMail.SmtpPort = 587;
                WebMail.EnableSsl = true;
                WebMail.UserName = "daragalcenko3@gmail.com";
                WebMail.Password = "iqer zkvm czuv lgqn";
                WebMail.From = "daragalcenko3@gmail.com";
                WebMail.Send(guest.Email, " Vastus kutsele ", guest.Nimi + " vastas "
                    + ((guest.WillAttend ?? false ? " tuleb peole" : " ei tule saatnud"))); ViewBag.Message = "Kiri on saatnud!";
                ViewBag.Message = "Kiri on saatnud!";
            }
            catch (Exception)
            {
                ViewBag.Message = "Mul on kahju!Ei saa kirja saada!!!";
            }
        }
        [HttpPost]
        //Добавьте кнопку "Отправить напоминание/Meeldetuletus", по нажатию на которую должно отправиться
        //письмо с приложением(файлом) и отобразиться соответствующее
        //представление с сообщением об отправке письма. 
        public ActionResult Meeldetuletus(Guest guest, string Meeldetuletus)
        {
            if (!string.IsNullOrEmpty(Meeldetuletus))
            {
                try
                {
                    WebMail.SmtpServer = "smtp.gmail.com";
                    WebMail.SmtpPort = 587;
                    WebMail.EnableSsl = true;
                    WebMail.UserName = "daragalcenko3@gmail.com";
                    WebMail.Password = "iqer zkvm czuv lgqn";
                    WebMail.From = "daragalcenko3@gmail.com";

                    WebMail.Send(guest.Email, "Meeldetuletus", guest.Nimi + ", ara unusta. Pidu toimub 25.01.25! Sind ootavad väga!",
                    null, guest.Email,
                    filesToAttach: new String[] { Path.Combine(Server.MapPath("~/Images/"), Path.GetFileName("kutse.jpg ")) }
                   );

                    ViewBag.Message = "Kutse saadetud!";
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Tekkis viga kutse saatmisel: " + ex.Message;
                }
            }

            return View("Thanks", guest);
        }
        GuestContext db = new GuestContext();
        [Authorize(Roles = "User")]
        public ActionResult Guests()
        {
            IEnumerable<Guest> guests = db.Guests;
            return View(guests);
        }
      
        [Authorize(Roles = "User")]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "User")]
        [HttpPost]
        public ActionResult Create(Guest guest)
        {
            db.Guests.Add(guest);
            db.SaveChanges();
            return RedirectToAction("Guests");
        }
        [Authorize(Roles = "User")]
        [HttpGet]
        public ActionResult Delete(int id)
        {
            Guest g = db.Guests.Find(id);
            if (g == null)
            {
                return HttpNotFound();
            }
            return View(g);
        }
        [Authorize(Roles = "User")]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Guest g = db.Guests.Find(id);
            if (g == null)
            {
                return HttpNotFound();
            }
            db.Guests.Remove(g);
            db.SaveChanges();
            return RedirectToAction("Guests");
        }
        // Действие для отображения деталей гостя
        [Authorize(Roles = "User")]
        public ActionResult Details(int id)
        {
            var guest = db.Guests.Find(id);
            if (guest == null)
            {
                return HttpNotFound(); // Если гость не найден, возвращаем ошибку 404
            }
            return View(guest); // Передаем гостя в представление Details
        }
        [Authorize(Roles = "User")]
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            Guest g =db.Guests.Find(id);
            if (g == null)
            {
                return HttpNotFound();
            }
            return View(g);
        }
        [Authorize(Roles = "User")]
        [HttpPost, ActionName("Edit")]
        public ActionResult EditConfirmed(Guest guest)
        {
            db.Entry(guest).State =System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Guests");
        }
    }
}
