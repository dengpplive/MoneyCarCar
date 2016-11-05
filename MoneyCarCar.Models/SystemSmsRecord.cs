using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyCarCar.Models
{
    /// <summary>
    /// 发送短信记录
    /// </summary>
    public class SystemSmsRecord
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
        private string _SendMobile = string.Empty;
        /// <summary>
        /// 发送手机号
        /// </summary>
        public string SendMobile
        {
            get { return _SendMobile; }
            set { _SendMobile = value; }
        }
        private string _AcceptMobile = string.Empty;
        /// <summary>
        /// 接收手机号
        /// </summary>
        public string AcceptMobile
        {
            get { return _AcceptMobile; }
            set { _AcceptMobile = value; }
        }
        private DateTime _SendTime = DateTime.Parse("1900-01-01");
        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime SendTime
        {
            get { return _SendTime; }
            set { _SendTime = value; }
        }
        private string _SendAccount = string.Empty;
        /// <summary>
        /// 发送账号
        /// </summary>
        public string SendAccount
        {
            get { return _SendAccount; }
            set { _SendAccount = value; }
        }
        private string _SendContent = string.Empty;
        /// <summary>
        /// 发送内容
        /// </summary>
        public string SendContent
        {
            get { return _SendContent; }
            set { _SendContent = value; }
        }
        private DateTime _AddTime = DateTime.Parse("1900-01-01");
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime AddTime
        {
            get { return _AddTime; }
            set { _AddTime = value; }
        }
    }
}
