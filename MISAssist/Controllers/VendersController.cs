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
    public class VendersController : Controller
    {
        private mis_assistantEntities db = new mis_assistantEntities();

        #region =============================================================== Index ===============================================================
        // GET: venders
        public ActionResult Index()
        {
            string company = Request.QueryString["company"];
            if (company == null || company == "") company = "全部";
            string[] paths = Request.Path.Split('/');
            if (paths.GetUpperBound(0) == 3)
            {
                company = paths[3];
            }

            ViewBag.vender_index = GetVenderList(company, "Query");
            if (company.IndexOf("全部") > -1)
            {
                return View(db.vender.ToList());
            }
            else
            {
                return View(db.vender.Where(d => d.company == company));
            }
        }
        #endregion 

        #region =============================================================== Detail ===============================================================
        // GET: venders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vender vender = db.vender.Find(id);
            if (vender == null)
            {
                return HttpNotFound();
            }
            return View(vender);
        }

        // GET: Contacts/Details/5
        public ActionResult _Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vender vender = db.vender.Find(id);
            if (vender == null)
            {
                return HttpNotFound();
            }
            return PartialView(vender);
        }
        #endregion

        #region =============================================================== Create ===============================================================
        // GET: venders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: venders/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,company,contact,title,email,tel_office,tel_mobile,fax,quick_no,note")] vender vender)
        {
            if (ModelState.IsValid)
            {
                db.vender.Add(vender);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vender);
        }

        [ChildActionOnly]
        public ActionResult _Create()
        {
            //ViewBag.dept = GetVenderList(Request.QueryString["company"]);
            ViewBag.company = Request.QueryString["company"];
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _Create([Bind(Include = "id,company,contact,title,email,tel_office,tel_mobile,fax,quick_no,note")] vender vender)
        {
            if (ModelState.IsValid)
            {
                db.vender.Add(vender);
                db.SaveChanges();
                return RedirectToAction("Index", new { company = vender.company });
            }
            return PartialView(vender);
        }
        #endregion

        #region =============================================================== Edit ===============================================================
        // GET: venders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vender vender = db.vender.Find(id);
            if (vender == null)
            {
                return HttpNotFound();
            }
            return View(vender);
        }

        // POST: venders/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,company,contact,title,email,tel_office,tel_mobile,fax,quick_no,note")] vender vender)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vender).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vender);
        }
        // GET: Contacts/Edit/5
        public ActionResult _Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vender vender = db.vender.Find(id);
            if (vender == null)
            {
                return HttpNotFound();
            }
            ViewBag.dept = GetVenderList(vender.company);
            return PartialView(vender);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _Edit([Bind(Include = "id,company,contact,title,email,tel_office,tel_mobile,fax,quick_no,note")] vender vender)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vender).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { company = vender.company });
            }
            return PartialView(vender);
        }
        #endregion

        #region =============================================================== Delete ===============================================================
        // GET: venders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vender vender = db.vender.Find(id);
            if (vender == null)
            {
                return HttpNotFound();
            }
            return View(vender);
        }

        // POST: venders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            vender vender = db.vender.Find(id);
            db.vender.Remove(vender);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Contacts/Delete/5
        public ActionResult _Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vender vender = db.vender.Find(id);
            if (vender == null)
            {
                return HttpNotFound();
            }
            return PartialView(vender);
        }

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("_Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult _DeleteConfirmed(int id)
        {
            vender vender = db.vender.Find(id);
            db.vender.Remove(vender);
            db.SaveChanges();
            return RedirectToAction("Index", new { company = vender.company });
        }
        #endregion

        #region =============================================================== Others ===============================================================
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        private List<SelectListItem> GetVenderList(string strSelected = "", string strType = "")
        {
            //廠商清單
            var vender = (from v in db.vender
                          orderby v.company
                          select v).GroupBy(e => new { t = e.company }).Select(g => g.FirstOrDefault());

            List<SelectListItem> selectListItems = new List<SelectListItem>();
            if (strType != "") selectListItems.Add(
                     new SelectListItem { Text = "** 全部 **", Value = "** 全部 **" }
                 );
            foreach (var item in vender)
            {
                selectListItems.Add(
                    new SelectListItem
                    {
                        Text = item.company,
                        Value = item.company,
                        Selected = (strSelected != "" && strSelected == item.company) ? true : false
                    }
                );
            }
            return selectListItems;
        }
        #endregion
    }
}
