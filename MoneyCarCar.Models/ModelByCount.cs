using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyCarCar.Models
{
    public class ModelByCount<T>
    {
        private int _allCount = 0;
        private int _pageSize = 10;

        public int PageIndex { get; set; }

        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = value;
            }
        }

        /// <summary>
        /// 总页码
        /// </summary>
        public int PageCount
        {
            get
            {
                return (AllCount % PageSize == 0) ? AllCount / PageSize : AllCount / PageSize + 1;
            }
        }

        /// <summary>
        /// 总的数据条数
        /// </summary>	
        public int AllCount
        {
            get { return _allCount; }
            set { _allCount = value; }
        }

        private List<T> _listAll = new List<T>();
        /// <summary>
        /// 对应的集合数据
        /// </summary>	
        public List<T> ListAll
        {
            get { return _listAll; }
            set { _listAll = value; }
        }
    }
}
