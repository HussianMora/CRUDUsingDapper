﻿using CRUDWITHDAPPER.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDWITHDAPPER.PatientBL
{
    interface IDataRepository
    {
        List<T> CommonGetMethod<T>(string spName);

        void CommonSavePatient(object param, string storedProcedure);

        object GetParticularPatient(object id, string spName);

        void CommonDelete(object id, string spName);

        DataSet ReturnedDataSet(string sp, dynamic param);

        object getCustomForm(object patient_id, string spName);

        LoginViewModel GetCredentials(object param, string spName);

        Tuple<IEnumerable<T1>, IEnumerable<T2>> QueryMultipleSP<T1, T2>(string storedProcedure, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, int? commandTimeout = null, string connectionString = null);

    }
}
