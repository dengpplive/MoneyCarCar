using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace MoneyCarCar.Models
{
    //请求记录表
    public class SystemRequestRecord
    {

        private int _id;
        /// <summary>
        /// 请求流水号
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
        private int _bussnessid = 0;
        /// <summary>
        /// 业务id(转账用明细id,其他类型操作依据相应的业务id)
        /// </summary>
        public int BussnessId
        {
            get { return _bussnessid; }
            set { _bussnessid = value; }
        }
        private decimal _requestmoney = 0;
        /// <summary>
        /// 请求金额(注册等直接为空即可)
        /// </summary>
        public decimal RequestMoney
        {
            get { return _requestmoney; }
            set { _requestmoney = value; }
        }
        private int _requesttype = 0;
        /// <summary>
        /// 请求类型(1:,注册2:充值,3:投资,4:提现,5:查询,6:绑卡,7:解绑,8:结息,9:返还本金)
        /// </summary>
        public int RequestType
        {
            get { return _requesttype; }
            set { _requesttype = value; }
        }
        private DateTime _requestdate = DateTime.Parse("1900-01-01 00:00:00");
        /// <summary>
        /// 请求日期
        /// </summary>
        public DateTime RequestDate
        {
            get { return _requestdate; }
            set { _requestdate = value; }
        }
        private int _requestoperstatus = 0;
        /// <summary>
        /// 请求处理状态(1:处理中,2:已处理,3:已过期)
        /// </summary>
        public int RequestOperStatus
        {
            get { return _requestoperstatus; }
            set { _requestoperstatus = value; }
        }
        private string _requestmark = "";
        /// <summary>
        /// 备注
        /// </summary>
        public string RequestMark
        {
            get { return _requestmark; }
            set { _requestmark = value; }
        }
    }
    public enum RequestOperStatus { 处理中 = 1, 已处理 = 2, 已过期 = 3 }
}

