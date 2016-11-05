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
    //SystemFeedback
    public class SystemFeedbackOper
    {
        SQLHelper sqlhelper = SQLHelper.Single;


        public int Exists(string where)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from SystemFeedback");
            strSql.Append(" where ");
            strSql.Append(where);
            return Convert.ToInt32(sqlhelper.GetTable(strSql.ToString()).Rows[0][0].ToString());

        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SystemFeedback model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SystemFeedback(");
            strSql.Append("UserId,UserName,FeedbackContet,FeedbackTime");
            strSql.Append(") values (");
            strSql.Append("@UserId,@UserName,@FeedbackContet,@FeedbackTime");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@UserId", SqlDbType.Int,4) ,            
                        new SqlParameter("@UserName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@FeedbackContet", SqlDbType.VarChar,1000) ,            
                        new SqlParameter("@FeedbackTime", SqlDbType.DateTime)             
              
            };

            parameters[0].Value = model.UserId;
            parameters[1].Value = model.UserName;
            parameters[2].Value = model.FeedbackContet;
            parameters[3].Value = model.FeedbackTime;
            return sqlhelper.ExecNon(strSql.ToString(), parameters);


        }


        /// <summary>
        /// 通过Id 更新部分数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="fileds"></param>
        /// <returns></returns>
        public bool Update(SystemFeedback model, List<string> fileds)
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
        public bool Update(SystemFeedback model, List<string> fileds, string sqlWhere)
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
            strSql.Append("update SystemFeedback set ");
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
        public SystemFeedback GetModel(string where)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id, UserId, UserName, FeedbackContet, FeedbackTime  ");
            strSql.Append("  from SystemFeedback ");
            strSql.Append(" where ");
            strSql.Append(where);

            var dt = sqlhelper.GetTable(strSql.ToString());
            SystemFeedback model = null;
            if (dt.Rows.Count > 0)
            {
                model = Mapper.DynamicMap<IDataReader, List<SystemFeedback>>(dt.CreateDataReader()).FirstOrDefault();
            }
            return model;

        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<SystemFeedback> GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM SystemFeedback ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            var dt = sqlhelper.GetTable(strSql.ToString());
            return Mapper.DynamicMap<IDataReader, List<SystemFeedback>>(dt.CreateDataReader());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public List<SystemFeedback> GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" * ");
            strSql.Append(" FROM SystemFeedback ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            var dt = sqlhelper.GetTable(strSql.ToString());
            return Mapper.DynamicMap<IDataReader, List<SystemFeedback>>(dt.CreateDataReader());
        }
        /// <summary>
        /// 获得分页数据
        /// </summary>
        public List<SystemFeedback> GetList(int Top, string strWhere, int index, int pageIndex, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" Id, UserId, UserName, FeedbackContet, FeedbackTime ");
            strSql.Append(" FROM SystemFeedback ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" and id not in ");
            strSql.Append("(select top " + index * pageIndex + " ");
            strSql.Append("Id, UserId, UserName, FeedbackContet, FeedbackTime ");
            strSql.Append(" FROM SystemFeedback");

            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(") order by " + filedOrder);
            var dt = sqlhelper.GetTable(strSql.ToString());
            return Mapper.DynamicMap<IDataReader, List<SystemFeedback>>(dt.CreateDataReader());
        }

    }
}
