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
    //字典表
    public class SystemDictionaryOper
    {
        SQLHelper sqlhelper = SQLHelper.Single;


        public int Exists(string where)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from SystemDictionary");
            strSql.Append(" where ");
            strSql.Append(where);
            return Convert.ToInt32(sqlhelper.GetTable(strSql.ToString()).Rows[0][0].ToString());

        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SystemDictionary model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SystemDictionary(");
            strSql.Append("DicKey,DicValue,DicType");
            strSql.Append(") values (");
            strSql.Append("@DicKey,@DicValue,@DicType");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@DicKey", SqlDbType.VarChar,200) ,            
                        new SqlParameter("@DicValue", SqlDbType.VarChar,8000) ,            
                        new SqlParameter("@DicType", SqlDbType.VarChar,20)             
              
            };

            parameters[0].Value = model.DicKey;
            parameters[1].Value = model.DicValue;
            parameters[2].Value = model.DicType; return sqlhelper.ExecNon(strSql.ToString(), parameters);


        }

        /// <summary>
        /// 通过Id 更新部分数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="fileds"></param>
        /// <returns></returns>
        public bool Update(SystemDictionary model, List<string> fileds)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("update {0} set ", model.GetType().Name);
            List<string> filedsList = new List<string>();
            List<SqlParameter> sqlParameter = new List<SqlParameter>();
            SqlParameter Param = new SqlParameter("@ID", SqlDbType.Int, 4);
            Param.Value = model.ID;
            sqlParameter.Add(Param);
            foreach (string filed in fileds)
            {
                filedsList.Add(string.Format("{0}=@{0}", filed));
                Param = new SqlParameter(string.Format("@{0}", filed), model.GetType().GetProperty(filed).GetValue(model, null));
                sqlParameter.Add(Param);
            }
            strSql.AppendFormat("{0}", string.Join(",", filedsList.ToArray()));
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = sqlParameter.ToArray();
            return sqlhelper.ExecNon(strSql.ToString(), parameters) > 0 ? true : false;
        }
        public bool Update(SystemDictionary model, List<string> fileds, string sqlWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("update {0} set ", model.GetType().Name);
            List<string> filedsList = new List<string>();
            List<SqlParameter> sqlParameter = new List<SqlParameter>();
            SqlParameter Param = new SqlParameter("@ID", SqlDbType.Int, 4);
            if (string.IsNullOrEmpty(sqlWhere))
            {
                Param.Value = model.ID;
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
                strSql.Append(" where ID=@ID ");
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
            strSql.Append("update SystemDictionary set ");
            strSql.AppendFormat(" {0} ", string.Join(",", keyVal.ToArray()));
            if (!string.IsNullOrEmpty(sqlWhere))
            {
                strSql.AppendFormat(" where 1=1 and {0} ", sqlWhere);
            }
            return sqlhelper.ExecNon(strSql.ToString(), null) > 0 ? true : false;
        }

        public bool Delete(string where)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SystemDictionary");
            strSql.Append(" where ");
            strSql.Append(where);
            return (sqlhelper.ExecNon(strSql.ToString()) > 0 ? true : false);
        }








        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SystemDictionary GetModel(string where)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID, DicKey, DicValue, DicType  ");
            strSql.Append("  from SystemDictionary ");
            strSql.Append(" where ");
            strSql.Append(where);




            var dt = sqlhelper.GetTable(strSql.ToString());
            SystemDictionary model = null;
            if (dt.Rows.Count > 0)
            {
                model = Mapper.DynamicMap<IDataReader, List<SystemDictionary>>(dt.CreateDataReader()).FirstOrDefault();
            }
            return model;
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<SystemDictionary> GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM SystemDictionary ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            var dt = sqlhelper.GetTable(strSql.ToString());
            return Mapper.DynamicMap<IDataReader, List<SystemDictionary>>(dt.CreateDataReader());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public List<SystemDictionary> GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" * ");
            strSql.Append(" FROM SystemDictionary ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            var dt = sqlhelper.GetTable(strSql.ToString());
            return Mapper.DynamicMap<IDataReader, List<SystemDictionary>>(dt.CreateDataReader());
        }
        /// <summary>
        /// 获得分页数据
        /// </summary>
        public List<SystemDictionary> GetList(int Top, string strWhere, int index, int pageIndex, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" ID, DicKey, DicValue, DicType ");
            strSql.Append(" FROM SystemDictionary ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" and id not in ");
            strSql.Append("(select top " + index * pageIndex + " ");
            strSql.Append("ID, DicKey, DicValue, DicType ");
            strSql.Append(" FROM SystemDictionary");

            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(") order by " + filedOrder);
            var dt = sqlhelper.GetTable(strSql.ToString());
            return Mapper.DynamicMap<IDataReader, List<SystemDictionary>>(dt.CreateDataReader());
        }
        /// <summary>
        /// 获得分页数据
        /// </summary>
        public List<SystemDictionary> GetPagerList(out int TotalCount, int PageSize, int PageIndex, string strWhere = " 1=1 ", string fileds = "*", string OrderBy = " Id desc")
        {
            var dt = sqlhelper.GetPagerTable(typeof(SystemDictionary).Name, out TotalCount, PageSize, PageIndex, strWhere, fileds, OrderBy);
            return Mapper.DynamicMap<IDataReader, List<SystemDictionary>>(dt.CreateDataReader());
        }

    }
}
