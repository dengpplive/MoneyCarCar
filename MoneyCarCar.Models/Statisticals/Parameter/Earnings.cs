using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyCarCar.Models.Statisticals.Parameter
{
    public class Earnings_Parameter
    {
        public Earnings_Parameter()
        {
            BeginDate = Convert.ToDateTime("1900-01-01"); ;
            EndDate = Convert.ToDateTime("1900-01-01");
        }
        /// <summary>
        /// 用户编号
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// 查询方式,(1:按天,2:按起始时间)
        /// </summary>
        public int SearchWay { get; set; }
        /// <summary>
        /// 查询天数
        /// </summary>
        public int Datas { get; set; }
        /// <summary>
        /// 查询开始时间
        /// </summary>
        public DateTime BeginDate { get; set; }
        /// <summary>
        /// 查询结束日期
        /// </summary>
        public DateTime EndDate { get; set; }


    }
}
