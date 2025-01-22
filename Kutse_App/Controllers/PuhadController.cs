using System.Linq;
using System.Web.Mvc;
using Kutse_App.Models;

namespace Kutse_App.Controllers
{
    public class PuhadController : Controller
    {
  
        private GuestContext db = new GuestContext();
        public ActionResult Index()
        {
            return View(db.Puhads.ToList());
        }

        //// Список праздников
        //public ActionResult Index()
        //{
        //    return View(db.Puhad.ToList());
        //}

        //// Добавление праздника (только для администратора)
        //[Authorize(Roles = "Administrator")]
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //[HttpPost]
        //[Authorize(Roles = "Administrator")]
        //public ActionResult Create(Puhad puhad)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Puhad.Add(puhad);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(puhad);
        //}

        //// Редактирование праздника
        //[Authorize(Roles = "Administrator")]
        //public ActionResult Edit(int id)
        //{
        //    var puhad = db.Puhad.Find(id);
        //    if (puhad == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(puhad);
        //}

        //[HttpPost]
        //[Authorize(Roles = "Administrator")]
        //public ActionResult Edit(Puhad puhad)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(puhad).State = System.Data.Entity.EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(puhad);
        //}

        //// Удаление праздника
        //[Authorize(Roles = "Administrator")]
        //public ActionResult Delete(int id)
        //{
        //    var puhad = db.Puhad.Find(id);
        //    if (puhad == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(puhad);
        //}

        //[HttpPost, ActionName("Delete")]
        //[Authorize(Roles = "Administrator")]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    var puhad = db.Puhad.Find(id);
        //    db.Puhad.Remove(puhad);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}
    }
}
