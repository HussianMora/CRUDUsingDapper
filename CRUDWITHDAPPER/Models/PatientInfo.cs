using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRUDWITHDAPPER.Models
{
    public class PatientInfo:XMlClass
    {  
        public string Name { get; set; }
        public Int64 Mobile_No { get; set; }
        public DateTime DOB { get; set; }
        public string Email { get; set; }
    }

    public class XMlClass
    {
        public int Patient_Id { get; set; }
        public string PatietXML { get; set; }
    }


}