using CRUDWITHDAPPER.Models;
using CRUDWITHDAPPER.PatientBL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
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
        //public PatientController(IPatientDAL patientDAL)
        //{
        //    _patientDAL = patientDAL;
        //}

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
            return View();
        }

        [HttpPost]
        public JsonResult AddUpdatePatient(Dictionary<string, string> formdata)
        {
            try
            {
                //Dictionary<string, string> EmployeeList = new Dictionary<string, string>();
                //EmployeeList.Add("Patient_Id", "0");
                //EmployeeList.Add("Name", "Mike");
                //EmployeeList.Add("Mobile_No", "369852147");
                //EmployeeList.Add("DOB", "02/02/2002");
                //EmployeeList.Add("Email", "test22@gmail.com");
                //var patientInfo = ToObject<PatientInfo>(formdata);

                //PatientInfo pi = new PatientInfo
                //{
                //    Patient_Id = patientInfo.Patient_Id,
                //    DOB=patientInfo.DOB,
                //    Email=patientInfo.Email,
                //    Mobile_No=patientInfo.Mobile_No,
                //    Name=patientInfo.Name
                //};
                System.Xml.Linq.XElement el = new System.Xml.Linq.XElement("PatientInfo",formdata.Select(kv => new System.Xml.Linq.XElement(kv.Key, kv.Value)));
                var xml = el.ToString();
                XMlClass xMLClass = new XMlClass
                {
                    PatietXML = xml,
                };
                //formdata.PatietXML = ObjectToXMLGeneric(formdata);
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

        //private static T DictionaryToObject<T>(IDictionary<string, string> dict) where T : new()
        //{
        //    var t = new T();
        //    PropertyInfo[] properties = t.GetType().GetProperties();

        //    foreach (PropertyInfo property in properties)
        //    {
        //        if (!dict.Any(x => x.Key.Equals(property.Name, StringComparison.InvariantCultureIgnoreCase)))
        //            continue;

        //        KeyValuePair<string, string> item = dict.First(x => x.Key.Equals(property.Name, StringComparison.InvariantCultureIgnoreCase));

        //        // Find which property type (int, string, double? etc) the CURRENT property is...
        //        Type tPropertyType = t.GetType().GetProperty(property.Name).PropertyType;

        //        // Fix nullables...
        //        Type newT = Nullable.GetUnderlyingType(tPropertyType) ?? tPropertyType;

        //        // ...and change the type
        //        //object newA = Convert.ChangeType(item.Value, newT);
        //        //t.GetType().GetProperty(property.Name).SetValue(t, newA, null);
        //    }
        //    return t;
        //}

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
                int Patient_Id = 2;
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
        //public JsonResult ExportRecordsToExcel()
        //{
        //    //var gv = new GridView();
        //    //gv.DataSource =patientDAL.GetPatientRecords();
        //    //gv.DataBind();
        //    //Response.ClearContent();
        //    //Response.Buffer = true;
        //    //Response.AddHeader("content-disposition", "attachment; filename=PatientRecords.xls");
        //    //Response.ContentType = "application/ms-excel";
        //    //Response.Charset = "";
        //    //StringWriter objStringWriter = new StringWriter();
        //    //HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
        //    //gv.RenderControl(objHtmlTextWriter);
        //    //Response.Output.Write(objStringWriter.ToString());
        //    //Response.Flush();
        //    //Response.End();
        //    //GridView gridview = new GridView();
        //    //gridview.DataSource = patientDAL.GetPatientRecords();
        //    //gridview.DataBind();

        //    //// Clear all the content from the current response
        //    //Response.ClearContent();
        //    //Response.Buffer = true;
        //    //// set the header
        //    //Response.AddHeader("content-disposition", "attachment;filename = Records.xls");
        //    //Response.ContentType = "application/ms-excel";
        //    //Response.Charset = "";
        //    //// create HtmlTextWriter object with StringWriter
        //    //using (StringWriter sw = new StringWriter())
        //    //{
        //    //    using (HtmlTextWriter htw = new HtmlTextWriter(sw))
        //    //    {
        //    //        // render the GridView to the HtmlTextWriter
        //    //        gridview.RenderControl(htw);
        //    //        // Output the GridView content saved into StringWriter
        //    //        Response.Output.Write(sw.ToString());
        //    //        Response.Flush();
        //    //        Response.End();
        //    //    }
        //    //}
        //    var products = new System.Data.DataTable("teste");
        //    products.Columns.Add("col1", typeof(int));
        //    products.Columns.Add("col2", typeof(string));

        //    products.Rows.Add(1, "product 1");
        //    products.Rows.Add(2, "product 2");
        //    products.Rows.Add(3, "product 3");
        //    products.Rows.Add(4, "product 4");
        //    products.Rows.Add(5, "product 5");
        //    products.Rows.Add(6, "product 6");
        //    products.Rows.Add(7, "product 7");


        //    var grid = new GridView();
        //    grid.DataSource = products;
        //    grid.DataBind();

        //    Response.ClearContent();
        //    Response.Buffer = true;
        //    Response.AddHeader("content-disposition", "attachment; filename=MyExcelFile.xls");
        //    Response.ContentType = "application/ms-excel";

        //    Response.Charset = "";
        //    StringWriter sw = new StringWriter();
        //    HtmlTextWriter htw = new HtmlTextWriter(sw);

        //    grid.RenderControl(htw);

        //    Response.Output.Write(sw.ToString());
        //    Response.Flush();
        //    Response.End();


        //    return Json("ÏmportSuccess");

        //}

        //public JsonResult ExportRecordsToExcelUsingSyncfusion()
        //{
        //    // Create an instance of ExcelEngine
        //    using (ExcelEngine excelEngine = new ExcelEngine())
        //    {
        //        //Initialize Application
        //        IApplication application = excelEngine.Excel;

        //        //Set the default application version as Excel 2016
        //        application.DefaultVersion = ExcelVersion.Excel2016;

        //        //Create a workbook with a worksheet
        //        IWorkbook workbook = application.Workbooks.Create(1);

        //        //Access first worksheet from the workbook instance
        //        IWorksheet worksheet = workbook.Worksheets[0];

        //        //Export data to Excel
        //        DataTable dataTable = GetDataTable();
        //        worksheet.ImportDataTable(dataTable, true, 1, 1);
        //        worksheet.UsedRange.AutofitColumns();

        //        //Save the workbook to disk in xlsx format
        //        workbook.SaveAs("Output.xlsx", ExcelSaveType.SaveAsXLS, HttpContext.ApplicationInstance.Response, ExcelDownloadType.Open);
        //        return Json("ÏmportSuccess");
        //    }

        //}

        //private static DataTable GetDataTable()
        //{
        //    //Create a DataTable with four columns
        //    DataTable table = new DataTable();
        //    table.Columns.Add("Dosage", typeof(int));
        //    table.Columns.Add("Drug", typeof(string));
        //    table.Columns.Add("Patient", typeof(string));
        //    table.Columns.Add("Date", typeof(DateTime));

        //    //Add five DataRows
        //    table.Rows.Add(25, "Indocin", "David", DateTime.Now);
        //    table.Rows.Add(50, "Enebrel", "Sam", DateTime.Now);
        //    table.Rows.Add(10, "Hydralazine", "Christoff", DateTime.Now);
        //    table.Rows.Add(21, "Combivent", "Janet", DateTime.Now);
        //    table.Rows.Add(100, "Dilantin", "Melanie", DateTime.Now);

        //    return table;
        //}
    }
}