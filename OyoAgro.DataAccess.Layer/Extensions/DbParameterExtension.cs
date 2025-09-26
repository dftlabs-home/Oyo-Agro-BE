using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;
using Oracle.ManagedDataAccess.Client;
using OyoAgro.DataAccess.Layer.Enums;
using OyoAgro.DataAccess.Layer.Helpers;

namespace OyoAgro.DataAccess.Layer.Extensions
{
    public class DbParameterExtension
    {
        // According to the database type configured in the configuration file
        // To create a parameter object for the corresponding database
        // <returns></returns>
        public static DbParameter CreateDbParameter()
        {
            switch (DbHelper.dbProvider)
            {
                case DatabaseProvider.SqlServer:
                    return new SqlParameter();

                case DatabaseProvider.MySql:
                    return new MySqlParameter();

                case DatabaseProvider.Oracle:
                    return new OracleParameter();

                case DatabaseProvider.PostgreSql:
                    return new Npgsql.NpgsqlParameter(); 

                default:
                    throw new Exception("The database type is currently not supported!");
            }
        }

        // According to the database type configured in the configuration file
        // To create a parameter object for the corresponding database
        // <returns></returns>
        public static DbParameter CreateDbParameter(string paramName, object value)
        {
            DbParameter param = CreateDbParameter();
            param.ParameterName = paramName;
            param.Value = value;
            return param;
        }

        // Convert the corresponding database parameters
        // <param name="dbParameter">Parameter</param>
        // <returns></returns>
        public static DbParameter[] ToDbParameter(DbParameter[] dbParameter)
        {
            int i = 0;
            int size = dbParameter.Length;
            DbParameter[]? _dbParameter = null;
            switch (DbHelper.dbProvider)
            {
                case DatabaseProvider.SqlServer:
                    _dbParameter = new SqlParameter[size];
                    while (i < size)
                    {
                        _dbParameter[i] = new SqlParameter(dbParameter[i].ParameterName, dbParameter[i].Value);
                        i++;
                    }
                    break;
                case DatabaseProvider.MySql:
                    _dbParameter = new MySqlParameter[size];
                    while (i < size)
                    {
                        _dbParameter[i] = new MySqlParameter(dbParameter[i].ParameterName, dbParameter[i].Value);
                        i++;
                    }
                    break;
                case DatabaseProvider.Oracle:
                    _dbParameter = new OracleParameter[size];
                    while (i < size)
                    {
                        _dbParameter[i] = new OracleParameter(dbParameter[i].ParameterName, dbParameter[i].Value);
                        i++;
                    }
                    break;
                default:
                    throw new Exception("The database type is currently not supported!");
            }
            return _dbParameter;
        }
    }
}
