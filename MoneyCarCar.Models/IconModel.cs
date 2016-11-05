using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyCarCar.Models
{
    public class IconModel
    {
        /// <summary>
        /// 图标类型-结算方式
        /// </summary>
        public string IconType { get; set; }
        /// <summary>
        /// 属性参数(可为空)-M：月结,Y:年结,Q:季结,数字:按数字天数结算
        /// </summary>
        public string AtrrCode { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 样式名称
        /// </summary>
        public string StyleName { get; set; }
    }
}
