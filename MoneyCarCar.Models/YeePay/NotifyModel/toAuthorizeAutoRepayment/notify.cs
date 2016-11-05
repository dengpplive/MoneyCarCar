using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyCarCar.Models.YeePay.NotifyModel.toAuthorizeAutoRepayment
{
   public  class notify : BaseNotify
    {
        private string _orderNo = "";// 标的编号
        public string orderNo
        {
            get { return _orderNo; }
            set { _orderNo = value; }
        }
    }
}
