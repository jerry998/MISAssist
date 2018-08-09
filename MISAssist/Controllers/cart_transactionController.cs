using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MISAssist.Models;

namespace MISAssistant.Controllers
{
    public class cart_transactionController : Controller
    {
        private mis_assistantEntities db = new mis_assistantEntities();

        // GET: cart_transaction
        public ActionResult Index()
        {
            var cart_transaction = db.cart_transaction.Include(c => c.printer);
            return View(cart_transaction.ToList());
        }

        #region =============================================================== Detail ===============================================================
        // GET: cart_transaction/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cart_transaction cart_transaction = db.cart_transaction.Find(id);
            if (cart_transaction == null)
            {
                return HttpNotFound();
            }
            return View(cart_transaction);
        }

        // GET: Contacts/Details/5
        public ActionResult _Details(int? printer_id, string cartridge, string color)
        {
            if (printer_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<cart_transaction> carts = db.cart_transaction.Where(c => c.printer_id == printer_id ).Where(c => c.cartridge == cartridge ).OrderBy( c => c.date).ToList();
            ViewBag.color = color;
            if (carts == null)
            {
                return HttpNotFound();
            }
            return PartialView(carts);
        }
        #endregion

        // GET: cart_transaction/Create
        public ActionResult Create()
        {
            ViewBag.printer_id = new SelectList(db.printer, "id", "department");
            return View();
        }

        // POST: cart_transaction/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,printer_id,cartridge,price,quantity,vender,in_out,date")] cart_transaction cart_transaction)
        {
            if (ModelState.IsValid)
            {
                db.cart_transaction.Add(cart_transaction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.printer_id = new SelectList(db.printer, "id", "department", cart_transaction.printer_id);
            return View(cart_transaction);
        }

        // GET: cart_transaction/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cart_transaction cart_transaction = db.cart_transaction.Find(id);
            if (cart_transaction == null)
            {
                return HttpNotFound();
            }
            ViewBag.printer_id = new SelectList(db.printer, "id", "department", cart_transaction.printer_id);
            return View(cart_transaction);
        }

        // POST: cart_transaction/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,printer_id,cartridge,price,quantity,vender,in_out,date")] cart_transaction cart_transaction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cart_transaction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.printer_id = new SelectList(db.printer, "id", "department", cart_transaction.printer_id);
            return View(cart_transaction);
        }

        // GET: cart_transaction/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cart_transaction cart_transaction = db.cart_transaction.Find(id);
            if (cart_transaction == null)
            {
                return HttpNotFound();
            }
            return View(cart_transaction);
        }

        // POST: cart_transaction/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            cart_transaction cart_transaction = db.cart_transaction.Find(id);
            db.cart_transaction.Remove(cart_transaction);
            db.SaveChanges();
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
