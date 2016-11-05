using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace MoneyCarCar.Models
{
    //用户信息表
    public class SystemUsers : BaseModel
    {
        private int _id;
        public int ID
        /// <summary>
        /// 用户ID
        /// </summary>
        {
            get { return _id; }
            set { _id = value; }
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
        private string _userpassword = "";
        /// <summary>
        /// 用户密码
        /// </summary>	
        public string UserPassword
        {
            get { return _userpassword; }
            set { _userpassword = value; }
        }
        private string _paypassword = "";
        /// <summary>
        /// 资金密码
        /// </summary>	
        public string PayPassword
        {
            get { return _paypassword; }
            set { _paypassword = value; }
        }
        private string _cellphone = "";
        /// <summary>
        /// 手机号码
        /// </summary>
        public string CellPhone
        {
            get { return _cellphone; }
            set { _cellphone = value; }
        }
        private bool _cellpahoneisauthenticate = false;
        /// <summary>
        /// 手机号码是否认证
        /// </summary>		
        public bool CellPahoneIsAuthenticate
        {
            get { return _cellpahoneisauthenticate; }
            set { _cellpahoneisauthenticate = value; }
        }
        private string _realname = "";
        /// <summary>
        /// 用户姓名
        /// </summary>	
        public string RealName
        {
            get { return _realname; }
            set { _realname = value; }
        }
        private string _idnumber = "";
        /// <summary>
        /// 身份证号码
        /// </summary>	
        public string IDNumber
        {
            get { return _idnumber; }
            set { _idnumber = value; }
        }
        private bool _idnumberisauthenticate = false;
        /// <summary>
        /// 身份证是否认证成功
        /// </summary>		
        public bool IDNumberIsAuthenticate
        {
            get { return _idnumberisauthenticate; }
            set { _idnumberisauthenticate = value; }
        }
        private string _useraddress = "";
        /// <summary>
        /// 联系地址
        /// </summary>	
        public string UserAddress
        {
            get { return _useraddress; }
            set { _useraddress = value; }
        }
        private string _useremail = "";
        /// <summary>
        /// 联系电子邮箱
        /// </summary>		
        public string UserEmail
        {
            get { return _useremail; }
            set { _useremail = value; }
        }
        private int _usertype = 0;
        /// <summary>
        /// 用户类型(1:投资借贷人,2:管理员,3:业务员)
        /// </summary>		
        public int UserType
        {
            get { return _usertype; }
            set { _usertype = value; }
        }
        private int _userstate = 0;
        /// <summary>
        /// 用户状态(1:启用,2:禁用)
        /// </summary>	
        public int UserState
        {
            get { return _userstate; }
            set { _userstate = value; }
        }
        private int _isenterprise = 0;
        /// <summary>
        /// 是否企业(1:个人,2:企业)
        /// </summary>	
        public int IsEnterprise
        {
            get { return _isenterprise; }
            set { _isenterprise = value; }
        }
        private decimal _balance = 0;
        /// <summary>
        /// 账户余额（不含冻结金额，总金额为余额+冻结）
        /// </summary>
        public decimal Balance
        {
            get { return _balance; }
            set { _balance = value; }
        }
        private decimal _freeze = 0;
        /// <summary>
        /// 冻结金额
        /// </summary>	
        public decimal Freeze
        {
            get { return _freeze; }
            set { _freeze = value; }
        }
        private string _regtime = "1900-01-01 00:00:00";
        /// <summary>
        /// 注册日期
        /// </summary>
        public string RegTime
        {
            get { return _regtime; }
            set { _regtime = value; }
        }

        private string _recommended = "";
        /// <summary>
        /// 推荐人手机号,可以不填
        /// </summary>
        public string Recommended
        {
            get { return _recommended; }
            set { _recommended = value; }
        }
    }
}

