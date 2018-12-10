using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using MISAssist.Models;

namespace MISAssistant.Controllers
{
    public class cart_transactionController : Controller
    {
        private mis_assistantEntities db = new mis_assistantEntities();

        // GET: cart_transaction
        public ActionResult Index()
        {
            var cart_transaction = db.異動記錄.Include(c => c.印表機ID);
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
            異動記錄 cart_transaction = db.異動記錄.Find(id);
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
            List<異動記錄> carts = db.異動記錄.Where(c => c.印表機ID == printer_id ).Where(c => c.碳粉匣 == cartridge ).OrderByDescending( c => c.日期).ToList();
            ViewBag.PrinterId = printer_id;
            ViewBag.CartId = cartridge;
            ViewBag.color = color;
            ViewBag.Dept = db.印表機.Where(c => c.ID == printer_id).Select(c => c.使用單位).Single();
            if (carts == null)
            {
                return HttpNotFound();
            }
            return PartialView(carts);
        }

        // GET: Contacts/Details/5
        public ActionResult _DetailsEditable(int? printer_id, string cartridge, string color)
        {
            if (printer_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<異動記錄> carts = db.異動記錄.Where(c => c.印表機ID == printer_id).Where(c => c.碳粉匣 == cartridge).OrderByDescending(c => c.日期).ToList();
            ViewBag.PrinterId = printer_id;
            ViewBag.CartId = cartridge;
            ViewBag.color = color;
            ViewBag.Dept = db.印表機.Where(c => c.ID == printer_id).Select(c => c.使用單位).Single();
            if (carts == null)
            {
                return HttpNotFound();
            }
            return PartialView(carts);
        }

        // POST: Contacts/Details/5
        [HttpPost]
        public JsonResult _DetailsEditable(異動記錄 data, string field)
        {
            異動記錄 cart_trans = db.異動記錄.Where<異動記錄>(c => c.ID == data.ID).FirstOrDefault<異動記錄>();
            cart_trans.GetType().GetProperty(field).SetValue(cart_trans, data.GetType().GetProperty(field).GetValue(data));

            db.Entry(cart_trans).State = EntityState.Modified;
            db.SaveChanges();
            return Json("success");
        }

        // GET: Contacts/Details/5
        public List<異動記錄> _DetailsList(int? printer_id, string cartridge, string color)
        {
            List<異動記錄> carts = db.異動記錄.Where(c => c.印表機ID == printer_id).Where(c => c.碳粉匣 == cartridge).OrderBy(c => c.日期).ToList();
            return carts;
        }
        #endregion

        //各單位年度成本
        public ActionResult _AnnualCost(int iYear)
        {
            ViewBag.year = iYear;

            //var costs = db.異動記錄.Where(c => c.日期.Value.Year == iYear).Where(c => c.入出 == "出").Sum(c => c.單價*c.數量);
            var tmp = db.印表機.Join(db.異動記錄.Where( c => c.日期.Value.Year == iYear & c.入出 == "出").Select( s => s), prn => prn.ID, cat => cat.印表機ID, (prn, cat) => new { prn.使用單位, cat.數量, cat.單價 }).Select(s => s);
            var tmpCosts = tmp.GroupBy(t => t.使用單位).Select(y => new { Id = y.Key, Cost = y.Sum(x => x.單價 * x.數量) }).ToList();

            List<AnnualCostViewModel> ListCosts = new List<AnnualCostViewModel>();
            foreach (var t in tmpCosts)
            {
                AnnualCostViewModel ac = new AnnualCostViewModel();
                ac.Id = t.Id;
                ac.Cost = Convert.ToInt32(t.Cost);
                ListCosts.Add(ac);
            }

            return PartialView(ListCosts);
        }

        // GET: cart_transaction/Create
        public ActionResult Create()
        {
            ViewBag.printer_id = new SelectList(db.印表機, "ID", "使用單位");
            return View();
        }

        // GET: cart_transaction/Create
        public ActionResult _Create(string id, string cartId)
        {
            ViewBag.printerId = id;
            ViewBag.cartId = cartId;
            return PartialView();
        }

        // POST: cart_transaction/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,印表機ID,碳粉匣,單價,數量,廠商,入出,日期")] 異動記錄 cart_transaction)
        {
            if (ModelState.IsValid)
            {
                db.異動記錄.Add(cart_transaction);
                db.SaveChanges();
                return RedirectToAction("Index", "Printers");
            }

            ViewBag.printer_id = new SelectList(db.印表機, "ID", "使用單位", cart_transaction.印表機ID);
            return PartialView();
        }

        // GET: cart_transaction/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            異動記錄 cart_transaction = db.異動記錄.Find(id);
            if (cart_transaction == null)
            {
                return HttpNotFound();
            }
            ViewBag.printer_id = new SelectList(db.印表機, "ID", "使用單位", cart_transaction.印表機ID);
            return View(cart_transaction);
        }

        // POST: cart_transaction/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,印表機ID,碳粉匣,單價,數量,廠商,入出,日期")] 異動記錄 cart_transaction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cart_transaction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.printer_id = new SelectList(db.印表機, "ID", "使用單位", cart_transaction.印表機ID);
            return View(cart_transaction);
        }

        // GET: cart_transaction/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            異動記錄 cart_transaction = db.異動記錄.Find(id);
            if (cart_transaction == null)
            {
                return HttpNotFound();
            }
            return View(cart_transaction);
        }

        public ActionResult _Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            異動記錄 cart_transaction = db.異動記錄.Find(id);
            if (cart_transaction == null)
            {
                return HttpNotFound();
            }
            return PartialView(cart_transaction);
        }

        // POST: cart_transaction/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            異動記錄 cart_transaction = db.異動記錄.Find(id);
            db.異動記錄.Remove(cart_transaction);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: cart_transaction/Delete/5
        [HttpPost, ActionName("_Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult _DeleteConfirmed(int id)
        {
            異動記錄 cart_transaction = db.異動記錄.Find(id);
            db.異動記錄.Remove(cart_transaction);
            db.SaveChanges();
            //List<異動記錄> carts = db.異動記錄.Where(c => c.印表機ID == cart_transaction.印表機ID).Where(c => c.碳粉匣 == cart_transaction.碳粉匣).OrderBy(c => c.日期).ToList();
            //if (carts == null)
            //{
            //    return HttpNotFound();
            //}
            return RedirectToAction("Index", "Printers");
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
