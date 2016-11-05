using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyCarCar.Models.ModelDto.RQParam
{
    public class RQIDCardAuthenticate
    {
        public string RealName { get; set; }
        public string IDCard { get; set; }
        public string Address { get; set; }
        public string Vcode { get; set; }
    }
}
