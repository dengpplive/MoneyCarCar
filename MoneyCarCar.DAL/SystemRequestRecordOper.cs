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
    //SystemRequestRecord
    public class SystemRequestRecordOper
    {
        SQLHelper db = SQLHelper.Single;

        public int SystemRequestRecord_Add(int userID, int bussnessId, decimal requestMoney, int requestType, out string errorMsg)
        {
            SqlCommand cmd = db.GetStoredProcedureCommand("Proc_SystemRequestRecord_Add");
            db.AddInputParameter(cmd, "@UserID", DbType.Int32, userID);
            db.AddInputParameter(cmd, "@BussnessId", DbType.Int32, bussnessId);
            db.AddInputParameter(cmd, "@RequestMoney", DbType.Decimal, requestMoney);
            db.AddInputParameter(cmd, "@RequestType", DbType.Int32, requestType);
            db.AddOutputParameter(cmd, "@ErrorMsg", DbType.String, 500);
            db.AddReturnValueParameter(cmd, "@ReturnValue", DbType.Int32, 4);
            db.ExecuteNonQuery(cmd);
            errorMsg = db.GetParameter(cmd, "@ErrorMsg").Value.ToString();
            return db.GetParameter(cmd, "@ReturnValue").Value.ToInt();
        }

        public int Exists(string where)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from SystemRequestRecord");
            strSql.Append(" where ");
            strSql.Append(where);
            return Convert.ToInt32(db.GetTable(strSql.ToString()).Rows[0][0].ToString());

        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SystemRequestRecord model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SystemRequestRecord(");
            strSql.Append("UserId,BussnessId,RequestMoney,RequestType,RequestDate,RequestOperStatus,RequestMark");
            strSql.Append(") values (");
            strSql.Append("@UserId,@BussnessId,@RequestMoney,@RequestType,@RequestDate,@RequestOperStatus,@RequestMark");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@UserId", SqlDbType.Int,4) ,            
                        new SqlParameter("@BussnessId", SqlDbType.Int,4) ,            
                        new SqlParameter("@RequestMoney", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@RequestType", SqlDbType.Int,4) ,            
                        new SqlParameter("@RequestDate", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@RequestOperStatus", SqlDbType.Int,4),             
                        new SqlParameter("@RequestMark", SqlDbType.VarChar,-1)
            };

            parameters[0].Value = model.UserId;
            parameters[1].Value = model.BussnessId;
            parameters[2].Value = model.RequestMoney;
            parameters[3].Value = model.RequestType;
            parameters[4].Value = model.RequestDate;
            parameters[5].Value = model.RequestOperStatus;
            parameters[6].Value = model.RequestMark;
            return db.ExecNon(strSql.ToString(), parameters);


        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SystemRequestRecord GetModel(string where)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id, UserId, BussnessId, RequestMoney, RequestType, RequestDate, RequestOperStatus,RequestMark  ");
            strSql.Append("  from SystemRequestRecord ");
            strSql.Append(" where ");
            strSql.Append(where);


            var dt = db.GetTable(strSql.ToString());
            SystemRequestRecord model = null;
            if (dt.Rows.Count > 0)
            {
                model = Mapper.DynamicMap<IDataReader, List<SystemRequestRecord>>(dt.CreateDataReader()).FirstOrDefault();
            }
            return model;

        }

        #region 查询过期及处理
        /// <summary>
        /// 获取需要处理的请求
        /// </summary>
        /// <returns></returns>
        public List<SystemRequestRecord> GetList()
        {
            SqlCommand cmd = db.GetStoredProcedureCommand("Proc_SystemRequestRecord_Scan");
            DataTable dt = db.ExecuteDataTable(cmd);
            return Mapper.DynamicMap<IDataReader, List<SystemRequestRecord>>(dt.CreateDataReader());
        }

        /// <summary>
        /// 处理业务查询回调
        /// </summary>
        /// <param name="payNo"></param>
        /// <param name="isSuccess"></param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public int InquiryQueryBack(string payNo,bool isSuccess,out string errorMsg)
        {
            SqlCommand cmd = db.GetStoredProcedureCommand("Proc_SystemRequestRecord_InquiryBack");
            db.AddInputParameter(cmd, "@PayNo", DbType.String, payNo);
            db.AddInputParameter(cmd, "@PayStatu", DbType.Boolean, isSuccess);
            db.AddOutputParameter(cmd, "@ErrorMsg", DbType.String, 500);
            db.AddReturnValueParameter(cmd, "@ReturnValue", DbType.Int32, 4);
            db.ExecuteNonQuery(cmd);
            errorMsg = db.GetParameter(cmd,"@ErrorMsg").Value.ToString();
            return db.GetParameter(cmd, "@ReturnValue").Value.ToInt();
        }
        #endregion
        
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<SystemRequestRecord> GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM SystemRequestRecord ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            var dt = db.GetTable(strSql.ToString());
            return Mapper.DynamicMap<IDataReader, List<SystemRequestRecord>>(dt.CreateDataReader());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public List<SystemRequestRecord> GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" * ");
            strSql.Append(" FROM SystemRequestRecord ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            var dt = db.GetTable(strSql.ToString());
            return Mapper.DynamicMap<IDataReader, List<SystemRequestRecord>>(dt.CreateDataReader());
        }
        /// <summary>
        /// 获得分页数据
        /// </summary>
        public List<SystemRequestRecord> GetList(int Top, string strWhere, int index, int pageIndex, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" Id, UserId, BussnessId, RequestMoney, RequestType, RequestDate, RequestOperStatus,RequestMark ");
            strSql.Append(" FROM SystemRequestRecord ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" and id not in ");
            strSql.Append(" (select top " + index * pageIndex + " ");
            strSql.Append(" Id, UserId, BussnessId, RequestMoney, RequestType, RequestDate, RequestOperStatus,RequestMark ");
            strSql.Append(" FROM SystemRequestRecord");

            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(") order by " + filedOrder);
            var dt = db.GetTable(strSql.ToString());
            return Mapper.DynamicMap<IDataReader, List<SystemRequestRecord>>(dt.CreateDataReader());
        }

        /// <summary>
        /// 获得分页数据
        /// </summary>
        public List<SystemRequestRecord> GetPagerList(out int TotalCount, int PageSize, int PageIndex, string strWhere = " 1=1 ", string fileds = "*", string OrderBy = " Id desc")
        {
            var dt = db.GetPagerTable(typeof(SystemClaims).Name, out TotalCount, PageSize, PageIndex, strWhere, fileds, OrderBy);
            return Mapper.DynamicMap<IDataReader, List<SystemRequestRecord>>(dt.CreateDataReader());
        }

    }
}
