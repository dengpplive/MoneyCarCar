using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MoneyCarCar.Models.YeePay
{
    /// <summary>
    /// 接口输出:（父级实体） callback  通知收不到的可能性很高， response
    /// </summary>
    public class BaseResponse
    {
       [XmlAttribute("_platformNo")]
        private string _platformNo = "";// Y 商户编号,商户在易宝唯一标识
        private string _requestNo = "";// Y 请求流水号
        private string _service = "";//Y 服务名称，固定值
        private string _code = "";//Y 返回码,【见返回码】
        private string _description = "";//N 描述，描述异常信息

        /// <summary>
        ///Y 商户编号,商户在易宝唯一标识
        /// </summary>
         [XmlAttribute("platformNo")]
        public string platformNo
        {
            get { return _platformNo; }
            set { _platformNo = value; }
        }
        /// <summary>
         /// Y 请求流水号
        /// </summary>
        public string requestNo
        {
            get { return _requestNo; }
            set { _requestNo = value; }
        }
        /// <summary>
        /// Y 服务名称，固定值
        /// </summary>
        public string service
        {
            get { return _service; }
            set { _service = value; }
        }
        /// <summary>
        /// Y 返回码 【见返回码】： 1 成功、0 失败、2 xml 参数格式错误、3 签名验证失败、101 引用了不存在的对象（例如错误的订单号）、102 业务状态不正确、103 由于业务限制导致业务不能执行、104 实名认证失败
        /// </summary>
        public string code
        {
            get { return _code; }
            set { _code = value; }
        }
        /// <summary>
        /// N 描述，描述异常信息
        /// </summary>
        public string description
        {
            get { return _description; }
            set { _description = value; }
        }
    }
}

