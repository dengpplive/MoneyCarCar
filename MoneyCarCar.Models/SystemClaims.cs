using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace MoneyCarCar.Models
{
    //债权表
    public class SystemClaims : BaseModel
    {

        private int _id;
        /// <summary>
        /// 债权表编号
        /// </summary>	
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _title = "";
        /// <summary>
        /// 标题
        /// </summary>		
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        private string _titleimagepath = "";
        /// <summary>
        /// 标题图片地址
        /// </summary>		
        public string TitleImagePath
        {
            get { return _titleimagepath; }
            set { _titleimagepath = value; }
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

        private decimal _apr = 0;
        /// <summary>
        /// 年利率(不带%)
        /// </summary>		
        public decimal APR
        {
            get { return _apr; }
            set { _apr = value; }
        }

        private int _loanperiod = 0;
        /// <summary>
        /// 借款期限
        /// </summary>		
        public int LoanPeriod
        {
            get { return _loanperiod; }
            set { _loanperiod = value; }
        }

        private decimal _singleamount = 0;
        /// <summary>
        /// 单份金额
        /// </summary>		
        public decimal SingleAmount
        {
            get { return _singleamount; }
            set { _singleamount = value; }
        }

        private decimal _alreadyamount = 0;
        /// <summary>
        /// 已投金额
        /// </summary>		
        public decimal AlreadyAmount
        {
            get { return _alreadyamount; }
            set { _alreadyamount = value; }
        }

        private string _guaranteeway = "";
        /// <summary>
        /// 担保方式
        /// </summary>	
        public string GuaranteeWay
        {
            get { return _guaranteeway; }
            set { _guaranteeway = value; }
        }

        private string _repaymentwat = "";
        /// <summary>
        /// 还款方式
        /// </summary>		
        public string RepaymentWat
        {
            get { return _repaymentwat; }
            set { _repaymentwat = value; }
        }

        private decimal _sellingprice = 0;
        /// <summary>
        /// 可售价格
        /// </summary>	
        public decimal SellingPrice
        {
            get { return _sellingprice; }
            set { _sellingprice = value; }
        }

        private string _investmentendtime = "1900-01-01 00:00:00";
        /// <summary>
        /// 投资结束日期
        /// </summary>		
        public string InvestmentEndTime
        {
            get { return _investmentendtime; }
            set { _investmentendtime = value; }
        }

        private string _earningsstarttime = "1900-01-01 00:00:00";
        /// <summary>
        /// 收益起始时间
        /// </summary>	
        public string EarningsStartTime
        {
            get { return _earningsstarttime; }
            set { _earningsstarttime = value; }
        }

       

        private string _pawnspec = "";
        /// <summary>
        /// 抵押物规格
        /// </summary>		
        public string PawnSpec
        {
            get { return _pawnspec; }
            set { _pawnspec = value; }
        }

        private string _publisher = "";
        /// <summary>
        /// 发布人用户名
        /// </summary>		
        public string Publisher
        {
            get { return _publisher; }
            set { _publisher = value; }
        }

        private string _publishtime = "1900-01-01 00:00:00";
        /// <summary>
        /// 发布时间
        /// </summary>	
        public string PublishTime
        {
            get { return _publishtime; }
            set { _publishtime = value; }
        }

        private bool _isapproved = false;
        /// <summary>
        /// 是否审核通过
        /// </summary>	
        public bool IsApproved
        {
            get { return _isapproved; }
            set { _isapproved = value; }
        }

        private string _approverusername = "";
        /// <summary>
        /// 审核人用户名
        /// </summary>		
        public string ApproverUserName
        {
            get { return _approverusername; }
            set { _approverusername = value; }
        }

        private string _approvertime = "1900-01-01 00:00:00";
        /// <summary>
        /// 审核时间
        /// </summary>	
        public string ApproverTime
        {
            get { return _approvertime; }
            set { _approvertime = value; }
        }
        private string _borrower = "";
        /// <summary>
        /// 借款方
        /// </summary>		
        public string Borrower
        {
            get { return _borrower; }
            set { _borrower = value; }
        }
        private int _borrowerid = 0;
        /// <summary>
        /// 借款人用户名ID
        /// </summary>	
        public int BorrowerID
        {
            get { return _borrowerid; }
            set { _borrowerid = value; }
        }

        private int _claimsApplayID;
        /// <summary>
        /// 债权申请表编号
        /// </summary>		
        public int ClaimsApplayID
        {
            get { return _claimsApplayID; }
            set { _claimsApplayID = value; }
        }

        private string _icons = "";
        /// <summary>
        /// 动态小图标
        /// </summary>		
        public string Icons
        {
            get { return _icons; }
            set { _icons = value; }
        }

        private string _detailsimages1 = "";
        /// <summary>
        /// DetailsImages1
        /// </summary>		
        public string DetailsImages1
        {
            get { return _detailsimages1; }
            set { _detailsimages1 = value; }
        }

        private string _detailsimages2 = "";
        /// <summary>
        /// DetailsImages2
        /// </summary>		
        public string DetailsImages2
        {
            get { return _detailsimages2; }
            set { _detailsimages2 = value; }
        }

        private string _detailsimages3 = "";
        /// <summary>
        /// DetailsImages3
        /// </summary>		
        public string DetailsImages3
        {
            get { return _detailsimages3; }
            set { _detailsimages3 = value; }
        }

        private string _detailsimages4 = "";
        /// <summary>
        /// DetailsImages4
        /// </summary>	
        public string DetailsImages4
        {
            get { return _detailsimages4; }
            set { _detailsimages4 = value; }
        }

        private string _detailsimages5 = "";
        /// <summary>
        /// DetailsImages5
        /// </summary>		
        public string DetailsImages5
        {
            get { return _detailsimages5; }
            set { _detailsimages5 = value; }
        }

      
    }
}

