using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace MoneyCarCar.Models
{
    //SystemNotice
    public class SystemNotice : BaseModel
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
        private string _noticetitle;
        /// <summary>
        /// 公告标题
        /// </summary>	
        public string NoticeTitle
        {
            get { return _noticetitle; }
            set { _noticetitle = value; }
        }
        private string _noticecontent;
        /// <summary>
        /// 公告内容
        /// </summary>	
        public string NoticeContent
        {
            get { return _noticecontent; }
            set { _noticecontent = value; }
        }
        private int _noticetype;
        /// <summary>
        /// 公告类型默认0 暂未定义
        /// </summary>		
        public int NoticeType
        {
            get { return _noticetype; }
            set { _noticetype = value; }
        }
        private int _noticestatus;
        /// <summary>
        /// 公告状态默认 0未审核 1已审核
        /// </summary>		
        public int NoticeStatus
        {
            get { return _noticestatus; }
            set { _noticestatus = value; }
        }
        private DateTime _noticeadddate = DateTime.Parse("1900-01-01");
        /// <summary>
        /// NoticeAddDate
        /// </summary>	
        public DateTime NoticeAddDate
        {
            get { return _noticeadddate; }
            set { _noticeadddate = value; }
        }
        private string _noticerealseaccount;
        /// <summary>
        /// 发布公告的账号
        /// </summary>		
        public string NoticeRealseAccount
        {
            get { return _noticerealseaccount; }
            set { _noticerealseaccount = value; }
        }
    }
}

