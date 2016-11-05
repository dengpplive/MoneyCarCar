using AutoMapper;
using MoneyCarCar.Commons;
using MoneyCarCar.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyCarCar.DAL
{
    /// <summary>
    /// 短信模板表
    /// </summary>
    public class SystemSmsTemplateOper
    {
        SQLHelper sqlhelper = SQLHelper.Single;
        public int Exists(string where)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from SystemSmsTemplate");
            strSql.Append(" where ");
            strSql.Append(where);
            return Convert.ToInt32(sqlhelper.GetTable(strSql.ToString()).Rows[0][0].ToString());
        }
        public bool IsExists(string where)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from SystemSmsTemplate");
            strSql.Append(" where ");
            strSql.Append(where);
            return Convert.ToInt32(sqlhelper.GetTable(strSql.ToString()).Rows[0][0].ToString()) > 0 ? true : false;
        }
        public bool Delete(string where)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SystemSmsTemplate");
            strSql.Append(" where ");
            strSql.Append(where);
            return (sqlhelper.ExecNon(strSql.ToString()) > 0 ? true : false);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SystemSmsTemplate model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SystemSmsTemplate(");
            strSql.Append("TemplateName,TemplateContent ");
            strSql.Append(") values (");
            strSql.Append("@TemplateName,@TemplateContent ");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@TemplateName", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@TemplateContent", SqlDbType.VarChar,500)                                                  
            };
            parameters[0].Value = model.TemplateName;
            parameters[1].Value = model.TemplateContent;
            return sqlhelper.ExecNon(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SystemSmsTemplate model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SystemSmsTemplate set ");

            strSql.Append(" TemplateName = @TemplateName , ");
            strSql.Append(" TemplateContent = @TemplateContent  ");
            strSql.Append(" where Id=@Id ");

            SqlParameter[] parameters = {
			            new SqlParameter("@Id", SqlDbType.Int,4) ,            
                        new SqlParameter("@TemplateName", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@TemplateContent", SqlDbType.VarChar,500)                         
            };

            parameters[0].Value = model.Id;
            parameters[1].Value = model.TemplateName;
            parameters[2].Value = model.TemplateContent;
            return sqlhelper.ExecNon(strSql.ToString(), parameters) > 0 ? true : false;
        }
        /// <summary>
        /// 通过ID 更新一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="fileds"></param>
        /// <returns></returns>
        public bool Update(SystemSmsTemplate model, List<string> fileds)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("update {0} set ", model.GetType().Name);
            List<string> filedsList = new List<string>();
            List<SqlParameter> sqlParameter = new List<SqlParameter>();
            SqlParameter Param = new SqlParameter("@Id", SqlDbType.Int, 4);
            Param.Value = model.Id;
            sqlParameter.Add(Param);
            foreach (string filed in fileds)
            {
                filedsList.Add(string.Format("{0}=@{0}", filed));
                Param = new SqlParameter(string.Format("@{0}", filed), model.GetType().GetProperty(filed).GetValue(model, null));
                sqlParameter.Add(Param);
            }
            strSql.AppendFormat("{0}", string.Join(",", filedsList.ToArray()));
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = sqlParameter.ToArray();
            return sqlhelper.ExecNon(strSql.ToString(), parameters) > 0 ? true : false;
        }

        public bool Update(SystemSmsTemplate model, List<string> fileds, string sqlWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("update {0} set ", model.GetType().Name);
            List<string> filedsList = new List<string>();
            List<SqlParameter> sqlParameter = new List<SqlParameter>();
            SqlParameter Param = new SqlParameter("@Id", SqlDbType.Int, 4);
            if (string.IsNullOrEmpty(sqlWhere))
            {
                Param.Value = model.Id;
                sqlParameter.Add(Param);
            }
            foreach (string filed in fileds)
            {
                filedsList.Add(string.Format("{0}=@{0}", filed));
                Param = new SqlParameter(string.Format("@{0}", filed), model.GetType().GetProperty(filed).GetValue(model, null));
                sqlParameter.Add(Param);
            }
            strSql.AppendFormat("{0}", string.Join(",", filedsList.ToArray()));
            if (string.IsNullOrEmpty(sqlWhere))
            {
                strSql.Append(" where Id=@Id ");
            }
            else
            {
                strSql.AppendFormat(" where 1=1 and {0} ", sqlWhere);
            }
            SqlParameter[] parameters = sqlParameter.ToArray();
            return sqlhelper.ExecNon(strSql.ToString(), parameters) > 0 ? true : false;
        }

        public bool Update(List<string> keyVal, string sqlWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SystemSmsTemplate set ");
            strSql.AppendFormat(" {0} ", string.Join(",", keyVal.ToArray()));
            if (!string.IsNullOrEmpty(sqlWhere))
            {
                strSql.AppendFormat(" where 1=1 and {0} ", sqlWhere);
            }
            return sqlhelper.ExecNon(strSql.ToString(), null) > 0 ? true : false;
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SystemSmsTemplate GetModel(string where)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 Id, TemplateName, TemplateContent ");
            strSql.Append("  from SystemSmsTemplate ");
            strSql.Append(" where ");
            strSql.Append(where);

            var dt = sqlhelper.GetTable(strSql.ToString());
            SystemSmsTemplate model = null;
            if (dt.Rows.Count > 0)
            {
                model = Mapper.DynamicMap<IDataReader, List<SystemSmsTemplate>>(dt.CreateDataReader()).FirstOrDefault();
            }
            return model;
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<SystemSmsTemplate> GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM SystemSmsTemplate ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            var dt = sqlhelper.GetTable(strSql.ToString());
            return Mapper.DynamicMap<IDataReader, List<SystemSmsTemplate>>(dt.CreateDataReader());
        }
        /// <summary>
        /// 获得分页数据
        /// </summary>
        public List<SystemSmsTemplate> GetPagerList(out int TotalCount, int PageSize, int PageIndex, string strWhere = " 1=1 ", string fileds = "*", string OrderBy = " Id desc")
        {
            var dt = sqlhelper.GetPagerTable(typeof(SystemSmsTemplate).Name, out TotalCount, PageSize, PageIndex, strWhere, fileds, OrderBy);
            return Mapper.DynamicMap<IDataReader, List<SystemSmsTemplate>>(dt.CreateDataReader());
        }
    }
}
