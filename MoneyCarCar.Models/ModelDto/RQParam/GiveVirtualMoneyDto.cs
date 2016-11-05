using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyCarCar.Models.ModelDto.RQParam
{
    public class GiveVirtualMoneyDto
    {
        private string _Ids = string.Empty;

        public string Ids
        {
            get { return _Ids; }
            set { _Ids = value; }
        }

        private int _IsAllUser = 0;

        public int IsAllUser
        {
            get { return _IsAllUser; }
            set { _IsAllUser = value; }
        }

        private int _GiveMoney = 0;

        public int GiveMoney
        {
            get { return _GiveMoney; }
            set { _GiveMoney = value; }
        }


        private int _bountyres = 0;
        /// <summary>
        /// 奖励来源(1，注册.2.推荐.3，活动赠送)
        /// </summary>		
        public int BountyRes
        {
            get { return _bountyres; }
            set { _bountyres = value; }
        }

        private int _bountytype = 0;
        /// <summary>
        /// 奖励类型(1:虚拟本金,2:利息奖励,3:现金奖励)
        /// </summary>		
        public int BountyType
        {
            get { return _bountytype; }
            set { _bountytype = value; }
        }

        private DateTime _OverTime = DateTime.Parse("1900-01-01 00:00:00");

        public DateTime OverTime
        {
            get { return _OverTime; }
            set { _OverTime = value; }
        }
        private int _OperatorUserId = 0;

        public int OperatorUserId
        {
            get { return _OperatorUserId; }
            set { _OperatorUserId = value; }
        }

        private string _OperatorUserName = string.Empty;

        public string OperatorUserName
        {
            get { return _OperatorUserName; }
            set { _OperatorUserName = value; }
        }
        private string _IP = string.Empty;

        public string IP
        {
            get { return _IP; }
            set { _IP = value; }
        }
    }
}
