using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace MoneyCarCar.Models
{
    //SystemBorrowerApply
    public class SystemBorrowerApply : BaseModel
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

        private int _borrowerid = 0;
        /// <summary>
        /// 借款人用户名id
        /// </summary>	
        public int BorrowerID
        {
            get { return _borrowerid; }
            set { _borrowerid = value; }
        }

        private string _borrowername = "";
        /// <summary>
        /// 借款人姓名
        /// </summary>	
        public string BorrowerName
        {
            get { return _borrowername; }
            set { _borrowername = value; }
        }

        private decimal _loanamount = 0;
        /// <summary>
        /// 借款金额
        /// </summary>	
        public decimal LoanAmount
        {
            get { return _loanamount; }
            set { _loanamount = value; }
        }

        private string _borrowerreason = "";
        /// <summary>
        /// 借款原因
        /// </summary>	
        public string BorrowerReason
        {
            get { return _borrowerreason; }
            set { _borrowerreason = value; }
        }

        private string _collateraldesc = "";
        /// <summary>
        /// 抵押物描述
        /// </summary>	
        public string CollateralDesc
        {
            get { return _collateraldesc; }
            set { _collateraldesc = value; }
        }

        private string _borrowertime = "1900-01-01 00:00:00";
        /// <summary>
        /// 申请时间
        /// </summary>	
        public string BorrowerTime
        {
            get { return _borrowertime; }
            set { _borrowertime = value; }
        }

        private int _borrowertype = 0;
        /// <summary>
        /// 审核状态(1未审核,2正在审核,3未通过,4已通过)
        /// </summary>	
        public int BorrowerType
        {
            get { return _borrowertype; }
            set { _borrowertype = value; }
        }

    }
}

