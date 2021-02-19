using CRUDWITHDAPPER.Models;
using CRUDWITHDAPPER.PatientBL;
using Newtonsoft.Json;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Serialization;

namespace CRUDWITHDAPPER.Controllers
{
    public class PatientController : Controller
    {
        private readonly IPatientDAL _patientDAL;

        public PatientController()
        {
            _patientDAL = new PatientDAL();
        }

        public ActionResult Index(string search)
        {
            try
            {
                List<PatientInfo> patients = _patientDAL.GetPatients<PatientInfo>();
                return View(patients.ToList());
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public ActionResult PatientHomePage()
        {
            if (Session["Patient_Id"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public JsonResult AddUpdatePatient(Dictionary<string, string> formdata)
        {
            try
            {
                System.Xml.Linq.XElement el = new System.Xml.Linq.XElement("PatientInfo",formdata.Select(kv => new System.Xml.Linq.XElement(kv.Key, kv.Value)));
                var xml = el.ToString();
                XMlClass xMLClass = new XMlClass
                {
                    PatietXML = xml,
                };
                _patientDAL.InsertUpdatePatient(xMLClass, "sp_InsertUpdatePEG");
                return Json("Data Saved Successfully");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpPost]
        public JsonResult RemovePatient(int id)
        {
            try
            {
                string result = _patientDAL.DeletePatient(id);
                return Json(result);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public JsonResult GetPatient(int id)
        {
            try
            {
                var patient = _patientDAL.GetPatientById(id);
                return Json(patient, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public JsonResult PatientPopoverDetails(int id)
        {
            try
            {
                var patient = _patientDAL.GetPatientById(id);
                return Json(patient);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static T ToObject<T>(IDictionary<string, string> source)
        where T : class, new()
        {
            var someObject = new T();
            var someObjectType = someObject.GetType();

            foreach (var item in source)
            {
                someObjectType
                         .GetProperty(item.Key)
                         .SetValue(someObject, item.Value, null);
            }

            return someObject;
        }

        public static String ObjectToXMLGeneric<T>(T filter)
        {
            string xml = null;
            XmlSerializer xsSubmit = new XmlSerializer(typeof(T));
            using (var sww = new StringWriter())
            {
                using (XmlTextWriter writer = new XmlTextWriter(sww))
                {
                    xsSubmit.Serialize(writer, filter);
                    try
                    {
                        xml = sww.ToString();
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
            }
            return xml;
        }

        public ActionResult ViewPEG()
        {
            return View();
        }

        public JsonResult CommonGetCustomFormDetails()
        {
            try
            {
                int Patient_Id = Int32.Parse(Session["Patient_id"].ToString());
                var subBaseModel = _patientDAL.GetCustomFormDetails(Patient_Id, "sp_GetPEG");
                var submodeldict = GetDataTableDictionaryList(subBaseModel);
                return Json(submodeldict, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public List<Dictionary<string, object>> GetDataTableDictionaryList(DataTable dt)
        {
            return dt.AsEnumerable().Select(
                row => dt.Columns.Cast<DataColumn>().ToDictionary(
                    column => column.ColumnName,
                    column => row[column]
                )).ToList();
        }

        public static List<T> CreateListFromTable<T>(DataTable tbl) where T : new()
        {
            // define return list
            List<T> lst = new List<T>();

            // go through each row
            foreach (DataRow r in tbl.Rows)
            {
                // add to the list
                lst.Add(CreateItemFromRow<T>(r));
            }

            // return the list
            return lst;
        }
        public static T CreateItemFromRow<T>(DataRow row) where T : new()
        {
            // create a new object
            T item = new T();

            // set the item
            SetItemFromRow(item, row);

            // return 
            return item;
        }
         public static void SetItemFromRow<T>(T item, DataRow row) where T : new()
        {
            // go through each column
            foreach (DataColumn c in row.Table.Columns)
            {
                // find the property for the column
                PropertyInfo p = item.GetType().GetProperty(c.ColumnName);

                // if exists, set the value
                if (p != null && row[c] != DBNull.Value)
                {
                    p.SetValue(item, row[c], null);
                }
            }
        }
        public void ExportExcel()
        {

            List<PatientInfo> patients = _patientDAL.GetPatients<PatientInfo>();

            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("PatientList");
            ws.Cells["A1:E1"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            ws.Cells["A1:E1"].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("Yellow")));
            for (int i = 1; i <= patients.Count()+1; i++)
            {
                ws.Cells["A" + i + ":" + "E" + i].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                ws.Cells["A" + i + ":" + "E" + i].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                ws.Cells["A" + i + ":" + "E" + i].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                ws.Cells["A" + i + ":" + "E" + i].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            }
            ws.Cells["A1"].Value = "Id";
            ws.Cells["B1"].Value = "Name";
            ws.Cells["C1"].Value = "MobileNo";
            ws.Cells["D1"].Value = "DOB";
            ws.Cells["E1"].Value = "Email";

            int row = 2;
            foreach (var item in patients)
            {
                ws.Cells[string.Format("A{0}", row)].Value = item.Patient_Id;
                ws.Cells[string.Format("B{0}", row)].Value = item.Name;
                ws.Cells[string.Format("C{0}", row)].Value = item.Mobile_No;
                ws.Cells[string.Format("D{0}", row)].Value = item.DOB;
                ws.Cells[string.Format("E{0}", row)].Value = item.Email;
                row++;
            }
            ws.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment; filename=PatientList.xlsx");
            Response.BinaryWrite(pck.GetAsByteArray());
            Response.End();
        }
    }
}