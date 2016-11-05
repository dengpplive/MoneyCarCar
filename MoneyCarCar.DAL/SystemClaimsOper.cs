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
using AutoMapper.Mappers;
using MoneyCarCar.Models.ModelDto.ResParam;
namespace MoneyCarCar.DAL
{
    //债权表
    public class SystemClaimsOper
    {
        //为了保持对AutoMappers.Net4的引用,AutoMappers.Net4反射动态使用
        //编译的时候不引用编译器会优化直接删除导致转换失败
        DataReaderMapper data = null;
        SQLHelper db = SQLHelper.Single;



        public int Exists(string where)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from SystemClaims");
            strSql.Append(" where ");
            strSql.Append(where);
            return Convert.ToInt32(db.GetTable(strSql.ToString()).Rows[0][0].ToString());

        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SystemClaims model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SystemClaims(");
            strSql.Append("RepaymentWat,SellingPrice,InvestmentEndTime,EarningsStartTime,Borrower,PawnSpec,Publisher,PublishTime,IsApproved,ApproverUserName,Title,ApproverTime,BorrowerID,Icons,ClaimsApplayID,DetailsImages1,DetailsImages2,DetailsImages3,DetailsImages4,DetailsImages5,TitleImagePath,LoanAmount,APR,LoanPeriod,SingleAmount,AlreadyAmount,GuaranteeWay");
            strSql.Append(") values (");
            strSql.Append("@RepaymentWat,@SellingPrice,@InvestmentEndTime,@EarningsStartTime,@Borrower,@PawnSpec,@Publisher,@PublishTime,@IsApproved,@ApproverUserName,@Title,@ApproverTime,@BorrowerID,@Icons,@ClaimsApplayID,@DetailsImages1,@DetailsImages2,@DetailsImages3,@DetailsImages4,@DetailsImages5,@TitleImagePath,@LoanAmount,@APR,@LoanPeriod,@SingleAmount,@AlreadyAmount,@GuaranteeWay");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@RepaymentWat", SqlDbType.VarChar,200) ,            
                        new SqlParameter("@SellingPrice", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@InvestmentEndTime", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@EarningsStartTime", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@Borrower", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@PawnSpec", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@Publisher", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@PublishTime", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@IsApproved", SqlDbType.Bit,1) ,            
                        new SqlParameter("@ApproverUserName", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@Title", SqlDbType.VarChar,200) ,            
                        new SqlParameter("@ApproverTime", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@BorrowerID", SqlDbType.Int,4) ,            
                        new SqlParameter("@Icons", SqlDbType.VarChar,8000) ,        
                        new SqlParameter("@ClaimsApplayID", SqlDbType.Int,4) ,          
                        new SqlParameter("@DetailsImages1", SqlDbType.VarChar,4000) ,            
                        new SqlParameter("@DetailsImages2", SqlDbType.VarChar,4000) ,            
                        new SqlParameter("@DetailsImages3", SqlDbType.VarChar,4000) ,            
                        new SqlParameter("@DetailsImages4", SqlDbType.VarChar,4000) ,            
                        new SqlParameter("@DetailsImages5", SqlDbType.VarChar,4000) ,            
                        new SqlParameter("@TitleImagePath", SqlDbType.VarChar,200) ,            
                        new SqlParameter("@LoanAmount", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@APR", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@LoanPeriod", SqlDbType.TinyInt,1) ,            
                        new SqlParameter("@SingleAmount", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@AlreadyAmount", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@GuaranteeWay", SqlDbType.VarChar,200)             
              
            };

            parameters[0].Value = model.RepaymentWat;
            parameters[1].Value = model.SellingPrice;
            parameters[2].Value = model.InvestmentEndTime;
            parameters[3].Value = model.EarningsStartTime;
            parameters[4].Value = model.Borrower;
            parameters[5].Value = model.PawnSpec;
            parameters[6].Value = model.Publisher;
            parameters[7].Value = model.PublishTime;
            parameters[8].Value = model.IsApproved;
            parameters[9].Value = model.ApproverUserName;
            parameters[10].Value = model.Title;
            parameters[11].Value = model.ApproverTime;
            parameters[12].Value = model.BorrowerID;
            parameters[13].Value = model.Icons;
            parameters[14].Value = model.ClaimsApplayID;
            parameters[15].Value = model.DetailsImages1;
            parameters[16].Value = model.DetailsImages2;
            parameters[17].Value = model.DetailsImages3;
            parameters[18].Value = model.DetailsImages4;
            parameters[19].Value = model.DetailsImages5;
            parameters[20].Value = model.TitleImagePath;
            parameters[21].Value = model.LoanAmount;
            parameters[22].Value = model.APR;
            parameters[23].Value = model.LoanPeriod;
            parameters[24].Value = model.SingleAmount;
            parameters[25].Value = model.AlreadyAmount;
            parameters[26].Value = model.GuaranteeWay; return db.ExecNon(strSql.ToString(), parameters);


        }
        /// <summary>
        /// 更新一条数据（有删减，注意看清楚字段）
        /// </summary>
        public bool Update(SystemClaims model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SystemClaims set ");

            strSql.Append(" RepaymentWat = @RepaymentWat , ");
            strSql.Append(" SellingPrice = @SellingPrice , ");
            strSql.Append(" InvestmentEndTime = @InvestmentEndTime , ");
            strSql.Append(" EarningsStartTime = @EarningsStartTime , ");
            strSql.Append(" Borrower = @Borrower , ");
            strSql.Append(" PawnSpec = @PawnSpec , ");
            strSql.Append(" Publisher = @Publisher , ");
            strSql.Append(" PublishTime = @PublishTime , ");
            strSql.Append(" IsApproved = @IsApproved , ");
            strSql.Append(" ApproverUserName = @ApproverUserName , ");
            strSql.Append(" Title = @Title , ");
            strSql.Append(" ApproverTime = @ApproverTime , ");
            strSql.Append(" BorrowerID = @BorrowerID , ");
            strSql.Append(" ClaimsApplayID = @ClaimsApplayID , ");
            strSql.Append(" Icons = @Icons , ");
            strSql.Append(" DetailsImages1 = @DetailsImages1 , ");
            strSql.Append(" DetailsImages2 = @DetailsImages2 , ");
            strSql.Append(" DetailsImages3 = @DetailsImages3 , ");
            strSql.Append(" DetailsImages4 = @DetailsImages4 , ");
            strSql.Append(" DetailsImages5 = @DetailsImages5 , ");
            strSql.Append(" TitleImagePath = @TitleImagePath , ");
            strSql.Append(" LoanAmount = @LoanAmount , ");
            strSql.Append(" APR = @APR , ");
            strSql.Append(" LoanPeriod = @LoanPeriod , ");
            strSql.Append(" SingleAmount = @SingleAmount , ");
            strSql.Append(" AlreadyAmount = @AlreadyAmount , ");
            strSql.Append(" GuaranteeWay = @GuaranteeWay  ");
            strSql.Append(" where ID=@ID ");

            SqlParameter[] parameters = {
			            new SqlParameter("@ID", SqlDbType.Int,4) ,            
                        new SqlParameter("@RepaymentWat", SqlDbType.VarChar,200) ,            
                        new SqlParameter("@SellingPrice", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@InvestmentEndTime", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@EarningsStartTime", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@Borrower", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@PawnSpec", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@Publisher", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@PublishTime", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@IsApproved", SqlDbType.Bit,1) ,            
                        new SqlParameter("@ApproverUserName", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@Title", SqlDbType.VarChar,200) ,            
                        new SqlParameter("@ApproverTime", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@BorrowerID", SqlDbType.Int,4) ,            
                        new SqlParameter("@ClaimsApplayID", SqlDbType.Int,4) ,            
                        new SqlParameter("@Icons", SqlDbType.VarChar,8000) ,            
                        new SqlParameter("@DetailsImages1", SqlDbType.VarChar,4000) ,            
                        new SqlParameter("@DetailsImages2", SqlDbType.VarChar,4000) ,            
                        new SqlParameter("@DetailsImages3", SqlDbType.VarChar,4000) ,            
                        new SqlParameter("@DetailsImages4", SqlDbType.VarChar,4000) ,            
                        new SqlParameter("@DetailsImages5", SqlDbType.VarChar,4000) ,            
                        new SqlParameter("@TitleImagePath", SqlDbType.VarChar,200) ,            
                        new SqlParameter("@LoanAmount", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@APR", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@LoanPeriod", SqlDbType.TinyInt,1) ,            
                        new SqlParameter("@SingleAmount", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@AlreadyAmount", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@GuaranteeWay", SqlDbType.VarChar,200)             
              
            };

            parameters[0].Value = model.ID;
            parameters[1].Value = model.RepaymentWat;
            parameters[2].Value = model.SellingPrice;
            parameters[3].Value = model.InvestmentEndTime;
            parameters[4].Value = model.EarningsStartTime;
            parameters[5].Value = model.Borrower;
            parameters[6].Value = model.PawnSpec;
            parameters[7].Value = model.Publisher;
            parameters[8].Value = model.PublishTime;
            parameters[9].Value = model.IsApproved;
            parameters[10].Value = model.ApproverUserName;
            parameters[11].Value = model.Title;
            parameters[12].Value = model.ApproverTime;
            parameters[13].Value = model.BorrowerID;
            parameters[14].Value = model.ClaimsApplayID;
            parameters[15].Value = model.Icons;
            parameters[16].Value = model.DetailsImages1;
            parameters[17].Value = model.DetailsImages2;
            parameters[18].Value = model.DetailsImages3;
            parameters[19].Value = model.DetailsImages4;
            parameters[20].Value = model.DetailsImages5;
            parameters[21].Value = model.TitleImagePath;
            parameters[22].Value = model.LoanAmount;
            parameters[23].Value = model.APR;
            parameters[24].Value = model.LoanPeriod;
            parameters[25].Value = model.SingleAmount;
            parameters[26].Value = model.AlreadyAmount;
            parameters[27].Value = model.GuaranteeWay;
            return db.ExecNon(strSql.ToString(), parameters) > 0 ? true : false;
        }

        /// <summary>
        /// 通过Id 更新部分数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="fileds"></param>
        /// <returns></returns>
        public bool Update(SystemClaims model, List<string> fileds)
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

        public bool Update(SystemClaims model, List<string> fileds, string sqlWhere)
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
            strSql.Append("update SystemClaims set ");
            strSql.AppendFormat(" {0} ", string.Join(",", keyVal.ToArray()));
            if (!string.IsNullOrEmpty(sqlWhere))
            {
                strSql.AppendFormat(" where 1=1 and {0} ", sqlWhere);
            }
            return db.ExecNon(strSql.ToString(), null) > 0 ? true : false;
        }


        public bool Delete(string where)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete ");
            strSql.Append(" from SystemClaims ");
            strSql.Append(" where ");
            strSql.Append(where);
            return (db.ExecNon(strSql.ToString()) > 0 ? true : false);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SystemClaims GetModel(string where)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID, RepaymentWat, SellingPrice, InvestmentEndTime, EarningsStartTime, Borrower, PawnSpec, Publisher, PublishTime, IsApproved, ApproverUserName, Title, ApproverTime, BorrowerID, Icons,ClaimsApplayID, DetailsImages1, DetailsImages2, DetailsImages3, DetailsImages4, DetailsImages5, TitleImagePath, LoanAmount, APR, LoanPeriod, SingleAmount, AlreadyAmount, GuaranteeWay  ");
            strSql.Append("  from SystemClaims ");
            strSql.Append(" where ");
            strSql.Append(where);

            DataTable dt = db.GetTable(strSql.ToString());
            SystemClaims model = null;
            if (dt.Rows.Count > 0)
            {
                model = Mapper.DynamicMap<IDataReader, List<SystemClaims>>(dt.CreateDataReader()).FirstOrDefault();
            }
            return model;



        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<SystemClaims> GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM SystemClaims ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            var dt = db.GetTable(strSql.ToString());
            return Mapper.DynamicMap<IDataReader, List<SystemClaims>>(dt.CreateDataReader());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public List<SystemClaims> GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" * ");
            strSql.Append(" FROM SystemClaims ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            var dt = db.GetTable(strSql.ToString());
            return Mapper.DynamicMap<IDataReader, List<SystemClaims>>(dt.CreateDataReader());
        }
        /// <summary>
        /// 获得分页数据
        /// </summary>
        public List<SystemClaims> GetList(int Top, string strWhere, int index, int pageIndex, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" ID, RepaymentWat, SellingPrice, InvestmentEndTime, EarningsStartTime, Borrower, PawnSpec, Publisher, PublishTime, IsApproved, ApproverUserName, Title, ApproverTime, BorrowerID, Icons,ClaimsApplayID, DetailsImages1, DetailsImages2, DetailsImages3, DetailsImages4, DetailsImages5, TitleImagePath, LoanAmount, APR, LoanPeriod, SingleAmount, AlreadyAmount, GuaranteeWay ");
            strSql.Append(" FROM SystemClaims ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" and id not in ");
            strSql.Append("(select top " + index * pageIndex + " ");
            strSql.Append("ID, RepaymentWat, SellingPrice, InvestmentEndTime, EarningsStartTime, Borrower, PawnSpec, Publisher, PublishTime, IsApproved, ApproverUserName, Title, ApproverTime, BorrowerID, Icons,ClaimsApplayID, DetailsImages1, DetailsImages2, DetailsImages3, DetailsImages4, DetailsImages5, TitleImagePath, LoanAmount, APR, LoanPeriod, SingleAmount, AlreadyAmount, GuaranteeWay ");
            strSql.Append(" FROM SystemClaims");

            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(") order by " + filedOrder);
            var dt = db.GetTable(strSql.ToString());
            return Mapper.DynamicMap<IDataReader, List<SystemClaims>>(dt.CreateDataReader());
        }
        /// <summary>
        /// 获得分页数据
        /// </summary>
        public List<SystemClaims> GetPagerList(out int TotalCount, int PageSize, int PageIndex, string strWhere = " 1=1 ", string fileds = "*", string OrderBy = " Id desc")
        {
            var dt = db.GetPagerTable(typeof(SystemClaims).Name, out TotalCount, PageSize, PageIndex, strWhere, fileds, OrderBy);
            //return Mapper.DynamicMap<IDataReader, List<SystemClaims>>(dt.CreateDataReader());
            return dt.CreateDataReader().ReaderToList<SystemClaims>();
        }

        /// <summary>
        /// 获取债权明细
        /// </summary>
        /// <param name="ClaimsId"></param>
        /// <returns></returns>
        public ClaimsDetailsDto GetInvestorsClaimsDetails(int ClaimsId)
        {
            BaseHelper baseHelper = new BaseHelper();
            ClaimsDetailsDto claimsDetailsDto = new ClaimsDetailsDto();
            SqlParameter sqlpa = new SqlParameter("@ClaimsId", SqlDbType.Int, 4);
            sqlpa.Value = ClaimsId;
            DataSet ds = db.GetDataSetByProc("proc_GetInvestorsClaimsDetails", new SqlParameter[]{
            sqlpa
            });
            DataTable usersTable = ds.Tables[0];
            DataTable claimsTable = ds.Tables[1];
            if (claimsTable != null && claimsTable.Rows.Count > 0)
            {
                claimsDetailsDto.ClaimsInfo = Mapper.DynamicMap<IDataReader, List<SystemClaims>>(claimsTable.CreateDataReader()).FirstOrDefault();
            }
            claimsDetailsDto.UserInfoList = new List<ClaimsDetailsItem>();
            if (ds.Tables.Count == 3)
            {
                if (usersTable != null && claimsTable.Rows.Count > 0)
                {
                    List<SystemUsers> users = Mapper.DynamicMap<IDataReader, List<SystemUsers>>(usersTable.CreateDataReader());
                    DataTable details = ds.Tables[2];
                    if (details != null && details.Rows.Count > 0)
                    {
                        List<SystemClaimsDetails> claimsDetails = Mapper.DynamicMap<IDataReader, List<SystemClaimsDetails>>(details.CreateDataReader());
                        claimsDetails.ForEach(p =>
                        {
                            ClaimsDetailsItem item = claimsDetailsDto.UserInfoList.Where(p1 => p1.User != null && p1.User.ID == p.InvestorsID).FirstOrDefault();
                            if (item == null)
                            {
                                item = new ClaimsDetailsItem();
                                item.User = users.Where(p2 => p2.ID == p.InvestorsID).FirstOrDefault();
                                if (item.User == null)
                                {
                                    item.User = baseHelper.GetModelById<SystemUsers>(p.InvestorsID);
                                }
                                if (item.ClaimsDetailsList == null) item.ClaimsDetailsList = new List<SystemClaimsDetails>();
                                item.ClaimsDetailsList.Add(p);
                                claimsDetailsDto.UserInfoList.Add(item);
                            }
                            else
                            {
                                if (item.ClaimsDetailsList == null) item.ClaimsDetailsList = new List<SystemClaimsDetails>();
                                item.ClaimsDetailsList.Add(p);
                            }
                        });
                    }
                }
            }
            return claimsDetailsDto;
        }

        /// <summary>
        /// 获取需要提交返还本金的请求
        /// </summary>
        /// <returns></returns>
        public List<SystemRequestRecord> GetReturnPrincipalList()
        {
            SqlCommand cmd = db.GetStoredProcedureCommand("Proc_Return_Principal");
            DataTable dt = db.ExecuteDataTable(cmd);
            return dt.CreateDataReader().ReaderToList<SystemRequestRecord>();
        }

        #region 统计
        /// <summary>
        /// 进行中的债权
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public DataTableCollection GetInvestment_Ing(int userID)
        {
            SqlCommand cmd = db.GetSqlStringCommand
                (
                @"SELECT 
                (SELECT COUNT(ClaimsID) FROM SystemClaimsDetails WHERE InvestorsID = @UserId AND PayStatus = 2 AND PrincipalClearState = 1 GROUP BY ClaimsID),
                (SELECT SUM(InvestorMoney) FROM SystemClaimsDetails WHERE InvestorsID = @UserId AND PayStatus = 2 AND PrincipalClearState = 1),
                (SELECT SUM(CAST(D.InvestorMoney/C.SingleAmount AS INT)) FROM SystemClaimsDetails AS D INNER JOIN SystemClaims AS C ON D.ClaimsID = C.ID WHERE D.InvestorsID = @UserId AND D.PayStatus = 2 AND D.PrincipalClearState = 1)

                SELECT CONVERT(VARCHAR(10),CAST(D.InvestorsTime AS DATETIME),120),C.Title,CAST(D.InvestorMoney/C.SingleAmount AS INT),D.InvestorMoney,'还款中' 
                FROM SystemClaimsDetails AS D INNER JOIN SystemClaims AS C ON D.ClaimsID = C.ID 
                WHERE D.InvestorsID = @UserId AND D.PayStatus = 2 AND D.PrincipalClearState = 1"
                );
            db.AddInputParameter(cmd, "@UserId", DbType.Int32, userID);
            return db.ExecuteDataSet(cmd).Tables;
        }
        /// <summary>
        /// 所有购买的信息
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public DataTable GetInvestment_All(int userID)
        {
            SqlCommand cmd = db.GetSqlStringCommand
                (
                @"SELECT C.TitleImagePath,C.Title,CONVERT(VARCHAR(10),DATEADD(MM,LoanPeriod,CAST(EarningsStartTime AS DATETIME)),120) ,
	                D.InvestorMoney/C.SingleAmount,D.InvestorMoney,D.ClaimsStatus

                FROM SystemClaims AS C INNER JOIN 
                (
                SELECT TOP 10000 ClaimsID,CASE PrincipalClearState WHEN 0 THEN '未知' WHEN 1 THEN '正在发息' WHEN 2 THEN '本金返还中' WHEN 3 THEN '本金已还' END AS ClaimsStatus,
	                SUM(InvestorMoney) AS InvestorMoney
                FROM SystemClaimsDetails 
                WHERE InvestorsID = @UserId GROUP BY ClaimsID,PrincipalClearState ORDER BY PrincipalClearState ASC) AS D ON C.ID = D.ClaimsID"
                );
            db.AddInputParameter(cmd, "@UserId", DbType.Int32, userID);
            return db.ExecuteDataTable(cmd);
        }
        /// <summary>
        /// 所有购买的信息
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public DataTable GetInvestment_Ed(int userID)
        {
            SqlCommand cmd = db.GetSqlStringCommand
                (
                @"SELECT C.TitleImagePath,C.Title,CONVERT(VARCHAR(10),DATEADD(MM,LoanPeriod,CAST(EarningsStartTime AS DATETIME)),120) ,
	                D.InvestorMoney/C.SingleAmount,D.InvestorMoney,D.ClaimsStatus

                FROM SystemClaims AS C INNER JOIN 
                (
                SELECT TOP 10000 ClaimsID,CASE PrincipalClearState WHEN 0 THEN '未知' WHEN 1 THEN '正在发息' WHEN 2 THEN '本金返还中' WHEN 3 THEN '本金已还' END AS ClaimsStatus,
	                SUM(InvestorMoney) AS InvestorMoney
                FROM SystemClaimsDetails 
                WHERE InvestorsID = @UserId GROUP BY ClaimsID,PrincipalClearState ORDER BY PrincipalClearState ASC) AS D ON C.ID = D.ClaimsID
                WHERE DATEDIFF(day,DATEADD(MM,LoanPeriod,CAST(EarningsStartTime AS DATETIME)), GETDATE()) >= 0"
                );
            db.AddInputParameter(cmd, "@UserId", DbType.Int32, userID);
            return db.ExecuteDataTable(cmd);
        }
        #endregion
    }
}
