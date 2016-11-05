using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace MoneyCarCar.Models
{
    //SystemMoenyDetail
    public class SystemMoenyDetail : BaseModel
    {

        private int _id;
        /// <summary>
        /// Id
        /// </summary>	
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        private int _userid = 0;
        /// <summary>
        /// 用户Id
        /// </summary>	
        public int UserId
        {
            get { return _userid; }
            set { _userid = value; }
        }
        private string _username = "";
        /// <summary>
        /// 用户名
        /// </summary>	
        public string UserName
        {
            get { return _username; }
            set { _username = value; }
        }
        private decimal _paymoney = 0;
        /// <summary>
        /// 交易金额
        /// </summary>	
        public decimal PayMoney
        {
            get { return _paymoney; }
            set { _paymoney = value; }
        }
        private decimal _preremainmoney = 0;
        /// <summary>
        /// 交易前金额
        /// </summary>	
        public decimal PreRemainMoney
        {
            get { return _preremainmoney; }
            set { _preremainmoney = value; }
        }
        private decimal _remainmoney = 0;
        /// <summary>
        /// 交易后余额
        /// </summary>	
        public decimal RemainMoney
        {
            get { return _remainmoney; }
            set { _remainmoney = value; }
        }
        private int _paytype = 0;
        /// <summary>
        /// 交易类型: (  1 充值、 2  提现、 3  冻结、 4 投资、 5  积分奖励 、6 结息 、7 返还本金、8 借款 )
        /// </summary>
        public int PayType
        {
            get { return _paytype; }
            set { _paytype = value; }
        }
        private string _remark = "";
        /// <summary>
        /// 备注说明
        /// </summary>		
        public string Remark
        {
            get { return _remark; }
            set { _remark = value; }
        }
        private string _paytime = "1900-01-01 00:00:00";
        /// <summary>
        /// 交易时间
        /// </summary>	
        public string PayTime
        {
            get { return _paytime; }
            set { _paytime = value; }
        }
        private string _inpayno = "";
        /// <summary>
        /// 内容流水号
        /// </summary>	
        public string InPayNo
        {
            get { return _inpayno; }
            set { _inpayno = value; }
        }
        private string _payno = "";
        /// <summary>
        /// 交易号
        /// </summary>	
        public string PayNo
        {
            get { return _payno; }
            set { _payno = value; }
        }

    }

    public enum PayType { 充值 = 1, 提现 = 2, 冻结 = 3, 投资 = 4, 积分奖励 = 5, 结息 = 6, 返还本金 = 7, 借款 = 8 }

}

