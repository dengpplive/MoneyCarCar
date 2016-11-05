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
    //债权明细表
    public class SystemClaimsDetailsOper
    {
        SQLHelper db = SQLHelper.Single;


        public int Exists(string where)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from SystemClaimsDetails");
            strSql.Append(" where ");
            strSql.Append(where);
            return Convert.ToInt32(db.GetTable(strSql.ToString()).Rows[0][0].ToString());

        }

        /// <summary>
        /// 添加债券明细
        /// </summary>
        /// <param name="userID">用户编号</param>
        /// <param name="claimsID">债券ID</param>
        /// <param name="buyCount">购买数量</param>
        /// <param name="dayEarnings">日结利息</param>
        /// <param name="expireEarnings">预期收益</param>
        /// <param name="IsUserBounty">是否使用虚拟本金</param>
        /// <param name="virtualMoney">虚拟本金数量</param>
        /// <param name="virtualMoneyDayEarnings">虚拟本金日结利息</param>
        /// <param name="errorMsg">错误信息</param>
        /// <param name="targetUserID">扣钱的用户ID</param>
        /// <returns></returns>
        public int SystemClaimsDetails_Add(int userID, int claimsID, int buyCount, decimal dayEarnings, decimal expireEarnings, int IsUserBounty, decimal virtualMoney, decimal virtualMoneyDayEarnings, out string errorMsg, out int targetUserID)
        {
            errorMsg = "";
            SqlCommand cmd = db.GetStoredProcedureCommand("Proc_SystemClaimsDetails_Add");
            db.AddInputParameter(cmd, "@UserID", DbType.Int32, userID);
            db.AddInputParameter(cmd, "@ClaimsID", DbType.Int32, claimsID);
            db.AddInputParameter(cmd, "@BuyCount", DbType.Int32, buyCount);
            db.AddInputParameter(cmd, "@DayEarnings", DbType.Decimal, dayEarnings);
            db.AddInputParameter(cmd, "@ExpireEarnings", DbType.Decimal, expireEarnings);
            db.AddInputParameter(cmd, "@IsUserBounty", DbType.Int32, IsUserBounty);
            db.AddInputParameter(cmd, "@VirtualMoney", DbType.Decimal, virtualMoney);
            db.AddInputParameter(cmd, "@VirtualMoneyDayEarnings", DbType.Decimal, virtualMoneyDayEarnings);
            db.AddOutputParameter(cmd, "@TargetUserID", DbType.Int32, 40);
            db.AddOutputParameter(cmd, "@ErrorMsg", DbType.String, 500);
            db.AddReturnValueParameter(cmd, "@ReturnValue", DbType.Int32, 40);
            db.ExecuteNonQuery(cmd);
            errorMsg = db.GetParameter(cmd, "@ErrorMsg").Value.ToString();
            targetUserID = db.GetParameter(cmd, "@TargetUserID").Value.ToInt();
            return db.GetParameter(cmd, "@ReturnValue").Value.ToInt();
        }

        /// <summary>
        /// 通过Id 更新部分数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="fileds"></param>
        /// <returns></returns>
        public bool Update(SystemClaimsDetails model, List<string> fileds)
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
            return db.ExecNon(strSql.ToString(), parameters) > 0 ? true : false;
        }

        public bool Update(SystemClaimsDetails model, List<string> fileds, string sqlWhere)
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
            return db.ExecNon(strSql.ToString(), parameters) > 0 ? true : false;
        }

        public bool Update(List<string> keyVal, string sqlWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SystemClaimsDetails set ");
            strSql.AppendFormat(" {0} ", string.Join(",", keyVal.ToArray()));
            if (!string.IsNullOrEmpty(sqlWhere))
            {
                strSql.AppendFormat(" where 1=1 and {0} ", sqlWhere);
            }
            return db.ExecNon(strSql.ToString(), null) > 0 ? true : false;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SystemClaimsDetails GetModel(string where)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID, ClaimsID, InvestorsID, InvestorsCellPhone, InvestorMoney, InvestorsTime, DayEarnings,ExpireEarnings, PayStatus,PayMark  ");
            strSql.Append("  from SystemClaimsDetails ");
            strSql.Append(" where ");
            strSql.Append(where);

            var dt = db.GetTable(strSql.ToString());
            SystemClaimsDetails model = null;
            if (dt.Rows.Count > 0)
            {
                model = Mapper.DynamicMap<IDataReader, List<SystemClaimsDetails>>(dt.CreateDataReader()).FirstOrDefault();
            }
            return model;

        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<SystemClaimsDetails> GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM SystemClaimsDetails ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            var dt = db.GetTable(strSql.ToString());
            return Mapper.DynamicMap<IDataReader, List<SystemClaimsDetails>>(dt.CreateDataReader());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public List<SystemClaimsDetails> GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" * ");
            strSql.Append(" FROM SystemClaimsDetails ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            var dt = db.GetTable(strSql.ToString());
            return Mapper.DynamicMap<IDataReader, List<SystemClaimsDetails>>(dt.CreateDataReader());
        }
        /// <summary>
        /// 获得分页数据
        /// </summary>
        public List<SystemClaimsDetails> GetList(int Top, string strWhere, int index, int pageIndex, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" ID, ClaimsID, InvestorsID, InvestorsCellPhone, InvestorMoney, InvestorsTime, DayEarnings, ExpireEarnings, PayStatus,PayMark ");
            strSql.Append(" FROM SystemClaimsDetails ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" and id not in ");
            strSql.Append("(select top " + index * pageIndex + " ");
            strSql.Append("ID, ClaimsID, InvestorsID, InvestorsCellPhone, InvestorMoney, InvestorsTime, DayEarnings, ExpireEarnings, PayStatus,PayMark ");
            strSql.Append(" FROM SystemClaimsDetails");

            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(") order by " + filedOrder);
            var dt = db.GetTable(strSql.ToString());
            return Mapper.DynamicMap<IDataReader, List<SystemClaimsDetails>>(dt.CreateDataReader());
        }

    }
}
