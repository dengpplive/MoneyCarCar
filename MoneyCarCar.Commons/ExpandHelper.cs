using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;
using System.Xml;

namespace MoneyCarCar.Commons
{
    public static class ExpandHelper
    {
        #region IDataReader
        /// <summary>
        /// DataReader转泛型
        /// </summary>
        /// <typeparam name="T">传入的实体类</typeparam>
        /// <param name="objReader">DataReader对象</param>
        /// <returns></returns>
        public static List<T> ReaderToList<T>(this IDataReader objReader)
        {
            using (objReader)
            {
                List<T> list = new List<T>();

                //获取传入的数据类型
                Type modelType = typeof(T);

                //遍历DataReader对象
                while (objReader.Read())
                {
                    //使用与指定参数匹配最高的构造函数，来创建指定类型的实例
                    T model = Activator.CreateInstance<T>();
                    for (int i = 0; i < objReader.FieldCount; i++)
                    {
                        //判断字段值是否为空或不存在的值
                        if (!IsNullOrDBNull(objReader[i]))
                        {
                            //匹配字段名
                            PropertyInfo pi = modelType.GetProperty(objReader.GetName(i), BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                            if (pi != null)
                            {
                                //绑定实体对象中同名的字段  
                                pi.SetValue(model, CheckType(objReader[i], pi.PropertyType), null);
                            }
                        }
                    }
                    list.Add(model);
                }
                return list;
            }
        }

        /// <summary>
        /// 对可空类型进行判断转换(*要不然会报错)
        /// </summary>
        /// <param name="value">DataReader字段的值</param>
        /// <param name="conversionType">该字段的类型</param>
        /// <returns></returns>
        private static object CheckType(object value, Type conversionType)
        {
            if (conversionType.IsGenericType && conversionType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null)
                    return null;
                System.ComponentModel.NullableConverter nullableConverter = new System.ComponentModel.NullableConverter(conversionType);
                conversionType = nullableConverter.UnderlyingType;
            }
            return Convert.ChangeType(value, conversionType);
        }

        /// <summary>
        /// 判断指定对象是否是有效值
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static bool IsNullOrDBNull(object obj)
        {
            return (obj == null || (obj is DBNull)) ? true : false;
        }


