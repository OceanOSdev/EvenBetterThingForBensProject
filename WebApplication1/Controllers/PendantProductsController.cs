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
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class PendantProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserManager<ApplicationUser> manager;

        public PendantProductsController()
        {
            manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db)); ;
        }

        // GET: PendantProducts
        public async Task<ActionResult> Index()
        {
            var currentUser = await manager.FindByIdAsync(User.Identity.GetUserId());
            return View(db.PendantProducts.ToList().Where(pendant => pendant.User.Id == currentUser.Id));
        }

        // GET: PendantProducts/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            var currentUser = await manager.FindByIdAsync(User.Identity.GetUserId());
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PendantProduct pendantProduct = await db.PendantProducts.FindAsync(id);
            if (pendantProduct == null)
            {
                return HttpNotFound();
            }
            if (pendantProduct.User.Id != currentUser.Id)
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            return View(pendantProduct);
        }

        // GET: PendantProducts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PendantProducts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Description,PendantSize")] PendantProduct pendantProduct)
        {
            var currentUser = await manager.FindByIdAsync(User.Identity.GetUserId());
            if (ModelState.IsValid)
            {
                //pendantProduct.User = new ApplicationUser { UserName = User.Identity.IsAuthenticated ? User.Identity.Name : "whoops" };
                pendantProduct.User = currentUser;
                pendantProduct.Price = pendantProduct.PendantSize == Size.Small ? 30.00m : 70.00m;
                db.PendantProducts.Add(pendantProduct);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(pendantProduct);
        }

        // GET: PendantProducts/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            var currentUser = await manager.FindByIdAsync(User.Identity.GetUserId());
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PendantProduct pendantProduct = await db.PendantProducts.FindAsync(id);
            if (pendantProduct == null)
            {
                return HttpNotFound();
            }
            if (pendantProduct.User.Id != currentUser.Id)
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            return View(pendantProduct);
        }

        // POST: PendantProducts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Description,PendantSize")] PendantProduct pendantProduct)
        {
            if (ModelState.IsValid)
            {
                pendantProduct.Price = pendantProduct.PendantSize == Size.Small ? 30.00m : 70.00m;
                db.Entry(pendantProduct).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(pendantProduct);
        }

        // GET: PendantProducts/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            var currentUser = await manager.FindByIdAsync(User.Identity.GetUserId());
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PendantProduct pendantProduct = await db.PendantProducts.FindAsync(id);
            if (pendantProduct == null)
            {
                return HttpNotFound();
            }
            if (pendantProduct.User.Id != currentUser.Id)
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            return View(pendantProduct);
        }

        // POST: PendantProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            PendantProduct pendantProduct = await db.PendantProducts.FindAsync(id);
            db.PendantProducts.Remove(pendantProduct);
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
