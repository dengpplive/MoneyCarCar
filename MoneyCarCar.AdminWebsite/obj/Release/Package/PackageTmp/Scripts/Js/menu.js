var _Menus = {
    "Menus": [
                 {
                     "MenuId": "1", "icon": "icon-sys", "MenuName": "用户管理",
                     "Menus": [{ "MenuId": "11", "MenuName": "用户信息", "icon": "icon-nav", "url": "Account/Index" }
                     ]
                 },
                 {
                     "MenuId": "2", "icon": "icon-sys", "MenuName": "债权管理",
                     "Menus": [
                         { "MenuId": "21", "MenuName": "借贷申请管理", "icon": "icon-nav", "url": "BorrowerApply/ApplayUserIndex" },
                         { "MenuId": "22", "MenuName": "债权管理", "icon": "icon-nav", "url": "Claims/Index" }
                         //,{ "MenuId": "23", "MenuName": "债权信息统计", "icon": "icon-nav", "url": "Claims/InfoStatistics" }
                     ]
                 },
                 {
                     "MenuId": "3", "icon": "icon-sys", "MenuName": "公告管理",
                     "Menus": [
                       { "MenuId": "31", "MenuName": "公告列表", "icon": "icon-page", "url": "Notice/NoticeIndex" }
                     ]
                 },
                 {
                     "MenuId": "4", "icon": "icon-sys", "MenuName": "新闻管理",
                     "Menus": [{ "MenuId": "41", "MenuName": "查看新闻", "icon": "icon-nav", "url": "News/NewsIndex" }
                     ]
                 },
                 {
                     "MenuId": "5", "icon": "icon-sys", "MenuName": "短信管理",
                     "Menus": [
                         { "MenuId": "51", "MenuName": "短信模板", "icon": "icon-nav", "url": "Sms/SmsTemplteIndex" },
                         { "MenuId": "51", "MenuName": "发送短信记录", "icon": "icon-nav", "url": "Sms/SmsRecordIndex" }
                     ]
                 },
                {
                    "MenuId": "6", "icon": "icon-sys", "MenuName": "系统管理",
                    "Menus": [{ "MenuId": "61", "MenuName": "管理员管理", "icon": "icon-nav", "url": "SysSetting/Index" },
                            { "MenuId": "62", "MenuName": "系统日志", "icon": "icon-nav", "url": "SysSetting/SystemLogIndex" },
                            { "MenuId": "63", "MenuName": "平台常量", "icon": "icon-nav", "url": "SysSetting/SysConso" },
                            { "MenuId": "64", "MenuName": "帮助设置", "icon": "icon-nav", "url": "SysSetting/SysHelpIndex" },
                            { "MenuId": "65", "MenuName": "问题反馈", "icon": "icon-nav", "url": "SysSetting/SysFeedbackIndex" },
                            { "MenuId": "66", "MenuName": "请求同步", "icon": "icon-nav", "url": "SysSetting/SysRequestRecordIndex" },
                    ]
                },
                 {
                     "MenuId": "7", "icon": "icon-sys", "MenuName": "业务推广",
                     "Menus": [
                         { "MenuId": "71", "MenuName": "赠送虚拟本金", "icon": "icon-nav", "url": "BusinessPop/GiveVirtualMoney" }
                     ]
                 }
    ]
};