using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MoneyCarCar.Models.YeePay
{
    /// <summary>
    /// 
    /// </summary>
    public class BaseRequest
    {
        [XmlAttribute("_platformNo")]
        public string _platformNo = "";// Y 商户编号,商户在易宝唯一标识
     
        public string _service = "";//Y 服务名称，固定值
        public string _callbackUrl = ""; //Y 页面回跳URL,页面回跳URL
        public string _notifyUrl = "";//Y 服务器通知URL

        public string _platformUserNo = "";//Y 商户平台会员标识,会员在商户平台唯一标识
        public string _requestNo = "";// Y 请求流水号

        /// <summary>
        /// Y 商户平台会员标识,会员在商户平台唯一标识
        /// </summary>
        public string platformUserNo
        {
            get { return _platformUserNo; }
            set { _platformUserNo = value; }
        }
        /// <summary>
        /// Y 商户编号,商户在易宝唯一标识
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
        ///Y 服务名称，固定值
        /// </summary>
        public string service
        {
            get { return _service; }
            set { _service = value; }
        }
        /// <summary>
        /// Y 页面回跳URL,页面回跳URL
        /// </summary>
        public string callbackUrl
        {
            get { return _callbackUrl; }
            set { _callbackUrl = value; }
        }
        /// <summary>
        /// Y 服务器通知URL
        /// </summary>
        public string notifyUrl
        {
            get { return _notifyUrl; }
            set { _notifyUrl = value; }
        }
    }
}
