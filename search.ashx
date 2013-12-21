<%@ WebHandler Language="C#" Class="search" %>

using System;
using System.Web;
using System.Web.SessionState;
public class search : IHttpHandler, IRequiresSessionState{
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/html";
        string foodname = context.Request.Form["search"];
        Guid businessid = (Guid)context.Session["businessid"];
        string username = (string)context.Session["username"];
        
        FoodInfo[] foods = FoodManagementBLL.GetFoodByName(foodname, businessid);
        var data = new { UserName = username, FoodInfo = foods };
        string strhtml = CommentHelper.RenderHtml("FoodSearchPage.html", data);
        context.Response.Write(strhtml);
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}