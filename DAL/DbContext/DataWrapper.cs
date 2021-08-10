using DAL.DataModel;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;

namespace DAL.DbContext
{
    public class DataWrapper
    {
        private readonly IConfiguration _configuration;
        private const string DB_TYPE_SQL_SERVER = "MSSQLServer";

        private enum DbReturnType
        {
            RETURN_PARAMETER,
            OBJECT_LIST
        }

        protected string mDbType;
        protected string mConnectionString;

        public DataWrapper(IConfiguration configuration)
        {
            _configuration = configuration;
            mConnectionString = _configuration.GetConnectionString("dbConnection");
            mDbType = DB_TYPE_SQL_SERVER;
        }

        public object ExecuteNonQuery(string storedProcedureName, Dictionary<string, object> parametersList)
        {
            return Execute<Model>(storedProcedureName, CommandType.StoredProcedure, parametersList, DbReturnType.RETURN_PARAMETER, null);
        }

        public PageResult ExecuteDataReader<T>(string storedProcedureName, Dictionary<string, object> parametersList, Type typeOfT)
        {
            return (PageResult)Execute<T>(storedProcedureName, CommandType.StoredProcedure, parametersList, DbReturnType.OBJECT_LIST, typeOfT);
        }

        public List<T> ExecuteSQLDataReader<T>(string sql, Dictionary<string, object> parametersList, Type typeOfT)
        {
            return (List<T>)Execute<T>(sql, CommandType.Text, parametersList, DbReturnType.OBJECT_LIST, typeOfT);
        }

        private object Execute<T>(string storedProcedureName, CommandType commandType, Dictionary<string, object> parametersList, DbReturnType returnType, Type typeOfT)
        {
            object tmpValue;
            return Execute<T>(storedProcedureName, commandType, parametersList, returnType, typeOfT, out tmpValue);
        }

        private object Execute<T>(string storedProcedureName, CommandType commandType, Dictionary<string, object> parametersList, DbReturnType returnType, Type typeOfT, out object returnValue)
        {
            PageResult pageResult = new PageResult();
            returnValue = null;
            using (DbConnection connection = GetDbConnection(mDbType, mConnectionString))
            {
                using (DbCommand command = connection.CreateCommand())
                {
                    command.CommandType = commandType;
                    command.CommandText = storedProcedureName;

                    DbParameter returnParameter = SetupParameters(command, parametersList, returnType);

                    connection.Open();

                    DbDataReader reader = null;

                    switch (returnType)
                    {
                        case DbReturnType.RETURN_PARAMETER:
                            command.ExecuteNonQuery();
                            return returnParameter.Value;
                        case DbReturnType.OBJECT_LIST:
                            if (typeOfT != null)
                            {
                                reader = command.ExecuteReader();
                                List<T> list = GetListFromReader<T>(reader, typeOfT);
                                pageResult.listItems = list;

                                reader.NextResult();

                                while (reader.Read())
                                {
                                    pageResult.totalCount = (int)reader["totalCount"];
                                }

                                reader.Close();
                                return pageResult;
                            }
                            return null;
                        default:
                            return null;
                    }
                }
            }
        }

        private List<T> GetListFromReader<T>(DbDataReader reader, Type typeOfT)
        {
            List<T> modelList = new List<T>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    T model = (T)Activator.CreateInstance(typeOfT);
                    object _object = model;
                    ((Model)_object).Populate(reader);
                    modelList.Add(model);
                }
            }
            return modelList;
        }

        private DbConnection GetDbConnection(string databaseType, string connectionString)
        {
            DbConnection connection = null;

            switch (databaseType)
            {
                case DB_TYPE_SQL_SERVER:
                    connection = new SqlConnection(connectionString);
                    break;
            }

            return connection;
        }

        private DbParameter SetupParameters(DbCommand command, Dictionary<string, object> parametersList, DbReturnType returnType)
        {
            DbParameter returnParameter = null;
            if (parametersList != null)
            {
                DbParameter[] parameters = new DbParameter[parametersList.Count + 1];

                int i = 0;
                foreach (var param in parametersList)
                {
                    string parameterName = param.Key;

                    switch (mDbType)
                    {
                        case DB_TYPE_SQL_SERVER:
                            parameterName = "@" + parameterName;
                            break;
                    }

                    DbParameter parameter = command.CreateParameter();
                    parameter.ParameterName = parameterName;
                    parameter.Value = param.Value == null ? DBNull.Value : param.Value;
                    parameters[i++] = parameter;
                }

                returnParameter = command.CreateParameter();

                if (returnType == DbReturnType.RETURN_PARAMETER)
                {
                    returnParameter.ParameterName = "uniqueid";
                    returnParameter.DbType = DbType.Int64;
                    returnParameter.Direction = ParameterDirection.Output;
                }
                else
                    returnParameter.Direction = ParameterDirection.ReturnValue;

                parameters[i] = returnParameter;

                command.Parameters.AddRange(parameters);
            }

            return returnParameter;
        }
    }
}
