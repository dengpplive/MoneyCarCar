using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyCarCar.Models.DtoModels
{
    /// <summary>
    /// 返回结果数据
    /// </summary>
    public class ApplayClaimsDto
    {
        public SystemBorrowerApply Applay { get; set; }
        public SystemClaims Claims { get; set; }
        public List<IconProperty> ShowIcons { get; set; }
        public List<IconProperty> ShowAttrCode { get; set; }
    }
    /// <summary>
    /// 每个小图标属性
    /// </summary>
    public class IconProperty
    {
        /// <summary>
        /// 是否为选择条目
        /// </summary>
        public bool IsOpen { get; set; }
        /// <summary>
        /// 后台添加时的显示文本
        /// </summary>
        public string AddShow { get; set; }

        /// <summary>
        /// 以下为具体内容
        /// </summary>
        public string IconType { get; set; }
        public string Title { get; set; }
        public string TipMessage { get; set; }
        public string BackgroundClass { get; set; }
        /// <summary>
        /// 属性参数(可为空)-M：月结,Y:年结,Q:季结,数字:按数字天数结算
        /// </summary>
        public string AtrrCode { get; set; }
        public override string ToString()
        {
            return string.Format("{0}^{1}^{2}^{3}^{4}", IconType, Title, TipMessage, BackgroundClass, AtrrCode);
        }
    }
}
