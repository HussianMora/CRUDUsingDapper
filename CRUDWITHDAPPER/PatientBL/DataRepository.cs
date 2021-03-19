using CRUDWITHDAPPER.Models;
using Dapper;
using System;
using System.Collections;
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

        public LoginViewModel GetCredentials(object param, string spName)
        {
            return SqlMapper.Query<LoginViewModel>(_con, spName, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
        }


        #endregion

        public Tuple<IEnumerable<T1>, IEnumerable<T2>> QueryMultipleSP<T1, T2>(string storedProcedure, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, int? commandTimeout = null, string connectionString = null)
        {
            IEnumerable[] lists = QueryMultiple<T1, T2, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object>(2, storedProcedure, param, outParam, transaction, commandTimeout, CommandType.StoredProcedure, connectionString);
            return new Tuple<IEnumerable<T1>, IEnumerable<T2>>(
                (IEnumerable<T1>)lists[0],
                (IEnumerable<T2>)lists[1]
            );
        }

        private IEnumerable[] QueryMultiple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25>(int readCount, string sql, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null, string connectionString = null)
        {
            CombineParameters(ref param, outParam);

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
            {
                connection.Open();
                SqlMapper.GridReader gr = connection.QueryMultiple(sql, param: (object)param, transaction: transaction, commandTimeout: GetTimeout(commandTimeout), commandType: CommandType.StoredProcedure);

                IEnumerable[] lists = new IEnumerable[readCount];

                if (readCount >= 1 && !gr.IsConsumed)
                {
                    lists[0] = gr.Read<T1>();
                    if (readCount >= 2 && !gr.IsConsumed)
                    {
                        lists[1] = gr.Read<T2>();
                        if (readCount >= 3 && !gr.IsConsumed)
                        {
                            lists[2] = gr.Read<T3>();
                            if (readCount >= 4 && !gr.IsConsumed)
                            {
                                lists[3] = gr.Read<T4>();
                                if (readCount >= 5 && !gr.IsConsumed)
                                {
                                    lists[4] = gr.Read<T5>();
                                    if (readCount >= 6 && !gr.IsConsumed)
                                    {
                                        lists[5] = gr.Read<T6>();
                                        if (readCount >= 7 && !gr.IsConsumed)
                                        {
                                            lists[6] = gr.Read<T7>();
                                            if (readCount >= 8 && !gr.IsConsumed)
                                            {
                                                lists[7] = gr.Read<T8>();
                                                if (readCount >= 9 && !gr.IsConsumed)
                                                {
                                                    lists[8] = gr.Read<T9>();
                                                    if (readCount >= 10 && !gr.IsConsumed)
                                                    {
                                                        lists[9] = gr.Read<T10>();
                                                        if (readCount >= 11 && !gr.IsConsumed)
                                                        {
                                                            lists[10] = gr.Read<T11>();
                                                            if (readCount >= 12 && !gr.IsConsumed)
                                                            {
                                                                lists[11] = gr.Read<T12>();
                                                                if (readCount >= 13 && !gr.IsConsumed)
                                                                {
                                                                    lists[12] = gr.Read<T13>();
                                                                    if (readCount >= 14 && !gr.IsConsumed)
                                                                    {
                                                                        lists[13] = gr.Read<T14>();
                                                                        if (readCount >= 15 && !gr.IsConsumed)
                                                                        {
                                                                            lists[14] = gr.Read<T15>();
                                                                            if (readCount >= 16 && !gr.IsConsumed)
                                                                            {
                                                                                lists[15] = gr.Read<T16>();
                                                                                if (readCount >= 17 && !gr.IsConsumed)
                                                                                {
                                                                                    lists[16] = gr.Read<T17>();
                                                                                    if (readCount >= 18 && !gr.IsConsumed)
                                                                                    {
                                                                                        lists[17] = gr.Read<T18>();
                                                                                        if (readCount >= 19 && !gr.IsConsumed)
                                                                                        {
                                                                                            lists[18] = gr.Read<T19>();
                                                                                            if (readCount >= 20 && !gr.IsConsumed)
                                                                                            {
                                                                                                lists[19] = gr.Read<T20>();
                                                                                                if (readCount >= 21 && !gr.IsConsumed)
                                                                                                {
                                                                                                    lists[20] = gr.Read<T21>();
                                                                                                    if (readCount >= 22 && !gr.IsConsumed)
                                                                                                    {
                                                                                                        lists[21] = gr.Read<T22>();
                                                                                                        if (readCount >= 23 && !gr.IsConsumed)
                                                                                                        {
                                                                                                            lists[22] = gr.Read<T23>();
                                                                                                            if (readCount >= 24 && !gr.IsConsumed)
                                                                                                            {
                                                                                                                lists[23] = gr.Read<T24>();
                                                                                                                if (readCount >= 25 && !gr.IsConsumed)
                                                                                                                {
                                                                                                                    lists[24] = gr.Read<T25>();
                                                                                                                }
                                                                                                            }
                                                                                                        }
                                                                                                    }
                                                                                                }
                                                                                            }
                                                                                        }
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                return lists;
            }
        }

        #region CombineParameters

        public void CombineParameters(ref dynamic param, dynamic outParam = null)
        {
            if (outParam != null)
            {
                if (param != null)
                {
                    param = new DynamicParameters(param);
                    ((DynamicParameters)param).AddDynamicParams(outParam);
                }
                else
                {
                    param = outParam;
                }
            }
        }

        #endregion

        #region Connection String & Timeout  
        public int ConnectionTimeout { get; set; }

        public int GetTimeout(int? commandTimeout = null)
        {
            if (commandTimeout.HasValue)
                return commandTimeout.Value;

            return ConnectionTimeout;
        }



        #endregion
    }
}