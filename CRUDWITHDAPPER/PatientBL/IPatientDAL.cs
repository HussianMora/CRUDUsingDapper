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


        Tuple<IEnumerable<PatientDetails1>, IEnumerable<PatientDetails2>> GetPatientById(int id);

        string DeletePatient(int id);

        void save();

        //DataTable GetCustomFormDetails(int patientId, string storedProcedure)

        DataTable GetCustomFormDetails(int patientId, string storedProcedure);

        void RegisterPatient(RegisterViewModel patient);

        LoginViewModel LoginPatient(LoginViewModel patient);




    }
}
