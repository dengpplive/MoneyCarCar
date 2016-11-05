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
    //SystemInterestSettlement
    public class SystemInterestSettlementOper
    {
        SQLHelper db = SQLHelper.Single;

        /// <summary>
        /// 获取需要提交结息支付的结息信息
        /// </summary>
        /// <returns></returns>
        public List<SystemRequestRecord> GetList()
        {
            SqlCommand cmd = db.GetStoredProcedureCommand("Proc_InterestSettlement_Add");
            DataTable dt = db.ExecuteDataTable(cmd);
            return Mapper.DynamicMap<IDataReader, List<SystemRequestRecord>>(dt.CreateDataReader());
        }

        public int Exists(string where)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from SystemInterestSettlement");
            strSql.Append(" where ");
            strSql.Append(where);
            return Convert.ToInt32(db.GetTable(strSql.ToString()).Rows[0][0].ToString());

        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SystemInterestSettlement model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SystemInterestSettlement(");
            strSql.Append("UserId,ClaimsId,GetInterestDate,BalanceDate,BalanceMoney,BalanceStatus,BalanceType");
            strSql.Append(") values (");
            strSql.Append("@UserId,@ClaimsId,@GetInterestDate,@BalanceDate,@BalanceMoney,@BalanceStatus,@BalanceType");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@UserId", SqlDbType.Int,4) ,            
                        new SqlParameter("@ClaimsId", SqlDbType.Int,4) ,            
                        new SqlParameter("@GetInterestDate", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@BalanceDate", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@BalanceMoney", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@BalanceStatus", SqlDbType.Int,4) ,            
                        new SqlParameter("@BalanceType", SqlDbType.Int,4)             
              
            };

            parameters[0].Value = model.UserId;
            parameters[1].Value = model.ClaimsId;
            parameters[2].Value = model.GetInterestDate;
            parameters[3].Value = model.BalanceDate;
            parameters[4].Value = model.BalanceMoney;
            parameters[5].Value = model.BalanceStatus;
            parameters[6].Value = model.BalanceType; return db.ExecNon(strSql.ToString(), parameters);


        }

        ///// <summary>
        ///// 更新一条数据
        ///// </summary>
        //public bool Update(SystemInterestSettlement model)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("update SystemInterestSettlement set ");

        //    strSql.Append(" UserId = @UserId , ");
        //    strSql.Append(" ClaimsId = @ClaimsId , ");
        //    strSql.Append(" GetInterestDate = @GetInterestDate , ");
        //    strSql.Append(" BalanceDate = @BalanceDate , ");
        //    strSql.Append(" BalanceMoney = @BalanceMoney , ");
        //    strSql.Append(" BalanceStatus = @BalanceStatus , ");
        //    strSql.Append(" BalanceType = @BalanceType  ");
        //    strSql.Append(" where ID=@ID ");

        //    SqlParameter[] parameters = {
        //                new SqlParameter("@ID", SqlDbType.Int,4) ,            
        //                new SqlParameter("@UserId", SqlDbType.Int,4) ,            
        //                new SqlParameter("@ClaimsId", SqlDbType.Int,4) ,            
        //                new SqlParameter("@GetInterestDate", SqlDbType.VarChar,20) ,            
        //                new SqlParameter("@BalanceDate", SqlDbType.VarChar,20) ,            
        //                new SqlParameter("@BalanceMoney", SqlDbType.Decimal,9) ,            
        //                new SqlParameter("@BalanceStatus", SqlDbType.Int,4) ,            
        //                new SqlParameter("@BalanceType", SqlDbType.Int,4)             
              
        //    };

        //    parameters[0].Value = model.ID;
        //    parameters[1].Value = model.UserId;
        //    parameters[2].Value = model.ClaimsId;
        //    parameters[3].Value = model.GetInterestDate;
        //    parameters[4].Value = model.BalanceDate;
        //    parameters[5].Value = model.BalanceMoney;
        //    parameters[6].Value = model.BalanceStatus;
        //    parameters[7].Value = model.BalanceType;
        //    return sqlhelper.ExecNon(strSql.ToString(), parameters) > 0 ? true : false;
        //}

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SystemInterestSettlement GetModel(string where)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID, UserId, ClaimsId, GetInterestDate, BalanceDate, BalanceMoney, BalanceStatus, BalanceType  ");
            strSql.Append("  from SystemInterestSettlement ");
            strSql.Append(" where ");
            strSql.Append(where);


            var dt = db.GetTable(strSql.ToString());
            SystemInterestSettlement model = null;
            if (dt.Rows.Count > 0)
            {
                model = Mapper.DynamicMap<IDataReader, List<SystemInterestSettlement>>(dt.CreateDataReader()).FirstOrDefault();
            }
            return model;

        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<SystemInterestSettlement> GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM SystemInterestSettlement ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            var dt = db.GetTable(strSql.ToString());
            return Mapper.DynamicMap<IDataReader, List<SystemInterestSettlement>>(dt.CreateDataReader());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public List<SystemInterestSettlement> GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" * ");
            strSql.Append(" FROM SystemInterestSettlement ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            var dt = db.GetTable(strSql.ToString());
            return Mapper.DynamicMap<IDataReader, List<SystemInterestSettlement>>(dt.CreateDataReader());
        }
        /// <summary>
        /// 获得分页数据
        /// </summary>
        public List<SystemInterestSettlement> GetList(int Top, string strWhere, int index, int pageIndex, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" ID, UserId, ClaimsId, GetInterestDate, BalanceDate, BalanceMoney, BalanceStatus, BalanceType ");
            strSql.Append(" FROM SystemInterestSettlement ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" and id not in ");
            strSql.Append(" (select top " + index * pageIndex + " ");
            strSql.Append(" ID, UserId, ClaimsId, GetInterestDate, BalanceDate, BalanceMoney, BalanceStatus, BalanceType ");
            strSql.Append(" FROM SystemInterestSettlement");

            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(") order by " + filedOrder);
            var dt = db.GetTable(strSql.ToString());
            return Mapper.DynamicMap<IDataReader, List<SystemInterestSettlement>>(dt.CreateDataReader());
        }

        /// <summary>
        /// 获得分页数据
        /// </summary>
        public List<SystemInterestSettlement> GetPagerList(out int TotalCount, int PageSize, int PageIndex, string strWhere = " 1=1 ", string fileds = "*", string OrderBy = " Id desc")
        {
            var dt = db.GetPagerTable(typeof(SystemClaims).Name, out TotalCount, PageSize, PageIndex, strWhere, fileds, OrderBy);
            return Mapper.DynamicMap<IDataReader, List<SystemInterestSettlement>>(dt.CreateDataReader());
        }

    }
}
