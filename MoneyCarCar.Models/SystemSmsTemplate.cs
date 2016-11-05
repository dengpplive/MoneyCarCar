using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyCarCar.Models
{
    /// <summary>
    /// 短信模板
    /// </summary>
    public class SystemSmsTemplate
    {
        /// <summary>
        /// Id
        /// </summary>		
        private int _id;
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        /// 短信模板名
        /// </summary>		
        private string  _TemplateName;
        public string TemplateName
        {
            get { return _TemplateName; }
            set { _TemplateName = value; }
        }

        /// <summary>
        /// 短信模板内容
        /// </summary>		
        private string _TemplateContent;
        public string TemplateContent
        {
            get { return _TemplateContent; }
            set { _TemplateContent = value; }
        }
    }
}
