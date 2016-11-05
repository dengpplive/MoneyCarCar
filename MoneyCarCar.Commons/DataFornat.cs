using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace MoneyCarCar.Commons
{
    public class DataFornat
    {
        public string UrlDecode(string strData)
        {
            return HttpUtility.UrlDecode(strData, Encoding.UTF8);
        }
        public string UrlEncode(string strData)
        {
            return HttpUtility.UrlEncode(strData, Encoding.UTF8);
        }
        public string HtmlEncode(string theString)
        {
            theString = theString.Replace(">", "&gt;");
            theString = theString.Replace("<", "&lt;");
            //theString = theString.Replace(" ", "&nbsp;");
            theString = theString.Replace("\"", "&quot;");
            theString = theString.Replace("\'", "&#39;");
            return theString;
        }

        /// <summary>
        /// 恢复html中的特殊字符
        /// </summary>
        /// <param name="theString">需要恢复的文本。</param>
        /// <returns>恢复好的文本。</returns>
        public string HtmlDiscode(string theString)
        {
            theString = theString.Replace("&gt;", ">");
            theString = theString.Replace("&lt;", "<");
            //theString = theString.Replace("&nbsp;", " ");
            theString = theString.Replace("&quot;", "\"");
            theString = theString.Replace("&#39;", "\'");
            return theString;
        }

        public string ReplaceDomain(string url, string hostUrl)
        {
            string oldValue = url;
            string reg = @"http\:\/\/[^\/][\S]+?\/";
            if (url.ToLower().StartsWith("http://"))
            {
                url = Regex.Replace(url, reg, "");
                oldValue = hostUrl.TrimEnd('/') + "/" + url.TrimStart('/');
            }
            else
            {
                oldValue = hostUrl.TrimEnd('/') + "/" + url.TrimStart('/');
            }
            return oldValue;
        }

        public string ReplaceImageSrc(string html, string url)
        {
            html = Regex.Replace(html, @"(?i)(?<=<img\b[^>]*?src=\s*(['""]))(.*/)+(?=[^'""/]+\1)",
            new MatchEvaluator((m) =>
            {
                string oldValue = string.Empty;
                if (m.Success)
                {
                    string strValue = m.Value;
                    oldValue = ReplaceDomain(strValue, url);
                }
                return oldValue;
            }));
            return html;
        }

        public string GetResovePath(string url)
        {
            string reg = @"http\:\/\/[^\/][\S]+?\/";
            if (url.ToLower().StartsWith("http://"))
            {
                url = Regex.Replace(url, reg, "");
                url = "/" + url.TrimStart('/');
            }
            return System.Web.HttpContext.Current.Server.MapPath(url);
        }

        public string HttpPost(string url, string data)
        {
            string res = "";
            string postData = data;	//xml数据
            string Web = url;	//网关地址
            WebRequest myWebRequest = null;
            WebResponse myWebResponse = null;
            try
            {
                //将数据提交到快钱服务器
                myWebRequest = WebRequest.Create(url);
                myWebRequest.Method = "POST";
                myWebRequest.ContentType = "application/x-www-form-urlencoded";
                Stream streamReq = myWebRequest.GetRequestStream();
                byte[] byteArray = Encoding.GetEncoding("utf-8").GetBytes(postData);
                streamReq.Write(byteArray, 0, byteArray.Length);
                streamReq.Close();

                //获取服务器返回的XML数据
                myWebResponse = myWebRequest.GetResponse();
                StreamReader sr = new StreamReader(myWebResponse.GetResponseStream());
                res = sr.ReadToEnd();
                sr.Close();
            }
            catch (Exception e)
            {
                res = e.Message.ToString();
            }
            finally
            {
                if (myWebResponse != null)
                {
                    myWebResponse.Close();
                }
                if (myWebRequest != null)
                {
                    myWebRequest.Abort();
                }
            }
            return res; //返回数据
        }
    }
}
