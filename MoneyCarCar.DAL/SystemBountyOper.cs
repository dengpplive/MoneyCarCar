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
    //SystemBounty
    public class SystemBountyOper
    {
        SQLHelper sqlhelper = SQLHelper.Single;


        public int Exists(string where)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from SystemBounty");
            strSql.Append(" where ");
            strSql.Append(where);
            return Convert.ToInt32(sqlhelper.GetTable(strSql.ToString()).Rows[0][0].ToString());

        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SystemBounty model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SystemBounty(");
            strSql.Append("UseType,BountyRes,OverTime,UserId,UserName,BountyType,Integral,operName,operTime,ClaimsId,UseTime");
            strSql.Append(") values (");
            strSql.Append("@UseType,@BountyRes,@OverTime,@UserId,@UserName,@BountyType,@Integral,@operName,@operTime,@ClaimsId,@UseTime");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@UseType", SqlDbType.Bit,1) ,            
                        new SqlParameter("@BountyRes", SqlDbType.Int,4) ,            
                        new SqlParameter("@OverTime", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@UserId", SqlDbType.Int,4) ,            
                        new SqlParameter("@UserName", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@BountyType", SqlDbType.Int,4) ,            
                        new SqlParameter("@Integral", SqlDbType.Int,4) ,            
                        new SqlParameter("@operName", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@operTime", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@ClaimsId", SqlDbType.Int,4) ,            
                        new SqlParameter("@UseTime", SqlDbType.VarChar,20)             
              
            };

            parameters[0].Value = model.UseType;
            parameters[1].Value = model.BountyRes;
            parameters[2].Value = model.OverTime;
            parameters[3].Value = model.UserId;
            parameters[4].Value = model.UserName;
            parameters[5].Value = model.BountyType;
            parameters[6].Value = model.Integral;
            parameters[7].Value = model.operName;
            parameters[8].Value = model.operTime;
            parameters[9].Value = model.ClaimsId;
            parameters[10].Value = model.UseTime; return sqlhelper.ExecNon(strSql.ToString(), parameters);


        }


        ///// <summary>
        ///// 更新一条数据
        ///// </summary>
        //public bool Update(SystemBounty model)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("update SystemBounty set ");

        //    strSql.Append(" UseType = @UseType , ");
        //    strSql.Append(" BountyRes = @BountyRes , ");
        //    strSql.Append(" OverTime = @OverTime , ");
        //    strSql.Append(" UserId = @UserId , ");
        //    strSql.Append(" UserName = @UserName , ");
        //    strSql.Append(" BountyType = @BountyType , ");
        //    strSql.Append(" Integral = @Integral , ");
        //    strSql.Append(" operName = @operName , ");
        //    strSql.Append(" operTime = @operTime , ");
        //    strSql.Append(" ClaimsId = @ClaimsId , ");
        //    strSql.Append(" UseTime = @UseTime  ");
        //    strSql.Append(" where ID=@ID ");

        //    SqlParameter[] parameters = {
        //                new SqlParameter("@ID", SqlDbType.Int,4) ,            
        //                new SqlParameter("@UseType", SqlDbType.Bit,1) ,            
        //                new SqlParameter("@BountyRes", SqlDbType.Int,4) ,            
        //                new SqlParameter("@OverTime", SqlDbType.VarChar,20) ,            
        //                new SqlParameter("@UserId", SqlDbType.Int,4) ,            
        //                new SqlParameter("@UserName", SqlDbType.VarChar,20) ,            
        //                new SqlParameter("@BountyType", SqlDbType.Int,4) ,            
        //                new SqlParameter("@Integral", SqlDbType.Int,4) ,            
        //                new SqlParameter("@operName", SqlDbType.VarChar,20) ,            
        //                new SqlParameter("@operTime", SqlDbType.VarChar,20) ,            
        //                new SqlParameter("@ClaimsId", SqlDbType.Int,4) ,            
        //                new SqlParameter("@UseTime", SqlDbType.VarChar,20)             
              
        //    };

        //    parameters[0].Value = model.ID;
        //    parameters[1].Value = model.UseType;
        //    parameters[2].Value = model.BountyRes;
        //    parameters[3].Value = model.OverTime;
        //    parameters[4].Value = model.UserId;
        //    parameters[5].Value = model.UserName;
        //    parameters[6].Value = model.BountyType;
        //    parameters[7].Value = model.Integral;
        //    parameters[8].Value = model.operName;
        //    parameters[9].Value = model.operTime;
        //    parameters[10].Value = model.ClaimsId;
        //    parameters[11].Value = model.UseTime;
        //    return sqlhelper.ExecNon(strSql.ToString(), parameters) > 0 ? true : false;
        //}







        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SystemBounty GetModel(string where)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID, UseType, BountyRes, OverTime, UserId, UserName, BountyType, Integral, operName, operTime, ClaimsId, UseTime  ");
            strSql.Append("  from SystemBounty ");
            strSql.Append(" where ");
            strSql.Append(where);


            var dt = sqlhelper.GetTable(strSql.ToString());
            SystemBounty model = null;
            if (dt.Rows.Count > 0)
            {
                model = Mapper.DynamicMap<IDataReader, List<SystemBounty>>(dt.CreateDataReader()).FirstOrDefault();
            }
            return model;

        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<SystemBounty> GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM SystemBounty ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            var dt = sqlhelper.GetTable(strSql.ToString());
            return Mapper.DynamicMap<IDataReader, List<SystemBounty>>(dt.CreateDataReader());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public List<SystemBounty> GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" * ");
            strSql.Append(" FROM SystemBounty ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            var dt = sqlhelper.GetTable(strSql.ToString());
            return Mapper.DynamicMap<IDataReader, List<SystemBounty>>(dt.CreateDataReader());
        }
        /// <summary>
        /// 获得分页数据
        /// </summary>
        public List<SystemBounty> GetList(int Top, string strWhere, int index, int pageIndex, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" ID, UseType, BountyRes, OverTime, UserId, UserName, BountyType, Integral, operName, operTime, ClaimsId, UseTime ");
            strSql.Append(" FROM SystemBounty ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" and id not in ");
            strSql.Append(" (select top " + index * pageIndex + " ");
            strSql.Append(" ID, UseType, BountyRes, OverTime, UserId, UserName, BountyType, Integral, operName, operTime, ClaimsId, UseTime ");
            strSql.Append(" FROM SystemBounty");

            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(") order by " + filedOrder);
            var dt = sqlhelper.GetTable(strSql.ToString());
            return Mapper.DynamicMap<IDataReader, List<SystemBounty>>(dt.CreateDataReader());
        }

        /// <summary>
        /// 获得分页数据
        /// </summary>
        public List<SystemBounty> GetPagerList(out int TotalCount, int PageSize, int PageIndex, string strWhere = " 1=1 ", string fileds = "*", string OrderBy = " Id desc")
        {
            var dt = sqlhelper.GetPagerTable(typeof(SystemClaims).Name, out TotalCount, PageSize, PageIndex, strWhere, fileds, OrderBy);
            return Mapper.DynamicMap<IDataReader, List<SystemBounty>>(dt.CreateDataReader());
        }

    }
}
