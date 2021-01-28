using CRUDWITHDAPPER.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDWITHDAPPER.PatientBL
{
     public interface IPatientDAL
    {
        List<T> GetPatients<T>();

        void InsertUpdatePatient<T>(T obj, string spName) where T : XMlClass;
 

         object GetPatientById(int id);

        string DeletePatient(int id);

        void save();

        //DataTable GetCustomFormDetails(int patientId, string storedProcedure)

        DataTable GetCustomFormDetails(int patientId, string storedProcedure);




    }
}
