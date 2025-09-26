using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OyoAgro.DataAccess.Layer.Extensions
{
    public class DbPageExtension
    {
        public static StringBuilder SqlServerPageSql(string strSql, DbParameter[] dbParameter, string sort, bool isAsc, int pageSize, int pageIndex)
        {
            CheckSqlParam(sort);

            StringBuilder sb = new StringBuilder();
            if (pageIndex == 0)
            {
                pageIndex = 1;
            }
            int startNum = (pageIndex - 1) * pageSize;
            int endNum = (pageIndex) * pageSize;
            string orderBy = string.Empty;

            if (!string.IsNullOrEmpty(sort))
            {
                if (sort.ToUpper().IndexOf("ASC") + sort.ToUpper().IndexOf("DESC") > 0)
                {
                    orderBy = " ORDER BY " + sort;
                }
                else
                {
                    orderBy = " ORDER BY " + sort + " " + (isAsc ? "ASC" : "DESC");
                }
            }
            else
            {
                orderBy = "ORDERE BY (SELECT 0)";
            }
            sb.Append("SELECT * FROM (SELECT ROW_NUMBER() Over (" + orderBy + ")");
            sb.Append(" AS ROWNUM, * From (" + strSql + ") t ) AS N WHERE ROWNUM > " + startNum + " AND ROWNUM <= " + endNum + "");
            return sb;
        }

        public static StringBuilder OraclePageSql(string strSql, DbParameter[] dbParameter, string sort, bool isAsc, int pageSize, int pageIndex)
        {
            CheckSqlParam(sort);

            StringBuilder sb = new StringBuilder();
            if (pageIndex == 0)
            {
                pageIndex = 1;
            }
            int startNum = (pageIndex - 1) * pageSize;
            int endNum = (pageIndex) * pageSize;
            string orderBy = string.Empty;

            if (!string.IsNullOrEmpty(sort))
            {
                if (sort.ToUpper().IndexOf("ASC") + sort.ToUpper().IndexOf("DESC") > 0)
                {
                    orderBy = " ORDER BY " + sort;
                }
                else
                {
                    orderBy = " ORDER BY " + sort + " " + (isAsc ? "ASC" : "DESC");
                }
            }
            sb.Append("SELECT * From (SELECT ROWNUM AS n,");
            sb.Append(" T.* From (" + strSql + orderBy + ") t )  N WHERE n > " + startNum + " AND n <= " + endNum + "");
            return sb;
        }

        public static StringBuilder MySqlPageSql(string strSql, DbParameter[] dbParameter, string sort, bool isAsc, int pageSize, int pageIndex)
        {
            CheckSqlParam(sort);

            StringBuilder sb = new StringBuilder();
            if (pageIndex == 0)
            {
                pageIndex = 1;
            }
            int num = (pageIndex - 1) * pageSize;
            string orderBy = string.Empty;

            if (!string.IsNullOrEmpty(sort))
            {
                if (sort.ToUpper().IndexOf("ASC") + sort.ToUpper().IndexOf("DESC") > 0)
                {
                    orderBy = " ORDER BY " + sort;
                }
                else
                {
                    orderBy = " ORDER BY " + sort + " " + (isAsc ? "ASC" : "DESC");
                }
            }
            sb.Append(strSql + orderBy);
            sb.Append(" LIMIT " + num + "," + pageSize + "");
            return sb;
        }

        public static string GetCountSql(string strSql)
        {
            string countSql = string.Empty;
            string strSqlCopy = strSql.ToLower();
            int selectIndex = strSqlCopy.IndexOf("SELECT ");
            int lastFromIndex = strSqlCopy.LastIndexOf(" FROM ");
            if (selectIndex >= 0 && lastFromIndex >= 0)
            {
                int backFromIndex = strSqlCopy.LastIndexOf(" FROM ", lastFromIndex);
                int backSelectIndex = strSqlCopy.LastIndexOf("SELECT ", lastFromIndex);
                if (backSelectIndex == selectIndex)
                {
                    countSql = "SELECT COUNT(*) " + strSql.Substring(lastFromIndex);
                    return countSql;
                }
            }
            countSql = "SELECT COUNT(1) FROM (" + strSql + ") t";
            return countSql;
        }

        public static StringBuilder PostgreSqlPageSql(string strSql, DbParameter[] dbParameter, string sort, bool isAsc, int pageSize, int pageIndex)
        {
            CheckSqlParam(sort);

            StringBuilder sb = new StringBuilder();

            if (pageIndex == 0)
            {
                pageIndex = 1;
            }

            int offset = (pageIndex - 1) * pageSize;
            string orderBy;

            if (!string.IsNullOrEmpty(sort))
            {
                if (sort.ToUpper().Contains("ASC") || sort.ToUpper().Contains("DESC"))
                {
                    orderBy = " ORDER BY " + sort;
                }
                else
                {
                    orderBy = " ORDER BY " + sort + " " + (isAsc ? "ASC" : "DESC");
                }
            }
            else
            {
                orderBy = " ORDER BY (SELECT NULL)"; // fallback ordering
            }

            sb.Append(strSql);
            sb.Append(orderBy);
            sb.Append($" LIMIT {pageSize} OFFSET {offset}");

            return sb;
        }
 

        private static void CheckSqlParam(string param)
        {
            if (IsSafeSqlParam(param))
            {
                throw new ArgumentException("Parameters with SQL injection");
            }
        }

        public static bool IsSafeSqlParam(string value)
        {
            return !Regex.IsMatch(value, @"[-|;|,|\/|\(|\)|\[|\]|\}|\{|%|@|\*|!|\']");
        }
    }
}
