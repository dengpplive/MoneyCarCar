using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoneyCarCar.Commons;
using System.Data;
using System.Data.SqlClient;
using MoneyCarCar.Models.DtoModels;
using MoneyCarCar.Models;
using MoneyCarCar.Models.Statisticals.Parameter;

namespace MoneyCarCar.DAL.DataGet
{
    public class WebSiteDatasOper
    {
        SQLHelper db = SQLHelper.Single;

        public DataTableCollection GetIndexDatas()
        {
            SqlCommand cmd = db.GetStoredProcedureCommand("Proc_IndexDatas");
            DataSet ds = db.ExecuteDataSet(cmd);
            return ds.Tables;
        }
        /// <summary>
        /// 查找并分页
        /// </summary>
        /// <param name="key"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ModelByCount<SystemHelp> GetHelps(string key, int pageIndex, int pageSize)
        {
            ModelByCount<SystemHelp> result = new ModelByCount<SystemHelp>();
            SqlCommand cmd = db.GetStoredProcedureCommand("Proc_GetHelpsByKeyPage");
            db.AddInputParameter(cmd, "@Key", DbType.String, "%" + key + "%");
            db.AddInputParameter(cmd, "@PageIndex", DbType.Int32, pageIndex);
            db.AddInputParameter(cmd, "@PageSize", DbType.Int32, pageSize);
            db.AddReturnValueParameter(cmd, "@ReturnValue", DbType.Int32, 4);
            DataTable dt = db.ExecuteDataTable(cmd);
            result.ListAll = dt.CreateDataReader().ReaderToList<SystemHelp>();
            result.PageIndex = pageIndex;
            result.PageSize = pageSize;
            result.AllCount = db.GetParameter(cmd, "@ReturnValue").Value.ToInt();
            return result;
        }
        
    }
}
