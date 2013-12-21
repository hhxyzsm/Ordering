<%@ WebHandler Language="C#" Class="Register" %>

using System;
using System.Web;

public class Register : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        
        Store store = new Store();
        store.StoreName = context.Request.Form["username"];
        store.StorePassword = CommentHelper.GetMD5(context.Request.Form["password"]);

        new UserLoginBLL().InsertUser(store);
        
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}