using System;
using System.IO;
using System.Net;
using System.Xml;
using System.Text;
using MoneyCarCar.Commons;
using MoneyCarCar.Models.SMS;

namespace MoneyCarCar.DAL
{
    public class SMSOper
    {
        #region 字段
        /// <summary>
        /// 服务地址
        /// </summary>
        private string m_restAddress = null;
        /// <summary>
        /// 服务端口
        /// </summary>
        private string m_restPort = null;
        /// <summary>
        /// 主账号
        /// </summary>
        private string m_mainAccount = null;
        /// <summary>
        /// 主账号令牌
        /// </summary>
        private string m_mainToken = null;
        /// <summary>
        /// 子账号
        /// </summary>
        private string m_subAccount = null;
        /// <summary>
        /// 子账号令牌
        /// </summary>
        private string m_subToken = null;
        /// <summary>
        /// VOIP账号
        /// </summary>
        private string m_voipAccount = null;
        /// <summary>
        /// VOIP密码
        /// </summary>
        private string m_voipPwd = null;
        /// <summary>
        /// 应用编号
        /// </summary>
        private string m_appId = null;
        /// <summary>
        /// 是否记录日志
        /// </summary>
        private bool m_isWriteLog = false;

        /// <summary>
        /// 服务器api版本
        /// </summary>
        private const string softVer = "2013-12-26";
        #endregion


        #region 初始化设置
        /// <summary>
        /// 初始化函数
        /// </summary>
        /// <param name="serverIP">服务器地址</param>
        /// <param name="serverPort">服务器端口</param>
        /// <returns></returns>
        public bool init(string restAddress, string restPort)
        {
            this.m_restAddress = restAddress;
            this.m_restPort = restPort;

            if (m_restAddress == null || m_restAddress.Length < 0 || m_restPort == null || m_restPort.Length < 0 || Convert.ToInt32(m_restPort) < 0)
                return false;

            return true;
        }
        /// <summary>
        /// 设置主帐号信息
        /// </summary>
        /// <param name="accountSid">主帐号</param>
        /// <param name="accountToken">主帐号令牌</param>
        public void setAccount(string accountSid, string accountToken)
        {
            this.m_mainAccount = accountSid;
            this.m_mainToken = accountToken;
        }

        /// <summary>
        /// 设置子帐号信息
        /// </summary>
        /// <param name="subAccountSid">子帐号</param>
        /// <param name="subAccountToken">子帐号令牌</param>
        /// <param name="voipAccount">VoIP帐号</param>
        /// <param name="voipPassword">VoIP密码</param>
        public void setSubAccount(string subAccountSid, string subAccountToken, string voipAccount, string voipPassword)
        {
            this.m_subAccount = subAccountSid;
            this.m_subToken = subAccountToken;
            this.m_voipAccount = voipAccount;
            this.m_voipPwd = voipPassword;
        }

        /// <summary>
        /// 设置应用ID
        /// </summary>
        /// <param name="appId">应用ID</param>
        public void setAppId(string appId)
        {
            this.m_appId = appId;
        }

        /// <summary>
        /// 日志开关
        /// </summary>
        /// <param name="enable">日志开关</param>
        public void enabeLog(bool enable)
        {
            this.m_isWriteLog = enable;
        }

        /// <summary>
        /// 获取日志路径
        /// </summary>
        /// <returns>日志路径</returns>
        public string GetLogPath()
        {
            string dllpath = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
            dllpath = dllpath.Substring(8, dllpath.Length - 8);    // 8是 file:// 的长度
            return System.IO.Path.GetDirectoryName(dllpath) + "\\log.txt";
        }
        #endregion

