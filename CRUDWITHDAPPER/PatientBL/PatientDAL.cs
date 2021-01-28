using CRUDWITHDAPPER.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
namespace CRUDWITHDAPPER.PatientBL
{
    public class PatientDAL:IPatientDAL
    {
        private readonly IDataRepository _dataRepositoty; 
        //private IDbConnection _con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString);
        ////Unable to call sp

        public PatientDAL()
        {
            _dataRepositoty = new DataRepository();
        }

        public List<T> GetPatients<T>()
        {
            try
            {
                List<T> patientInfo = _dataRepositoty.CommonGetMethod<T>("sp_GetPatients");
                return patientInfo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertUpdatePatient<T>(T obj,string spName) where T:XMlClass
        {
            try
            {
                    DynamicParameters param = new DynamicParameters();
                    param.Add("@PatietXML", obj.PatietXML);
                    _dataRepositoty.CommonSavePatient(param,spName);               
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public object GetPatientById(int id)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@Id", id);
                return  _dataRepositoty.GetParticularPatient(param, "sp_GetPatientbyID");
                
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public string DeletePatient(int id)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@Id", id);
                _dataRepositoty.CommonDelete(param, "sp_DeletePatient");
                return "DeleteSuccess";
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void save()
        { }

        public DataTable GetCustomFormDetails(int patientId,string storedProcedure)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@Patient_Id", patientId);
                var data = _dataRepositoty.ReturnedDataSet(storedProcedure, param);
                DataTable firstTable = data.Tables[0];
                return firstTable;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

        }

    }
}