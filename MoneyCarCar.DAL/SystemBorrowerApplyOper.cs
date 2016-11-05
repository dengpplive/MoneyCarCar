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
    //SystemBorrowerApply
    public class SystemBorrowerApplyOper
    {
        SQLHelper sqlhelper = SQLHelper.Single;


        public int Exists(string where)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from SystemBorrowerApply");
            strSql.Append(" where ");
            strSql.Append(where);
            return Convert.ToInt32(sqlhelper.GetTable(strSql.ToString()).Rows[0][0].ToString());

        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SystemBorrowerApply model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SystemBorrowerApply(");
            strSql.Append("BorrowerID,BorrowerName,LoanAmount,BorrowerReason,CollateralDesc,BorrowerTime,BorrowerType");
            strSql.Append(") values (");
            strSql.Append("@BorrowerID,@BorrowerName,@LoanAmount,@BorrowerReason,@CollateralDesc,@BorrowerTime,@BorrowerType");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@BorrowerID", SqlDbType.Int,4) ,            
                        new SqlParameter("@BorrowerName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@LoanAmount", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@BorrowerReason", SqlDbType.VarChar,-1) ,            
                        new SqlParameter("@CollateralDesc", SqlDbType.VarChar,-1) ,            
                        new SqlParameter("@BorrowerTime", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@BorrowerType", SqlDbType.Int,4)             
              
            };

            parameters[0].Value = model.BorrowerID;
            parameters[1].Value = model.BorrowerName;
            parameters[2].Value = model.LoanAmount;
            parameters[3].Value = model.BorrowerReason;
            parameters[4].Value = model.CollateralDesc;
            parameters[5].Value = model.BorrowerTime;
            parameters[6].Value = model.BorrowerType; return sqlhelper.ExecNon(strSql.ToString(), parameters);


        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SystemBorrowerApply model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SystemBorrowerApply set ");

            strSql.Append(" BorrowerID = @BorrowerID , ");
            strSql.Append(" BorrowerName = @BorrowerName , ");
            strSql.Append(" LoanAmount = @LoanAmount , ");
            strSql.Append(" BorrowerReason = @BorrowerReason , ");
            strSql.Append(" CollateralDesc = @CollateralDesc , ");
            strSql.Append(" BorrowerTime = @BorrowerTime , ");
            strSql.Append(" BorrowerType = @BorrowerType  ");
            strSql.Append(" where Id=@Id ");

            SqlParameter[] parameters = {
			            new SqlParameter("@Id", SqlDbType.Int,4) ,            
                        new SqlParameter("@BorrowerID", SqlDbType.Int,4) ,            
                        new SqlParameter("@BorrowerName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@LoanAmount", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@BorrowerReason", SqlDbType.VarChar,-1) ,            
                        new SqlParameter("@CollateralDesc", SqlDbType.VarChar,-1) ,            
                        new SqlParameter("@BorrowerTime", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@BorrowerType", SqlDbType.Int,4)             
              
            };

            parameters[0].Value = model.Id;
            parameters[1].Value = model.BorrowerID;
            parameters[2].Value = model.BorrowerName;
            parameters[3].Value = model.LoanAmount;
            parameters[4].Value = model.BorrowerReason;
            parameters[5].Value = model.CollateralDesc;
            parameters[6].Value = model.BorrowerTime;
            parameters[7].Value = model.BorrowerType;
            return sqlhelper.ExecNon(strSql.ToString(), parameters) > 0 ? true : false;
        }

        /// <summary>
        /// 通过Id 更新部分数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="fileds"></param>
        /// <returns></returns>
        public bool Update(SystemBorrowerApply model, List<string> fileds)
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
        public bool Update(SystemBorrowerApply model, List<string> fileds, string sqlWhere)
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
            strSql.Append("update SystemBorrowerApply set ");
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
            strSql.Append("delete from SystemBorrowerApply ");
            strSql.Append(" where ");
            strSql.Append(where);
            return (sqlhelper.ExecNon(strSql.ToString()) > 0 ? true : false);
        }




        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SystemBorrowerApply GetModel(string where)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id, BorrowerID, BorrowerName, LoanAmount, BorrowerReason, CollateralDesc, BorrowerTime, BorrowerType  ");
            strSql.Append("  from SystemBorrowerApply ");
            strSql.Append(" where ");
            strSql.Append(where);


            var dt = sqlhelper.GetTable(strSql.ToString());
            SystemBorrowerApply model = null;
            if (dt.Rows.Count > 0)
            {
                model = Mapper.DynamicMap<IDataReader, List<SystemBorrowerApply>>(dt.CreateDataReader()).FirstOrDefault();
            }
            return model;

        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<SystemBorrowerApply> GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM SystemBorrowerApply ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            var dt = sqlhelper.GetTable(strSql.ToString());
            return Mapper.DynamicMap<IDataReader, List<SystemBorrowerApply>>(dt.CreateDataReader());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public List<SystemBorrowerApply> GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" * ");
            strSql.Append(" FROM SystemBorrowerApply ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            var dt = sqlhelper.GetTable(strSql.ToString());
            return Mapper.DynamicMap<IDataReader, List<SystemBorrowerApply>>(dt.CreateDataReader());
        }
        /// <summary>
        /// 获得分页数据
        /// </summary>
        public List<SystemBorrowerApply> GetList(int Top, string strWhere, int index, int pageIndex, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" Id, BorrowerID, BorrowerName, LoanAmount, BorrowerReason, CollateralDesc, BorrowerTime, BorrowerType ");
            strSql.Append(" FROM SystemBorrowerApply ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" and id not in ");
            strSql.Append(" (select top " + index * pageIndex + " ");
            strSql.Append(" Id, BorrowerID, BorrowerName, LoanAmount, BorrowerReason, CollateralDesc, BorrowerTime, BorrowerType ");
            strSql.Append(" FROM SystemBorrowerApply");

            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(") order by " + filedOrder);
            var dt = sqlhelper.GetTable(strSql.ToString());
            return Mapper.DynamicMap<IDataReader, List<SystemBorrowerApply>>(dt.CreateDataReader());
        }

        /// <summary>
        /// 获得分页数据
        /// </summary>
        public List<SystemBorrowerApply> GetPagerList(out int TotalCount, int PageSize, int PageIndex, string strWhere = " 1=1 ", string fileds = "*", string OrderBy = " Id desc")
        {
            var dt = sqlhelper.GetPagerTable(typeof(SystemBorrowerApply).Name, out TotalCount, PageSize, PageIndex, strWhere, fileds, OrderBy);
            return Mapper.DynamicMap<IDataReader, List<SystemBorrowerApply>>(dt.CreateDataReader());
        }

    }
}