        /// <summary>
        /// DataReader转模型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objReader"></param>
        /// <returns></returns>
        public static T ReaderToModel<T>(this IDataReader objReader)
        {
            using (objReader)
            {
                if (objReader.Read())
                {
                    Type modelType = typeof(T);
                    int count = objReader.FieldCount;
                    T model = Activator.CreateInstance<T>();
                    for (int i = 0; i < count; i++)
                    {
                        if (!IsNullOrDBNull(objReader[i]))
                        {
                            PropertyInfo pi = modelType.GetProperty(objReader.GetName(i), BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                            if (pi != null)
                            {
                                pi.SetValue(model, CheckType(objReader[i], pi.PropertyType), null);
                            }
                        }
                    }
                    return model;
                }
            }
            return default(T);
        }
        #endregion

        #region T
        public static string ToJsonString<T>(this T obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static int ToInt<T>(this T obj)
        {
            try
            {
                return Convert.ToInt32(obj);
            }
            catch
            {
                return int.MinValue;
            }
        }

        public static decimal ToDecimal<T>(this T obj)
        {
            try
            {
                return Convert.ToDecimal(obj);
            }
            catch
            {
                return decimal.MinValue;
            }
        }

        #endregion

        #region string
        public static T ToModel<T>(this string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static int ToInt(this string input)
        {
            try
            {
                if (input.IndexOf(".") > -1)
                {
                    return Convert.ToInt32(input.Split('.')[0]);
                }
                return Convert.ToInt32(input);
            }
            catch (Exception)
            {
                return int.MinValue;
            }

        }

        public static string GetMd5Code(this string inStr)
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(inStr, "MD5");
        }

        public static DateTime ToDateTime(this string inStr)
        {
            try
            {
                return Convert.ToDateTime(inStr);
            }
            catch
            {
                return DateTime.MinValue;
            }
        }

        public static string ToMoneyChina(this string numstr)
        {
            try
            {
                decimal num = Convert.ToDecimal(numstr);
                return CmycurD(num);
            }
            catch
            {
                return "非数字形式！";
            }
        }

        /// <summary>  
        /// XML反序列化  
        /// </summary>  
        /// <typeparam name="T">反序列话的类型</typeparam>
        /// <param name="xml">XML字符串</param>  
        /// <returns></returns>  
        public static T XmlDeserialize<T>(this string xml) where T : class,new()
        {
            try
            {
                using (StringReader sr = new StringReader(xml))
                {
                    XmlSerializer xmldes = new XmlSerializer(typeof(T));
                    return (T)xmldes.Deserialize(sr);
                }
            }
            catch (Exception ex)
            {
                Log.RecordLog("XmlDeserialize", " XmlDeserialize(catch)  ex1:" + ex.ToString(), false);
                return null;
            }
        }

        /// <summary>  
        /// 序列化 XML  
        /// </summary>  
        /// <typeparam name="T">反序列话的类型</typeparam>
        /// <param name="obj">实体</param>  
        /// <returns></returns>  
        public static string Deserialize(object obj)
        {
            try
            {
                MemoryStream Stream = new MemoryStream();
                XmlSerializer xml = new XmlSerializer(obj.GetType());
                try
                {
                    //序列化对象
                    xml.Serialize(Stream, obj);
                }
                catch (InvalidOperationException e)
                {
                    Log.RecordLog("Deserialize", " Deserialize(catch)  ex1:" + e.ToString(), false);
                    return null;
                }
                Stream.Position = 0;
                StreamReader sr = new StreamReader(Stream);
                string str = sr.ReadToEnd();

                sr.Dispose();
                Stream.Dispose();

                return str;
            }
            catch (Exception ex)
            {
                Log.RecordLog("Deserialize", " Deserialize(catch)  ex1:" + ex.ToString(), false);
                return null;
            }
        }
        #endregion

        #region Datetime
        /// <summary>
        /// 时间格式转换
        /// </summary>
        /// <param name="time">时间</param>
        /// <param name="type">转换类型(1:yyyy-MM-dd HH:mm:ss)</param>
        /// <returns></returns>
        public static string ToString(this DateTime time, int type)
        {
            switch (type)
            {
                case 1:
                    {
                        return time.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                default:
                    {
                        return time.ToString();
                    }
            }
        }
        #endregion

        #region decimal
        public static string ToMoney(this decimal inDec)
        {
            return inDec.ToString("#,##0.00");
        }

        public static string ToMoneyChina(this decimal idDec)
        {
            return CmycurD(idDec);
        }

        public static string ToMoney(this decimal idDec, int len)
        {
            string tValue = idDec + "";
            var index = tValue.IndexOf(".");
            if (index < 0)
            {
                tValue = tValue + ".";
                for (var i = 0; i < len; i++)
                {
                    tValue = tValue + "0";
                }
                index = tValue.IndexOf(".");
            }
            if (tValue.Length < index + len + 1)
            {
                for (var i = tValue.Length; i < index + len + 1; i++)
                {
                    tValue = tValue + "0";
                }
            }
            return tValue.Substring(0, index + len + 1);
        }

        /// <summary>
        /// 转换人民币大小金额
        /// </summary>
        /// <param name="num">金额</param>
        /// <returns>返回大写形式</returns>
        private static string CmycurD(this decimal num)
        {
            string str1 = "零壹贰叁肆伍陆柒捌玖";            //0-9所对应的汉字
            string str2 = "万仟佰拾亿仟佰拾万仟佰拾元角分"; //数字位所对应的汉字
            string str3 = "";    //从原num值中取出的值
            string str4 = "";    //数字的字符串形式
            string str5 = "";  //人民币大写金额形式
            int i;    //循环变量
            int j;    //num的值乘以100的字符串长度
            string ch1 = "";    //数字的汉语读法
            string ch2 = "";    //数字位的汉字读法
            int nzero = 0;  //用来计算连续的零值是几个
            int temp;            //从原num值中取出的值

            num = Math.Round(Math.Abs(num), 2);    //将num取绝对值并四舍五入取2位小数
            str4 = ((long)(num * 100)).ToString();        //将num乘100并转换成字符串形式
            j = str4.Length;      //找出最高位
            if (j > 15) { return "溢出"; }
            str2 = str2.Substring(15 - j);   //取出对应位数的str2的值。如：200.55,j为5所以str2=佰拾元角分

            //循环取出每一位需要转换的值
            for (i = 0; i < j; i++)
            {
                str3 = str4.Substring(i, 1);          //取出需转换的某一位的值
                temp = Convert.ToInt32(str3);      //转换为数字
                if (i != (j - 3) && i != (j - 7) && i != (j - 11) && i != (j - 15))
                {
                    //当所取位数不为元、万、亿、万亿上的数字时
                    if (str3 == "0")
                    {
                        ch1 = "";
                        ch2 = "";
                        nzero = nzero + 1;
                    }
                    else
                    {
                        if (str3 != "0" && nzero != 0)
                        {
                            ch1 = "零" + str1.Substring(temp * 1, 1);
                            ch2 = str2.Substring(i, 1);
                            nzero = 0;
                        }
                        else
                        {
                            ch1 = str1.Substring(temp * 1, 1);
                            ch2 = str2.Substring(i, 1);
                            nzero = 0;
                        }
                    }
                }
                else
                {
                    //该位是万亿，亿，万，元位等关键位
                    if (str3 != "0" && nzero != 0)
                    {
                        ch1 = "零" + str1.Substring(temp * 1, 1);
                        ch2 = str2.Substring(i, 1);
                        nzero = 0;
                    }
                    else
                    {
                        if (str3 != "0" && nzero == 0)
                        {
                            ch1 = str1.Substring(temp * 1, 1);
                            ch2 = str2.Substring(i, 1);
                            nzero = 0;
                        }
                        else
                        {
                            if (str3 == "0" && nzero >= 3)
                            {
                                ch1 = "";
                                ch2 = "";
                                nzero = nzero + 1;
                            }
                            else
                            {
                                if (j >= 11)
                                {
                                    ch1 = "";
                                    nzero = nzero + 1;
                                }
                                else
                                {
                                    ch1 = "";
                                    ch2 = str2.Substring(i, 1);
                                    nzero = nzero + 1;
                                }
                            }
                        }
                    }
                }
                if (i == (j - 11) || i == (j - 3))
                {
                    //如果该位是亿位或元位，则必须写上
                    ch2 = str2.Substring(i, 1);
                }
                str5 = str5 + ch1 + ch2;
                if (i == j - 1 && str3 == "0")
                {
                    //最后一位（分）为0时，加上“整”
                    str5 = str5 + '整';
                }
            }
            if (num == 0)
            {
                str5 = "零元整";
            }
            return str5;
        }


        #endregion
    }
}
