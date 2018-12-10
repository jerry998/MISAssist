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
    public class PrintersController : Controller
    {
        private mis_assistantEntities db = new mis_assistantEntities();

        #region =============================================================== Index ===============================================================
        // GET: Printers
        public ActionResult Index()
        {
            //計算碳粉匣存量
            var rawSqlCmd = db.Database.Connection.CreateCommand();
            rawSqlCmd.CommandText = "select 碳粉匣, sum(CASE WHEN 入出 = '入' THEN 數量 ELSE - 1 * 數量 END) from 異動記錄 group by 碳粉匣";
            rawSqlCmd.CommandType = CommandType.Text;

            rawSqlCmd.Connection.Open();
            var reader = rawSqlCmd.ExecuteReader();
            List<CartViewModel> carts = new List<CartViewModel>();
            while (reader.Read())
            {
                CartViewModel cart = new CartViewModel();
                cart.CartNo = reader.GetValue(0).ToString();
                cart.Quantity = Convert.ToInt32(reader.GetValue(1));
                carts.Add(cart);
            }
            ViewBag.carts = carts;
            rawSqlCmd.Connection.Close();

            //Years
            int thisYear = DateTime.Now.Year;
            List<SelectListItem> listYears = new List<SelectListItem>();
            for (int i = 0; i < 5; i++)
            {
                string strYear = (thisYear - i).ToString();
                listYears.Add(new SelectListItem { Text = strYear, Value = strYear });
            }
            ViewBag.Years = listYears;

            //Index
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
                return View(db.印表機.OrderBy(c => c.使用單位));
            }
            else
            {
                return View(db.印表機.Where(d => d.使用單位 == department));
            }
        }
        #endregion

        #region =============================================================== Detail ===============================================================
        // GET: Printers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            印表機 printer = db.印表機.Find(id);
            if (printer == null)
            {
                return HttpNotFound();
            }
            return View(printer);
        }

        // GET: Contacts/Details/5
        public ActionResult _Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            印表機 printer = db.印表機.Find(id);
            if (printer == null)
            {
                return HttpNotFound();
            }
            return PartialView(printer);
        }
        #endregion

        #region =============================================================== Create ===============================================================
        // GET: Printers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Printers/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,department,brand,model,type,vender,price,date,ip,cartridge_black,cartridge_blue,cartridge_red,cartridge_yellow,note")] 印表機 printer)
        {
            if (ModelState.IsValid)
            {
                db.印表機.Add(printer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(printer);
        }

        [ChildActionOnly]
        public ActionResult _Create()
        {
            ViewBag.dept = GetDeptList(Request.QueryString["dept"]);
            ViewBag.ptype = GetTypeList(Request.QueryString["type"]);
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _Create([Bind(Include = "id,department,brand,model,type,vender,price,date,ip,cartridge_black,cartridge_blue,cartridge_red,cartridge_yellow,note")] 印表機 printer)
        {
            if (ModelState.IsValid)
            {
                db.印表機.Add(printer);
                db.SaveChanges();
                return RedirectToAction("Index", new { dept = printer.使用單位 });
            }
            return PartialView(printer);
        }
        #endregion

        #region =============================================================== Edit ===============================================================
        // GET: Printers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            印表機 printer = db.印表機.Find(id);
            if (printer == null)
            {
                return HttpNotFound();
            }
            return View(printer);
        }

        // POST: Printers/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,使用單位,廠牌,型號,類別,購買廠商,價格,購買日期,IP位址,碳粉匣_黑,碳粉匣_青,碳粉匣_紅,碳粉匣_黃,備註")] 印表機 printer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(printer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(printer);
        }

        // GET: Contacts/Edit/5
        public ActionResult _Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            印表機 printer = db.印表機.Find(id);
            if (printer == null)
            {
                return HttpNotFound();
            }
            ViewBag.dept = GetDeptList(printer.使用單位);
            ViewBag.ptype = GetTypeList(printer.類別);
            return PartialView(printer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _Edit([Bind(Include = "ID,使用單位,廠牌,型號,類別,購買廠商,價格,購買日期,IP位址,碳粉匣_黑,碳粉匣_青,碳粉匣_紅,碳粉匣_黃,備註")] 印表機 printer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(printer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { dept = printer.使用單位 });
            }
            return PartialView(printer);
        }
        #endregion

        #region =============================================================== Delete ===============================================================
        // GET: Printers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            印表機 printer = db.印表機.Find(id);
            if (printer == null)
            {
                return HttpNotFound();
            }
            return View(printer);
        }

        // POST: Printers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            印表機 printer = db.印表機.Find(id);
            db.印表機.Remove(printer);
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
            印表機 printer = db.印表機.Find(id);
            if (printer == null)
            {
                return HttpNotFound();
            }
            return PartialView(printer);
        }

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("_Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult _DeleteConfirmed(int id)
        {
            印表機 printer = db.印表機.Find(id);
            db.印表機.Remove(printer);
            db.SaveChanges();
            return RedirectToAction("Index", new { dept = printer.使用單位 });
        }
        #endregion

        #region=============================================================== Others ===============================================================
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
            //使用單位
            List<SelectListItem> selectListItems = new List<SelectListItem>();
            if (strType != "") selectListItems.Add(
                     new SelectListItem { Text = "** 全部 **", Value = "** 全部 **" }
                 );
            selectListItems.Add(new SelectListItem { Text = "財務組", Value = "財務組", Selected = (strSelected != "" && strSelected == "財務組") ? true : false });
            selectListItems.Add(new SelectListItem { Text = "會計室", Value = "會計室", Selected = (strSelected != "" && strSelected == "會計室") ? true : false });
            selectListItems.Add(new SelectListItem { Text = "出納室", Value = "出納室", Selected = (strSelected != "" && strSelected == "出納室") ? true : false });
            selectListItems.Add(new SelectListItem { Text = "人事室", Value = "人事室", Selected = (strSelected != "" && strSelected == "人事室") ? true : false });
            selectListItems.Add(new SelectListItem { Text = "收租處", Value = "收租處", Selected = (strSelected != "" && strSelected == "收租處") ? true : false });
            selectListItems.Add(new SelectListItem { Text = "社工組", Value = "社工組", Selected = (strSelected != "" && strSelected == "社工組") ? true : false });
            selectListItems.Add(new SelectListItem { Text = "傳達室", Value = "傳達室", Selected = (strSelected != "" && strSelected == "傳達室") ? true : false });
            selectListItems.Add(new SelectListItem { Text = "資訊室", Value = "資訊室", Selected = (strSelected != "" && strSelected == "資訊室") ? true : false });
            selectListItems.Add(new SelectListItem { Text = "測量室", Value = "測量室", Selected = (strSelected != "" && strSelected == "測量室") ? true : false });
            selectListItems.Add(new SelectListItem { Text = "主任室", Value = "主任室", Selected = (strSelected != "" && strSelected == "主任室") ? true : false });
            return selectListItems;
        }

        private List<SelectListItem> GetTypeList(string strSelected  = "")
        {
            //類別
            List<SelectListItem> selectListItems = new List<SelectListItem>();
            selectListItems.Add(new SelectListItem { Text = "彩色", Value = "彩色", Selected = (strSelected != "" && strSelected == "彩色") ? true : false });
            selectListItems.Add(new SelectListItem { Text = "黑白", Value = "黑白", Selected = (strSelected != "" && strSelected == "黑白") ? true : false });
            return selectListItems;
        }

        #endregion
    }
}