using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using OyoAgro.DataAccess.Layer.Extensions;
using OyoAgro.DataAccess.Layer.Helpers;
using OyoAgro.DataAccess.Layer.Interfaces;
using OyoAgro.DataAccess.Layer.Models.Entities;
using OyoAgro.DataAccess.Layer.Models.Entities.Operator;
using OyoAgro.DataAccess.Layer.Models.Params;
using OyoAgro.DataAccess.Layer.Models.ViewModels;

namespace OyoAgro.DataAccess.Layer.Repositories
{
    public class ProfileActivityRepository : DataRepository, IProfileActivityRepository
    {
        public async Task<List<Profileactivity>> GetList(ProfileActivityParam param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<Profileactivity>> GetList()
        {
            var list = await BaseRepository().FindList<Profileactivity>();
            return list.ToList();
        }

        public async Task<Profileactivity> GetEntity(int activityId)
        {
            var list = await BaseRepository().FindEntity<Profileactivity>(x => x.Activityid == activityId);
            return list;
        }



        public async Task<List<Profileactivity>> GetListParentId(int ParentId)
        {
            var list = await BaseRepository().FindList<Profileactivity>(x => x.Activityparentid == ParentId);
            return list.ToList();
        }

        public async Task<List<Profileactivity>> GetListByActivity(int activityId)
        {
            var list = await BaseRepository().FindList<Profileactivity>(x => x.Activityid == activityId);
            return list.ToList();
        }

        public async Task<List<Profileactivity>> GetPageList(ProfileActivityParam param, Pagination pagination)
        {
            var strSql = new StringBuilder();
            List<DbParameter> filter = ListFilter(param, strSql);
            var list = await BaseRepository().FindList<Profileactivity>(strSql.ToString(), filter.ToArray(), pagination);
            return list.ToList();
        }

        public async Task DeleteForm(int ids)
        {
            await BaseRepository().Delete<Profileactivity>(ids);
        }

        public async Task SaveForm(Profileactivity entity)
        {
            var message = string.Empty;
            var db = await BaseRepository().BeginTrans();
            try
            {
                if (entity.Activityid == 0)
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




        private Expression<Func<Profileactivity, bool>> ListFilter(ProfileActivityParam param)
        {
            var expression = ExtensionLinq.True<Profileactivity>();
            if (param != null)
            {
                if (param.Activityid > 0)
                {
                    expression = expression.And(t => t.Activityid == param.Activityid);
                }


            }
            return expression;
        }

        private List<DbParameter> ListFilter(ProfileActivityParam param, StringBuilder strSql)
        {
            strSql.Append(@"SELECT a.activityid,
                                   a.activityparentid,
                            FROM profileactivity a
                            WHERE 1 = 1");
            var parameter = new List<DbParameter>();
            if (param != null)
            {
                //if (!string.IsNullOrEmpty(param.additionalActivityId.ToString()))
                //{
                //    strSql.Append(" AND a.ADDITIONALACTIVITYID like @ADDITIONALACTIVITYID");
                //    parameter.Add(DbParameterExtension.CreateDbParameter("@ADDITIONALACTIVITYID", '%' + param.additionalActivityId + '%'));
                //}
            }
            return parameter;
        }
    }
}
