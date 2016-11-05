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
    //SystemVirtualMoney
    public class SystemVirtualMoneyOper
    {
        SQLHelper sqlhelper = SQLHelper.Single;


        public int Exists(string where)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from SystemVirtualMoney");
            strSql.Append(" where ");
            strSql.Append(where);
            return Convert.ToInt32(sqlhelper.GetTable(strSql.ToString()).Rows[0][0].ToString());

        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SystemVirtualMoney model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SystemVirtualMoney(");
            strSql.Append("UserId,ClaimsId,VirtualMoney,BuyDate");
            strSql.Append(") values (");
            strSql.Append("@UserId,@ClaimsId,@VirtualMoney,@BuyDate");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@UserId", SqlDbType.Int,4) ,            
                        new SqlParameter("@ClaimsId", SqlDbType.Int,4) ,            
                        new SqlParameter("@VirtualMoney", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@BuyDate", SqlDbType.VarChar,20)             
              
            };

            parameters[0].Value = model.UserId;
            parameters[1].Value = model.ClaimsId;
            parameters[2].Value = model.VirtualMoney;
            parameters[3].Value = model.BuyDate; return sqlhelper.ExecNon(strSql.ToString(), parameters);


        }


        ///// <summary>
        ///// 更新一条数据
        ///// </summary>
        //public bool Update(SystemVirtualMoney model)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("update SystemVirtualMoney set ");

        //    strSql.Append(" UserId = @UserId , ");
        //    strSql.Append(" ClaimsId = @ClaimsId , ");
        //    strSql.Append(" VirtualMoney = @VirtualMoney , ");
        //    strSql.Append(" BuyDate = @BuyDate  ");
        //    strSql.Append(" where ID=@ID ");

        //    SqlParameter[] parameters = {
        //                new SqlParameter("@ID", SqlDbType.Int,4) ,            
        //                new SqlParameter("@UserId", SqlDbType.Int,4) ,            
        //                new SqlParameter("@ClaimsId", SqlDbType.Int,4) ,            
        //                new SqlParameter("@VirtualMoney", SqlDbType.Decimal,9) ,            
        //                new SqlParameter("@BuyDate", SqlDbType.VarChar,20)             
              
        //    };

        //    parameters[0].Value = model.ID;
        //    parameters[1].Value = model.UserId;
        //    parameters[2].Value = model.ClaimsId;
        //    parameters[3].Value = model.VirtualMoney;
        //    parameters[4].Value = model.BuyDate;
        //    return sqlhelper.ExecNon(strSql.ToString(), parameters) > 0 ? true : false;
        //}







        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SystemVirtualMoney GetModel(string where)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID, UserId, ClaimsId, VirtualMoney, BuyDate  ");
            strSql.Append("  from SystemVirtualMoney ");
            strSql.Append(" where ");
            strSql.Append(where);


            var dt = sqlhelper.GetTable(strSql.ToString());
            SystemVirtualMoney model = null;
            if (dt.Rows.Count > 0)
            {
                model = Mapper.DynamicMap<IDataReader, List<SystemVirtualMoney>>(dt.CreateDataReader()).FirstOrDefault();
            }
            return model;

        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<SystemVirtualMoney> GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM SystemVirtualMoney ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            var dt = sqlhelper.GetTable(strSql.ToString());
            return Mapper.DynamicMap<IDataReader, List<SystemVirtualMoney>>(dt.CreateDataReader());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public List<SystemVirtualMoney> GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" * ");
            strSql.Append(" FROM SystemVirtualMoney ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            var dt = sqlhelper.GetTable(strSql.ToString());
            return Mapper.DynamicMap<IDataReader, List<SystemVirtualMoney>>(dt.CreateDataReader());
        }
        /// <summary>
        /// 获得分页数据
        /// </summary>
        public List<SystemVirtualMoney> GetList(int Top, string strWhere, int index, int pageIndex, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" ID, UserId, ClaimsId, VirtualMoney, BuyDate ");
            strSql.Append(" FROM SystemVirtualMoney ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" and id not in ");
            strSql.Append(" (select top " + index * pageIndex + " ");
            strSql.Append(" ID, UserId, ClaimsId, VirtualMoney, BuyDate ");
            strSql.Append(" FROM SystemVirtualMoney");

            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(") order by " + filedOrder);
            var dt = sqlhelper.GetTable(strSql.ToString());
            return Mapper.DynamicMap<IDataReader, List<SystemVirtualMoney>>(dt.CreateDataReader());
        }

        /// <summary>
        /// 获得分页数据
        /// </summary>
        public List<SystemVirtualMoney> GetPagerList(out int TotalCount, int PageSize, int PageIndex, string strWhere = " 1=1 ", string fileds = "*", string OrderBy = " Id desc")
        {
            var dt = sqlhelper.GetPagerTable(typeof(SystemClaims).Name, out TotalCount, PageSize, PageIndex, strWhere, fileds, OrderBy);
            return Mapper.DynamicMap<IDataReader, List<SystemVirtualMoney>>(dt.CreateDataReader());
        }

    }
}
