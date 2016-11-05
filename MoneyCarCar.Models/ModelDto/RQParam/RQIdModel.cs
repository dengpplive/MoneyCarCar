using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyCarCar.Models.ModelDto.RQParam
{
    /// <summary>
    /// 用于传递数组对象的类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RQIdModel<T>
    {
        private List<T> _IdList = new List<T>();
        public List<T> IdList
        {
            get
            {
                return _IdList;
            }
            set
            {
                _IdList = value;
            }
        }
    }
}
