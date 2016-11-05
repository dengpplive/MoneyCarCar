using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace MoneyCarCar.Models
{
    //债权明细表
    public class SystemClaimsDetails : BaseModel
    {

        private int _id;
        /// <summary>
        /// 债权明细表ID
        /// </summary>	
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }
        private int _claimsid = 0;
        /// <summary>
        /// 债权编号
        /// </summary>	
        public int ClaimsID
        {
            get { return _claimsid; }
            set { _claimsid = value; }
        }
        private int _investorsid = 0;
        /// <summary>
        /// 投资人用户ID
        /// </summary>		
        public int InvestorsID
        {
            get { return _investorsid; }
            set { _investorsid = value; }
        }
        private string _investorscellphone = "";
        /// <summary>
        /// 投资人手机号码
        /// </summary>	
        public string InvestorsCellPhone
        {
            get { return _investorscellphone; }
            set { _investorscellphone = value; }
        }
        private decimal _investormoney = 0;
        /// <summary>
        /// 投资金额
        /// </summary>	
        public decimal InvestorMoney
        {
            get { return _investormoney; }
            set { _investormoney = value; }
        }
        private string _investorstime = "";
        /// <summary>
        /// 投资时间
        /// </summary>	
        public string InvestorsTime
        {
            get { return _investorstime; }
            set { _investorstime = value; }
        }
        private decimal _dayearnings = 0;
        /// <summary>
        /// 预计每个结算单位的收益
        /// </summary>
        public decimal DayEarnings
        {
            get { return _dayearnings; }
            set { _dayearnings = value; }
        }
        private decimal _expireearnings = 0;
        /// <summary>
        /// 到期收益
        /// </summary>
        public decimal ExpireEarnings
        {
            get { return _expireearnings; }
            set { _expireearnings = value; }
        }
        private int _paystatus = 0;
        /// <summary>
        /// 认购状态(0:默认值,1:认购中，2:认购完成,3:认购失败-过期)
        /// </summary>		
        public int PayStatus
        {
            get { return _paystatus; }
            set { _paystatus = value; }
        }

        /// <summary>
        /// 支付说明
        /// </summary>		
        private string _paymark="";
        public string PayMark
        {
            get { return _paymark; }
            set { _paymark = value; }
        }

        private int _principalClearState = 0;
        /// <summary>
        /// 本金结算0:无状态,1:未结算,2:结算中,3:已结算
        /// </summary>
        public int PrincipalClearState
        {
            get { return _principalClearState; }
            set { _principalClearState = value; }
        }

    }
}

