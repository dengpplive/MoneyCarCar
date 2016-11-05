using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyCarCar.Models.DtoModels
{
    /// <summary>
    /// POST请求的分页参数数据
    /// </summary>
    public class RQPagerDto
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        private string _Where = " 1=1 ";
        public string Where
        {
            get { return _Where; }
            set { _Where = value; }
        }
        private string _QueryFileds = "*";
        public string QueryFileds
        {
            get { return _QueryFileds; }
            set { _QueryFileds = value; }
        }

        private string _OrderBy = "  Id desc ";
        public string OrderBy
        {
            get { return _OrderBy; }
            set { _OrderBy = value; }
        }
    }
}
