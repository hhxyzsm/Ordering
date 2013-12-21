<%@ WebHandler Language="C#" Class="BusinessInfoEdit" %>

using System;
using System.Web;
using System.Web.SessionState;

public class BusinessInfoEdit : IHttpHandler, IRequiresSessionState{
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/html";
        Guid businessid = new Guid(context.Request.Form["businessid"]);
        string action = context.Request.Form["action"];
        string username = (string)context.Session["username"];
        
        if (action == "edit")
        {
            Business business = BusinessManagementBLL.GetById(businessid);
            var data = new { UserName = username, Business = business };
            string strhtml = CommentHelper.RenderHtml("BusinessInfoEdit.html", data);
            context.Response.Write(strhtml);
        }
        else
        {
            string businessname = context.Request.Form["businessname"];
            string businessstyle = context.Request.Form["businessstyle"];
            string businessphone = context.Request.Form["businessphone"];
            string businessaddress = context.Request.Form["businessaddress"].Trim();
            string businessdescribe = context.Request.Form["businessdescribe"].Trim();
            Business business = new Business();
            business.BusinessID = businessid;
            business.BusinessName = businessname;
            business.BusinessStyle = businessstyle;
            business.BusinessPhone = businessphone;
            business.BusinessAddress = businessaddress;
            business.BusinessDescribe = businessdescribe;
            HttpPostedFile photo = context.Request.Files["imgupload"];
            if (photo.ContentLength > 0)
            {
                int upPhotoLength = photo.ContentLength;
                byte[] PhotoArray = new Byte[upPhotoLength];
                System.IO.Stream PhotoStream = photo.InputStream;
                PhotoStream.Read(PhotoArray, 0, upPhotoLength);
                business.BusinessImage = PhotoArray;
            }
            else
            {
                Business temp = BusinessManagementBLL.GetById(businessid);
                business.BusinessImage = temp.BusinessImage;
            }
            BusinessManagementBLL.Update(business);
            context.Response.Redirect("BusinessInfo.ashx");
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}