using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyCarCar.Models.DtoModels
{
    /// <summary>
    /// 登录返回信息
    /// </summary>
    public class BaseResultDto<T>
    {
        /// <summary>
        /// 成功或者失败
        /// </summary>
        public bool IsSeccess { get; set; }

        /// <summary>
        /// 携带信息
        /// </summary>
        public T Tag { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMsg { get; set; }
        /// <summary>
        /// 错误码
        /// </summary>
        public int ErrorCode { get; set; }
    }
}
