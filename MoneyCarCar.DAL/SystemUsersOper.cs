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
using MoneyCarCar.Models.DtoModels;
using MoneyCarCar.Models.Statisticals.Return;
using MoneyCarCar.Models.Statisticals.Parameter;

namespace MoneyCarCar.DAL
{
    /// <summary>
    /// 用户信息表
    /// </summary>
    public class SystemUsersOper
    {
        SQLHelper db = SQLHelper.Single;
        SystemRequestRecordOper request = new SystemRequestRecordOper();

        #region 自动生成的代码
        public int Exists(string where)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from SystemUsers");
            strSql.Append(" where ");
            strSql.Append(where);
            return Convert.ToInt32(db.GetTable(strSql.ToString()).Rows[0][0].ToString());
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SystemUsers model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SystemUsers(");
            strSql.Append("UserAddress,UserEmail,UserType,UserState,IsEnterprise,Balance,Freeze,RegTime,UserName,UserPassword,PayPassword,CellPhone,CellPahoneIsAuthenticate,RealName,IDNumber,IDNumberIsAuthenticate");
            strSql.Append(") values (");
            strSql.Append("@UserAddress,@UserEmail,@UserType,@UserState,@IsEnterprise,@Balance,@Freeze,@RegTime,@UserName,@UserPassword,@PayPassword,@CellPhone,@CellPahoneIsAuthenticate,@RealName,@IDNumber,@IDNumberIsAuthenticate");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@UserAddress", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@UserEmail", SqlDbType.VarChar,200) ,            
                        new SqlParameter("@UserType", SqlDbType.TinyInt,1) ,            
                        new SqlParameter("@UserState", SqlDbType.TinyInt,1) ,            
                        new SqlParameter("@IsEnterprise", SqlDbType.Int,4) ,            
                        new SqlParameter("@Balance", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@Freeze", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@RegTime", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@UserName", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@UserPassword", SqlDbType.VarChar,32) ,            
                        new SqlParameter("@PayPassword", SqlDbType.VarChar,32) ,            
                        new SqlParameter("@CellPhone", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@CellPahoneIsAuthenticate", SqlDbType.Bit,1) ,            
                        new SqlParameter("@RealName", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@IDNumber", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@IDNumberIsAuthenticate", SqlDbType.Bit,1)             
              
            };

            parameters[0].Value = model.UserAddress;
            parameters[1].Value = model.UserEmail;
            parameters[2].Value = model.UserType;
            parameters[3].Value = model.UserState;
            parameters[4].Value = model.IsEnterprise;
            parameters[5].Value = model.Balance;
            parameters[6].Value = model.Freeze;
            parameters[7].Value = model.RegTime;
            parameters[8].Value = model.UserName;
            parameters[9].Value = model.UserPassword;
            parameters[10].Value = model.PayPassword;
            parameters[11].Value = model.CellPhone;
            parameters[12].Value = model.CellPahoneIsAuthenticate;
            parameters[13].Value = model.RealName;
            parameters[14].Value = model.IDNumber;
            parameters[15].Value = model.IDNumberIsAuthenticate;
            return db.ExecNon(strSql.ToString(), parameters);
        }
        #endregion

        #region 通用代码
        //public bool UpdateByName(string UserName,int type)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("update SystemUsers set ");
        //    strSql.Append(" IDNumberIsAuthenticate = 1  ");
        //    strSql.Append(" where UserName=@UserName ");

        //    SqlParameter[] parameters = {
        //                new SqlParameter("@UserName", SqlDbType.VarChar,20)
        //                                };
        //    parameters[0].Value = UserName;
        //    return db.ExecNon(strSql.ToString(), parameters) > 0 ? true : false;
        //}

        public bool UpdateById(string id, int type)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SystemUsers set ");
            strSql.Append(" IDNumberIsAuthenticate = " + type);
            strSql.Append(" where Id=@Id ");

