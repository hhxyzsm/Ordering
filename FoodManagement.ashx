<%@ WebHandler Language="C#" Class="FoodManagement" %>

using System;
using System.Web;
using System.Collections.Generic;
using System.Web.SessionState;

public class FoodManagement : IHttpHandler, IRequiresSessionState{
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/html";
        Guid businessid = (Guid)context.Session["businessid"];
        string username = (string)context.Session["username"];
        string thiskind = context.Request["Kind"];
        FoodKind[] kinds = FoodManagementBLL.ListAllKind(businessid);
        if (thiskind == "default")
        {
            if (kinds.Length != 0)
            {
                thiskind = kinds[0].KindName;
            }
        }
        FoodInfo[] foods = FoodManagementBLL.ListAll(businessid, thiskind);


        var data = new {UserName=username, Kind = thiskind, FoodKind = kinds, FoodInfo = foods };
        string strhtml = CommentHelper.RenderHtml("Food.html", data);
        context.Response.Write(strhtml);
        
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}