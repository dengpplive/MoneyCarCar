using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoneyCarCar.Website.Controllers.CommHelper;
using MoneyCarCar.Models.YeePay.YeePayEnum;

namespace MoneyCarCar.Models.YeePay.RequestModel
{
    /// <summary>
    /// 2.6 企业用户注册:接口输入
    /// </summary>
    public class ToEnterpriseRegister : YeePayConfig
    {
        public ToEnterpriseRegister()
        {
            _memberClassType = EnumMemberType.ENTERPRISE.ToEnumDesc();
        }

        /// <summary>
        ///  Y 企业名称，企业名称
        /// </summary>
        public string enterpriseName { get; set; }
        /// <summary>
        ///  Y 开户银行许可证
        /// </summary>
        public string bankLicense { get; set; }
        /// <summary>
        /// Y 组织机构代码
        /// </summary>
        public string orgNo { get; set; }
        /// <summary>
        /// Y 营业执照编号
        /// </summary>
        public string businessLicense { get; set; } 
        /// <summary>
        /// Y 税务登记号
        /// </summary>
        public string taxNo { get; set; }
        /// <summary>
        /// Y 法人姓名
        /// </summary>
        public string legal { get; set; }
        /// <summary>
        /// Y 法人身份证号
        /// </summary>
        public string legalIdNo { get; set; } 
        /// <summary>
        /// Y 企业联系人
        /// </summary>
        public string contact { get; set; }
        /// <summary>
        /// Y 联系人手机号
        /// </summary>
        public string contactPhone { get; set; } 
        /// <summary>
        /// Y 联系人邮箱
        /// </summary>
        public string email { get; set; }
        /// <summary>
        /// Y 会员类型, ENTERPRISE：企业借款人 、 GUARANTEE_CORP：担保公司
        /// </summary>
        public string _memberClassType{ get; set; }

    }
}
