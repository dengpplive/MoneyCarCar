using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyCarCar.Models.ModelDto.RQParam
{
    /// <summary>
    /// 调用存储过程参数
    /// </summary>
    public class RQProcParam
    {
        private string _ProcName = string.Empty;
        /// <summary>
        /// 存储过程名称
        /// </summary>
        public string ProcName
        {
            get
            {
                return _ProcName;
            }
            set
            {
                _ProcName = value;
            }
        }

        private Dictionary<string, Object> _DicParam = new Dictionary<string, Object>();
        /// <summary>
        /// 普通参数列表
        /// </summary>
        public Dictionary<string, Object> DicParam
        {
            get
            {
                return _DicParam;
            }
            set
            {
                _DicParam = value;
            }
        }
        private Dictionary<string, TVPItem> _DicTvpParam = new Dictionary<string, TVPItem>();
        /// <summary>
        /// 表值参数列表
        /// </summary>
        public Dictionary<string, TVPItem> DicTvpParam
        {
            get
            {
                return _DicTvpParam;
            }
            set
            {
                _DicTvpParam = value;
            }
        }
    }

    public class TVPItem
    {
        private DataTable _Value = null;
        /// <summary>
        /// 表值数据
        /// </summary>
        public DataTable Value
        {
            get { return _Value; }
            set { _Value = value; }
        }

        private string _TypeName = string.Empty;
        /// <summary>
        /// 表值参数类型名
        /// </summary>
        public string TypeName
        {
            get { return _TypeName; }
            set { _TypeName = value; }
        }
    }
}
