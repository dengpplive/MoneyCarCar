using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyCarCar.Models.Propertys
{
    public class UserLogin
    {
        public UserLogin()
        {
            UserType = 1;
        }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserNameOrPhone { get; set; }
        /// <summary>
        /// 用户密码
        /// </summary>
        public string UserPassword { get; set; }
        /// <summary>
        /// 用户IP
        /// </summary>
        public string UserIP { get; set; }
        /// <summary>
        /// 用户类型(默认为1：前台用户)
        /// </summary>
        public int UserType { get; set; }
        /// <summary>
        /// 登录验证码
        /// </summary>
        public string Vcode { get; set; }
    }
}
