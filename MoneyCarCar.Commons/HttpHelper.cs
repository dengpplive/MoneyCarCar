using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.IO;

namespace MoneyCarCar.Commons
{
    public class HttpHelper
    {
        private static HttpHelper model = null;
        public static HttpHelper CreatHelper()
        {
            if (model == null)
            {
                model = new HttpHelper();
            }
            return model;
        }
        /// <summary>
        /// 通过GET方法调用URL
        /// </summary>
        /// <param name="url">URL地址</param>
        /// <param name="ErrInfo">如果有错误，则返回错误信息,如果没有则返回控制符传</param>
        /// <param name="ResponseCode">状态,正常是200</param>
        /// <param name="TimeOut">超时时间(单位：秒)</param>
        /// <returns>返回字符串</returns>
        public string DoGet(string url, out string ErrInfo, out int ResponseCode, int TimeOut = 30)
        {
            ResponseCode = 400;
            ErrInfo = "";
            StreamReader sr = null;
            HttpWebResponse wr = null;
            HttpWebRequest hp = null;
            try
            {
                hp = (HttpWebRequest)WebRequest.Create(url);

                hp.Timeout = TimeOut * 1000;
                System.Text.Encoding encoding = System.Text.Encoding.GetEncoding("UTF-8");

                wr = (HttpWebResponse)hp.GetResponse();
                sr = new StreamReader(wr.GetResponseStream(), encoding);
                ResponseCode = Convert.ToInt32(wr.StatusCode);
                string strData = sr.ReadToEnd();
                sr.Close();
                wr.Close();
                return strData;
            }
            catch (Exception exp)
            {
                ErrInfo += exp.Message;
                if (wr != null)
                {
                    ResponseCode = Convert.ToInt32(wr.StatusCode);
                }
                return "";
            }
        }

        /// <summary>
        /// 通过GET方法调用URL
        /// </summary>
        /// <param name="url">URL地址</param>
        /// <param name="TimeOut">超时时间(单位：秒)</param>
        /// <returns>返回字符串</returns>
        public string DoGet(string url, int TimeOut = 30)
        {
            int ResponseCode = 400;
            string ErrInfo = "";
            return DoGet(url, out ErrInfo, out ResponseCode, TimeOut);
        }
        /// <summary>
        /// 通过GET方法调用URL
        /// </summary>
        /// <param name="url">URL地址</param>
        /// <param name="TimeOut">超时时间(单位：秒)</param>
        /// <returns>返回实体</returns>
        public T DoGetObject<T>(string url, int TimeOut = 30)
        {
            string jsonString = DoGet(url, TimeOut);
            return JsonConvert.DeserializeObject<T>(jsonString);
        }

        /// <summary>
        /// 通过POST方法调用URL
        /// </summary>
        /// <param name="url">URL地址</param>
        /// <param name="postData">POST数据</param>
        /// <param name="ErrInfo">如果有错误，则返回错误信息,如果没有则返回控制符传</param>
        /// <param name="ResponseCode">状态,正常是200</param>
        /// <param name="TimeOut">超时时间(单位：秒)</param>
        /// <returns>返回字符串</returns>
        public string DoPost(string url, string postData, out string ErrInfo, out int ResponseCode, int TimeOut = 30)
        {
            ResponseCode = 400;
            ErrInfo = "";
            StreamReader sr = null;
            HttpWebResponse wr = null;
            HttpWebRequest hp = null;
            try
            {
                hp = (HttpWebRequest)WebRequest.Create(url);
                hp.Timeout = TimeOut * 1000;
                if (postData != null)
                {
                    byte[] data = Encoding.UTF8.GetBytes(postData);
                    hp.Method = "POST";
                    hp.ContentType = "application/Json";
                    hp.ContentLength = data.Length;
                    Stream ws = hp.GetRequestStream();
                    // 发送数据
                    ws.Write(data, 0, data.Length);
                    ws.Close();
                }

                wr = (HttpWebResponse)hp.GetResponse();
                sr = new StreamReader(wr.GetResponseStream(), Encoding.UTF8);
                ResponseCode = Convert.ToInt32(wr.StatusCode);
                string result = sr.ReadToEnd(); ;
                return result;
            }
            catch (Exception exp)
            {
                ErrInfo += exp.Message;
                if (wr != null)
                {
                    ResponseCode = Convert.ToInt32(wr.StatusCode);
                }
                return "";
            }
            finally
            {
                try
                {
                    if (hp != null)
                    {
                        hp.Abort();
                        hp = null;
                    }
                    if (sr != null)
                    {
                        sr.Close();
                        sr = null;
                    }
                    if (wr != null)
                    {
                        wr.Close();
                        wr = null;
                    }
                }
                catch { }
            }
        }

        /// <summary>
        /// 通过POST方法调用URL
        /// </summary>
        /// <param name="url">URL地址</param>
        /// <param name="postData">POST数据</param>
        /// <param name="TimeOut">超时时间(单位：秒)</param>
        /// <returns>返回字符串</returns>
        public string DoPost(string url, string postData, int TimeOut = 30)
        {
            int ResponseCode = 400;
            string ErrInfo = "";
            return DoPost(url, postData, out ErrInfo, out ResponseCode, TimeOut);
        }

        /// <summary>
        /// PostXML数据到服务器及获取返回的xml值
        /// </summary>
        public string HttpPost(string url, string data)
        {
            string res = "";
            string postData = data;	//xml数据
            string Web = url;	//网关地址

            try
            {
                //将数据提交到快钱服务器
                WebRequest myWebRequest = WebRequest.Create(url);
                myWebRequest.Method = "POST";
                myWebRequest.ContentType = "application/x-www-form-urlencoded";
                Stream streamReq = myWebRequest.GetRequestStream();
                byte[] byteArray = Encoding.GetEncoding("utf-8").GetBytes(postData);
                streamReq.Write(byteArray, 0, byteArray.Length);
                streamReq.Close();

                //获取服务器返回的XML数据
                WebResponse myWebResponse = myWebRequest.GetResponse();
                StreamReader sr = new StreamReader(myWebResponse.GetResponseStream());
                res = sr.ReadToEnd();
                sr.Close();
            }
            catch (Exception e)
            {
                res = e.Message.ToString();

                //RequestLog("ActionFrom:catch" + res.ToString(), false);
            }

            return res; //返回数据
        }

        /// <summary>
        /// 通过POST方法调用URL
        /// </summary>
        /// <param name="url">URL地址</param>
        /// <param name="postBody">POST数据</param>
        /// <param name="TimeOut">超时时间(单位：秒)</param>
        /// <returns>返回字符串</returns>
        public string DoPost(string url, object postBody, int TimeOut = 30)
        {
            return DoPost(url, postBody.ToJsonString(), TimeOut);
        }
        /// <summary>
        /// 通过POST方法调用URL
        /// </summary>
        /// <param name="url">URL地址</param>
        /// <param name="postData">POST数据</param>
        /// <param name="TimeOut">超时时间(单位：秒)</param>
        /// <returns>返回实体</returns>
        public T DoPostObject<T>(string url, string postData, int TimeOut = 30)
        {
            return DoPost(url, postData, TimeOut).ToModel<T>();
        }
        /// <summary>
        /// 通过POST方法调用URL
        /// </summary>
        /// <param name="url">URL地址</param>
        /// <param name="postBody">POST数据</param>
        /// <param name="TimeOut">超时时间(单位：秒)</param>
        /// <returns>返回实体</returns>
        public T DoPostObject<T>(string url, object postBody, int TimeOut = 30)
        {
            return DoPost(url, postBody, TimeOut).ToModel<T>();
        }
    }
}