using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoneyCarCar.Commons;
using MoneyCarCar.Models.YeePay;
using MoneyCarCar.DAL;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void YeePayConfig()
        {
            Log.RecordLog("1", "1", false);
        }
        public string ReplaceImageSrc(string html, string url)
        {
            string yourhtml = @"<img alt= ""test"" alt=network-adaptors.jpg src= ""http://www.sinaimg.cn/IT/cr/2007/0704/3441139462.jpg "" style= ""border:0px solid #000; "" _extended= ""true "">  
<img alt= ""test"" alt=network-adaptors.jpg src= ""http://www.sinaimg.cn/IT/cr/2008/34/3656462.jpg "" style= ""border:0px solid #000; "" _extended= ""true "">  ";
            string str = "指定的路径/";


            yourhtml = Regex.Replace(yourhtml, @"(?i)(?<=<img\b[^>]*?src=\s*(['""]))(.*/)+(?=[^'""/]+\1)", new MatchEvaluator((m) =>
            {
                string oldValue = string.Empty;
                if (m.Success)
                {
                    oldValue = url + m.Value;
                }
                return oldValue;
            }));

            return html;
        }

        public string ReplaceImageSrc1(string html, string url)
        {
            string reg = @"http\:\/\/[^\/][\S]+?\/";
            html = Regex.Replace(html, @"(?i)(?<=<img\b[^>]*?src=\s*(['""]))(.*/)+(?=[^'""/]+\1)",
            new MatchEvaluator((m) =>
            {
                string oldValue = string.Empty;
                if (m.Success)
                {
                    string strValue = m.Value;
                    if (strValue.ToLower().StartsWith("http://"))
                    {
                        strValue = Regex.Replace(strValue, reg, "");
                        oldValue = url.TrimEnd('/') + "/" + strValue.TrimStart('/');
                    }
                    else
                    {
                        oldValue = url.TrimEnd('/') + "/" + m.Value.TrimStart('/');
                    }
                }
                return oldValue;
            }));
            return html;
        }

        [TestMethod]
        public void MyTestMethod232()
        {
            string yourhtml = @"<img alt= ""test"" alt=network-adaptors.jpg src= ""http://www.sinaimg.cn/IT/cr/2007/0704/3441139462.jpg "" style= ""border:0px solid #000; "" _extended= ""true "">  
<img alt= ""test"" alt=network-adaptors.jpg src= ""http://www.sinaimg.cn/IT/cr/2008/34/3656462.jpg "" style= ""border:0px solid #000; "" _extended= ""true "">  ";
            string str = "http://www.111111.cn/";
            string aa = ReplaceImageSrc1(yourhtml, str);

        }
        public string readFile(string filePath)
        {
            StreamReader sr = new StreamReader(filePath, Encoding.Default);
            string str = sr.ReadToEnd();
            sr.Close();
            return str;
        }

        [TestMethod]
        public void MyTestMethod()
        {
            YeePayNotify yeePayNotify = new YeePayNotify();
            YeePay yeePay = new YeePay();
            string xmlStr = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>" +
"<notify>" +
    "<requestNo>c81d5f6f03d14a1ab2b1f4ae6f1b335f</requestNo>" +
    "<platformNo>10012425968</platformNo>" +
    "<bizType>REGISTER</bizType>" +
    "<code>1</code>" +
    "<message>注册成功</message>" +
    "<platformUserNo>cd01</platformUserNo>" +
"</notify>";
            xmlStr = readFile(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\notify.txt");
            string sign = readFile(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\sign.txt");//@"MIIFLQYJKoZIhvcNAQcCoIIFHjCCBRoCAQExCzAJBgUrDgMCGgUAMC8GCSqGSIb3DQEHAaAiBCAyNzVhODM3MDk1N2YxMTc3M2YxMjY2NDQyNzMyOGVkYqCCA+8wggPrMIIDVKADAgECAhBdhWwmCJ6J4I7FOXDt/QXLMA0GCSqGSIb3DQEBBQUAMCoxCzAJBgNVBAYTAkNOMRswGQYDVQQKExJDRkNBIE9wZXJhdGlvbiBDQTIwHhcNMTQwMzMxMDgxMzM0WhcNMTcwMzMxMDgxMzM0WjCBhjELMAkGA1UEBhMCQ04xGzAZBgNVBAoTEkNGQ0EgT3BlcmF0aW9uIENBMjEWMBQGA1UECxMNcmEueWVlcGF5LmNvbTEUMBIGA1UECxMLRW50ZXJwcmlzZXMxLDAqBgNVBAMUIzA0MUBaeWVlcGF5LmNvbUB5ZWVwYXkuY29tQDAwMDAwMDAxMIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDUVZmAbx6za66GdcZS9T09zwWTNxHb/M56UG6/o/ejgWqSytKm/GIYlYc03nZs1Isbvz/WKUrf/TmJ7RjyoXKEyaNMecVqwN2V5me/58n023R5CUO09X0t4jhGRtY6PQkqMt0v+HjjtNNXyVrPi8TseI7Za1GF+pAVkSEqjFnX8QIDAQABo4IBszCCAa8wHwYDVR0jBBgwFoAU8I3ts0G7++8IHlUCwzE37zwUTs0wHQYDVR0OBBYEFM+Hqel2sjNtEREEhcOw/+HGRazZMAsGA1UdDwQEAwIE8DAMBgNVHRMEBTADAQEAMDsGA1UdJQQ0MDIGCCsGAQUFBwMBBggrBgEFBQcDAgYIKwYBBQUHAwMGCCsGAQUFBwMEBggrBgEFBQcDCDCB/wYDVR0fBIH3MIH0MFegVaBTpFEwTzELMAkGA1UEBhMCQ04xGzAZBgNVBAoTEkNGQ0EgT3BlcmF0aW9uIENBMjEMMAoGA1UECxMDQ1JMMRUwEwYDVQQDEwxjcmwxMDRfMTA2ODIwgZiggZWggZKGgY9sZGFwOi8vY2VydDg2My5jZmNhLmNvbS5jbjozODkvQ049Y3JsMTA0XzEwNjgyLE9VPUNSTCxPPUNGQ0EgT3BlcmF0aW9uIENBMixDPUNOP2NlcnRpZmljYXRlUmV2b2NhdGlvbkxpc3Q/YmFzZT9vYmplY3RjbGFzcz1jUkxEaXN0cmlidXRpb25Qb2ludDATBgMqVgEEDBMKeWVlcGF5LmNvbTANBgkqhkiG9w0BAQUFAAOBgQBoIXByRAavrQaQ4blG0X6+n0Z6IOhdhkabLWobkK49l3Fhv2KuUsU+4vNCFa99cDWPKaV+IlNpk29f1i849GXPefAiWMCFggZkJOxUIdJU6gm8OZCEtteCiTq1Z8M6ywktM5Jmm5/y/aYeKSbHNcHMbIbs+BrS6IkZUssl4S1mcTGB4zCB4AIBATA+MCoxCzAJBgNVBAYTAkNOMRswGQYDVQQKExJDRkNBIE9wZXJhdGlvbiBDQTICEF2FbCYInongjsU5cO39BcswCQYFKw4DAhoFADANBgkqhkiG9w0BAQEFAASBgDUircc2OtIL19e0kR3FpNkO+EKRDDTxc15vl3pzs0MQffngdy7D+JcY6bixp/E9aDrrgkxMSLkgPn/YVKmNhyLEdP7cYsEsrbKp0QFesMPSY2E6oBzMSmqFlSaSRmGc4+z3yZpng1VvDF3a8t6Xjy9OWRJGvwTdmcXV5ZSQ1bdw";
            //string url = "http://211.149.204.89:8088/verify";
            //string strHttpPost = yeePay.HttpPost(url, "req=" + xmlStr + "&sign=" + sign);

            DataFornat http = new DataFornat();
            xmlStr = http.UrlEncode(xmlStr);
            sign = http.UrlEncode(sign);
            string postData = string.Format("sign={0}&notify={1}", sign, xmlStr);
            //string postData = string.Format("url={0}&type=post&sign={1}&notify={2}", "http://211.149.204.89:81/YeePayNotify/Notify", sign, xmlStr);
            //string TestUrl = "http://211.149.204.89:81/TransData.aspx";
            string TestUrl = "http://211.149.204.89:81/YeePayNotify/Notify";
            string dd = http.HttpPost(TestUrl, postData);




        }


        [TestMethod]
        public void MyTestMethods()
        {
            string dir = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "//2015-01-151.txt";

            System.IO.StreamReader sr = new StreamReader(dir, System.Text.Encoding.Default);

            string str = sr.ReadToEnd();

            string xml = str;

            string dir2 = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "//2015-01-152.txt";

            System.IO.StreamReader sr2 = new StreamReader(dir2, System.Text.Encoding.Default);

            string sign = sr2.ReadToEnd();


            string url = "http://211.149.204.89:81/YeePayNotify/Notify";


            YeePay yeepay = new YeePay();

            DataFornat df = new DataFornat();

            sign = df.UrlEncode(sign);

            //xml = df.UrlEncode(xml);


            string stringNoty = yeepay.HttpPost(url, "notify=" + xml + "&sign=" + sign);

            stringNoty = stringNoty + "";
        }

        [TestClass]
        public class MyTestClass
        {
            public string readFile(string filePath)
            {
                StreamReader sr = new StreamReader(filePath, Encoding.Default);
                string str = sr.ReadToEnd();
                sr.Close();
                return str;
            }
            [TestMethod]
            public void MyTestMethod()
            {
                string xmlStr = readFile(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\notify.txt");
                string sign = readFile(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\sign.txt");//@"MIIFLQYJKoZIhvcNAQcCoIIFHjCCBRoCAQExCzAJBgUrDgMCGgUAMC8GCSqGSIb3DQEHAaAiBCAyNzVhODM3MDk1N2YxMTc3M2YxMjY2NDQyNzMyOGVkYqCCA+8wggPrMIIDVKADAgECAhBdhWwmCJ6J4I7FOXDt/QXLMA0GCSqGSIb3DQEBBQUAMCoxCzAJBgNVBAYTAkNOMRswGQYDVQQKExJDRkNBIE9wZXJhdGlvbiBDQTIwHhcNMTQwMzMxMDgxMzM0WhcNMTcwMzMxMDgxMzM0WjCBhjELMAkGA1UEBhMCQ04xGzAZBgNVBAoTEkNGQ0EgT3BlcmF0aW9uIENBMjEWMBQGA1UECxMNcmEueWVlcGF5LmNvbTEUMBIGA1UECxMLRW50ZXJwcmlzZXMxLDAqBgNVBAMUIzA0MUBaeWVlcGF5LmNvbUB5ZWVwYXkuY29tQDAwMDAwMDAxMIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDUVZmAbx6za66GdcZS9T09zwWTNxHb/M56UG6/o/ejgWqSytKm/GIYlYc03nZs1Isbvz/WKUrf/TmJ7RjyoXKEyaNMecVqwN2V5me/58n023R5CUO09X0t4jhGRtY6PQkqMt0v+HjjtNNXyVrPi8TseI7Za1GF+pAVkSEqjFnX8QIDAQABo4IBszCCAa8wHwYDVR0jBBgwFoAU8I3ts0G7++8IHlUCwzE37zwUTs0wHQYDVR0OBBYEFM+Hqel2sjNtEREEhcOw/+HGRazZMAsGA1UdDwQEAwIE8DAMBgNVHRMEBTADAQEAMDsGA1UdJQQ0MDIGCCsGAQUFBwMBBggrBgEFBQcDAgYIKwYBBQUHAwMGCCsGAQUFBwMEBggrBgEFBQcDCDCB/wYDVR0fBIH3MIH0MFegVaBTpFEwTzELMAkGA1UEBhMCQ04xGzAZBgNVBAoTEkNGQ0EgT3BlcmF0aW9uIENBMjEMMAoGA1UECxMDQ1JMMRUwEwYDVQQDEwxjcmwxMDRfMTA2ODIwgZiggZWggZKGgY9sZGFwOi8vY2VydDg2My5jZmNhLmNvbS5jbjozODkvQ049Y3JsMTA0XzEwNjgyLE9VPUNSTCxPPUNGQ0EgT3BlcmF0aW9uIENBMixDPUNOP2NlcnRpZmljYXRlUmV2b2NhdGlvbkxpc3Q/YmFzZT9vYmplY3RjbGFzcz1jUkxEaXN0cmlidXRpb25Qb2ludDATBgMqVgEEDBMKeWVlcGF5LmNvbTANBgkqhkiG9w0BAQUFAAOBgQBoIXByRAavrQaQ4blG0X6+n0Z6IOhdhkabLWobkK49l3Fhv2KuUsU+4vNCFa99cDWPKaV+IlNpk29f1i849GXPefAiWMCFggZkJOxUIdJU6gm8OZCEtteCiTq1Z8M6ywktM5Jmm5/y/aYeKSbHNcHMbIbs+BrS6IkZUssl4S1mcTGB4zCB4AIBATA+MCoxCzAJBgNVBAYTAkNOMRswGQYDVQQKExJDRkNBIE9wZXJhdGlvbiBDQTICEF2FbCYInongjsU5cO39BcswCQYFKw4DAhoFADANBgkqhkiG9w0BAQEFAASBgDUircc2OtIL19e0kR3FpNkO+EKRDDTxc15vl3pzs0MQffngdy7D+JcY6bixp/E9aDrrgkxMSLkgPn/YVKmNhyLEdP7cYsEsrbKp0QFesMPSY2E6oBzMSmqFlSaSRmGc4+z3yZpng1VvDF3a8t6Xjy9OWRJGvwTdmcXV5ZSQ1bdw";
                DataFornat http = new DataFornat();

                string postData = string.Format("url={0}&type=post&req={1}", "http://211.149.204.89:8088/sign", http.UrlEncode(http.HtmlEncode(xmlStr)));
                string TestUrl = "http://211.149.204.89:81/TransData.aspx";
                string result = http.HttpPost(TestUrl, postData);
                if (result == sign)
                {

                }
                else
                {

                }
            }
        }
        [TestMethod]
        public void MywwTestMethod1111()
        {
            string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>" +
    "<response platformNo=\"platformNo\">" +
    "<code>1</code>" +
    "<description>操作成功</description>" +
        "<records>" +
            "<record bizType=\"PAYMENT\" fee=\"0\" balance=\"1.00\" amount=\"5.00\" time=\"2014-01-15 14:17:39\" requestNo=\"xfe13901246549\" platformNo=\"10040008878\"/>" +
            "<record bizType=\"REPAYMENT\" fee=\"0\" balance=\"1.00\" amount=\"5.00\" time=\"2014-01-15 14:17:39\" requestNo=\"xfe13901246549\" platformNo=\"10040008878\"/>" +
            "<record bizType=\"WITHDRAW\" fee=\"0\" balance=\"1.00\" amount=\"5.00\" time=\"2014-01-15 14:17:39\" requestNo=\"xfe13901246549\" platformNo=\"10040008878\"/>" +
            "<record bizType=\"RECHARGE\" fee=\"0\" balance=\"1.00\" amount=\"5.00\" time=\"2014-01-15 14:17:39\" requestNo=\"xfe13901246549\" platformNo=\"10040008878\"/>" +
        "</records>" +
    "</response>";
           
            dynamic d =  new DynamicXml(xml);
            string ss = d.records[0].record[0][""].ToString();

        }
    }
}


