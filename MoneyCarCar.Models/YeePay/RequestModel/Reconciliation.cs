using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyCarCar.Models.YeePay.RequestModel
{
    /// <summary>
    /// 3.10.对账: 接口输入
    /// </summary>
    public class Reconciliation : YeePayConfig
    {
        public Reconciliation()
        {
            date = DateTime.Now.ToString("yyyy-MM-dd");
        }
        /// <summary>
        /// 日期 Y, yyyy-MM-dd 格式
        /// </summary>
        public string date { get; set; }
    }
}