using AutoMapper;
using MoneyCarCar.Commons;
using MoneyCarCar.Models.ModelDto.RQParam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MoneyCarCar.DAL
{
    /// <summary>
    /// 基本的数据查询CRUD 类型T需要有Id
    /// </summary>    
    public class BaseHelper
    {
        public SQLHelper sqlhelper = SQLHelper.Single;
        public int Exists<T>(string where)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select count(1) from {0}", typeof(T).Name);
            strSql.Append(" where ");
            strSql.Append(where);
            return Convert.ToInt32(sqlhelper.GetTable(strSql.ToString()).Rows[0][0].ToString());
        }
        public bool IsExists<T>(string where)
        {
            return Exists<T>(where) > 0 ? true : false;
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add<T>(T model)
        {
            StringBuilder strSql = new StringBuilder();
            Type t = model.GetType();
            PropertyInfo[] properties = t.GetProperties(BindingFlags.Public | BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.GetProperty);
            List<SqlParameter> paramList = new List<SqlParameter>();
            List<string> fileds = new List<string>();
            foreach (var item in properties)
            {
                var val = Convert.ChangeType(item.GetValue(model, null), item.PropertyType);
                //id自增长不用添加数据
                if (item.Name.ToLower() != "id" && val != null)
                {
                    fileds.Add("@" + item.Name);
                    paramList.Add(new SqlParameter(string.Format("@{0}", item.Name), val));
                }
            }
            strSql.AppendFormat(" insert into {0}(", typeof(T).Name);
            strSql.AppendFormat(" {0} ", string.Join(",", fileds.Select(p => p.TrimStart('@'))));
            strSql.Append(" ) values ( ");
            strSql.AppendFormat(" {0} ", string.Join(",", fileds.ToArray()));
            strSql.Append(" ) ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = paramList.ToArray();
            return sqlhelper.ExecNon(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 通过ID更新一条数据
        /// </summary>
        public bool Update<T>(T model)
        {
            StringBuilder strSql = new StringBuilder();
            Type t = model.GetType();
            PropertyInfo[] properties = t.GetProperties(BindingFlags.Public | BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.GetProperty);
            List<SqlParameter> paramList = new List<SqlParameter>();
            List<string> fileds = new List<string>();
            string Id = string.Empty;
            foreach (var item in properties)
            {
                if (item.Name.ToLower() != "id")
                {
                    fileds.Add(string.Format("{0}=@{0}", item.Name));
                }
                else
                {
                    Id = string.Format("{0}=@{0}", item.Name);
                }
                var val = Convert.ChangeType(item.GetValue(model, null), item.PropertyType);
                paramList.Add(new SqlParameter(string.Format("@{0}", item.Name), val));
            }
            strSql.AppendFormat(" update {0} set {1} ", typeof(T).Name, string.Join(",", fileds.ToArray()));
            strSql.AppendFormat(" where {0}", Id);
            SqlParameter[] parameters = paramList.ToArray();
            return sqlhelper.ExecNon(strSql.ToString(), parameters) > 0 ? true : false;
        }

        /// <summary>
        /// 通过Id 更新部分数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="fileds"></param>
        /// <returns></returns>
        public bool Update<T>(T model, List<string> updateFileds)
        {
            Type t = model.GetType();
            PropertyInfo[] properties = t.GetProperties(BindingFlags.Public | BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.GetProperty);
            List<SqlParameter> paramList = new List<SqlParameter>();
            List<string> fileds = new List<string>();
            string Id = string.Empty;
            foreach (var item in properties)
            {
                if (item.Name.ToLower() != "id")
                {
                    if (updateFileds.Exists(p => p.ToLower() == item.Name.ToLower()))
                    {
                        fileds.Add(string.Format("{0}=@{0}", item.Name));
                    }
                }
                else
                {
                    Id = string.Format("{0}=@{0}", item.Name);
                }
                var val = Convert.ChangeType(item.GetValue(model, null), item.PropertyType);
                paramList.Add(new SqlParameter(string.Format("@{0}", item.Name), val));
            }
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(" update {0} set {1} ", typeof(T).Name, string.Join(",", fileds.ToArray()));
            strSql.AppendFormat(" where {0}", Id);
            SqlParameter[] parameters = paramList.ToArray();
            return sqlhelper.ExecNon(strSql.ToString(), parameters) > 0 ? true : false;
        }

        /// <summary>
        /// 通过where条件更新部分数据
        /// </summary>
        /// <param name="model">更新为model中的数据</param>
        /// <param name="fileds">更新字段</param>
        /// <returns></returns>
        public bool Update<T>(T model, List<string> updateFileds, string sqlWhere)
        {
            Type t = model.GetType();
            PropertyInfo[] properties = t.GetProperties(BindingFlags.Public | BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.GetProperty);
            List<SqlParameter> paramList = new List<SqlParameter>();
            List<string> fileds = new List<string>();
            string Id = string.Empty;
            foreach (var item in properties)
            {
                if (item.Name.ToLower() != "id")
                {
                    if (updateFileds.Exists(p => p.ToLower() == item.Name.ToLower()))
                    {
                        fileds.Add(string.Format("{0}=@{0}", item.Name));
                    }
                }
                else
                {
                    Id = string.Format("{0}=@{0}", item.Name);
                }
                var val = Convert.ChangeType(item.GetValue(model, null), item.PropertyType);
                paramList.Add(new SqlParameter(string.Format("@{0}", item.Name), val));
            }
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(" update {0} set {1} ", typeof(T).Name, string.Join(",", fileds.ToArray()));
            if (!string.IsNullOrEmpty(sqlWhere))
            {
                strSql.AppendFormat(" where 1=1 and {0}", sqlWhere);
            }
            else
            {
                strSql.AppendFormat(" where {0}", Id);
            }
            SqlParameter[] parameters = paramList.ToArray();
            return sqlhelper.ExecNon(strSql.ToString(), parameters) > 0 ? true : false;
        }
        /// <summary>
        /// 根据条件更新数据   数据列表 key=value
        /// </summary>
        /// <param name="keyVal"></param>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public bool Update<T>(List<string> keyVal, string sqlWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("update {0} set ", typeof(T).Name);
            strSql.AppendFormat(" {0} ", string.Join(",", keyVal.ToArray()));
            if (!string.IsNullOrEmpty(sqlWhere))
            {
                strSql.AppendFormat(" where 1=1 and {0} ", sqlWhere);
            }
            return sqlhelper.ExecNon(strSql.ToString(), null) > 0 ? true : false;
        }
        /// <summary>
        /// 根据条件删除数据
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public bool Delete<T>(string where)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("delete from {0}", typeof(T).Name);
            if (!string.IsNullOrEmpty(where))
                strSql.AppendFormat(" where 1=1 and {0}", where);
            return (sqlhelper.ExecNon(strSql.ToString()) > 0 ? true : false);
        }
        /// <summary>
        /// 根据Id删除数据
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public bool DeleteById<T>(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("delete from {0}", typeof(T).Name);
            strSql.AppendFormat(" where Id={0} ", Id);
            return (sqlhelper.ExecNon(strSql.ToString()) > 0 ? true : false);
        }
        /// <summary>
        /// 通过Id 得到一个对象实体
        /// </summary>
        public T GetModelById<T>(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select *  from {0} ", typeof(T).Name);
            strSql.AppendFormat(" where Id={0} ", Id);
            var dt = sqlhelper.GetTable(strSql.ToString());
            T model = default(T);
            if (dt.Rows.Count > 0)
            {
                model = Mapper.DynamicMap<IDataReader, List<T>>(dt.CreateDataReader()).FirstOrDefault();
            }
            return model;
        }

        /// <summary>
        /// 获取对象列表
        /// </summary>
        public List<T> GetList<T>(string sqlWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select * FROM {0} ", typeof(T).Name);
            if (sqlWhere.Trim() != "")
            {
                strSql.AppendFormat(" where 1=1 and {0}", sqlWhere);
            }
            var dt = sqlhelper.GetTable(strSql.ToString());
            return Mapper.DynamicMap<IDataReader, List<T>>(dt.CreateDataReader());
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public List<T> GetList<T>(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.AppendFormat(" top {0} ", Top);
            }
            strSql.Append(" * ");
            strSql.AppendFormat(" FROM {0} ", typeof(T).Name);
            if (strWhere.Trim() != "")
            {
                strSql.AppendFormat(" where 1=1 and {0}", strWhere);
            }
            if (!string.IsNullOrEmpty(filedOrder))
            {
                strSql.AppendFormat(" order by {0}", filedOrder);
            }
            else
            {
                strSql.Append(" order by Id Desc");
            }
            var dt = sqlhelper.GetTable(strSql.ToString());
            return Mapper.DynamicMap<IDataReader, List<T>>(dt.CreateDataReader());
        }

        /// <summary>
        /// 获得分页数据
        /// </summary>
        public List<T> GetPagerList<T>(out int TotalCount, int PageSize, int PageIndex, string strWhere = " 1=1 ", string fileds = "*", string OrderBy = " Id desc")
        {
            var dt = sqlhelper.GetPagerTable(typeof(T).Name, out TotalCount, PageSize, PageIndex, strWhere, fileds, OrderBy);
            return Mapper.DynamicMap<IDataReader, List<T>>(dt.CreateDataReader());
        }
        /// <summary>
        /// 调用存储过程 返回列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="procName"></param>
        /// <returns></returns>
        public List<T> GetDataByProc<T>(RQProcParam param)
        {
            DataTable table = GetDataTableByProc(param);
            return Mapper.DynamicMap<IDataReader, List<T>>(table.CreateDataReader());
        }
        /// <summary>
        /// 调用存储过程返回DataTable
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public DataTable GetDataTableByProc(RQProcParam param)
        {
            List<SqlParameter> sqlParamterList = new List<SqlParameter>();
            foreach (KeyValuePair<string, object> item in param.DicParam)
            {
                if (item.Value != null)
                    sqlParamterList.Add(new SqlParameter(string.Format("@{0}", item.Key), item.Value));
            }
            foreach (KeyValuePair<string, TVPItem> item in param.DicTvpParam)
            {
                if (item.Value.Value != null && !string.IsNullOrEmpty(item.Key))
                {
                    SqlParameter sqlParameter = new SqlParameter(string.Format("@{0}", item.Key), item.Value.Value);
                    sqlParameter.SqlDbType = SqlDbType.Structured;
                    sqlParameter.TypeName = item.Value.TypeName;
                }
            }
            System.Data.DataSet ds = sqlhelper.GetDataSetByProc(param.ProcName, sqlParamterList.ToArray());
            return ds.Tables[0];
        }
        /// <summary>
        /// 执行存储过程  返回结果: 受影响的行数。        
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public int ExecByProc(RQProcParam param)
        {
            List<SqlParameter> sqlParamterList = new List<SqlParameter>();
            foreach (KeyValuePair<string, object> item in param.DicParam)
            {
                if (item.Value != null)
                    sqlParamterList.Add(new SqlParameter(string.Format("@{0}", item.Key), item.Value));
            }
            foreach (KeyValuePair<string, TVPItem> item in param.DicTvpParam)
            {
                if (item.Value.Value != null && !string.IsNullOrEmpty(item.Key))
                {
                    SqlParameter sqlParameter = new SqlParameter(string.Format("@{0}", item.Key), item.Value.Value);
                    sqlParameter.SqlDbType = SqlDbType.Structured;
                    sqlParameter.TypeName = item.Value.TypeName;
                }
            }
            return sqlhelper.ExecByProc(param.ProcName, sqlParamterList.ToArray());
        }
        /// <summary>
        ///  执行存储过程  带返回值
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public object ExecReturnByProc(RQProcParam param)
        {
            List<SqlParameter> sqlParamterList = new List<SqlParameter>();
            foreach (KeyValuePair<string, object> item in param.DicParam)
            {
                if (item.Value != null)
                    sqlParamterList.Add(new SqlParameter(string.Format("@{0}", item.Key), item.Value));
            }
            foreach (KeyValuePair<string, TVPItem> item in param.DicTvpParam)
            {
                if (item.Value.Value != null && !string.IsNullOrEmpty(item.Key))
                {
                    SqlParameter sqlParameter = new SqlParameter(string.Format("@{0}", item.Key), item.Value.Value);
                    sqlParameter.SqlDbType = SqlDbType.Structured;
                    sqlParameter.TypeName = item.Value.TypeName;
                }
            }
            return sqlhelper.ExecReturnByProc(param.ProcName, sqlParamterList.ToArray());
        }
    }
}
