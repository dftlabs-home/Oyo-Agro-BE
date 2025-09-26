using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OyoAgro.DataAccess.Layer.Extensions;
using OyoAgro.DataAccess.Layer.Helpers;
using OyoAgro.DataAccess.Layer.Interfaces;
using OyoAgro.DataAccess.Layer.Models;
using OyoAgro.DataAccess.Layer.Models.Entities;
using OyoAgro.DataAccess.Layer.Models.Entities.Operator;
using OyoAgro.DataAccess.Layer.Models.Params;
using OyoAgro.DataAccess.Layer.Models.ViewModels;

namespace OyoAgro.DataAccess.Layer.Repositories
{
    public class ProfileAdditionalActivityRepository: DataRepository, IProfileAdditionalActivityRepository
    {
        public async Task<List<Profileadditionalactivity>> GetList(ProfileadditionalactivityParam param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<Profileadditionalactivity>> GetList()
        {
            var list = await BaseRepository().FindList<Profileadditionalactivity>();
            return list.ToList();
        }



        public async Task<List<Profileadditionalactivity>> GetListByUser(int userId)
        {
            var list = await BaseRepository().FindList<Profileadditionalactivity>(x => x.Userid == userId);
            return list.ToList();
        }

        public async Task<List<Profileadditionalactivity>> GetListByActivity(int activityId)
        {
            var list = await BaseRepository().FindList<Profileadditionalactivity>(x => x.Activityid == activityId);
            return list.ToList();
        }

        public async Task<List<Profileadditionalactivity>> GetPageList(ProfileadditionalactivityParam param, Pagination pagination)
        {
            var strSql = new StringBuilder();
            List<DbParameter> filter = ListFilter(param, strSql);
            var list = await BaseRepository().FindList<Profileadditionalactivity>(strSql.ToString(), filter.ToArray(), pagination);
            return list.ToList();
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await BaseRepository().Delete<Profileadditionalactivity>(idArr);
        }

        public async Task SaveForm(Profileadditionalactivity entity)
        {
            var message = string.Empty;
            var db = await BaseRepository().BeginTrans();
            try
            {
                if (entity.Userid == 0)
                {
                    await entity.Create();
                    await db.Insert(entity);
                }
                else
                {
                    await entity.Modify();
                    await db.Update(entity);
                }

              

                await db.CommitTrans();
            }
            catch (Exception ex)
            {
                await db.RollbackTrans();
                throw;
            }
        }


        private Expression<Func<Profileadditionalactivity, bool>> ListFilter(ProfileadditionalactivityParam param)
        {
            var expression = ExtensionLinq.True<Profileadditionalactivity>();
            if (param != null)
            {
                if (param.UserId > 0)
                {
                    expression = expression.And(t => t.Userid == param.UserId);
                }


            }
            return expression;
        }

        private List<DbParameter> ListFilter(ProfileadditionalactivityParam param, StringBuilder strSql)
        {
            strSql.Append(@"SELECT a.additionalactivityid,
                                   a.userid,
                                   a.activityid,
                            FROM profileadditionalactivity a
                            WHERE 1 = 1");
            var parameter = new List<DbParameter>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.additionalActivityId.ToString()))
                {
                    strSql.Append(" AND a.ADDITIONALACTIVITYID like @ADDITIONALACTIVITYID");
                    parameter.Add(DbParameterExtension.CreateDbParameter("@ADDITIONALACTIVITYID", '%' + param.additionalActivityId + '%'));
                }
            }
            return parameter;
        }

    }
}
