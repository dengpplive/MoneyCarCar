using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyCarCar.Models.YeePay
{
    /// <summary>
    /// 回调通知：notify是服务器之间之间通讯，并且有补发机制，可靠性更高。
    /// </summary>
    public class BaseNotify
    {
        private string _platformNo = "";// Y 商户编号,商户在易宝唯一标识
        private string _bizType = "";// Y 业务名称,固定值
        private string _code = "";//Y 返回码,【见返回码】： 1 成功、0 失败、2 xml 参数格式错误、3 签名验证失败、101 引用了不存在的对象（例如错误的订单号）、102 业务状态不正确、103 由于业务限制导致业务不能执行、104 实名认证失败
        private string _message = "";//N 描述，描述异常信息
        private string _platformUserNo = "";//Y 平台的用户编号
        private string _requestNo = "";//Y 请求流水号
        private string _status = "";// 状态固定值，不同业务值不一样

        /// <summary>
        /// 状态固定值
        /// </summary>
        public string status
        {
            get { return _status; }
            set { _status = value; }
        }
        /// <summary>
        /// Y 商户编号,商户在易宝唯一标识
        /// </summary>
        public string platformNo
        {
            get { return _platformNo; }
            set { _platformNo = value; }
        }
        /// <summary>
        /// Y 业务名称,固定值
        /// </summary>
        public string bizType
        {
            get { return _bizType; }
            set { _bizType = value; }
        }
        /// <summary>
        /// 返回码,【见返回码】    4.2. 返回状态码、枚举值 枚举描述、1 成功、0 失败、2 xml 参数格式错误、3 签名验证失败、101 引用了不存在的对象（例如错误的订单号）、102 业务状态不正确、103 由于业务限制导致业务不能执行、104 实名认证失败
        /// </summary>
        public string code
        {
            get { return _code; }
            set { _code = value; }
        }
        /// <summary>
        /// N 描述，描述异常信息
        /// </summary>
        public string message
        {
            get { return _message; }
            set { _message = value; }
        }
        /// <summary>
        /// Y 平台的用户编号
        /// </summary>
        public string platformUserNo
        {
            get { return _platformUserNo; }
            set { _platformUserNo = value; }
        }
        /// <summary>
        /// Y 请求流水号
        /// </summary>
        public string requestNo
        {
            get { return _requestNo; }
            set { _requestNo = value; }
        }
    }
}
