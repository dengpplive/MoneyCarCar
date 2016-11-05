using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using MoneyCarCar.Website.Controllers.CommHelper;
using MoneyCarCar.Models.YeePay.YeePayEnum;

namespace MoneyCarCar.Models.YeePay.RequestModel
{
    /// <summary>
    /// 2.7 转账授权:接口输入
    /// </summary>
    public class ToCpTransaction : YeePayConfig
    {
        public ToCpTransaction()
        {
            _userType = EnumUserType.MEMBER.ToEnumDesc(); //  出款人用户类型，目前只支持传入： MEMBER 个人会员

            #region 过期时间处理

            try
            {
                XmlDocument xmldoc = new XmlDocument();
                string path = AppDomain.CurrentDomain.BaseDirectory + "\\YeePayConfig.xml";
                xmldoc.Load(path);
                XElement xmlRoot = XElement.Parse(xmldoc.InnerXml);

                foreach (XElement xe in xmlRoot.Elements("expired"))
                {
                    if (xe.Element("expiredTime") != null && !string.IsNullOrEmpty(xe.Element("expiredTime").Value))
                        _expired = DateTime.Now.AddMinutes(int.Parse(xe.Element("expiredTime").Value.Trim())).ToString("yyyy-MM-dd HH:mm:ss");
                    // DateTime.Now.AddMinutes(65).ToString(1);
                }
            }
            catch (Exception)
            {

            }

            #endregion
        }
        /// <summary>
        ///  Y 超过此时间即不允许提交订单
        /// </summary>
        public string _expired { get; set; }
        /// <summary>
        /// Y 资金明细记录
        /// </summary>
        public List<ToCpTransactionDetail> details { get; set; }
    }
}
