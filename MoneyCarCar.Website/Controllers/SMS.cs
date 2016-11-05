using MoneyCarCar.Commons;
using MoneyCarCar.Models.DtoModels;
using MoneyCarCar.Models.SMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoneyCarCar.Website.Controllers
{
    public class SMS
    {
        /// <summary>
        /// 发送注册验证码
        /// </summary>
        public static BaseResultDto<string> SendRegisterSMS(string phoneNo)
        {
            BaseResultDto<string> re = new BaseResultDto<string>();
            string phoneVcode = VerificationCode.GetCheckCode(6);
            SendInfo info = new SendInfo() { templateId = "1", to = phoneNo, datas = new string[] { phoneVcode, "10" } };
            ResponseInfo result = HttpHelper.CreatHelper().DoPostObject<ResponseInfo>(ApplicationPropertys.WEBAPI_URL + "/Sms/SendTemplateSMS/", info);

            if (result.statusCode.Equals("000000"))
            {
                re.IsSeccess = true;
                re.Tag = phoneVcode;
            }
            else
            {
                re.IsSeccess = false;
                re.ErrorCode = result.statusCode.ToInt();
                re.ErrorMsg = result.statusMsg;
            }
            return re;
        }

        /// <summary>
        /// 发送身份证验的验证码
        /// </summary>
        public static BaseResultDto<string> SendAuthenticateIDCardSMS(string phoneNo)
        {
            BaseResultDto<string> re = new BaseResultDto<string>();
            string phoneVcode = VerificationCode.GetCheckCode(6);
            SendInfo info = new SendInfo() { templateId = "1", to = phoneNo, datas = new string[] { phoneVcode, "10" } };
            ResponseInfo result = HttpHelper.CreatHelper().DoPostObject<ResponseInfo>(ApplicationPropertys.WEBAPI_URL + "/Sms/SendTemplateSMS/", info);

            if (result.statusCode.Equals("000000"))
            {
                re.IsSeccess = true;
                re.Tag = phoneVcode;
            }
            else
            {
                re.IsSeccess = false;
                re.ErrorCode = result.statusCode.ToInt();
                re.ErrorMsg = result.statusMsg;
            }
            return re;
        }
    }
}