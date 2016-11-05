using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyCarCar.Models.Statisticals.Return
{
    public class TransactionRecord_Return
    {
        /// <summary>
        /// 记录时间
        /// </summary>
        public DateTime PayTime { get; set; }
        /// <summary>
        /// 记录类型
        /// </summary>
        public int PayType { get; set; }

        /// <summary>
        /// 收入
        /// </summary>
        public decimal InMoney { get; set; }

        /// <summary>
        /// 支出
        /// </summary>
        public decimal OutMoney { get; set; }

        /// <summary>
        /// 余额
        /// </summary>
        public decimal RemainMoney { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Remark { get; set; }
    }
}
