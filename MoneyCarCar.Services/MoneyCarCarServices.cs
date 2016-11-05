using MoneyCarCar.Commons;
using MoneyCarCar.Models;
using MoneyCarCar.Models.DtoModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MoneyCarCar.Commons;

namespace MoneyCarCar.Services
{
    /// <summary>
    /// 结息服务
    /// </summary>
    public partial class MoneyCarCarServices : ServiceBase
    {
        public MoneyCarCarServices()
        {
            InitializeComponent();

            ApiPath = System.Configuration.ConfigurationManager.AppSettings["DataApiUrl"];
            PrincipalBeginHour = System.Configuration.ConfigurationManager.AppSettings["PrincipalBeginHour"].ToInt();
            PrincipalSteep = System.Configuration.ConfigurationManager.AppSettings["PrincipalSteep"].ToInt() * 60 * 1000;
            BearBeginHour = System.Configuration.ConfigurationManager.AppSettings["BearBeginHour"].ToInt();
            BearSteep = System.Configuration.ConfigurationManager.AppSettings["BearSteep"].ToInt() * 60 * 1000;
            ScanSteep = System.Configuration.ConfigurationManager.AppSettings["ScanSteep"].ToInt() * 60 * 1000;
        }

        #region 共享属性参数
        /// <summary>
        /// 线程开关，一旦关闭无法重新打开
        /// </summary>
        private bool Power = false;

        /// <summary>
        /// 暂停开关,暂停时候为false，运行时候为true。可反复设置
        /// </summary>
        private bool IsRunning = false;

        private string ApiPath = "";

        private HttpHelper http = HttpHelper.CreatHelper();

        #endregion

        #region override
        protected override void OnStart(string[] args)
        {
            this.Power = true;
            this.IsRunning = true;

            Log.WriteRecord("即将开始服务");
            Log.WriteRecord("参数记录：DataApiUrl:" + ApiPath + ",PrincipalBeginHour:" + PrincipalBeginHour + ",PrincipalSteep:" + PrincipalSteep + ",BearBeginHour:" + BearBeginHour + ",BearSteep:" + BearSteep + ",ScanSteep:" + ScanSteep);

            Thread ReturnPrincipalThread = new Thread(new ThreadStart(Run_ReturnPrincipal));
            ReturnPrincipalThread.Start();

            Thread BearThread = new Thread(new ThreadStart(Run_BearInterest));
            BearThread.Start();

            Thread ScanThread = new Thread(new ThreadStart(Run_Scan));
            ScanThread.Start();

            Log.WriteRecord("服务已经启动");
        }

        protected override void OnStop()
        {
            this.IsRunning = false;
            this.Power = false;
        }

        protected override void OnPause()
        {
            this.IsRunning = false;
            base.OnPause();
        }

        protected override void OnContinue()
        {
            this.IsRunning = true;
            base.OnContinue();
        }
        #endregion

        #region function
        #region 返还本金

        #region 返还本金属性参数
        /// <summary>
        /// 当天是否已经执行
        /// </summary>
        private bool IsPrincipalAction = false;

        /// <summary>
        /// 每天指定时间执行(每天3点)
        /// </summary>
        private int PrincipalBeginHour = 3;

        /// <summary>
        /// 执行间隔(20分钟)
        /// </summary>
        private int PrincipalSteep = 60 * 20 * 1000;

        private int PrincipalTempSteep = 0;
        #endregion

        #region 返还本金线程
        /// <summary>
        /// 返还本金线程
        /// </summary>
        private void Run_ReturnPrincipal()
        {
            while (this.Power)//先判断总开关
            {
                if (this.IsRunning)//再判断暂停开关
                {
                    if (PrincipalTempSteep >= this.PrincipalSteep)//判断是否达到执行的标准时间
                    {
                        DateTime now = DateTime.Now;

                        if (now.Hour == PrincipalBeginHour)//判断是否执行时间
                        {
                            if (!IsPrincipalAction)//判断是否已经执行过
                            {
                                Log.WriteRecord("返还本金线程启动。");
                                try
                                {
                                    IsPrincipalAction = true;//首先修改成已经执行过
                                    //调用请求，执行结息
                                    BaseResultDto<bool> list = http.DoGetObject<BaseResultDto<bool>>(ApiPath + "/Services/ReturnPrincipal");
                                }
                                catch (Exception ex)
                                {
                                    Log.WriteRecord("返还本金线程异常：" + ex.ToString());
                                }
                               
                            }
                        }
                        else
                        {
                            IsPrincipalAction = false;//如果不是执行的小时，则把是否执行过修改成未执行
                        }
                        PrincipalTempSteep = 0;
                    }
                }
                Thread.Sleep(100);
                PrincipalTempSteep += 100;
            }
        }
        #endregion
        #endregion

        #region 结息

        #region 结息属性参数
        /// <summary>
        /// 当天是否已经执行
        /// </summary>
        private bool IsBearAction = false;

        /// <summary>
        /// 每天指定时间执行(每天2点)
        /// </summary>
        private int BearBeginHour = 2;

        /// <summary>
        /// 执行间隔(20分钟)
        /// </summary>
        private int BearSteep = 60 * 20 * 1000;

        private int BearTempSteep = 0;
        #endregion

        #region 结息线程
        /// <summary>
        /// 结息线程
        /// </summary>
        private void Run_BearInterest()
        {
            while (this.Power)//先判断总开关
            {
                if (this.IsRunning)//再判断暂停开关
                {
                    if (BearTempSteep >= this.BearSteep)//判断是否达到执行的标准时间
                    {
                        DateTime now = DateTime.Now;

                        if (now.Hour == BearBeginHour)//判断是否执行时间
                        {
                            if (!IsBearAction)//判断是否已经执行过
                            {
                                Log.WriteRecord("结息线程启动。");
                                try
                                {
                                    IsBearAction = true;//首先修改成已经执行过
                                    //调用请求，执行结息
                                    BaseResultDto<bool> list = http.DoGetObject<BaseResultDto<bool>>(ApiPath + "/Services/InterestSettlement_Add");
                                }
                                catch (Exception ex)
                                {
                                    Log.WriteRecord("结息线程异常：" + ex.ToString());
                                }
                            }
                        }
                        else
                        {
                            IsBearAction = false;//如果不是执行的小时，则把是否执行过修改成未执行
                        }
                        BearTempSteep = 0;
                    }
                }
                Thread.Sleep(100);
                BearTempSteep += 100;
            }
        }
        #endregion

        #endregion

        #region 扫描

        #region 扫描属性参数
        /// <summary>
        /// 执行间隔(5分钟)
        /// </summary>
        private int ScanSteep = 60 * 5 * 1000;

        private int ScanTempSteep = 0;
        #endregion

        #region 扫描线程
        /// <summary>
        /// 扫描线程
        /// </summary>
        private void Run_Scan()
        {
            while (this.Power)//先判断总开关
            {
                if (this.IsRunning)//再判断暂停开关
                {
                    if (ScanTempSteep >= this.ScanSteep)//判断是否达到执行的标准时间
                    {
                        Log.WriteRecord("扫描线程启动。");
                        try
                        {
                            //获取需要处理的请求
                            List<SystemRequestRecord> list = http.DoGetObject<List<SystemRequestRecord>>(ApiPath + "/Services/GetScanList");
                        }
                        catch (Exception ex)
                        {
                            Log.WriteRecord("扫描线程异常：" + ex.ToString());
                        }

                        ScanTempSteep = 0;
                    }
                }
                Thread.Sleep(100);
                ScanTempSteep += 100;
            }
        }
        #endregion

        #endregion

        #endregion
    }
}
