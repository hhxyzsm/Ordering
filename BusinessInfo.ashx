<%@ WebHandler Language="C#" Class="BusinessInfo" %>

using System;
using System.Web;
using System.Web.SessionState;

public class BusinessInfo : IHttpHandler, IRequiresSessionState{
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/html";
        string username = (string)context.Session["username"];
        Guid businessid = (Guid)context.Session["businessid"];
        Business business = BusinessManagementBLL.GetById(businessid);
        var data=new {UserName=username,Business=business};
        string strhtml = CommentHelper.RenderHtml("BusinessInfo.html",data);
        context.Response.Write(strhtml);
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}