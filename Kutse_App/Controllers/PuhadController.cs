using System.Linq;
using System.Web.Mvc;
using Kutse_App.Models;
using System.Collections.Generic;

public class PuhadController : Controller
{
    private readonly GuestContext db = new GuestContext();

    // Отображение списка праздников
    public ActionResult Index()
    {
        var puhad = db.Puhads.ToList();
        return View(puhad);
    }

    // Регистрация на праздник
    [Authorize]
    public ActionResult Register(int id)
    {
        var puhkuse = db.Puhads.Find(id);
        if (puhkuse == null)
        {
            return HttpNotFound();
        }

        var guest = new Guest { PuhadId = id };
        return View(guest);
    }

    [HttpPost]
    [Authorize]
    public ActionResult Register(Guest guest)
    {
        if (ModelState.IsValid)
        {
            db.Guests.Add(guest);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        return View(guest);
    }

    // Просмотр участников праздника
    [Authorize]
    public ActionResult Osalejad(int id)
    {
        // Находим праздник по ID
        var puhkuse = db.Puhads.Find(id);
        if (puhkuse == null)
        {
            return HttpNotFound();
        }

        // Получаем список участников
        var participants = db.Guests.Where(g => g.PuhadId == id).ToList();

        // Передаем имя праздника через ViewBag
        ViewBag.PuhkuseNimi = puhkuse.Puhkuse_nimi;

        // Возвращаем представление с участниками
        return View(participants);
    }
}
