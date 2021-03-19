using CRUDWITHDAPPER.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
namespace CRUDWITHDAPPER.PatientBL
{
    public class PatientDAL : IPatientDAL
    {
        private readonly IDataRepository _dataRepositoty;
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

        public void InsertUpdatePatient<T>(T obj, string spName) where T : XMlClass
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@PatietXML", obj.PatietXML);
                _dataRepositoty.CommonSavePatient(param, spName);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Tuple<IEnumerable<PatientDetails1>, IEnumerable<PatientDetails2>> GetPatientById(int id)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@Id", id);
                return _dataRepositoty.QueryMultipleSP<PatientDetails1, PatientDetails2>("sp_GetPatientbyID", param, null);

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

        public DataTable GetCustomFormDetails(int patientId, string storedProcedure)
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

        public void RegisterPatient(RegisterViewModel patient)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@Name", patient.Name);
                param.Add("@Email", patient.Email);
                param.Add("@DOB", patient.DOB);
                param.Add("@Password", patient.Password);
                param.Add("@RememberMe", 0);
                _dataRepositoty.CommonSavePatient(param, "USP_RegisterPatient");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public LoginViewModel LoginPatient(LoginViewModel patient)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@Name", patient.Name);
                param.Add("@Password", patient.Password);
                LoginViewModel login = _dataRepositoty.GetCredentials(param, "USP_LoginPatient");
                return login;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}