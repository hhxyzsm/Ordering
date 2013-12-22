<%@ WebHandler Language="C#" Class="BusinessAccountPage" %>

using System;
using System.Web;
using System.Web.SessionState;

public class BusinessAccountPage : IHttpHandler, IRequiresSessionState{
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/html";
        string username = (string)context.Session["username"];
        Guid storeid = (Guid)context.Session["storeid"];
        var data = new { UserName = username, StoreID = storeid };
        string strhtml = CommentHelper.RenderHtml("BusinessAccountPage.html", data);
        context.Response.Write(strhtml);
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}