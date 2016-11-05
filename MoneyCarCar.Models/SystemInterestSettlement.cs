using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using MoneyCarCar.Commons;
namespace MoneyCarCar.Models
{
    //SystemInterestSettlement
    public class SystemInterestSettlement
    {

        /// <summary>
        /// 结息id
        /// </summary>		
        private int _id;
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        /// 用户id
        /// </summary>		
        private int _userid = 0;
        public int UserId
        {
            get { return _userid; }
            set { _userid = value; }
        }
        /// <summary>
        /// 债权id
        /// </summary>		
        private int _claimsid = 0;
        public int ClaimsId
        {
            get { return _claimsid; }
            set { _claimsid = value; }
        }
        /// <summary>
        /// 获息日期
        /// </summary>		
        private DateTime _getinterestdate = "1900-01-01 00:00:00".ToDateTime();
        public DateTime GetInterestDate
        {
            get { return _getinterestdate; }
            set { _getinterestdate = value; }
        }
        /// <summary>
        /// 结算日期
        /// </summary>		
        private DateTime _balancedate = "1900-01-01 00:00:00".ToDateTime();
        public DateTime BalanceDate
        {
            get { return _balancedate; }
            set { _balancedate = value; }
        }
        /// <summary>
        /// 结算金额
        /// </summary>		
        private decimal _balancemoney = 0;
        public decimal BalanceMoney
        {
            get { return _balancemoney; }
            set { _balancemoney = value; }
        }
        /// <summary>
        /// 结息状态(1:未接到通知,2:接到成功通知,3:接到失败通知)
        /// </summary>		
        private int _balancestatus = 0;
        public int BalanceStatus
        {
            get { return _balancestatus; }
            set { _balancestatus = value; }
        }
        /// <summary>
        /// 结息类型(1:正常债权,2:虚拟本金奖励,3:利息奖励,)
        /// </summary>		
        private int _balancetype = 0;
        public int BalanceType
        {
            get { return _balancetype; }
            set { _balancetype = value; }
        }

    }
}

