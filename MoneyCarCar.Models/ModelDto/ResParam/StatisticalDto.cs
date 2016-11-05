using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyCarCar.Models.ModelDto.ResParam
{
    /// <summary>
    /// 数据统计需要的数据
    /// </summary>
    public class StatisticalDto
    {
        /// <summary>
        /// 投资用户总数
        /// </summary>
        public int UserCount { get; set; }
        /// <summary>
        /// 已投资用户数
        /// </summary>
        public int InvestUserCount { get; set; }
        /// <summary>
        /// 已发债权总数 
        /// </summary>
        public int PubClaimsCount { get; set; }
        /// <summary>
        /// 未满标债权总数
        /// </summary>
        public int AvailableClaimsCount { get; set; }

        /// <summary>
        /// 已满标债权总数
        /// </summary>
        public int OverFullClaimsCount { get; set; }

        /// <summary>
        /// 总借贷金额
        /// </summary>
        public decimal TotalBorrowMoney { get; set; }
        /// <summary>
        /// 已投资总金额
        /// </summary>
        public decimal TotalInvestMoney { get; set; }
        /// <summary>
        /// 已产生利息总金额
        /// </summary>
        public decimal TotalInterestMoney { get; set; }
    }
}
