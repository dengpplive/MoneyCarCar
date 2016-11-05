using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace MoneyCarCar.Models
{
    //字典表
    public class SystemDictionary : BaseModel
    {

        /// <summary>
        /// 系统信息字典表
        /// </summary>		
        private int _id;
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        /// 字典表Key
        /// </summary>		
        private string _dickey;
        public string DicKey
        {
            get { return _dickey; }
            set { _dickey = value; }
        }
        /// <summary>
        /// 字典描述
        /// </summary>		
        private string _dicvalue;
        public string DicValue
        {
            get { return _dicvalue; }
            set { _dicvalue = value; }
        }
        /// <summary>
        /// 字典类型
        /// </summary>		
        private string _dictype;
        public string DicType
        {
            get { return _dictype; }
            set { _dictype = value; }
        }

    }
}

