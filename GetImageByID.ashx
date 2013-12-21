<%@ WebHandler Language="C#" Class="GetImageByID" %>

using System;
using System.Web;

public class GetImageByID : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        
        //string strSql = "select * from Business where BusinessName='新发现'";
        //DataTable dt = SqlHelper.ExecuteDataTable(strSql);
        //Response.BinaryWrite((Byte[])dt.Rows[0]["BusinessImage"]);
        Guid id=new Guid(context.Request.QueryString["id"]);
        string key = context.Request.QueryString["key"];
        if (key == "food")
        {
            context.Response.BinaryWrite(FoodManagementBLL.GetImage(id));
        }
        else if (key == "business")
        {
            context.Response.BinaryWrite(BusinessManagementBLL.GetImgById(id));
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}