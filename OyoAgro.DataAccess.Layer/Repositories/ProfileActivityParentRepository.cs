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
    public class ProfileActivityParentRepository : DataRepository, IProfileActivityParentRepository
    {
        public async Task<List<Profileactivityparent>> GetList(ProfileActivityParentParam param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<Profileactivityparent>> GetList()
        {
            var list = await BaseRepository().FindList<Profileactivityparent>();
            return list.ToList();
        }

        public async Task<Profileactivityparent> GetEntity(int activityParentId)
        {
            var list = await BaseRepository().FindEntity<Profileactivityparent>(x => x.Activityparentid == activityParentId);
            return list;
        }



        public async Task<List<Profileactivityparent>> GetListParentId(int ParentId)
        {
            var list = await BaseRepository().FindList<Profileactivityparent>(x => x.Activityparentid == ParentId);
            return list.ToList();
        }

        public async Task<List<Profileactivityparent>> GetListByActivity(int activityParentId)
        {
            var list = await BaseRepository().FindList<Profileactivityparent>(x => x.Activityparentid == activityParentId);
            return list.ToList();
        }

        public async Task<List<Profileactivityparent>> GetPageList(ProfileActivityParentParam param, Pagination pagination)
        {
            var strSql = new StringBuilder();
            List<DbParameter> filter = ListFilter(param, strSql);
            var list = await BaseRepository().FindList<Profileactivityparent>(strSql.ToString(), filter.ToArray(), pagination);
            return list.ToList();
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await BaseRepository().Delete<Profileactivityparent>(idArr);
        }

        public async Task SaveForm(Profileactivityparent entity)
        {
            var message = string.Empty;
            var db = await BaseRepository().BeginTrans();
            try
            {
                if (entity.Activityparentid == 0)
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




        private Expression<Func<Profileactivityparent, bool>> ListFilter(ProfileActivityParentParam param)
        {
            var expression = ExtensionLinq.True<Profileactivityparent>();
            if (param != null)
            {
                if (param.Activityparentid > 0)
                {
                    expression = expression.And(t => t.Activityparentid == param.Activityparentid);
                }


            }
            return expression;
        }

        private List<DbParameter> ListFilter(ProfileActivityParentParam param, StringBuilder strSql)
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
