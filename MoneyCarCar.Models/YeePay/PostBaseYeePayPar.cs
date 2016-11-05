using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyCarCar.Models.YeePay
{
    /// <summary>
    /// POST 请求易宝实体
    /// </summary>
   public class PostBaseYeePayPar
    {
       /// <summary>
       /// 签名
       /// </summary>
       public string sign { get; set; }
       /// <summary>
       /// 请求XML
       /// </summary>
       public string req { get; set; }
       /// <summary>
       /// 服务名称
       /// </summary>
       public string service { get; set; }
    }
}