        #region 操作
        /// <summary>
        /// 发送模板短信
        /// </summary>
        /// <param name="to">短信接收端手机号码集合，用英文逗号分开，每批发送的手机号数量不得超过100个</param>
        /// <param name="templateId">模板Id</param>
        /// <param name="data">可选字段 内容数据，用于替换模板中{序号}</param>
        /// <exception cref="ArgumentNullException">参数不能为空</exception>
        /// <exception cref="Exception"></exception>
        /// <returns></returns>
        public ResponseInfo SendTemplateSMS(SendInfo body)
        {
            ResponseInfo initError = paramCheckRest();
            if (initError != null)
            {
                return initError;
            }
            initError = paramCheckMainAccount();
            if (initError != null)
            {
                return initError;
            }
            initError = paramCheckAppId();
            if (initError != null)
            {
                return initError;
            }

            if (body.to == null)
            {
                throw new ArgumentNullException("to");
            }

            if (body.templateId == null)
            {
                throw new ArgumentNullException("templateId");
            }

            try
            {
                string date = DateTime.Now.ToString("yyyyMMddhhmmss");

                // 构建URL内容
                string sigstr = MD5Encrypt(m_mainAccount + m_mainToken + date);
                string uriStr = string.Format("https://{0}:{1}/{2}/Accounts/{3}/SMS/TemplateSMS?sig={4}", m_restAddress, m_restPort, softVer, m_mainAccount, sigstr);
                Uri address = new Uri(uriStr);

                WriteLog("SendTemplateSMS url = " + uriStr);

                // 创建网络请求  
                HttpWebRequest request = WebRequest.Create(address) as HttpWebRequest;
                setCertificateValidationCallBack();

                // 构建Head
                request.Method = "POST";
                request.Accept = "application/json";
                request.ContentType = "application/json;charset=utf-8";

                Encoding myEncoding = Encoding.GetEncoding("utf-8");
                byte[] myByte = myEncoding.GetBytes(m_mainAccount + ":" + date);
                string authStr = Convert.ToBase64String(myByte);
                request.Headers.Add("Authorization", authStr);
                body.appId = this.m_appId;
                

                byte[] byteData = UTF8Encoding.UTF8.GetBytes(body.ToJsonString());

                WriteLog("SendTemplateSMS requestBody = " + body.ToJsonString());

                // 开始请求
                using (Stream postStream = request.GetRequestStream())
                {
                    postStream.Write(byteData, 0, byteData.Length);
                }

                // 获取请求
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    // Get the response stream  
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    string responseStr = reader.ReadToEnd();

                    WriteLog("SendTemplateSMS responseBody = " + responseStr);

                    if (responseStr != null && responseStr.Length > 0)
                    {
                        ResponseInfo responseResult = responseStr.ToModel<ResponseInfo>();
                        return responseResult;
                    }

                    return new ResponseInfo { statusCode = 172002 + "", statusMsg = "无返回", data = null };
                }
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        #endregion

        #region MD5 和 https交互函数定义

        private void WriteLog(string log)
        {
            if (m_isWriteLog)
            {
                string strFilePath = GetLogPath();
                System.IO.FileStream fs = new System.IO.FileStream(strFilePath, System.IO.FileMode.Append);
                System.IO.StreamWriter sw = new System.IO.StreamWriter(fs, System.Text.Encoding.Default);
                sw.WriteLine(DateTime.Now.ToString() + "\t" + log);
                sw.Close();
                fs.Close();
            }
        }

        /// <summary>
        /// 检查服务器地址信息
        /// </summary>
        /// <returns></returns>
        private ResponseInfo paramCheckRest()
        {
            int statusCode = 0;
            string statusMsg = null;

            if (m_restAddress == null)
            {
                statusCode = 172004;
                statusMsg = "IP空";
            }
            else if (m_restPort == null)
            {
                statusCode = 172005;
                statusMsg = "端口错误";
            }

            if (statusCode != 0)
            {
                return new ResponseInfo { statusCode = statusCode + "", statusMsg = statusMsg, data = null };
            }

            return null;
        }

        /// <summary>
        /// 检查主帐号信息
        /// </summary>
        /// <returns></returns>
        private ResponseInfo paramCheckMainAccount()
        {
            int statusCode = 0;
            string statusMsg = null;

            if (m_mainAccount == null)
            {
                statusCode = 172006;
                statusMsg = "主帐号空";
            }
            else if (m_mainToken == null)
            {
                statusCode = 172007;
                statusMsg = "主帐号令牌空";
            }

            if (statusCode != 0)
            {
                return new ResponseInfo { statusCode = statusCode + "", statusMsg = statusMsg, data = null };
            }

            return null;
        }

        /// <summary>
        /// 检查子帐号信息
        /// </summary>
        /// <returns></returns>
        private ResponseInfo paramCheckSunAccount()
        {
            int statusCode = 0;
            string statusMsg = null;

            if (m_subAccount == null)
            {
                statusCode = 172008;
                statusMsg = "子帐号空";
            }
            else if (m_subToken == null)
            {
                statusCode = 172009;
                statusMsg = "子帐号令牌空";
            }
            else if (m_voipAccount == null)
            {
                statusCode = 1720010;
                statusMsg = "VoIP帐号空";
            }
            else if (m_voipPwd == null)
            {
                statusCode = 172011;
                statusMsg = "VoIP密码空";
            }

            if (statusCode != 0)
            {
                return new ResponseInfo { statusCode = statusCode + "", statusMsg = statusMsg, data = null };
            }

            return null;
        }

        /// <summary>
        /// 检查应用ID
        /// </summary>
        /// <returns></returns>
        private ResponseInfo paramCheckAppId()
        {
            if (m_appId == null)
            {
                return new ResponseInfo { statusCode = 172012 + "", statusMsg = "应用ID为空", data = null };
            }

            return null;
        }


        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="source">原内容</param>
        /// <returns>加密后内容</returns>
        public static string MD5Encrypt(string source)
        {
            // Create a new instance of the MD5CryptoServiceProvider object.
            System.Security.Cryptography.MD5 md5Hasher = System.Security.Cryptography.MD5.Create();

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(source));

            // Create a new Stringbuilder to collect the bytes and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("X2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }


        /// <summary>
        /// 设置服务器证书验证回调
        /// </summary>
        public void setCertificateValidationCallBack()
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback = CertificateValidationResult;
        }

        /// <summary>
        ///  证书验证回调函数  
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="cer"></param>
        /// <param name="chain"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public bool CertificateValidationResult(object obj, System.Security.Cryptography.X509Certificates.X509Certificate cer, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors error)
        {
            return true;
        }
        #endregion
    }
}
