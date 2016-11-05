using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyCarCar.Models.ModelDto.RQParam
{
    /// <summary>
    /// 需要更新的数据字段
    /// </summary>
    public class RQUpdate<T>
    {
        private List<string> _UpdateFileds = new List<string>();
        /// <summary>
        /// 更新的字段名列表
        /// </summary>
        public List<string> UpdateFileds
        {
            get
            {
                return _UpdateFileds;
            }
            set
            {
                _UpdateFileds = value;
            }
        }
        /// <summary>
        /// 更新数据实体
        /// </summary>
        public T Tag;
    }
}
