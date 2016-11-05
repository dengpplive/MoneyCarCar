using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyCarCar.Models.ModelDto.RQParam
{
    public class RQUserListDto
    {
        private List<UserRow> _UserList = new List<UserRow>();
        /// <summary>
        /// 注册投资用户列表
        /// </summary>
        public List<UserRow> UserList
        {
            get;
            set;
        }
    }
    public class UserRow
    {
        /// <summary>
        /// id
        /// </summary>
        public int Id
        {
            get;
            set;
        }
        /// <summary>
        /// 用户账号
        /// </summary>
        public string UserName
        {
            get;
            set;
        }
        /// <summary>
        /// 姓名
        /// </summary>
        public string RealName
        {
            get;
            set;
        }
    }
}