            SqlParameter[] parameters = {
                        new SqlParameter("@Id", SqlDbType.VarChar,20)
                                        };
            parameters[0].Value = id;
            return db.ExecNon(strSql.ToString(), parameters) > 0 ? true : false;
        }

        /// <summary>
        /// 通过ID 更新一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="fileds"></param>
        /// <returns></returns>
        public bool Update(SystemUsers model, List<string> fileds)
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

        public bool Update(SystemUsers model, List<string> fileds, string sqlWhere)
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
            strSql.Append("update SystemUsers set ");
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
        public SystemUsers GetModel(string where)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 ID, UserAddress, UserEmail, UserType, UserState, IsEnterprise, Balance, Freeze, RegTime, UserName, UserPassword, PayPassword, CellPhone, CellPahoneIsAuthenticate, RealName, IDNumber, IDNumberIsAuthenticate  ");
            strSql.Append("  from SystemUsers ");
            strSql.Append(" where ");
            strSql.Append(where);

            var dt = db.GetTable(strSql.ToString());
            SystemUsers model = null;
            if (dt.Rows.Count > 0)
            {
                model = Mapper.DynamicMap<IDataReader, List<SystemUsers>>(dt.CreateDataReader()).FirstOrDefault();
            }
            return model;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SystemUsers GetUserInfo(int userID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT TOP 1 ID, UserAddress, UserEmail, UserType, UserState, IsEnterprise, Balance, Freeze, RegTime, UserName, UserPassword, PayPassword, CellPhone, CellPahoneIsAuthenticate, RealName, IDNumber, IDNumberIsAuthenticate ");
            strSql.Append("FROM SystemUsers ");
            strSql.Append("WHERE ID = @ID");
            SqlCommand cmd = db.GetSqlStringCommand(strSql.ToString());
            db.AddInputParameter(cmd, "@ID", DbType.Int32, userID);

            var dt = db.ExecuteDataTable(cmd);
            SystemUsers model = null;
            if (dt.Rows.Count > 0)
            {
                model = Mapper.DynamicMap<IDataReader, List<SystemUsers>>(dt.CreateDataReader()).FirstOrDefault();
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<SystemUsers> GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM SystemUsers ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            var dt = db.GetTable(strSql.ToString());
            return Mapper.DynamicMap<IDataReader, List<SystemUsers>>(dt.CreateDataReader());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public List<SystemUsers> GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" * ");
            strSql.Append(" FROM SystemUsers ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            var dt = db.GetTable(strSql.ToString());
            return Mapper.DynamicMap<IDataReader, List<SystemUsers>>(dt.CreateDataReader());
        }
        /// <summary>
        /// 获得分页数据
        /// </summary>
        public List<SystemUsers> GetList(int Top, string strWhere, int index, int pageIndex, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" ID, UserAddress, UserEmail, UserType, UserState, IsEnterprise, Balance, Freeze, RegTime, UserName, UserPassword, PayPassword, CellPhone, CellPahoneIsAuthenticate, RealName, IDNumber, IDNumberIsAuthenticate ");
            strSql.Append(" FROM SystemUsers ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" and id not in ");
            strSql.Append("(select top " + index * pageIndex + " ");
            strSql.Append("ID, UserAddress, UserEmail, UserType, UserState, IsEnterprise, Balance, Freeze, RegTime, UserName, UserPassword, PayPassword, CellPhone, CellPahoneIsAuthenticate, RealName, IDNumber, IDNumberIsAuthenticate ");
            strSql.Append(" FROM SystemUsers");

            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(") order by " + filedOrder);
            var dt = db.GetTable(strSql.ToString());
            return Mapper.DynamicMap<IDataReader, List<SystemUsers>>(dt.CreateDataReader());
        }

        /// <summary>
        /// 获得分页数据
        /// </summary>
        public List<SystemUsers> GetPagerList(out int TotalCount, int PageSize, int PageIndex, string strWhere = " 1=1 ", string fileds = "*", string OrderBy = " Id desc")
        {
            var dt = db.GetPagerTable(typeof(SystemUsers).Name, out TotalCount, PageSize, PageIndex, strWhere, fileds, OrderBy);
            return Mapper.DynamicMap<IDataReader, List<SystemUsers>>(dt.CreateDataReader());
        }

        #endregion

        #region 操作函数
        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int SystemUsers_Registered(SystemUsers model, out string errorMsg)
        {
            SqlCommand cmd = db.GetStoredProcedureCommand("Proc_SystemUsers_Registered");
            db.AddInputParameter(cmd, "@UserName", DbType.String, model.UserName);
            db.AddInputParameter(cmd, "@UserPassword", DbType.String, model.UserPassword);
            db.AddInputParameter(cmd, "@UserEmail", DbType.String, model.UserEmail);
            db.AddInputParameter(cmd, "@CellPhone", DbType.String, model.CellPhone);
            db.AddInputParameter(cmd, "@CellPahoneIsAuthenticate", DbType.Boolean, model.CellPahoneIsAuthenticate);
            db.AddInputParameter(cmd, "@UserState", DbType.Int32, model.UserState);
            db.AddInputParameter(cmd, "@UserType", DbType.Int32, model.UserType);
            db.AddInputParameter(cmd, "@Recommended", DbType.String, model.Recommended);
            db.AddInputParameter(cmd, "@IsEnterprise", DbType.Int32, model.IsEnterprise);
            db.AddInputParameter(cmd, "@RegTime", DbType.String, model.RegTime);
            db.AddOutputParameter(cmd, "@ErrorMsg", DbType.String, 500);
            db.AddReturnValueParameter(cmd, "@ReturnValue", DbType.Int32, 4);
            db.ExecuteNonQuery(cmd);
            errorMsg = db.GetParameter(cmd, "@ErrorMsg").Value.ToString();
            return db.GetParameter(cmd, "@ReturnValue").Value.ToInt();
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userNameOrPhone">用户名或者手机好嘛</param>
        /// <param name="userPwd">用户密码(MD5加密)</param>
        /// <param name="userIP">用户登录IP</param>
        /// <param name="userType">用户类型</param>
        /// <returns></returns>
        public BaseResultDto<SystemUsers> Landing(string userNameOrPhone, string userPwd, string userIP, int userType)
        {
            BaseResultDto<SystemUsers> model = new BaseResultDto<SystemUsers>();
            SqlCommand cmd = db.GetStoredProcedureCommand("Proc_Landing");
            db.AddInputParameter(cmd, "@UserNameOrPhone", DbType.String, userNameOrPhone);
            db.AddInputParameter(cmd, "@pwd", DbType.String, userPwd);
            db.AddInputParameter(cmd, "@ip", DbType.String, userIP);
            db.AddInputParameter(cmd, "@UserType", DbType.Int32, userType);
            db.AddOutputParameter(cmd, "@ErrorMsg", DbType.String, 500);
            db.AddReturnValueParameter(cmd, "@ReturnValue", DbType.Int32, 4);
            var dt = db.ExecuteDataTable(cmd);
            int returnValue = db.GetParameter(cmd, "@ReturnValue").Value.ToInt();
            if (dt != null)
            {
                if (returnValue <= 0)
                {
                    model.IsSeccess = false;
                    model.ErrorMsg = db.GetParameter(cmd, "@ErrorMsg").Value.ToString();
                }
                else
                {
                    model.IsSeccess = true;
                    model.Tag = dt.CreateDataReader().ReaderToModel<SystemUsers>();
                }
            }
            return model;
        }

        /// <summary>
        /// 身份认证
        /// </summary>
        /// <returns></returns>
        public BaseResultDto<bool> AuthenticateIDCard(SystemUsers user)
        {
            BaseResultDto<bool> resu = new BaseResultDto<bool>();
            SqlCommand cmd = db.GetStoredProcedureCommand("Proc_AuthenticateIDCard");
            db.AddInputParameter(cmd, "@UserID", DbType.Int32, user.ID);
            db.AddInputParameter(cmd, "@UserRealName", DbType.String, user.RealName);
            db.AddInputParameter(cmd, "@UserIDCard", DbType.String, user.IDNumber);
            db.AddInputParameter(cmd, "@UserAddress", DbType.String, user.UserAddress);
            db.AddOutputParameter(cmd, "@ErrorMsg", DbType.String, 500);
            db.AddReturnValueParameter(cmd, "@ReturnValue", DbType.Int32, 4);
            db.ExecuteNonQuery(cmd);
            int result = db.GetParameter(cmd, "@ReturnValue").Value.ToInt();
            if (result > 0)
            {
                resu.IsSeccess = true;
            }
            else
            {
                resu.IsSeccess = false;
                resu.ErrorMsg = db.GetParameter(cmd, "@ErrorMsg").Value.ToString();
            }
            return resu;
        }

        /// <summary>
        /// 获取用户资金
        /// </summary>
        /// <param name="userID">用户编号</param>
        /// <returns></returns>
        public BaseResultDto<decimal> GetUserMoney(int userID)
        {
            BaseResultDto<decimal> result = new BaseResultDto<decimal>();
            SqlCommand cmd = db.GetSqlStringCommand("SELECT Balance FROM SystemUsers WHERE ID = @ID");
            db.AddInputParameter(cmd, "@ID", DbType.Int32, userID);
            using (IDataReader dr = db.ExecuteReader(cmd))
            {
                if (dr.Read())
                {
                    result.IsSeccess = true;
                    result.Tag = dr[0].ToDecimal();
                }
                dr.Close();
                cmd.Dispose();
            }

            return result;
        }

        /// <summary>
        /// 获取用户虚拟本金
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns></returns>
        public BaseResultDto<decimal> GetUserVirtualMoney(int userID)
        {
            BaseResultDto<decimal> result = new BaseResultDto<decimal>();
            SqlCommand cmd = db.GetSqlStringCommand("SELECT ISNULL(SUM(Integral),0) FROM SystemBounty WHERE UserID = @ID AND UseType = 0 AND OverTime > GETDATE() AND BountyType = 1");
            db.AddInputParameter(cmd, "@ID", DbType.Int32, userID);
            using (IDataReader dr = db.ExecuteReader(cmd))
            {
                if (dr.Read())
                {
                    result.IsSeccess = true;
                    result.Tag = dr[0].ToDecimal();
                }
                dr.Close();
                cmd.Dispose();
            }

            return result;
        }

        /// <summary>
        /// 银行卡绑定
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public int BindBankCard(int userID, out string errorMsg)
        {
            return request.SystemRequestRecord_Add(userID, 0, 0, 7, out errorMsg);
        }

        /// <summary>
        /// 获取用户绑定的卡
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public SystemBankCard GetBindCard(int userID)
        {
            SqlCommand cmd = db.GetSqlStringCommand("SELECT * FROM SystemBankCard WHERE UserId = @UserId AND IsDefault = 1");
            db.AddInputParameter(cmd, "@UserId", DbType.Int32, userID);
            DataTable dt = db.ExecuteDataTable(cmd);
            if (dt.Rows.Count <= 0)
            {
                return null;
            }
            return dt.CreateDataReader().ReaderToModel<SystemBankCard>();
        }

        /// <summary>
        /// 用户解绑
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public BaseResultDto<bool> UnBankCard(int userID)
        {
            BaseResultDto<bool> result = new BaseResultDto<bool>();
            // 同步返回处理
            SystemBankCardOper systemBankCardOper = new SystemBankCardOper();

            bool isOK = systemBankCardOper.UpdateByUserId(userID + "");
            if (isOK)
            {
                result.IsSeccess = true;
            }
            else
            {
                result.IsSeccess = false;
                result.ErrorMsg = "取消绑卡失败";
            }
            return result;
        }

        /// <summary>
        /// 充值
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="money"></param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public int Rechare(int userID, decimal money, out string errorMsg)
        {
            return request.SystemRequestRecord_Add(userID, 0, money, 2, out errorMsg);
        }

        /// <summary>
        /// 提款
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="money"></param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public int Withdraw(int userID, decimal money, out string errorMsg)
        {
            return request.SystemRequestRecord_Add(userID, 0, money, 4, out errorMsg);
        }
        #endregion

        #region 统计函数
        /// <summary>
        /// 统计总收益和总投资
        /// </summary>
        public Earnings_Return Totalrevenue(Earnings_Parameter par)
        {
            SqlCommand cmd = db.GetStoredProcedureCommand("Proc_Totalrevenue");
            db.AddInputParameter(cmd, "@UserID", DbType.Int32, par.UserID);
            db.AddInputParameter(cmd, "@SearchWay", DbType.Int32, par.SearchWay);
            db.AddInputParameter(cmd, "@Datas", DbType.Int32, par.Datas);
            db.AddInputParameter(cmd, "@BeginDate", DbType.DateTime, par.BeginDate);
            db.AddInputParameter(cmd, "@EndDate", DbType.DateTime, par.EndDate);
            db.AddReturnValueParameter(cmd, "@ReturnValue", DbType.Int32, 4);
            DataTable dt = db.ExecuteDataTable(cmd);
            return dt.CreateDataReader().ReaderToModel<Earnings_Return>();
        }

        /// <summary>
        /// 资金流水
        /// </summary>
        /// <param name="par"></param>
        /// <returns></returns>
        public ModelByCount<TransactionRecord_Return> TransactionRecord(TransactionRecord_Parameter par)
        {
            ModelByCount<TransactionRecord_Return> model = new ModelByCount<TransactionRecord_Return>();
            SqlCommand cmd = db.GetStoredProcedureCommand("Proc_TransactionRecord");
            db.AddInputParameter(cmd, "@UserID", DbType.Int32, par.UserId);
            db.AddInputParameter(cmd, "@PageIndex", DbType.Int32, par.PageIndex);
            db.AddInputParameter(cmd, "@PageSize", DbType.Int32, par.PageSize);
            db.AddReturnValueParameter(cmd, "@ReturnValue", DbType.Int32, 4);
            DataTable dt = db.ExecuteDataTable(cmd);
            model.PageIndex = par.PageIndex;
            model.PageSize = par.PageSize;
            model.ListAll = dt.CreateDataReader().ReaderToList<TransactionRecord_Return>();
            model.AllCount = db.GetParameter(cmd, "@ReturnValue").Value.ToInt();
            return model;
        }

        /// <summary>
        /// 获取体现记录(分页)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ModelByCount<SystemRequestRecord> GetWithdrawRecord(TransactionRecord_Parameter model)
        {
            ModelByCount<SystemRequestRecord> result = new ModelByCount<SystemRequestRecord>();
            SqlCommand cmd = db.GetStoredProcedureCommand("Proc_GetWithdrawRecord");
            db.AddInputParameter(cmd, "@UserID", DbType.Int32, model.UserId);
            db.AddInputParameter(cmd, "@PageIndex", DbType.Int32, model.PageIndex);
            db.AddInputParameter(cmd, "@PageSize", DbType.Int32, model.PageSize);
            db.AddReturnValueParameter(cmd, "@ReturnValue", DbType.Int32, 4);
            DataTable dt = db.ExecuteDataTable(cmd);
            result.ListAll = dt.CreateDataReader().ReaderToList<SystemRequestRecord>();
            result.PageIndex = model.PageIndex;
            result.PageSize = model.PageSize;
            result.AllCount = db.GetParameter(cmd, "@ReturnValue").Value.ToInt();
            return result;
        }
        #endregion
    }
}
