using System.Linq;
using System.Web.Mvc;
using Kutse_App.Models;

namespace Kutse_App.Controllers
{
    public class PuhadController : Controller
    {
        private GuestContext db = new GuestContext();
        public ActionResult Puhad()
        {
            return View(db.Puhads.ToList());
        }

        // Добавление праздника (только для администратора)
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Create(Puhad puhad)
        {
            db.Puhads.Add(puhad);
            db.SaveChanges();
            return RedirectToAction("Puhad");
        }

        // Редактирование праздника
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            Puhad p = db.Puhads.Find(id);
            if (p == null)
            {
                return HttpNotFound();
            }
            return View(p);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Edit")]
        public ActionResult EditConfirmed(Puhad puhad)
        {
            db.Entry(puhad).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Puhad");
        }

        // Удаление праздника
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult Delete(int id)
        {
            Puhad p = db.Puhads.Find(id);
            if (p == null)
            {
                return HttpNotFound();
            }
            return View(p);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Puhad p = db.Puhads.Find(id);
            if (p == null)
            {
                return HttpNotFound();
            }
            db.Puhads.Remove(p);
            db.SaveChanges();
            return RedirectToAction("Puhad");
        }
    }
}
