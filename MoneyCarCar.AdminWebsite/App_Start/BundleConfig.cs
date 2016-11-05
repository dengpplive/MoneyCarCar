using System.Web;
using System.Web.Optimization;

namespace MoneyCarCar.Website
{
    public class BundleConfig
    {
        // 有关 Bundling 的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));

            //--------------以下是对基本常用的库---------------------------
            bundles.Add(new StyleBundle("~/easyui/css").Include(
                   "~/Scripts/easyui/themes/icon.css",
                   "~/Scripts/easyui/themes/default/easyui.css"
                   ));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                       "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.layout-latest.js",
                        "~/Scripts/json2.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/easyui").Include(
                       "~/Scripts/easyui/jquery.js",
                       "~/Scripts/easyui/jquery.easyui.js",
                        "~/Scripts/easyui/locale/easyui-lang-zh_CN.js"
                     ));
            bundles.Add(new ScriptBundle("~/bundles/knockout").Include(
                      "~/Scripts/knockout-{version}.js",
                      "~/Scripts/knockout.mapping-latest.js"));

            bundles.Add(new ScriptBundle("~/bundles/common").Include(
                   "~/Scripts/common/extend.js",
                   "~/Scripts/common/ko.custombinding.js",
                   "~/Scripts/common/commonViewModel.js"
                   ));

            bundles.Add(new ScriptBundle("~/bundles/menu").Include(
                "~/Scripts/easyui/plugins/jquery.mask.js",
                "~/Scripts/Js/tab_menu.js",
                "~/Scripts/Js/menu.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/ueditor").Include(
                  "~/ueditor/ueditor.all.min.js",
                   "~/ueditor/ueditor.config.js",
                  "~/ueditor/lang/zh-cn/zh-cn.js"
                  ));
        }
    }
}