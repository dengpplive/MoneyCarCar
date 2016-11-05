using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MoneyCarCar.Models;
using MoneyCarCar.Commons;
using System.Data.SqlClient;

namespace MoneyCarCar.DAL
{
    //SystemNotice
    public class SystemNoticeOper
    {
        SQLHelper sqlhelper = SQLHelper.Single;


        public int Exists(string where)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from SystemNotice");
            strSql.Append(" where ");
            strSql.Append(where);
            return Convert.ToInt32(sqlhelper.GetTable(strSql.ToString()).Rows[0][0].ToString());

        }
        public bool Delete(string where)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SystemNotice");
            strSql.Append(" where ");
            strSql.Append(where);
            return (sqlhelper.ExecNon(strSql.ToString()) > 0 ? true : false);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SystemNotice model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SystemNotice(");
            strSql.Append("NoticeTitle,NoticeContent,NoticeType,NoticeStatus,NoticeAddDate,NoticeRealseAccount");
            strSql.Append(") values (");
            strSql.Append("@NoticeTitle,@NoticeContent,@NoticeType,@NoticeStatus,@NoticeAddDate,@NoticeRealseAccount");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@NoticeTitle", SqlDbType.VarChar,200) ,            
                        new SqlParameter("@NoticeContent", SqlDbType.VarChar,-1) ,            
                        new SqlParameter("@NoticeType", SqlDbType.Int,4) ,            
                        new SqlParameter("@NoticeStatus", SqlDbType.Int,4) ,            
                        new SqlParameter("@NoticeAddDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@NoticeRealseAccount", SqlDbType.VarChar,50)             
              
            };

            parameters[0].Value = model.NoticeTitle;
            parameters[1].Value = model.NoticeContent;
            parameters[2].Value = model.NoticeType;
            parameters[3].Value = model.NoticeStatus;
            parameters[4].Value = model.NoticeAddDate;
            parameters[5].Value = model.NoticeRealseAccount; return sqlhelper.ExecNon(strSql.ToString(), parameters);


        }

        /// <summary>
        /// 更新一条数据 通过ID更新所有
        /// </summary>
        public bool Update(SystemNotice model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SystemNotice set ");

            strSql.Append(" NoticeTitle = @NoticeTitle , ");
            strSql.Append(" NoticeContent = @NoticeContent , ");
            strSql.Append(" NoticeType = @NoticeType , ");
            strSql.Append(" NoticeStatus = @NoticeStatus , ");
            strSql.Append(" NoticeAddDate = @NoticeAddDate , ");
            strSql.Append(" NoticeRealseAccount = @NoticeRealseAccount  ");
            strSql.Append(" where Id=@Id ");

            SqlParameter[] parameters = {
			            new SqlParameter("@Id", SqlDbType.Int,4) ,            
                        new SqlParameter("@NoticeTitle", SqlDbType.VarChar,200) ,            
                        new SqlParameter("@NoticeContent", SqlDbType.VarChar,-1) ,            
                        new SqlParameter("@NoticeType", SqlDbType.Int,4) ,            
                        new SqlParameter("@NoticeStatus", SqlDbType.Int,4) ,            
                        new SqlParameter("@NoticeAddDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@NoticeRealseAccount", SqlDbType.VarChar,50)             
              
            };

            parameters[0].Value = model.Id;
            parameters[1].Value = model.NoticeTitle;
            parameters[2].Value = model.NoticeContent;
            parameters[3].Value = model.NoticeType;
            parameters[4].Value = model.NoticeStatus;
            parameters[5].Value = model.NoticeAddDate;
            parameters[6].Value = model.NoticeRealseAccount;
            return sqlhelper.ExecNon(strSql.ToString(), parameters) > 0 ? true : false;
        }


        /// <summary>
        /// 通过Id 更新部分数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="fileds"></param>
        /// <returns></returns>
        public bool Update(SystemNotice model, List<string> fileds)
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

        public bool Update(SystemNotice model, List<string> fileds, string sqlWhere)
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
            strSql.Append("update SystemNotice set ");
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
        public SystemNotice GetModel(string where)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id, NoticeTitle, NoticeContent, NoticeType, NoticeStatus, NoticeAddDate, NoticeRealseAccount  ");
            strSql.Append("  from SystemNotice ");
            strSql.Append(" where ");
            strSql.Append(where);



            var dt = sqlhelper.GetTable(strSql.ToString());
            SystemNotice model = null;
            if (dt.Rows.Count > 0)
            {
                model = Mapper.DynamicMap<IDataReader, List<SystemNotice>>(dt.CreateDataReader()).FirstOrDefault();
            }
            return model;

        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<SystemNotice> GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM SystemNotice ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            var dt = sqlhelper.GetTable(strSql.ToString());
            return Mapper.DynamicMap<IDataReader, List<SystemNotice>>(dt.CreateDataReader());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public List<SystemNotice> GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" * ");
            strSql.Append(" FROM SystemNotice ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            var dt = sqlhelper.GetTable(strSql.ToString());
            return Mapper.DynamicMap<IDataReader, List<SystemNotice>>(dt.CreateDataReader());
        }
        /// <summary>
        /// 获得分页数据
        /// </summary>
        public List<SystemNotice> GetPagerList(out int TotalCount, int PageSize, int PageIndex, string strWhere = " 1=1 ", string fileds = "*", string OrderBy = " Id desc")
        {
            var dt = sqlhelper.GetPagerTable(typeof(SystemNotice).Name, out TotalCount, PageSize, PageIndex, strWhere, fileds, OrderBy);
            return Mapper.DynamicMap<IDataReader, List<SystemNotice>>(dt.CreateDataReader());
        }

    }
}
