using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyCarCar.Models.ModelDto.ResParam
{
    public class ClaimsDetailsItem
    {
        /// <summary>
        /// 用户
        /// </summary>
        public SystemUsers User { get; set; }

        /// <summary>
        /// 该用户的债权明细列表
        /// </summary>
        public List<SystemClaimsDetails> ClaimsDetailsList
        {
            get;
            set;
        }
    }


    /// <summary>
    /// 债权明细数据
    /// </summary>
    public class ClaimsDetailsDto
    {
        /// <summary>
        /// 投资债权的用户
        /// </summary>
        public List<ClaimsDetailsItem> UserInfoList
        {
            get;
            set;
        }
        /// <summary>
        /// 该条债权信息
        /// </summary>
        public SystemClaims ClaimsInfo
        {
            get;
            set;
        }
    }
}
