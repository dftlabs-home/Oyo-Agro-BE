using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OyoAgro.DataAccess.Layer.Models.Dtos;
using OyoAgro.DataAccess.Layer.Repositories.Base;

namespace OyoAgro.DataAccess.Layer.Models.Entities.Operator
{
    public class DataRepository: RepositoryFactory
    {
        public async Task<OperatorInfo?> GetUserByToken(string token)
        {
            //var userToken = token.ToStr().Trim();

            //bool validate = SecurityHelper.IsSafeSqlParam(token);
            //if (validate)
            //{
            //    return null;
            //}



            if (string.IsNullOrWhiteSpace(token))
                return null;

            var userToken = token.Trim();


            var strSql = $@"
                        SELECT * 
                        FROM useraccount 
                        WHERE apitoken = '{userToken.Replace("'", "''")}'";



            //var strSql = new StringBuilder();
            //strSql.Append(@"SELECT *
            //                FROM TBL_USER a
            //                WHERE ApiToken='" + userToken + "' ");
            var operatorInfo = await BaseRepository().FindObject<OperatorInfo>(strSql.ToString());
            //if (operatorInfo != null)
            //{
            //    #region Tenant Info
            //    if (!string.IsNullOrEmpty(operatorInfo.TenantId.ToString()))
            //    {
            //    }
            //    #endregion
            //}
            return operatorInfo;
        }

        public async Task<int> RunScript(string script)
        {
            var strSql = new StringBuilder();
            strSql.Append(script);
            return (int)await BaseRepository().FindObject(strSql.ToString());
        }   
    }
}
