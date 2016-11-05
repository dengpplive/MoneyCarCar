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
    /// 2.1 注册：接口输入
    /// </summary>
    public class ToRegister : YeePayConfig
    {
        public ToRegister() 
        {
            _idCardType = EnumIdCardType.G2_IDCARD.ToEnumDesc(); //  出款人用户类型，目前只支持传入： MEMBER 个人会员
        }
        /// <summary>
        /// N 昵称,交易查询时希望显示的会员名称，若不写则与会员标识一致
        /// </summary>
        public string nickName{get;set;}
        /// <summary>
        /// Y 会员真实姓名,会员真实姓名
        /// </summary>
        public string realName{get;set;}
        /// <summary>
        /// Y 身份证号,会员身份证号
        /// </summary>
        public string idCardNo{ get; set; }
        /// <summary>
        /// Y 手机号,接收短信验证码的手机号
        /// </summary>
        public string mobile{ get; set; }
        /// <summary>
        /// Y 邮箱，邮箱
        /// </summary>
        public string email{ get; set; }
        /// <summary>
        /// N 身份证类型,【见身份证类型】 //  1 代身份证：G1_IDCARD、2 代身份证：G2_IDCARD
        /// </summary>
        public string _idCardType { get; set; }
    }
}
