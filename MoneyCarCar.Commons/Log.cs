using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace MoneyCarCar.Commons
{
    public class Log
    {
        private static object RootLock = new object();

        private static string islog = "";

        public static void WriteRecord(string text)
        {
            lock (RootLock)
            {
                StreamWriter fs = null;
                StringBuilder sb = new StringBuilder();
                try
                {
                    #region 记录文本日志
                    sb.AppendFormat("记录时间：" + DateTime.Now.ToString() + "\r\n");
                    string dir = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "Logs\\Services\\";
                    if (!Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }
                    sb.Append(text);
                    fs = new StreamWriter(dir + System.DateTime.Now.ToString("yyyy-MM-dd") + ".txt", true, System.Text.Encoding.Default);
                    fs.WriteLine(sb.ToString());

                    #endregion
                }
                catch (Exception ex) { }
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
        /// 支付接口记录日志(记事本)
        /// </summary>
        /// <param name="PageName">页面名</param>
        /// <param name="logContents">日志内容</param>
        /// <param name="bRecordRequest">是否记录参数</param>
        public static void RecordLog(string PageName, string logContents, bool bRecordRequest)
        {
            lock (RootLock)
            {
                try
                {
                    XmlDocument xmldoc = new XmlDocument();
                    string path = AppDomain.CurrentDomain.BaseDirectory + "\\YeePayConfig.xml";
                    xmldoc.Load(path);
                    XElement xmlRoot = XElement.Parse(xmldoc.InnerXml);
                    foreach (XElement xe in xmlRoot.Elements("log"))
                    {
                        if (xe.Element("islog") != null)
                        {
                            islog = xe.Element("islog").Value;
                        }
                    }
                }
                catch (Exception)
                {


                }

                if (islog.Contains("1"))
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
}