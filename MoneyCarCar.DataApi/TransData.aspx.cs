using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MoneyCarCar.Commons;
using System.Text;
using System.Collections.Specialized;
namespace MoneyCarCar.DataApi
{
    public partial class TransData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string result = string.Empty;
            StringBuilder sbLog = new StringBuilder();
            try
            {
                if (Request["url"] != null)
                {
                    DataFornat dataFornat = new DataFornat();
                    string strUrl = dataFornat.UrlDecode(Request["url"].ToString());
                    string strRequestType = Request["type"].ToString();
                    sbLog.AppendFormat("转发类型:{0}\r\n", strRequestType);
                    string strResult = string.Empty;
                    NameValueCollection nv = new NameValueCollection();
                    List<string> datalist = new List<string>();
                    if (strRequestType.ToUpper() == "GET")
                    {
                        nv = Request.QueryString;
                        StringBuilder sbData = new StringBuilder();

                        foreach (string key in nv.Keys)
                        {
                            if (key != "url" && key != "type")
                            {
                                if (key == "sign")
                                {
                                    datalist.Add(key + "=" + dataFornat.UrlEncode(dataFornat.HtmlDiscode(dataFornat.UrlDecode(nv[key]))));
                                }
                                else
                                {
                                    datalist.Add(key + "=" + dataFornat.HtmlDiscode(nv[key]));
                                }
                            }
                        }
                        sbData.AppendFormat("{0}?{1}", strUrl, string.Join("&", datalist.ToArray()));
                        sbLog.AppendFormat("转发数据:{0}\r\n", sbData.ToString());
                        HttpHelper helper = new HttpHelper();
                        result = helper.DoGet(sbData.ToString(), 60);
                    }
                    else if (strRequestType.ToUpper() == "POST")
                    {
                        nv = Request.Form;
                        foreach (string key in nv.Keys)
                        {
                            if (key != "url" && key != "type")
                            {
                                if (key == "sign")
                                {
                                    datalist.Add(key + "=" + dataFornat.UrlEncode(nv[key]));
                                }
                                else if (key == "notify")
                                {
                                    datalist.Add(key + "=" + dataFornat.UrlEncode(nv[key]));
                                }
                                else
                                {
                                    if (key == "req")
                                    {
                                        string aa = dataFornat.HtmlDiscode(nv[key]);
                                        datalist.Add(key + "=" + dataFornat.UrlEncode(aa));
                                    }
                                    else
                                    {
                                        datalist.Add(key + "=" + dataFornat.UrlEncode(dataFornat.HtmlDiscode(nv[key])));
                                    }
                                }
                            }
                        }
                        sbLog.AppendFormat("转发Url:{0}\r\n", strUrl);
                        sbLog.AppendFormat("转发数据:{0}\r\n", string.Join("&", datalist.ToArray()));
                        result = dataFornat.HttpPost(strUrl, string.Join("&", datalist.ToArray()));
                    }
                    sbLog.AppendFormat("返回结果:{0}\r\n", result);
                }
                else
                {
                    sbLog.Append("没有此方法");
                }
            }
            catch (Exception ex)
            {
                sbLog.Append(ex.Message);
            }
            finally
            {
                Log.RecordLog("TransData", sbLog.ToString(), false);
                Response.Write(result);
            }
        }
    }
}
