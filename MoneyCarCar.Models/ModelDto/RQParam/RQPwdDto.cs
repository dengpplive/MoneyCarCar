using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyCarCar.Models.ModelDto.RQParam
{
    public class RQPwdDto
    {
        public int UserId { get; set; }
        public string OriPwd { get; set; }
        public string NewPwd { get; set; }
    }
    public class RQUserStateDto
    {
        public int UserId { get; set; }
        /// <summary>
        /// 0禁用 1启用
        /// </summary>
        public bool UserState { get; set; }
    }
}
