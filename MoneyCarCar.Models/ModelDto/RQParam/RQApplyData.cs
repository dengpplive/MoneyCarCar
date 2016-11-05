using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyCarCar.Models.ModelDto.RQParam
{
    /// <summary>
    /// 处理申请需要的数据
    /// </summary>
    public class RQApplyData
    {
        public int ApplyId { get; set; }
        public string Publisher { get; set; }
        public int OperatorUserId { get; set; }
        public string OperatorUserName { get; set; }
        public string IP { get; set; }
    }
}
