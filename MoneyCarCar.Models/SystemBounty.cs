using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace MoneyCarCar.Models
{
    //SystemBounty
    public class SystemBounty
    {

        /// <summary>
        /// 奖励id
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
        /// 用户姓名
        /// </summary>		
        private string _username="";
        public string UserName
        {
            get { return _username; }
            set { _username = value; }
        }
        /// <summary>
        /// 奖励类型(1:虚拟本金,2:利息奖励,3:现金奖励)
        /// </summary>		
        private int _bountytype=0;
        public int BountyType
        {
            get { return _bountytype; }
            set { _bountytype = value; }
        }
        /// <summary>
        /// 奖励值
        /// </summary>		
        private int _integral=0;
        public int Integral
        {
            get { return _integral; }
            set { _integral = value; }
        }
        /// <summary>
        /// 操作用户名
        /// </summary>		
        private string _opername="";
        public string operName
        {
            get { return _opername; }
            set { _opername = value; }
        }
        /// <summary>
        /// 操作时间
        /// </summary>		
        private string _opertime="";
        public string operTime
        {
            get { return _opertime; }
            set { _opertime = value; }
        }
        /// <summary>
        /// 债券id
        /// </summary>		
        private int _claimsid=0;
        public int ClaimsId
        {
            get { return _claimsid; }
            set { _claimsid = value; }
        }
        /// <summary>
        /// 使用时间
        /// </summary>		
        private DateTime _usetime = Convert.ToDateTime("1900-01-01 00:00:00");
        public DateTime UseTime
        {
            get { return _usetime; }
            set { _usetime = value; }
        }
        /// <summary>
        /// 使用状态
        /// </summary>		
        private bool _usetype=false;
        public bool UseType
        {
            get { return _usetype; }
            set { _usetype = value; }
        }
        /// <summary>
        /// 奖励来源(1，注册.2.推荐.3，活动赠送)
        /// </summary>		
        private int _bountyres=0;
        public int BountyRes
        {
            get { return _bountyres; }
            set { _bountyres = value; }
        }
        /// <summary>
        /// 过期日期
        /// </summary>		
        private DateTime _overtime = Convert.ToDateTime("1900-01-01 00:00:00");
        public DateTime OverTime
        {
            get { return _overtime; }
            set { _overtime = value; }
        }

    }
}

