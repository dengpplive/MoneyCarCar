using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace MoneyCarCar.Commons
{
    public class SQLHelper
    {
        private static SQLHelper model = null;
        private string connString = "";
        private SqlConnection conn = null;

        internal SqlConnection Connection
        {
            get { return conn; }
        }

        public static SQLHelper Single
        {
            get
            {
                if (model == null)
                {
                    model = new SQLHelper();
                }
                return model;
            }
        }

        private SQLHelper()
        {
            this.connString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            CreatConnection();
        }

        /// <summary>
        /// 创建SqlConnection
        /// </summary>
        private void CreatConnection()
        {
            this.conn = new SqlConnection(connString);
        }

        #region 获取SqlCommand
        /// <summary>
        /// 获取sql语句的Command
        /// </summary>
        /// <param name="sqlString">sql语句</param>
        public SqlCommand GetSqlStringCommand(string sqlString)
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = sqlString;
            return cmd;
        }

        /// <summary>
        /// 获取存储过程语句的Command
        /// </summary>
        /// <param name="sqlString">存储过程名称</param>
        public SqlCommand GetStoredProcedureCommand(string storedProcedureName)
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = storedProcedureName;
            return cmd;
        }
        #endregion


        #region 添加参数
        public void AddParmeters(SqlCommand cmd, SqlParameter[] pars)
        {
            cmd.Parameters.AddRange(pars);
        }

        public void AddInputParameter(SqlCommand cmd, string pName, System.Data.DbType dbType, object value)
        {
            SqlParameter par = new SqlParameter();
            par.DbType = dbType;
            par.ParameterName = pName;
            par.Value = value;
            par.Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters.Add(par);
        }

        public void AddReturnValueParameter(SqlCommand cmd, string pName, System.Data.DbType dbType, int size)
        {
            SqlParameter par = new SqlParameter();
            par.DbType = dbType;
            par.ParameterName = pName;
            par.Size = size;
            par.Direction = System.Data.ParameterDirection.ReturnValue;
            cmd.Parameters.Add(par);
        }

        public void AddOutputParameter(SqlCommand cmd, string pName, System.Data.DbType dbType, int size)
        {
            SqlParameter par = new SqlParameter();
            par.DbType = dbType;
            par.ParameterName = pName;
            par.Size = size;
            par.Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.Add(par);
        }
        #endregion

        #region 获取参数

        public SqlParameter GetParameter(SqlCommand cmd, string parameterName)
        {
            if (cmd == null) return null;
            if (cmd.Parameters == null) return null;
            if (cmd.Parameters.Count == 0) return null;
            return cmd.Parameters[parameterName];
        }
        #endregion

        #region 执行

        public int ExecuteNonQuery(SqlCommand cmd)
        {
            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                {
                    if (conn.State != System.Data.ConnectionState.Closed)
                    {
                        conn.Close();
                    }
                    conn.Open();
                }
                return cmd.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                throw e;
            }
            finally
            {
                if (conn.State != System.Data.ConnectionState.Closed)
                    conn.Close();
            }
        }

        /// <summary>
        /// 必须关闭Reader
        /// </summary>
        public SqlDataReader ExecuteReader(SqlCommand cmd)
        {
            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                {
                    if (conn.State != System.Data.ConnectionState.Closed)
                    {
                        conn.Close();
                    }
                    conn.Open();
                }
                return cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            }
            catch (SqlException e)
            {
                throw e;
            }
            finally
            {
                //if (conn.State != System.Data.ConnectionState.Closed)
                //    conn.Close();
            }
        }

        public DataSet ExecuteDataSet(SqlCommand cmd)
        {
            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                {
                    if (conn.State != System.Data.ConnectionState.Closed)
                    {
                        conn.Close();
                    }
                    conn.Open();
                }
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
            catch (SqlException e)
            {
                throw e;
            }
            finally
            {
                if (conn.State != System.Data.ConnectionState.Closed)
                    conn.Close();
            }
        }

        public DataTable ExecuteDataTable(SqlCommand cmd)
        {
            try
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }
                DataTable data = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(data);
                return data;
            }
            catch (SqlException e)
            {
                throw e;
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
        }

        /// <summary>
        /// 增删查改调用
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public int ExecNon(string sql, SqlParameter[] parameters = null)
        {
            SqlCommand sc = Single.GetSqlStringCommand(sql);
            if (parameters != null && parameters.Length > 0)
                Single.AddParmeters(sc, parameters);
            int obj = Single.ExecuteNonQuery(sc);
            return obj;
        }
        /// <summary>
        /// 查询并且返回datatable
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataTable GetTable(string sql)
        {
            SqlCommand sc = Single.GetSqlStringCommand(sql);
            DataTable table = Single.ExecuteDataTable(sc);
            return table;
        }

        public DataSet GetDataSetByProc(string storedProcedureName, params SqlParameter[] parameters)
        {
            SqlCommand sc = Single.GetStoredProcedureCommand(storedProcedureName);
            if (parameters != null && parameters.Length > 0)
                Single.AddParmeters(sc, parameters);
            return Single.ExecuteDataSet(sc);
        }
        public int ExecByProc(string storedProcedureName, params SqlParameter[] parameters)
        {
            SqlCommand sc = Single.GetStoredProcedureCommand(storedProcedureName);
            if (parameters != null && parameters.Length > 0)
                Single.AddParmeters(sc, parameters);
            return Single.ExecuteNonQuery(sc);
        }
        public object ExecReturnByProc(string storedProcedureName, params SqlParameter[] parameters)
        {
            SqlCommand sc = Single.GetStoredProcedureCommand(storedProcedureName);
            if (parameters != null && parameters.Length > 0)
                Single.AddParmeters(sc, parameters);
            AddReturnValueParameter(sc, "@ReturnValue", DbType.Int32, 4);
            Single.ExecuteNonQuery(sc);
            return GetParameter(sc, "@ReturnValue").Value;
        }
        /// <summary>
        /// 分页   
        /// </summary>
        public DataTable GetPagerTable(string tableName, out int TotalCount, int PageSize, int PageIndex, string strWhere = " 1=1 ", string Fileds = "*", string OrderStr = "")
        {
            TotalCount = 0;
            SqlCommand sc = Single.GetStoredProcedureCommand("Proc_Page");
            this.AddInputParameter(sc, "PageIndex", DbType.Int32, PageIndex);
            this.AddInputParameter(sc, "pageSize", DbType.Int32, PageSize);
            this.AddInputParameter(sc, "tableName", DbType.String, tableName);
            this.AddInputParameter(sc, "selFields", DbType.String, (string.IsNullOrEmpty(Fileds) ? "*" : Fileds));
            this.AddInputParameter(sc, "SqlWhere", DbType.String, (string.IsNullOrEmpty(strWhere) ? " " : strWhere));
            if (!string.IsNullOrEmpty(OrderStr) && !OrderStr.Trim().ToLower().StartsWith("order by"))
            {
                OrderStr = OrderStr.ToLower().Replace("order by", " ");
            }
            if (string.IsNullOrEmpty(OrderStr))
            {
                OrderStr = " Id desc";
            }
            this.AddInputParameter(sc, "SortOrder", DbType.String, OrderStr);
            this.AddOutputParameter(sc, "PageTotal", DbType.String, 4);
            DataTable table = Single.ExecuteDataTable(sc);
            object outTotal = this.GetParameter(sc, "PageTotal").Value;
            if (outTotal != null)
                int.TryParse(outTotal.ToString(), out TotalCount);
            return table;
        }


        #endregion

        #region 事务执行
        /// <summary>
        /// 在事务状态下执行数据库命令
        /// </summary>
        /// <param name="cmd">数据库命令</param>
        /// <param name="t">事务类</param>
        /// <returns>受影响的行数</returns>
        public int ExecuteNonQuery(SqlCommand cmd, Trans t)
        {
            try
            {
                cmd.Connection = t.DbConnection;
                cmd.Transaction = t.DbTrans;
                int ret = cmd.ExecuteNonQuery();
                return ret;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// 必须关闭Reader
        /// </summary>
        public SqlDataReader ExecuteReader(SqlCommand cmd, Trans t)
        {
            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                {
                    if (conn.State != System.Data.ConnectionState.Closed)
                    {
                        conn.Close();
                    }
                    conn.Open();
                }
                cmd.Connection = t.DbConnection;
                cmd.Transaction = t.DbTrans;
                return cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
            }
            catch (SqlException e)
            {
                throw e;
            }
            finally
            {
                //if (conn.State != System.Data.ConnectionState.Closed)
                //    conn.Close();
            }
        }

        #endregion
    }
    /// <summary>
    /// 数据库事务类
    /// </summary>
    public class Trans : IDisposable
    {
        private SqlConnection conn;
        private SqlTransaction dbTrans;

        public SqlConnection DbConnection
        {
            get { return this.conn; }
        }
        public SqlTransaction DbTrans
        {
            get { return this.dbTrans; }
        }

        public Trans()
        {
            conn = SQLHelper.Single.Connection;
            if (conn.State != System.Data.ConnectionState.Open)
            {
                if (conn.State != System.Data.ConnectionState.Closed)
                {
                    conn.Close();
                }
                conn.Open();
            }
            dbTrans = conn.BeginTransaction();
        }
        ~Trans()
        {
            if (conn != null && conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();
            }
        }
        public void Commit()
        {
            dbTrans.Commit();
            this.Colse();
        }

        public void RollBack()
        {
            dbTrans.Rollback();
            this.Colse();
        }

        public void Colse()
        {
            if (conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();
            }
        }

        public void Dispose()
        {
            this.Colse();
        }
    }
}
