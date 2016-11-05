using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace MoneyCarCar.Models
{
    //SystemLog
    public class SystemLog : BaseModel
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
        /// 操作用户id
        /// </summary>		
        private int _operatoruserid=0;
        public int OperatorUserId
        {
            get { return _operatoruserid; }
            set { _operatoruserid = value; }
        }
        /// <summary>
        /// 操作用户名
        /// </summary>		
        private string _operatorusername="";
        public string OperatorUserName
        {
            get { return _operatorusername; }
            set { _operatorusername = value; }
        }
        /// <summary>
        /// 操作类型 : (1:增加,2:修改,3:删除,4:其他）
        /// </summary>		
        private int _operatortype=0;
        public int OperatorType
        {
            get { return _operatortype; }
            set { _operatortype = value; }
        }
        /// <summary>
        /// 业务类型
        /// </summary>		
        private string _businesstype="";
        public string BusinessType
        {
            get { return _businesstype; }
            set { _businesstype = value; }
        }
        /// <summary>
        /// 操作时间
        /// </summary>		
        private string _operatortime = "1900-01-01 00:00:00";
        public string OperatorTime
        {
            get { return _operatortime; }
            set { _operatortime = value; }
        }
        /// <summary>
        /// OperatorContent
        /// </summary>		
        private string _operatorcontent="";
        public string OperatorContent
        {
            get { return _operatorcontent; }
            set { _operatorcontent = value; }
        }
        /// <summary>
        /// 操作IP
        /// </summary>		
        private string _operatorip="";
        public string OperatorIP
        {
            get { return _operatorip; }
            set { _operatorip = value; }
        }

    }
}

