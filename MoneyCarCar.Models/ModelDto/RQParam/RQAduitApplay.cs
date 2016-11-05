using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyCarCar.Models.ModelDto.RQParam
{
    /// <summary>
    /// 债权审核数据
    /// </summary>
    public class RQAduitApplay
    {
        /// <summary>
        /// 债权申请表ID
        /// </summary>
        public int ApplyID { get; set; }
        /// <summary>
        ///  债权表ID
        /// </summary>
        public int ClaimsID { get; set; }
        /// <summary>
        /// 审核是否通过 true通过 false未通过
        /// </summary>
        public bool Succeed { get; set; }
        /// <summary>
        /// 操作用户Id
        /// </summary>
        public int OperatorUserId { get; set; }
        /// <summary>
        /// 操作用户姓名
        /// </summary>
        public string ApproverUserName { get; set; }
        /// <summary>
        /// 操作内容
        /// </summary>
        public string OperatorContent { get; set; }
        /// <summary>
        /// Ip
        /// </summary>
        public string IP { get; set; }
    }
}
