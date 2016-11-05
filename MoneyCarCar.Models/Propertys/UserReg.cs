using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyCarCar.Models.Propertys
{
    public class UserReg
    {
        public UserReg()
        {
            UserTpye = 1;
        }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 用户密码
        /// </summary>
        public string UserPwd { get; set; }
        /// <summary>
        /// 用户重复密码
        /// </summary>
        public string UserPwdRe { get; set; }
        /// <summary>
        /// Email地址
        /// </summary>
        public string UserEmail { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string UserPhone { get; set; }
        /// <summary>
        /// 验证码
        /// </summary>
        public string Vcode { get; set; }
        /// <summary>
        /// 手机验证码
        /// </summary>
        public string PhoneVcode { get; set; }
        /// <summary>
        /// 用户类型，默认为1:前台账户
        /// </summary>
        public int UserTpye { get; set; }
        /// <summary>
        /// 推荐人手机号
        /// </summary>
        public string Recommended { get; set; }
    }
}
