<%@ WebHandler Language="C#" Class="FoodEdit" %>

using System;
using System.Web;
using System.Collections.Generic;
using System.Web.SessionState;

public class FoodEdit : IHttpHandler, IRequiresSessionState{
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/html";
        string action = context.Request["Action"];
        string username = (string)context.Session["username"];
        bool save = Convert.ToBoolean(context.Request["Save"]);
        FoodInfo food = new FoodInfo();
        if (action == "Add")
        {
            if (save)
            {
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
                string kind = context.Request["Kind"];
                string url = "FoodManagement.ashx?Kind=" + kind;
                context.Response.Redirect(url);
            }
            else
            {
                string kind = context.Request["Kind"];
                var data = new { UserName = username, Kind = kind, Action = "Add", FoodInfo = new { FoodName = "", FoodPrice = "", FoodTaste = "", FoodKind = kind } };
                string strhtml = CommentHelper.RenderHtml("FoodUpload.html", data);
                context.Response.Write(strhtml);
            }
        }
        else if (action == "Edit")
        {
            if (save)
            {
                food.FoodID = new Guid(context.Request["Id"]);
                food.FoodName = context.Request.Form["foodname"];
                food.FoodPrice = Convert.ToInt32(context.Request.Form["foodprice"]);
                food.FoodTaste = context.Request.Form["foodtaste"];
                food.FoodKind = context.Request.Form["foodkind"];
                food.FoodBusinessID = (Guid)context.Session["businessid"];
                HttpPostedFile photo = context.Request.Files["imgupload"];
                if (photo.ContentLength > 0)
                {
                    int upPhotoLength = photo.ContentLength;
                    byte[] PhotoArray = new Byte[upPhotoLength];
                    System.IO.Stream PhotoStream = photo.InputStream;
                    PhotoStream.Read(PhotoArray, 0, upPhotoLength);
                    food.FoodImage = PhotoArray;
                    food.FoodImgURL = "Image/" + photo.FileName;
                }
                else
                {
                    FoodInfo temp = FoodManagementBLL.GetFoodById(food.FoodID);
                    food.FoodImage = temp.FoodImage;
                    food.FoodImgURL = temp.FoodImgURL;
                }
                
                FoodManagementBLL.UpdateFood(food);
                string kind = context.Request["Kind"];
                string url = "FoodManagement.ashx?Kind=" + kind;
                context.Response.Redirect(url);
            }
            else
            {
                string kind = context.Request["Kind"];
                Guid id = new Guid(context.Request["Id"]);
                food = FoodManagementBLL.GetFoodById(id);
                var data = new { UserName = username, Kind = kind, Action = "Edit", FoodInfo = food };
                string strhtml = CommentHelper.RenderHtml("FoodUpload.html", data);
                context.Response.Write(strhtml);
            }
         }
        else if (action == "Delete")
        {
            Guid id = new Guid(context.Request.Form["Id"]);
            FoodManagementBLL.DeleteFood(id);
            string kind = context.Request["Kind"];
            string url = "FoodManagement.ashx?Kind=" + kind;
            context.Response.Redirect(url);
        }
        else
        {
            context.Response.Write("Action 错误！");
        }
       
      }
    
    public bool IsReusable {
        get {
            return false;
        }
    }

}