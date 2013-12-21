<%@ WebHandler Language="C#" Class="UserOut" %>

using System;
using System.Web;
using System.Web.SessionState;
public class UserOut : IHttpHandler, IRequiresSessionState{
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        HttpContext.Current.Session.Clear();//清除Session内容
        HttpContext.Current.Session.Abandon();//取消当前会话
        context.Response.Redirect("Login.html");
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}