using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        //获得图象并把图象转换为byte[] 
        List<byte[]> photolist = new List<byte[]>();

        //HttpPostedFile upPhoto = UpPhoto.PostedFile;
        //int upPhotoLength = upPhoto.ContentLength;
        //byte[] PhotoArray = new Byte[upPhotoLength];
        //Stream PhotoStream = upPhoto.InputStream;
        //PhotoStream.Read(PhotoArray, 0, upPhotoLength);

        //连接数据库 
        //SqlConnection conn = new SqlConnection();
        //conn.ConnectionString = "Data Source=localhost;Database=test;User Id=sa;Pwd=sa";
        //SqlCommand cmd = new SqlCommand("UpdateImage", conn);
        //cmd.CommandType = CommandType.StoredProcedure;
        //cmd.Parameters.Add("@UpdateImage", SqlDbType.Image);
        //cmd.Parameters["@UpdateImage"].Value = PhotoArray;
        //如果你希望不使用存储过程来添加图片把上面四句代码改为： 
        string strSqlname = "select BusinessID from Business";
        DataTable dt = SqlHelper.ExecuteDataTable(strSqlname);

        foreach(System.Web.UI.Control ctl in this.Form.Controls)
        {
            if (ctl is System.Web.UI.WebControls.FileUpload)
            {
                System.Web.UI.WebControls.FileUpload fb = (System.Web.UI.WebControls.FileUpload)ctl;
                GetImage(ref photolist, fb);
            }
        }
        //foreach (Control control in this.Controls)
        //{
        //    if (control is this.Controls.
        //    {
        //        //FileUpload fileHander = webcontrol as System.Web.UI.WebControls.FileUpload;//得到input
        //        //GetImage(ref photolist, fileHander);
        //    }
        //}
        int i=0;
        foreach (byte[] PhotoArray in photolist)
        {
            DataRow row = dt.Rows[i++]; 
            Guid id = (Guid)row["BusinessID"];
            //string strSql = "Insert into Business(BusinessImage) values(@FImage) where BusinessID=@id";
            string strSql = "UPDATE Business SET BusinessImage = @FImage WHERE BusinessName = '院子餐厅'";
            SqlParameter ID = new SqlParameter("@id", id);
            SqlParameter img = new SqlParameter("@FImage", SqlDbType.Image);
            img.Value = PhotoArray;
            SqlHelper.ExecuteNonQuery(strSql, ID,img);
        }
        
        //SqlCommand cmd = new SqlCommand(strSql, conn);
        //cmd.Parameters.Add("@FImage", SqlDbType.Image);
        //cmd.Parameters["@FImage"].Value = PhotoArray; 
        //conn.Open();
        //cmd.ExecuteNonQuery();
        //conn.Close(); 
    }

    private void GetImage(ref List<byte[]> photolist, FileUpload con)
    {
        HttpPostedFile photo = con.PostedFile;
        int upPhotoLength = photo.ContentLength;
        byte[] PhotoArray = new Byte[upPhotoLength];
        Stream PhotoStream = photo.InputStream;
        PhotoStream.Read(PhotoArray, 0, upPhotoLength);
        photolist.Add(PhotoArray);
    }
}