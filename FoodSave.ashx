<%@ WebHandler Language="C#" Class="FoodSava" %>

using System;
using System.Web;
using System.Web.SessionState;

public class FoodSava : IHttpHandler,IRequiresSessionState{
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        FoodInfo food = new FoodInfo();
        food.FoodName = context.Request.Form["foodname"];
        food.FoodPrice = Convert.ToInt32(context.Request.Form["foodprice"]);
        food.FoodTaste = context.Request.Form["foodtaste"];
        food.FoodKind = context.Request.Form["foodkind"];
        food.FoodBusinessID = (Guid)context.Session["businessid"];

        HttpPostedFile photo = context.Request.Files["imgupload"];
        int upPhotoLength = photo.ContentLength;
        byte[] PhotoArray = new Byte[upPhotoLength];
        System.IO.Stream PhotoStream = photo.InputStream;
        PhotoStream.Read(PhotoArray, 0, upPhotoLength);
        food.FoodImage = PhotoArray;

        food.FoodImgURL = "Image/" + photo.FileName;
        FoodManagementBLL.InsertFood(food);
        context.Response.Redirect("FoodManagement.ashx");
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}