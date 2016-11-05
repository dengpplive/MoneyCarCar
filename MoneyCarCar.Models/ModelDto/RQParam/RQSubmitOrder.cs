using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyCarCar.Models.ModelDto.RQParam
{
    public class RQSubmitOrder
    {
        /// <summary>
        /// 投资用户的ID
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// 债券表ID
        /// </summary>
        public int InvestorsID { get; set; }
        /// <summary>
        /// 购买数量
        /// </summary> 
        public int BuyCount { get; set; }
        /// <summary>
        /// 是否使用奖励(0:不使用,1:使用虚拟本金,2:使用利息)
        /// </summary>
        public int IsUserBounty { get; set; }
        /// <summary>
        /// 使用奖励的数量(-1:全部使用,0:不适用,其他为写多少使用多少)
        /// </summary>
        public int BountyCount { get; set; }
    }
}
