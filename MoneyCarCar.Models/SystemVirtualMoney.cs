using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace MoneyCarCar.Models
{
    //SystemVirtualMoney
    public class SystemVirtualMoney
    {

        /// <summary>
        /// 虚拟本金购买id
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
        private int _userid=0;
        public int UserId
        {
            get { return _userid; }
            set { _userid = value; }
        }
        /// <summary>
        /// 债权id
        /// </summary>		
        private int _claimsid=0;
        public int ClaimsId
        {
            get { return _claimsid; }
            set { _claimsid = value; }
        }
        /// <summary>
        /// 虚拟本金金额
        /// </summary>		
        private decimal _virtualmoney=0;
        public decimal VirtualMoney
        {
            get { return _virtualmoney; }
            set { _virtualmoney = value; }
        }
        /// <summary>
        /// 购买日期
        /// </summary>		
        private string _buydate = "1900-01-01 00:00:00";
        public string BuyDate
        {
            get { return _buydate; }
            set { _buydate = value; }
        }

    }
}

