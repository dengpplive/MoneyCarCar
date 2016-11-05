using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace MoneyCarCar.Models
{
    //SystemNews
    public class SystemNews : BaseModel
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
        private string _newstitle;
        /// <summary>
        /// 新闻标题
        /// </summary>		
        public string NewsTitle
        {
            get { return _newstitle; }
            set { _newstitle = value; }
        }
        private string _newscontent;
        /// <summary>
        /// 新闻内容
        /// </summary>	
        public string NewsContent
        {
            get { return _newscontent; }
            set { _newscontent = value; }
        }
        private int _userid;
        /// <summary>
        /// 发布人Id
        /// </summary>	
        public int UserId
        {
            get { return _userid; }
            set { _userid = value; }
        }
        private string _username;
        /// <summary>
        /// 发布新闻的发布人姓名
        /// </summary>		
        public string UserName
        {
            get { return _username; }
            set { _username = value; }
        }
        private int _newsstatus;
        /// <summary>
        /// 新闻状态 0未生效 1已生效 
        /// </summary>		
        public int NewsStatus
        {
            get { return _newsstatus; }
            set { _newsstatus = value; }
        }
        private DateTime _newsrealsetime = DateTime.Parse("1900-01-01");
        /// <summary>
        /// 新闻发布的时间
        /// </summary>		
        public DateTime NewsRealseTime
        {
            get { return _newsrealsetime; }
            set { _newsrealsetime = value; }
        }

    }
}

