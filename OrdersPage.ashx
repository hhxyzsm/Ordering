<%@ WebHandler Language="C#" Class="OrdersPage" %>

using System;
using System.Web;
using System.Web.SessionState;
public class OrdersPage : IHttpHandler, IRequiresSessionState{
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/html";
        string username = (string)context.Session["username"];

        var data = new { UserName = username};
        string strhtml = CommentHelper.RenderHtml("OrdersPage.html", data);
        context.Response.Write(strhtml);
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}