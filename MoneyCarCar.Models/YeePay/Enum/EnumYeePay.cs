using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MoneyCarCar.Models.YeePay.YeePayEnum
{
    public enum EnumYeePay
    { 
    
    }
    /// <summary>
    /// 方法 网关请求
    /// </summary>
    public enum EnumActionRequest
    {
        /// <summary>
        /// 2.1 注册（测试）: 在易宝托管账户平台注册新用户 1.商户平台会员标识若重复，认为是该用户重新申请注册，最后注册信息以最后一次注册为准，若实名信息相同直接返回已注册成功 2.注册流程含有实名认证流程
        /// </summary>
        [Description("toRegister")]
        toRegister = 1,
        /// <summary>
        /// 2.2 充值
        /// </summary>
        [Description("toRecharge")]
        toRecharge = 2,
        /// <summary>
        /// 2.3 提现: 将用户的账户余额提现至绑定的银行卡。用户当天冲的值，当天不计入可提现金额。
        /// </summary>
        [Description("toWithdraw")]
        toWithdraw = 3,
        /// <summary>
        /// 2.4 绑卡: 在资金托管平台提现前,须进行绑卡寿仔卡会进行实名认证只能绑定用户本人的卡。实名认证需要较长时间，因此此接口返回成功只代表绑卡受理成功，不代表绑卡认证成功。
        /// </summary>
        [Description("toBindBankCard")]
        toBindBankCard = 4,
        /// <summary>
        ///  2.5 取消绑卡
        /// </summary>
        [Description("toUnbindBankCard")]
        toUnbindBankCard = 5,
        /// <summary>
        /// 2.6 企业用户注册
        /// </summary>
        [Description("toEnterpriseRegister")]
        toEnterpriseRegister = 6,
        /// <summary>
        /// 2.7  转账、投标、还款、债权转让
        /// </summary>
        [Description("toCpTransaction")]
        toCpTransaction = 7,
        /// <summary>
        /// 2.8 自动投标授权
        /// </summary>
        [Description("toAuthorizeAutoTransfer")]
        toAuthorizeAutoTransfer = 8,
        /// <summary>
        /// 2.9 自动还款授权
        /// </summary>
        [Description("toAuthorizeAutoRepayment")]
        toAuthorizeAutoRepayment = 9,
    }

    /// <summary>
    /// 方法 直连请求
    /// </summary>
    public enum EnumServiceRequest
    {
        /// <summary>
        /// 3.1账户查询 
        /// </summary>
        [Description("ACCOUNT_INFO")]
        ACCOUNT_INFO = 1,
        /// <summary>
        /// 3.2资金冻结
        /// </summary>
        [Description("FREEZE")]
        FREEZE = 2,
        /// <summary>
        /// 3.3.资金解冻
        /// </summary>
        [Description("UNFREEZE")]
        UNFREEZE = 3,
        /// <summary>
        /// 3.4.直接转账
        /// </summary>
        [Description("DIRECT_TRANSACTION")]
        DIRECT_TRANSACTION = 4,
        /// <summary>
        /// 3.5.自动转账授权
        /// </summary>
        [Description("AUTO_TRANSACTION")]
        AUTO_TRANSACTION = 5,
        /// <summary>
        /// 3.6.单笔业务查询
        /// </summary>
        [Description("QUERY")]
        QUERY = 6,
        /// <summary>
        /// 3.7.转账确认
        /// </summary>
        [Description("COMPLETE_TRANSACTION")]
        COMPLETE_TRANSACTION = 7,
        /// <summary>
        /// 3.10.对账
        /// </summary>
        [Description("RECONCILIATION")]
        RECONCILIATION = 10,
    }

    /// <summary>
    /// 4.1.（接口输入，同步） 服务名称：REGISTER 注册、RECHARGE 充值、WITHDRAW 提现、ACCOUNT_INFO 查询账户信息、AUTHORIZE_AUTO_TRANSFER 自动转账授权、AUTHORIZE_AUTO_REPAYMENT 自动还款授权、TRANSFER 资金冻结、LOAN 放款、
    /// </summary>
    public enum EnumServiceNotify
    {
        /// <summary>
        ///  注册
        /// </summary>
        [Description("REGISTER")]
        REGISTER = 1,
        /// <summary>
        /// 充值
        /// </summary>
        [Description("RECHARGE")]
        RECHARGE = 2,
        /// <summary>
        /// 提现
        /// </summary>
        [Description("WITHDRAW")]
        WITHDRAW = 3,
        /// <summary>
        /// 查询账户信息
        /// </summary>
        [Description("ACCOUNT_INFO")]
        ACCOUNT_INFO = 4,
        /// <summary>
        /// 自动转账授权
        /// </summary>
        [Description("AUTHORIZE_AUTO_TRANSFER")]
        AUTHORIZE_AUTO_TRANSFER = 5,
        /// <summary>
        /// 自动还款授权
        /// </summary>
        [Description("AUTHORIZE_AUTO_REPAYMENT")]
        AUTHORIZE_AUTO_REPAYMENT = 6,
        /// <summary>
        /// 放款
        /// </summary>
        [Description("LOAN")]
        LOAN = 8,
    }

    /// <summary>
    /// 4.2 返回状态码：1 成功、0 失败、2 xml 参数格式错误、3 签名验证失败、101 引用了不存在的对象（例如错误的订单号）、102 业务状态不正确、103 由于业务限制导致业务不能执行、104 实名认证失败、
    /// </summary>
    public enum EnumCode
    {
        /// <summary>
        /// 失败
        /// </summary>
        [Description("0")]
        CodeFalse = 0,
        /// <summary>
        /// 成功
        /// </summary>
        [Description("1")]
        CodeTrue = 1,
        /// <summary>
        /// 参数格式错误
        /// </summary>
        [Description("2")]
        CodeXml = 2,
        /// <summary>
        /// 签名验证失败
        /// </summary>
        [Description("3")]
        CodeSign = 3,
        /// <summary>
        /// 引用了不存在的对象（例如错误的订单号）
        /// </summary>
        [Description("101")]
        CodeO = 101,
        /// <summary>
        /// 业务状态不正确
        /// </summary>
        [Description("102")]
        CodeT = 102,
        /// <summary>
        /// 由于业务限制导致业务不能执行
        /// </summary>
        [Description("103")]
        CodeS = 103,
        /// <summary>
        ///  实名认证失败
        /// </summary>
        [Description("104")]
        CodeF = 104
    }

    /// <summary>
    /// 4.3 费率模式：PLATFORM 收取商户手续费、USER 收取用户手续费、
    /// </summary>
    public enum EnumFeeMode
    {
        /// <summary>
        /// 收取商户手续费
        /// </summary>
        [Description("PLATFORM")]
        PLATFORM = 1,
        /// <summary>
        /// 收取用户手续费
        /// </summary>
        [Description("USER")]
        USER = 2,
    }

    /// <summary>
    /// 4.4 身份证类型：G1_IDCARD 1代身份证、G2_IDCARD 2代身份证
    /// </summary>
    public enum EnumIdCardType
    {
        /// <summary>
        /// 1代身份证
        /// </summary>
        [Description("G1_IDCARD")]
        G1_IDCARD = 1,
        /// <summary>
        /// 2代身份证
        /// </summary>
        [Description("G2_IDCARD")]
        G2_IDCARD = 2,
    }

    /// <summary>
    /// 4.5 用户类型：MEMBER 个人会员、MERCHANT 商户
    /// </summary>
    public enum EnumUserType
    {
        /// <summary>
        /// 个人会员
        /// </summary>
        [Description("MEMBER")]
        MEMBER = 1,
        /// <summary>
        /// 商户(企业)
        /// </summary>
        [Description("MERCHANT")]
        MERCHANT = 2,
    }

    /// <summary>
    /// 4.6  绑卡状态：VERIFYING 认证中、VERIFIED 已认证
    /// </summary>
    public enum EnumCardStatus
    {
        /// <summary>
        /// 认证中
        /// </summary>
        [Description("VERIFYING")]
        VERIFYING = 1,
        /// <summary>
        /// 已认证
        /// </summary>
        [Description("VERIFIED")]
        VERIFIED = 2,
    }

    /// <summary>
    /// 4.8 会员激活状态 ：ACTIVATED 已激活、DEACTIVATED 未激活
    /// </summary>
    public enum EnumActiveStatus
    {
        /// <summary>
        /// 已激活
        /// </summary>
        [Description("ACTIVATED")]
        ACTIVATED = 1,
        /// <summary>
        /// 未激活
        /// </summary>
        [Description("DEACTIVATED")]
        DEACTIVATED = 2,
    }

    /// <summary>
    /// 4.9 会员类型：PERSONAL 个人会员、ENTERPRISE 企业会员
    /// </summary>
    public enum EnumMemberType
    {
        /// <summary>
        /// 个人会员
        /// </summary>
        [Description("PERSONAL")]
        PERSONAL = 1,
        /// <summary>
        /// 企业会员
        /// </summary>
        [Description("ENTERPRISE")]
        ENTERPRISE = 2,
    }

    /// <summary>
    ///  //4.10 业务类型：TENDER 投标、REPAYMENT 还款、CREDIT_ASSIGNMENT 债权转让、TRANSFER 转账、COMMISSION 分润仅在资金转账明细中使用、
    /// </summary>
    public enum EnumBizType
    {
        /// <summary>
        /// PAYMENT 交易
        /// </summary>
        [Description("PAYMENT")]
        PAYMENT = 6,
        ///// <summary>
        ///// REPAYMENT 还款
        ///// </summary>
        //[Description("REPAYMENT")]
        //REPAYMENT = 7,
        /// <summary>
        /// WITHDRAW 提现
        /// </summary>
        [Description("WITHDRAW")]
        WITHDRAW = 8,
        /// <summary>
        /// RECHARGE 充值
        /// </summary>
        [Description("RECHARGE")]
        RECHARGE = 9,

        /// <summary>
        /// 投标
        /// </summary>
        [Description("TENDER")]
        TENDER = 1,
        /// <summary>
        /// 还款
        /// </summary>
        [Description("REPAYMENT")]
        REPAYMENT = 2,
        /// <summary>
        /// 债权转让
        /// </summary>
        [Description("CREDIT_ASSIGNMENT")]
        CREDIT_ASSIGNMENT = 3,
        /// <summary>
        /// 转账
        /// </summary>
        [Description("TRANSFER")]
        TRANSFER = 4,
        /// <summary>
        /// 分润仅在资金转账明细中使用
        /// </summary>
        [Description("COMMISSION")]
        COMMISSION = 5,
    }

    /// <summary>
    /// 4.7 银行代码
    /// </summary>
    public enum EnumBank
    {
        /// <summary>
        /// 成都市商业银行
        /// </summary>
        [Description("成都市商业银行")]
        CDCB = 1,
        /// <summary>
        /// 杭州市商业银行
        /// </summary>
        [Description("杭州市商业银行")]
        HZCB = 2,
        /// <summary>
        /// 南洋商业银行
        /// </summary>
        [Description("南洋商业银行")]
        NOBC = 3,
        /// <summary>
        /// 昆仑银行
        /// </summary>
        [Description("昆仑银行")]
        KLB = 4,
        /// <summary>
        /// 郑州银行
        /// </summary>
        [Description("ZZYH")]
        ZZYH = 5,
        /// <summary>
        /// 温州银行
        /// </summary>
        [Description("温州银行")]
        WZYH = 6,
        /// <summary>
        /// 汉口银行
        /// </summary>
        [Description("汉口银行")]
        HKYH = 7,
        /// <summary>
        /// 齐鲁银行
        /// </summary>
        [Description("齐鲁银行")]
        QLYH = 8,
        /// <summary>
        /// 丹东银行
        /// </summary>
        [Description("丹东银行")]
        DDYH = 9,
        /// <summary>
        /// 恒生银行
        /// </summary>
        [Description("恒生银行")]
        HBC = 10,
        /// <summary>
        /// 南京银行
        /// </summary>
        [Description("南京银行")]
        NJCB = 11,
        /// <summary>
        /// 厦门银行
        /// </summary>
        [Description("厦门银行")]
        XMYH = 12,
        /// <summary>
        /// 南昌银行
        /// </summary>
        [Description("南昌银行")]
        NCYH = 13,
        /// <summary>
        /// 东莞银行
        /// </summary>
        [Description("东莞银行")]
        DONGGUANBC = 14,
        /// <summary>
        /// 江苏银行
        /// </summary>
        [Description("江苏银行")]
        JSBCHINA = 15,
        /// <summary>
        /// 东亚银行(中国)
        /// </summary>
        [Description("东亚银行(中国)")]
        HKBEA = 16,
        /// <summary>
        /// 安阳银行
        /// </summary>
        [Description("安阳银行")]
        AYYH = 17,
        /// <summary>
        /// 成都银行
        /// </summary>
        [Description("成都银行")]
        CDYH = 18,
        /// <summary>
        /// 宁波银行
        /// </summary>
        [Description("宁波银行")]
        NBB = 19,
        /// <summary>
        /// 长沙银行
        /// </summary>
        [Description("长沙银行")]
        CSCB = 20,
        /// <summary>
        /// 河北银行
        /// </summary>
        [Description("河北银行")]
        HBYH = 21,
        /// <summary>
        /// 农业发展银行
        /// </summary>
        [Description("农业发展银行")]
        NYFZYH = 22,
        /// <summary>
        /// 广州银行
        /// </summary>
        [Description("广州银行")]
        GZYH = 23,
        /// <summary>
        /// 交通银行
        /// </summary>
        [Description("交通银行")]
        BOCO = 24,
        /// <summary>
        /// 光大银行
        /// </summary>
        [Description("光大银行")]
        CEB = 25,
        /// <summary>
        /// 上海浦东发展银行
        /// </summary>
        [Description("上海浦东发展银行")]
        SPDB = 26,
        /// <summary>
        /// 农业银行
        /// </summary>
        [Description("农业银行")]
        ABC = 27,
        /// <summary>
        /// 中信银行
        /// </summary>
        [Description("中信银行")]
        ECITIC = 28,
        /// <summary>
        /// 平安银行
        /// </summary>
        [Description("平安银行")]
        PAB = 29,
        /// <summary>
        /// 建设银行
        /// </summary>
        [Description("建设银行")]
        CCB = 30,
        /// <summary>
        /// 民生银行
        /// </summary>
        [Description("民生银行")]
        CMBC = 31,
        /// <summary>
        /// 深圳发展银行
        /// </summary>
        [Description("深圳发展银行")]
        SDB = 32,
        /// <summary>
        /// 中国邮政储蓄
        /// </summary>
        [Description("中国邮政储蓄")]
        POST = 33,
        /// <summary>
        /// 招商银行
        /// </summary>
        [Description("招商银行")]
        CMBCHINA = 34,
        /// <summary>
        /// 招商银行
        /// </summary>
        [Description("兴业银行")]
        CIB = 35,
        /// <summary>
        /// 中国工商银行
        /// </summary>
        [Description("中国工商银行")]
        ICBC = 36,
        /// <summary>
        /// 中国银行
        /// </summary>
        [Description("中国银行")]
        BOC = 37,
        /// <summary>
        /// 北京银行
        /// </summary>
        [Description("北京银行")]
        BCCB = 38,
        /// <summary>
        /// 广发银行
        /// </summary>
        [Description("广发银行")]
        GDB = 39,
        /// <summary>
        /// 华夏银行
        /// </summary>
        [Description("华夏银行")]
        HXB = 40,
        /// <summary>
        /// 西安市商业银行
        /// </summary>
        [Description("西安市商业银行")]
        XACB = 41,
        /// <summary>
        /// 上海银行
        /// </summary>
        [Description("上海银行")]
        SHB = 42,
        /// <summary>
        /// 天津市商业银行
        /// </summary>
        [Description("天津市商业银行")]
        TJCB = 43,
        /// <summary>
        /// 太原市商业银行
        /// </summary>
        [Description("太原市商业银行")]
        TYCB = 44,
        /// <summary>
        /// 广州市商业银行
        /// </summary>
        [Description("广州市商业银行")]
        GZCB = 45,
        /// <summary>
        /// 深圳农村商业银行
        /// </summary>
        [Description("深圳农村商业银行")]
        SNXS = 46,
        /// <summary>
        /// 上海农商银行
        /// </summary>
        [Description("上海农商银行")]
        SHRCB = 47
    }

    /// <summary>
    /// 回调通知，异步
    /// </summary>
    public enum EnumNotifyBizType
    {
        /// <summary>
        ///  2.1 注册 2.6 注册
        /// </summary>
        [Description("REGISTER")]
        REGISTER = 1,
        /// <summary>
        /// 2.2 充值
        /// </summary>
        [Description("RECHARGE")]
        RECHARGE = 2,
        /// <summary>
        /// 2.3 提现
        /// </summary>
        [Description("WITHDRAW")]
        WITHDRAW = 3,
        /// <summary>
        /// 2.4 
        /// </summary>
        [Description("BIND_BANK_CARD")]
        BIND_BANK_CARD = 4,
        /// <summary>
        /// 2.5
        /// </summary>
        [Description("UNBIND_BANK_CARD")]
        UNBIND_BANK_CARD = 5,
        /// <summary>
        /// 2.7   、 3 直连接口  457
        /// </summary>
        [Description("TRANSACTION")]
        TRANSACTION = 7,
        /// <summary>
        /// 2.8
        /// </summary>
        [Description("AUTHORIZE_AUTO_TRANSFER")]
        AUTHORIZE_AUTO_TRANSFER = 8,
        /// <summary>
        /// 2.9
        /// </summary>
        [Description("AUTHORIZE_AUTO_REPAYMENT")]
        AUTHORIZE_AUTO_REPAYMENT = 9
    }

    /// <summary>
    /// 回调通知，异步 ： 2.7转账、投标、还款、债权转让、3.4 直接转账
    /// </summary>
    public enum EnumNotifyStatus
    {
        /// <summary>
        ///  2.7  转账、投标、还款、债权转让
        /// </summary>
        [Description("PREAUTH")]
        PREAUTH = 1,
        /// <summary>
        ///3.4 直接转账 
        /// </summary>
        [Description("DIRECT")]
        DIRECT = 2,
        /// <summary>
        ///3.6 状态 失败
        /// </summary>
        [Description("INIT")]
        INIT = 3,
        /// <summary>
        ///3.6 状态 成功
        /// </summary>
        [Description("SUCCESS")]
        SUCCESS = 4,
        /// <summary>
        ///3.6 状态 打款成功
        /// </summary>
        [Description("REMIT_SUCCESS")]
        REMIT_SUCCESS = 5,
        /// <summary>
        ///3.6 状态 打款失败
        /// </summary>
        [Description("REMIT_FAILURE")]
        REMIT_FAILURE = 6,
        /// <summary>
        ///3.6 状态 打款中
        /// </summary>
        [Description("REMITING")]
        REMITING = 7,
        /// <summary>
        ///3.7 状态  CONFIRM 表示解冻后完成资金划转  、 3.5 直接转账授权
        /// </summary>
        [Description("CONFIRM")]
        CONFIRM = 8,
        /// <summary>
        ///3.7 状态 CANCEL 表示解冻后取消转账
        /// </summary>
        [Description("CANCEL")]
        CANCEL = 9
    }

    /// <summary>
    /// 3.6 单笔业务查询
    /// </summary>
    public enum EnumMode
    {
        /// <summary>
        /// 转款记录
        /// </summary>
        [Description("CP_TRANSACTION")]
        CP_TRANSACTION = 1,
        /// <summary>
        /// 提现记录
        /// </summary>
        [Description("WITHDRAW_RECORD")]
        WITHDRAW_RECORD = 2,
        /// <summary>
        /// 充值记录
        /// </summary>
        [Description("RECHARGE_RECORD")]
        RECHARGE_RECORD = 3,
    }

    /// <summary>
    /// 3.7 转账确认
    /// </summary>
    public enum EnumModeCOMPLETETRANSACTION
    {
        /// <summary>
        /// 表示解冻后完成资金划转
        /// </summary>
        [Description("CONFIRM")]
        CONFIRM = 1,
        /// <summary>
        /// 表示解冻后取消转账
        /// </summary>
        [Description("CANCEL")]
        CANCEL = 2,
    }

    /// <summary>
    /// 返回服务器
    /// </summary>
    public enum EnumServiceType
    {
        /// <summary>
        /// 2.1 注册
        /// </summary>
        [Description("REGISTER")]
        toRegister = 1,
        /// <summary>
        /// 2.2 充值
        /// </summary>
        [Description("RECHARGE")]
        toRecharge = 2,
        /// <summary>
        /// 2.3 提现
        /// </summary>
        [Description("WITHDRAW")]
        toWithdraw = 3,
        /// <summary>
        /// 2.4 绑卡
        /// </summary>
        [Description("BIND_BANK_CARD")]
        toBindBankCard = 4,
        /// <summary>
        /// 2.5. 取消绑卡
        /// </summary>
        [Description("UNBIND_BANK_CARD")]
        toUnbindBankCard = 5,
        /// <summary>
        /// 2.6 企业用户注册 
        /// </summary>
        [Description("REGISTER")]
        toEnterpriseRegister = 6,
        /// <summary>
        /// 2.7 转账授权
        /// </summary>
        [Description("TRANSACTION")]
        toCpTransaction = 7,
        /// <summary>
        ///2.8 自动投标授权
        /// </summary>
        [Description("AUTHORIZE_AUTO_TRANSFER")]
        toAuthorizeAutoTransfer = 8,
        /// <summary>
        ///2.9 自动还款授权
        /// </summary>
        [Description("AUTHORIZE_AUTO_REPAYMENT")]
        toAuthorizeAutoRepayment = 8,
    }
}