<%@ WebHandler Language="C#" Class="FoodKindEdit" %>

using System;
using System.Web;

public class FoodKindEdit : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        Guid businessid = (Guid)(context.Session["businessid"]);
        FoodKind[] kinds = FoodManagementBLL.ListAllKind(businessid);
        string username = (string)context.Session["username"];
        var data=new {UserNmae=username,FoodKind=kinds};
        string strhtml = CommentHelper.RenderHtml("FoodKindEditPage", data);
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}