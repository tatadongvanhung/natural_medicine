using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using natural_medicine.Models;

namespace natural_medicine.Areas.Admin.Controllers
{
    public class ReportController : Controller
    {
        // GET: Admin/Report
        private MyDBContext context = new MyDBContext();

        public ActionResult ReportImport(DateTime ?start_date, DateTime ?end_date)
        {
            if(start_date != null && end_date != null)
            {
                DateTime start = (DateTime)start_date;
                DateTime end = (DateTime) end_date;
                var model = context.IMPORTS_REPORT(start.Date, end.Date)
                    .ToList();
                ViewBag.date = start.ToString("dd/MM/yyyy") + " - " + end.ToString("dd/MM/yyyy");
                ViewBag.start_date = start;
                ViewBag.end_date = end;
                return View(model);
            }
            else
            {
                DateTime start = DateTime.Today;
                DateTime end = DateTime.Today;
                var model = context.IMPORTS_REPORT(start, end).ToList();
                ViewBag.date = start.ToString("dd/MM/yyyy");
                ViewBag.start_date = start;
                ViewBag.end_date = end;
                return View(model);
            }
        }
        [HttpPost]
        public JsonResult ExcelImport(DateTime? start_date, DateTime? end_date)
        {
            var gv = new GridView();
            gv.DataSource = context.IMPORTS_REPORT(start_date, end_date)
                .Select(r => new {
                    id = r.id,
                    name = r.name,
                    uses = r.uses,
                    dosage = r.dosage,
                    description = r.description,
                    sum_quantity = r.sum_quantity,
                    sum_total = r.sum_total
                })
                .ToList();
            gv.DataBind();
            Response.Clear();
            Response.Buffer = true;
            //Response.AddHeader("content-disposition",
            // "attachment;filename=GridViewExport.xls");
            Response.Charset = "utf-8";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=thong-ke-nhap-"+DateTime.Now.ToString("yyyyMMdd-hhmmss") + ".xls");
            //Mã hóa chữa sang UTF8
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.BinaryWrite(System.Text.Encoding.UTF8.GetPreamble());

            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            for (int i = 0; i < gv.Rows.Count; i++)
            {
                //Apply text style to each Row
                gv.Rows[i].Attributes.Add("class", "textmode");
            }
            //Add màu nền cho header của file excel
            gv.HeaderRow.BackColor = System.Drawing.Color.DarkBlue;
            //Màu chữ cho header của file excel
            gv.HeaderStyle.ForeColor = System.Drawing.Color.White;

            gv.HeaderRow.Cells[0].Text = "Mã Sản Phẩm";
            gv.HeaderRow.Cells[1].Text = "Tên Sản Phẩm";
            gv.HeaderRow.Cells[2].Text = "Công Dụng";
            gv.HeaderRow.Cells[3].Text = "Liều Lượng";
            gv.HeaderRow.Cells[4].Text = "Mô Tả";
            gv.HeaderRow.Cells[5].Text = "Tổng Lượng Nhập";
            gv.HeaderRow.Cells[6].Text = "Tổng Tiền Nhập";
            gv.RenderControl(hw);

            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
            return Json("success", JsonRequestBehavior.AllowGet);
        }

        public ActionResult ReportExport(DateTime? start_date, DateTime? end_date)
        {
            if (start_date != null && end_date != null)
            {
                DateTime start = (DateTime)start_date;
                DateTime end = (DateTime)end_date;
                var model = context.EXPORTS_REPORT(start.Date, end.Date)
                    .ToList();
                ViewBag.date = start.ToString("dd/MM/yyyy") + " - " + end.ToString("dd/MM/yyyy");
                ViewBag.start_date = start;
                ViewBag.end_date = end;
                return View(model);
            }
            else
            {
                DateTime start = DateTime.Today;
                DateTime end = DateTime.Today;
                var model = context.EXPORTS_REPORT(start, end).ToList();
                ViewBag.date = start.ToString("dd/MM/yyyy");
                ViewBag.start_date = start;
                ViewBag.end_date = end;
                return View(model);
            }
        }
        [HttpPost]
        public JsonResult ExcelExport(DateTime? start_date, DateTime? end_date)
        {
            var gv = new GridView();
            gv.DataSource = context.EXPORTS_REPORT(start_date, end_date)
                .Select(r => new {
                    id = r.id,
                    name = r.name,
                    uses = r.uses,
                    dosage = r.dosage,
                    description = r.description,
                    sum_quantity = r.sum_quantity
                })
                .ToList();
            gv.DataBind();
            Response.Clear();
            Response.Buffer = true;
            //Response.AddHeader("content-disposition",
            // "attachment;filename=GridViewExport.xls");
            Response.Charset = "utf-8";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=thong-ke-xuat-" + DateTime.Now.ToString("yyyyMMdd-hhmmss") + ".xls");
            //Mã hóa chữa sang UTF8
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.BinaryWrite(System.Text.Encoding.UTF8.GetPreamble());

            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            for (int i = 0; i < gv.Rows.Count; i++)
            {
                //Apply text style to each Row
                gv.Rows[i].Attributes.Add("class", "textmode");
            }
            //Add màu nền cho header của file excel
            gv.HeaderRow.BackColor = System.Drawing.Color.DarkBlue;
            //Màu chữ cho header của file excel
            gv.HeaderStyle.ForeColor = System.Drawing.Color.White;

            gv.HeaderRow.Cells[0].Text = "Mã Sản Phẩm";
            gv.HeaderRow.Cells[1].Text = "Tên Sản Phẩm";
            gv.HeaderRow.Cells[2].Text = "Công Dụng";
            gv.HeaderRow.Cells[3].Text = "Liều Lượng";
            gv.HeaderRow.Cells[4].Text = "Mô Tả";
            gv.HeaderRow.Cells[5].Text = "Tổng Lượng Xuất";
            gv.RenderControl(hw);

            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
            return Json("success", JsonRequestBehavior.AllowGet);
        }
        public ActionResult TotalRevenue()
        {
            return View();
        }
    }
}