<%@ WebHandler Language="C#" Class="UploadHandler" %>

using System;
using System.Web;
using System.IO;
/*
public class UploadHandler : IHttpHandler
{
  
      public void ProcessRequest(HttpContext context)
      {
        
          context.Response.ContentType = "text/plain";
          context.Response.Charset = "utf-8";

          HttpPostedFile file = context.Request.Files["Filedata"];
          string uploadPath =
              HttpContext.Current.Server.MapPath(@context.Request["folder"]) + "\\";

          if (file != null)
          {
              if (!System.IO.Directory.Exists(uploadPath))
              {
                  System.IO.Directory.CreateDirectory(uploadPath);
              }
              file.SaveAs(uploadPath + file.FileName);
              //下面这句代码缺少的话，上传成功后上传队列的显示不会自动消失
              context.Response.Write("1");
          }
          else
          {
              context.Response.Write("0");
          }
                                
      }


 
      public bool IsReusable {
          get {
              return false;
          }
      }
}
*/

/// <summary>
/// 上传文件
/// </summary>
public class UploadHandler : IHttpHandler
{
    System.Drawing.Image image, image64, image48, image32, image16; //定义image类的对象
    protected string imagePath;//图片路径
    protected string imageType;//图片类型
    protected string imageName;//图片名称
    protected string fileName;//图片名称
    //提供一个回调方法,用于确定Image对象在执行生成缩略图操作时何时提前取消执行
    //如果此方法确定 GetThumbnailImage 方法应提前停止执行，则返回 true；否则返回 false
    System.Drawing.Image.GetThumbnailImageAbort callb = null;

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        string date = System.DateTime.Now.ToString("yyyy-MM-dd_HH");
        string folder = context.Request["folder"] + "\\" + date;
        HttpPostedFile UploadImage = context.Request.Files["FileData"];
        //物理路径
        string uploadpath = HttpContext.Current.Server.MapPath(folder);

        if (UploadImage != null)
        {
            //是否有目录，如没有就创建
            if (!Directory.Exists(uploadpath))
            {
                Directory.CreateDirectory(uploadpath);
            }
            //客户端文件完全名称
            string filename = UploadImage.FileName;
            string extname = filename.Substring(filename.LastIndexOf(".") + 1);
            //为不重复，设置文件名
            fileName = Guid.NewGuid().ToString() + "." + extname;

            //context.Response.Write("1");
            context.Response.Write(date + "##" + fileName);
        }
        else
        {
            context.Response.Write("0");
        }

        string mPath;

        imagePath = UploadImage.FileName;
        //取得图片类型
        imageType = imagePath.Substring(imagePath.LastIndexOf(".") + 1);
        //取得图片名称
        imageName = imagePath.Substring(imagePath.LastIndexOf("\\") + 1);
        Stream imgStream = UploadImage.InputStream;//流文件，准备读取上载文件的内容
        int imgLen = UploadImage.ContentLength;//上载文件大小        
        //建立虚拟路径
        mPath = HttpContext.Current.Server.MapPath(folder);
        //保存到虚拟路径
        UploadImage.SaveAs(mPath + "\\" + fileName);
        ////显示原图
        //imageSource.ImageUrl = "upFile/" + imageName;
        //为上传的图片建立引用
        image = System.Drawing.Image.FromFile(mPath + "\\" + fileName);

        //生成缩略图
        image64 = image.GetThumbnailImage(64, 64, callb, new System.IntPtr());
        //把缩略图保存到指定的虚拟路径
        image64.Save(HttpContext.Current.Server.MapPath(folder) + "\\64_" + fileName);
        //释放image64对象的资源
        image64.Dispose();
        /*
        //生成缩略图
        image48 = image.GetThumbnailImage(48, 48, callb, new System.IntPtr());
        image48.Save(HttpContext.Current.Server.MapPath(context.Request["folder"]) + "\\48_" + fileName);
        image48.Dispose();

        //生成缩略图
        image32 = image.GetThumbnailImage(32, 32, callb, new System.IntPtr());
        image32.Save(HttpContext.Current.Server.MapPath(context.Request["folder"]) + "\\32_" + fileName);
        image32.Dispose();

        //生成缩略图
        image16 = image.GetThumbnailImage(16, 16, callb, new System.IntPtr());
        image16.Save(HttpContext.Current.Server.MapPath(context.Request["folder"]) + "\\16_" + fileName);
        image16.Dispose();

        //释放image对象占用的资源
        image.Dispose();
        */


    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}