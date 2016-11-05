using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class YeePayCallBackTest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            StringBuilder sblog = new StringBuilder();

            //1.记录日志
            //MoneyCarCar.BLL.Logs.Log.RecordLog("YeePayCallBack(callBack)", "callBack", true);

            RecordLog("YeePayCallBackTest", "YeePayCallBackTest", true);

            string resp = Request.Form["resp"];//xml
            string sign = Request.Form["sign"]; //签名

            RecordLog("YeePayCallBackTest", "YeePayCallBackTest:resp" + resp, false);
            RecordLog("YeePayCallBackTest", "YeePayCallBackTest:sign" + sign, false);



            //2.验证签名
            if (sign != "")
            {

            }


            // // 解码(内侧使用)
            //// resp = HttpUtility.UrlDecode(resp, Encoding.UTF8);

            // sblog.Append("\r\n resp:" + resp);
            // sblog.Append("\r\n sign:" + sign);


            //MoneyCarCar.Models.YeePay.response _response = (MoneyCarCar.Models.YeePay.response)XmlUtil.Deserialize(typeof(MoneyCarCar.Models.YeePay.response), resp);

            // 日志 model to xml
            //sblog.Append("\r\n model to xml:");
            //sblog.Append("<response platformNo=\"" + _response.platformNo + "\">");
            //sblog.Append("<requestNo>" + _response.requestNo + "</requestNo>");
            //sblog.Append("<service>" + _response.service + "</service>");
            //sblog.Append("<code>" + _response.code + "</code>");
            //sblog.Append("<description>" + _response.description + "</description>");
            //MoneyCarCar.BLL.Logs.Log.RecordLog("YeePayCallBack(callBack)", "sblog=" + sblog.ToString(), false);


            //3.处理（不进行业务处理）

            //4.处理成功

        }
        catch (Exception ex)
        {
            //MoneyCarCar.BLL.Logs.Log.RecordLog("YeePayCallBack(Exception)", "ex:" + ex.ToString(), true);
        }

        //if (result)
    }


    private static object RootLock = new object();

    /// <summary>
    /// 记录日志(记事本)
    /// </summary>
    /// <param name="PageName">页面名</param>
    /// <param name="logContents">日志内容</param>
    /// <param name="bRecordRequest">是否记录参数</param>
    public static void RecordLog(string PageName, string logContents, bool bRecordRequest)
    {
        lock (RootLock)
        {
            StreamWriter fs = null;
            StringBuilder sb = new StringBuilder();
            try
            {
                #region 记录文本日志

                sb.AppendFormat("记录时间：" + DateTime.Now.ToString() + "\r\n");
                sb.AppendFormat("内    容: " + logContents + "\r\n");

                if (HttpContext.Current != null && HttpContext.Current.Request != null)
                {
                    sb.AppendFormat("      IP：" + System.Web.HttpContext.Current.Request.UserHostAddress + "\r\n");
                    sb.AppendFormat("  Request.HttpMethod:" + HttpContext.Current.Request.HttpMethod + "\r\n");

                    if (bRecordRequest)
                    {
                        #region 记录 Request 参数
                        try
                        {

                            if (HttpContext.Current.Request.HttpMethod == "POST")
                            {
                                #region POST 提交
                                if (HttpContext.Current.Request.Form.Count != 0)
                                {
                                    //__VIEWSTATE
                                    //__EVENTVALIDATION 
                                    System.Collections.Specialized.NameValueCollection nv = HttpContext.Current.Request.Form;
                                    if (nv != null && nv.Keys.Count > 0)
                                    {
                                        foreach (string key in nv.Keys)
                                        {
                                            if (key == "__VIEWSTATE" || key == "__EVENTVALIDATION")
                                            {
                                                continue;
                                            }
                                            sb.AppendFormat("{0} ={1} \r\n", key, (nv[key] != null ? nv[key].ToString() : ""));
                                        }
                                    }
                                }
                                else
                                {
                                    sb.AppendFormat(" HttpContext.Current.Request.Form.Count = 0 \r\n");
                                }

                                #endregion
                            }
                            else if (HttpContext.Current.Request.HttpMethod == "GET")
                            {
                                #region GET 提交

                                if (HttpContext.Current.Request.QueryString.Count != 0)
                                {
                                    System.Collections.Specialized.NameValueCollection nv = HttpContext.Current.Request.QueryString;
                                    if (nv != null && nv.Keys.Count > 0)
                                    {
                                        foreach (string key in nv.Keys)
                                        {
                                            sb.AppendFormat("{0}={1} \r\n", key, nv[key]);
                                        }
                                    }
                                }
                                else
                                {
                                    sb.AppendFormat(" HttpContext.Current.QueryString.Form.Count = 0 \r\n");
                                }

                                #endregion
                            }
                            else
                            {

                            }

                        }
                        catch (Exception ex)
                        {
                            sb.AppendFormat("  异常内容: " + ex + "\r\n");
                            sb.AppendFormat("----------------------------------------------------------------------------------------------------\r\n\r\n");
                            AgainWrite(sb, PageName);
                        }

                        #endregion
                    }
                }
                else
                {
                    sb.AppendFormat("  HttpContext.Current.Request=null \r\n");
                }

                sb.AppendFormat("----------------------------------------------------------------------------------------------------\r\n\r\n");

                string dir = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "Logs\\" + PageName + "\\";
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                fs = new StreamWriter(dir + System.DateTime.Now.ToString("yyyy-MM-dd") + ".txt", true, System.Text.Encoding.Default);
                fs.WriteLine(sb.ToString());

                #endregion
            }
            catch (Exception ex)
            {
                sb.AppendFormat("catch(Exception ex): " + ex.ToString() + "\r\n");
                AgainWrite(sb, PageName);
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }
            }
        }
    }

    /// <summary>
    /// 再次记录
    /// </summary>
    /// <param name="sb"></param>
    /// <param name="PageName"></param>
    private static void AgainWrite(StringBuilder sb, string PageName)
    {
        StreamWriter fs = null;
        try
        {
            string dir = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "Logs\\" + PageName + "\\";
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            fs = new StreamWriter(dir + System.DateTime.Now.ToString("yyyy-MM-dd") + "again" + ".txt", true, System.Text.Encoding.Default);
            fs.Write(sb.ToString());
        }
        catch (Exception)
        {

        }
        finally
        {
            if (fs != null)
            {
                fs.Close();
                fs.Dispose();
            }
        }
    }
}
