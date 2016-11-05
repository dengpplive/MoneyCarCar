﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyCarCar.Models.YeePay.NotifyModel.toBindBankCard
{
    /// <summary>
    /// 2.4. 绑卡: 回调通知(notify) 
    /// </summary>
    public class notify : BaseNotify
    {
        /// <summary>
        /// Y 绑定的卡号  public string bankCardNo { get; set; }
        /// </summary>
        public string cardNo { get; set; }
        /// <summary>
        /// Y 【见绑卡状态】: VERIFYING 认证中、VERIFIED 已认证、
        /// </summary>
        public string cardStatus { get; set; }
        /// <summary>
        /// Y 【见银行代码】
        /// </summary>
        public string bank { get; set; }

        //银行代码：
        //BJRCB 北京农商银行
        //CDCB 成都市商业银行
        //HZCB 杭州市商业银行
        //NOBC 南洋商业银行
        //KLB 昆仑银行
        //ZZYH 郑州银行
        //WZYH 温州银行
        //HKYH 汉口银行
        //QLYH 齐鲁银行
        //DDYH 丹东银行
        //HBC 恒生银行
        //NJCB 南京银行
        //XMYH 厦门银行
        //NCYH 南昌银行
        //DONGGUANBC 东莞银行
        //JSBCHINA 江苏银行
        //HKBEA 东亚银行(中国)
        //AYYH 安阳银行
        //CDYH 成都银行
        //NBB 宁波银行
        //CSCB 长沙银行
        //HBYH 河北银行
        //NYFZYH 农业发展银行
        //GZYH 广州银行
        //BOCO 交通银行
        //CEB 光大银行
        //SPDB 上海浦东发展银行
        //ABC 农业银行
        //ECITIC 中信银行
        //PAB 平安银行
        //CCB 建设银行
        //CMBC 民生银行
        //SDB 深圳发展银行
        //POST 中国邮政储蓄
        //CMBCHINA 招商银行
        //CIB 兴业银行
        //ICBC 中国工商银行
        //BOC 中国银行
        //BCCB 北京银行
        //GDB 广发银行
        //HXB 华夏银行
        //XACB 西安市商业银行
        //SHB 上海银行
        //TJCB 天津市商业银行
        //TYCB 太原市商业银行
        //GZCB 广州市商业银行
        //SNXS 深圳农村商业银行
        //SHRCB 上海农商银行
    }
}
