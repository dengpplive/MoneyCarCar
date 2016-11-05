using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace MoneyCarCar.Models
{
    //SystemFeedback
    public class SystemFeedback : BaseModel
    {

        /// <summary>
        /// Id
        /// </summary>		
        private int _id;
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        /// 意见反馈的用户Id
        /// </summary>		
        private int _userid;
        public int UserId
        {
            get { return _userid; }
            set { _userid = value; }
        }
        /// <summary>
        /// 意见反馈的用户名
        /// </summary>		
        private string _username;
        public string UserName
        {
            get { return _username; }
            set { _username = value; }
        }
        /// <summary>
        /// 反馈内容
        /// </summary>		
        private string _feedbackcontet;
        public string FeedbackContet
        {
            get { return _feedbackcontet; }
            set { _feedbackcontet = value; }
        }
        /// <summary>
        /// 反馈时间
        /// </summary>		
        private DateTime _feedbacktime;
        public DateTime FeedbackTime
        {
            get { return _feedbacktime; }
            set { _feedbacktime = value; }
        }

    }
}

