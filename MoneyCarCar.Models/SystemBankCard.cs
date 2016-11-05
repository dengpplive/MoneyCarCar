using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace MoneyCarCar.Models
{
    //SystemBankCard
    public class SystemBankCard : BaseModel
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
        /// 用户id
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
        private string _bankcardnumber = "";
        /// <summary>
        /// 银行卡卡号
        /// </summary>		
        public string BankCardNumber
        {
            get { return _bankcardnumber; }
            set { _bankcardnumber = value; }
        }
        private string _openanaccountbankcard = "";
        /// <summary>
        /// 开户银行
        /// </summary>
        public string OpenAnAccountBankCard
        {
            get { return _openanaccountbankcard; }
            set { _openanaccountbankcard = value; }
        }
        private string _openanaccountadd = "";
        /// <summary>
        /// 开户地址
        /// </summary>	
        public string OpenAnAccountAdd
        {
            get { return _openanaccountadd; }
            set { _openanaccountadd = value; }
        }
        private string _openanaccountuser = "";
        /// <summary>
        /// 开户人
        /// </summary>	
        public string OpenAnAccountUser
        {
            get { return _openanaccountuser; }
            set { _openanaccountuser = value; }
        }
        private string _addtime = "1900-01-01 00:00:00";
        /// <summary>
        /// 添加时间
        /// </summary>	
        public string AddTime
        {
            get { return _addtime; }
            set { _addtime = value; }
        }
        private bool _isdefault = false;
        /// <summary>
        /// 是否默认 银行卡
        /// </summary>		
        public bool IsDefault
        {
            get { return _isdefault; }
            set { _isdefault = value; }
        }
    }
}

