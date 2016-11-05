using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyCarCar.Models.Statisticals.Return
{
    /// <summary>
    /// 收益统计所需要的数据
    /// </summary>
    public class Earnings_Return
    {
        /// <summary>
        /// 总投资
        /// </summary>
        public decimal TotalInvestment { get; set; }
        /// <summary>
        /// 总收益
        /// </summary>
        public decimal TotalInterest { get; set; }
        /// <summary>
        /// 现金购买利息
        /// </summary>
        public decimal CashInterest { get; set; }

        /// <summary>
        /// 虚拟本金购买利息
        /// </summary>
        public decimal VirtualInterest { get; set; }

        /// <summary>
        /// 奖励利息
        /// </summary>
        public decimal RewardInterest { get; set; }
        /// <summary>
        /// 查询计算
        /// </summary>
        public decimal SumInterest
        {
            get { return CashInterest + VirtualInterest + RewardInterest; }
        }

    }
}
