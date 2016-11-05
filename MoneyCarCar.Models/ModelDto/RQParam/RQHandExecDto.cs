using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyCarCar.Models.ModelDto.RQParam
{
    public class RQHandExecDto
    {
        public SystemRequestRecord RequestRecord { get; set; }
        public int OperatorUserId { get; set; }
        public string OperatorUserName { get; set; }
        public string OperatorContent { get; set; }
        public string IP { get; set; }
    }
}
