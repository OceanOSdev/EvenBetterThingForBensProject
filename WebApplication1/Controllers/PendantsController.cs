using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class PendantsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Pendants
        public async Task<ActionResult> Index()
        {
            return View(await db.Pendants.ToListAsync());
        }

        // GET: Pendants/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pendant pendant = await db.Pendants.FindAsync(id);
            if (pendant == null)
            {
                return HttpNotFound();
            }
            return View(pendant);
        }

        // GET: Pendants/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pendants/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Description,PendantSize")] Pendant pendant)
        {
            if (ModelState.IsValid)
            {
                db.Pendants.Add(pendant);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(pendant);
        }

        // GET: Pendants/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pendant pendant = await db.Pendants.FindAsync(id);
            if (pendant == null)
            {
                return HttpNotFound();
            }
            return View(pendant);
        }

        // POST: Pendants/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Description,PendantSize")] Pendant pendant)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pendant).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(pendant);
        }

        // GET: Pendants/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pendant pendant = await db.Pendants.FindAsync(id);
            if (pendant == null)
            {
                return HttpNotFound();
            }
            return View(pendant);
        }

        // POST: Pendants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Pendant pendant = await db.Pendants.FindAsync(id);
            db.Pendants.Remove(pendant);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
