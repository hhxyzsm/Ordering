using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack) 
        {
            //SqlConnection conn = new SqlConnection();
            //conn.ConnectionString = "Data Source=.;Initial Catalog=ordering; User ID=ordering;Password=00.123"; 
            //string strSql="select * from T_Image where id=1 and id=2";//这里假设获取id为2的图片 
            //SqlCommand cmd=new SqlCommand(strSql,conn); 
            //conn.Open(); 
            //SqlDataReader reader=cmd.ExecuteReader(); 
            //reader.Read(); 
            //Response.ContentType="application/octet-stream"; 
            //Response.BinaryWrite((Byte[])reader["Img"]); 
            //Response.End(); 
            //reader.Close(); 
            string strSql = "select * from Business where BusinessName='新发现'";
            DataTable dt = SqlHelper.ExecuteDataTable(strSql);
            Response.BinaryWrite((Byte[])dt.Rows[0]["BusinessImage"]);
            
            Response.End();
        } 
    }
}