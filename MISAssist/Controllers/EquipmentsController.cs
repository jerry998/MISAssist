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
    public class EquipmentsController : Controller
    {
        private mis_assistantEntities db = new mis_assistantEntities();

        //=============================================================== Index ===============================================================
        // GET: equipments
        public ActionResult Index()
        {
            string department = Request.QueryString["dept"];
            if (department == null || department == "") department = "全部";
            string[] paths = Request.Path.Split('/');
            if (paths.GetUpperBound(0) == 3)
            {
                department = paths[3];
            }

            ViewBag.dept_index = GetDeptList(department, "Query");
            if (department.IndexOf("全部") > -1)
            {
                return View(db.equipment.ToList());
            }
            else
            {
                return View(db.equipment.Where(d => d.department == department));
            }
        }

        //=============================================================== Detail ===============================================================
        // GET: equipments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            equipment equipment = db.equipment.Find(id);
            if (equipment == null)
            {
                return HttpNotFound();
            }
            return View(equipment);
        }

        // GET: Contacts/Details/5
        public ActionResult _Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            equipment equipment = db.equipment.Find(id);
            if (equipment == null)
            {
                return HttpNotFound();
            }
            return PartialView(equipment);
        }

        //=============================================================== Create ===============================================================
        // GET: equipments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: equipments/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,department,ftype,name,feature,ip,op,op_bit,op_copyright,db,office,offcie_copyright,antivirus,note")] equipment equipment)
        {
            if (ModelState.IsValid)
            {
                db.equipment.Add(equipment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(equipment);
        }

        [ChildActionOnly]
        public ActionResult _Create()
        {
            ViewBag.dept = GetDeptList(Request.QueryString["dept"]);
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _Create([Bind(Include = "id,department,ftype,name,feature,ip,op,op_bit,op_copyright,db,office,offcie_copyright,antivirus,note")] equipment equipment)
        {
            if (ModelState.IsValid)
            {
                db.equipment.Add(equipment);
                db.SaveChanges();
                return RedirectToAction("Index", new { dept = equipment.department });
            }
            return PartialView(equipment);
        }

        //=============================================================== Edit ===============================================================
        // GET: equipments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            equipment equipment = db.equipment.Find(id);
            if (equipment == null)
            {
                return HttpNotFound();
            }
            return View(equipment);
        }

        // POST: equipments/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,department,ftype,name,feature,ip,op,op_bit,op_copyright,db,office,offcie_copyright,antivirus,note")] equipment equipment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(equipment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(equipment);
        }

        // GET: Contacts/Edit/5
        public ActionResult _Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            equipment equipment = db.equipment.Find(id);
            if (equipment == null)
            {
                return HttpNotFound();
            }
            ViewBag.dept = GetDeptList(equipment.department);
            return PartialView(equipment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _Edit([Bind(Include = "id,department,ftype,name,feature,ip,op,op_bit,op_copyright,db,office,offcie_copyright,antivirus,note")] equipment equipment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(equipment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { dept = equipment.department });
            }
            return PartialView(equipment);
        }

        //=============================================================== Delete ===============================================================
        // GET: equipments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            equipment equipment = db.equipment.Find(id);
            if (equipment == null)
            {
                return HttpNotFound();
            }
            return View(equipment);
        }

        // POST: equipments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            equipment equipment = db.equipment.Find(id);
            db.equipment.Remove(equipment);
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
            equipment equipment = db.equipment.Find(id);
            if (equipment == null)
            {
                return HttpNotFound();
            }
            return PartialView(equipment);
        }

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("_Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult _DeleteConfirmed(int id)
        {
            equipment equipment = db.equipment.Find(id);
            db.equipment.Remove(equipment);
            db.SaveChanges();
            return RedirectToAction("Index", new { dept = equipment.department });
        }

        //=============================================================== Others ===============================================================
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private List<SelectListItem> GetDeptList(string strSelected = "", string strType = "")
        {
            //單位別清單
            var dept = from d in db.department
                       orderby d.dept_no
                       select d;
            List<SelectListItem> selectListItems = new List<SelectListItem>();
            if (strType != "") selectListItems.Add(
                     new SelectListItem { Text = "** 全部 **", Value = "** 全部 **" }
                 );
            foreach (var item in dept)
            {
                selectListItems.Add(
                    new SelectListItem
                    {
                        Text = item.dept_name,
                        Value = item.dept_name,
                        Selected = (strSelected != "" && strSelected == item.dept_name) ? true : false
                    }
                );
            }
            return selectListItems;
        }
    }
}
