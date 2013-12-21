<%@ WebHandler Language="C#" Class="Login" %>

using System;
using System.Web;
using System.Web.SessionState;

public class Login : IHttpHandler, IRequiresSessionState{
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        string username, password;
        //if (context.Request.Cookies["userInfo"] != null)
        //{           
        //    username = context.Request.Cookies["userInfo"]["username"];
        //    password = CommentHelper.GetMD5(context.Request.Cookies["userInfo"]["password"]);

        //}
        //else
        //{
        username = context.Request.Form["username"];
        password = CommentHelper.GetMD5(context.Request.Form["password"]);
        //}

        Store store = new UserLoginBLL().CheckStoreUserLogin(username,password);
        if (store != null)
        {
            context.Session["username"] = username;
            context.Session["businessid"] = store.StoreBusinessID;
            //if (context.Request.Form["ckremember"] == "remember-me")
            //{
            //    context.Response.Cookies["userInfo"]["username"] = username;
            //    context.Response.Cookies["userInfo"]["password"] = password;
            //    context.Response.Cookies["userInfo"]["lastVisit"] = DateTime.Now.ToString();
            //    context.Response.Cookies["userInfo"].Expires = DateTime.Now.AddDays(7);
            //}
            context.Response.Redirect("../FoodManagement.ashx?Kind=default");
        }
        else
        {
            context.Response.Redirect("Login.html");
        }
        
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}