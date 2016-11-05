using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MoneyCarCar.Models;
using MoneyCarCar.DAL;
using MoneyCarCar.Models.Propertys;
using MoneyCarCar.Commons;
using MoneyCarCar.Models.DtoModels;
using MoneyCarCar.Models.ModelDto.RQParam;
using MoneyCarCar.Models.YeePay;
using System.Text;

namespace MoneyCarCar.DataApi.Controllers
{
    public class YeePayCallBackController : ApiController
    {
        YeePay yeePay = new YeePay();
        YeePayConfig yeePayConfig = new YeePayConfig();
        DataFornat dataFornat = new DataFornat();


        [HttpPost]
        public void CallBack(YeePayCallBack yeePayCallBack)
        {
            try
            {

                //1.记录请求日志
                Log.RecordLog("YeePayCallBackController", " resp: " + yeePayCallBack.resp + " sign: " + yeePayCallBack.sign, false);


                StringBuilder postData = new StringBuilder();
                postData.Append("req=" + dataFornat.UrlEncode(yeePayCallBack.resp)); //编码
                postData.Append("&sign=" + dataFornat.UrlEncode(yeePayCallBack.sign)); //编码

                //Log.RecordLog("YeePayCallBackController", "(postData):" + postData.ToString(), false);

                string strHttpPost = yeePay.HttpPost(yeePayConfig._verifyUrl, postData.ToString());
                // 2. 验证签名
                if (strHttpPost.Contains("SUCCESS"))
                {
                    Log.RecordLog("YeePayCallBackController", "验证签名通过(strHttpPost):" + strHttpPost, false);
                }
                else
                {
                    Log.RecordLog("YeePayCallBackController", "验证签名不通过(strHttpPost):" + strHttpPost, false);
                }

                //序列化 XML 转 实体 
                // MoneyCarCar.Models.YeePay.response _response = yeePayCallBack.resp.XmlDeserialize<MoneyCarCar.Models.YeePay.response>();
            }
            catch (Exception)
            {

            }
        }
    }
}