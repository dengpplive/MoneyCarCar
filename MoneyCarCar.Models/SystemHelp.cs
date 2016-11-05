using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace MoneyCarCar.Models
{
    //SystemHelp
    public class SystemHelp : BaseModel
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
        private string _askcontent;
        /// <summary>
        /// 提问内容
        /// </summary>
        public string AskContent
        {
            get { return _askcontent; }
            set { _askcontent = value; }
        }
        private string _replyconent;
        /// <summary>
        /// 回复内容
        /// </summary>	
        public string ReplyConent
        {
            get { return _replyconent; }
            set { _replyconent = value; }
        }
        private string _askaccount;
        /// <summary>
        /// 提问账户
        /// </summary>		
        public string AskAccount
        {
            get { return _askaccount; }
            set { _askaccount = value; }
        }
        private string _replyaccount;
        /// <summary>
        /// 回答人账户
        /// </summary>	
        public string ReplyAccount
        {
            get { return _replyaccount; }
            set { _replyaccount = value; }
        }
        private DateTime _askdate;
        /// <summary>
        /// 提问时间
        /// </summary>	
        public DateTime AskDate
        {
            get { return _askdate; }
            set { _askdate = value; }
        }
        private DateTime _replydate;
        /// <summary>
        /// 回答时间
        /// </summary>	
        public DateTime ReplyDate
        {
            get { return _replydate; }
            set { _replydate = value; }
        }
        private int _helptype;
        /// <summary>
        /// 问题类型默认0 暂未定义
        /// </summary>	
        public int HelpType
        {
            get { return _helptype; }
            set { _helptype = value; }
        }

    }
}

