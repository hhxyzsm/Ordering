<%@ WebHandler Language="C#" Class="BusinessAccountSave" %>

using System;
using System.Web;

public class BusinessAccountSave : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        Guid storeid = new Guid(context.Request.Form["storeid"]);
        string password = CommentHelper.GetMD5(context.Request.Form["password"]);
        Store store = new Store();
        store.StoreID = storeid;
        store.StorePassword = password;
        AccountManagementBLL.Update(store);
        context.Response.Redirect("FoodManagement.ashx?Kind=default");
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}