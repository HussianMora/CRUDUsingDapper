using CRUDWITHDAPPER.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CRUDWITHDAPPER.PatientBL
{
    public class DataRepository:IDataRepository
    {
        private IDbConnection _con;

        #region Initialize DBConnection
        public DataRepository()
        {
            _con= new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString);
        }

        #endregion

        #region Query

        public List<T> CommonGetMethod<T>(string spName)
        {
            return SqlMapper.Query<T>(_con, spName).ToList();
        }

        public object GetParticularPatient(object id,string spName)
        {
            return SqlMapper.Query<PatientInfo>(_con, spName, id, commandType: CommandType.StoredProcedure).FirstOrDefault();
        }

        #endregion

        #region Execute

        public void CommonSavePatient(object param,string storedProcedure)
        {
            _con.Execute(storedProcedure, param, commandType: CommandType.StoredProcedure);
        }

        public void CommonDelete(object id,string spName)
        {
            _con.Execute(spName, id, commandType: CommandType.StoredProcedure);
        }

        #endregion

        #region ToDataset
        public DataSet ReturnedDataSet(string sp, dynamic param)
        {
            DataSet ds = new DataSet();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
            {
                using (var cmd = new SqlCommand(sp, connection))
                {

                    foreach (var paramName in param.ParameterNames)
                    {
                        try
                        {
                            //if (param.Get<dynamic>(paramName) != null)
                            //{
                           //var value = param.Get<dynamic>(paramName);
                            cmd.Parameters.AddWithValue("@" + paramName, 2);
                            //}
                        }
                        catch (Exception e)
                        {

                        }
                    }

                    using (var da = new SqlDataAdapter(cmd))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        da.Fill(ds);

                    }
                }
            }

            return ds;
        }

        public object getCustomForm(object patient_id, string spName)
        {
            return SqlMapper.Query<PatientInfo>(_con, spName, patient_id, commandType: CommandType.StoredProcedure).FirstOrDefault();
        }


        #endregion

    }
}