using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// StoreDAL 的摘要说明
/// </summary>
public class StoreDAL
{
	public StoreDAL()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}


    /// <summary>
    /// 检验用户名和密码
    /// </summary>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <returns>通过返回 true ，否则返回 false </returns>
    public Store CheckUserInfo(string username, string password)
    {
        string sql = "select StoreName,StorePassword,StoreBusinessID from Store where StoreName=@StoreName and StorePassword=@Password";
        //为了安全性，就直接提示用户名和密码错误，而不是提示“用户名不存在”或者“密码错误”
        //不分开检查 email 和 password 了
        //string sqlEmail = "select Email from Tb_UserLogin where Email=@Email";
        //string sqlPassword = "select Password from Tb_UserLogin where Password=@Password";

        //为了从 cookies 直接登录， 将 MD5 加密放在 LoginButton_Click 事件中
        //password = CommentHelper.GetMD5(password);

        SqlParameter emailParameter = new SqlParameter("StoreName", username);
        SqlParameter passwordParameter = new SqlParameter("Password", password);

        //判断邮箱和密码
        //1.判断两者都符合
        DataTable dt = SqlHelper.ExecuteDataTable(sql, emailParameter, passwordParameter);
        if (dt.Rows.Count == 1)
        {
            Store store = new Store();
            store.StoreBusinessID =(Guid) dt.Rows[0]["StoreBusinessID"];
            return store;
        }
        else
        {
            return null;
        }
    }

    public void InsertUser(string username, string password)
    {
        //ID 和 IsDeleted 在数据库中有默认值 newid() 和 0 
        string sql = "Insert into Store(StoreName,StorePassword) values(@Name,@Password) ";
        password = CommentHelper.GetMD5(password);
        SqlHelper.ExecuteNonQuery(sql, new SqlParameter("Name", username),
            new SqlParameter("Password", password));
    }



    private Store ToModel(DataRow row)
    {
        Store model = new Store();
        model.StoreName = (System.String)SqlHelper.FromDBValue(row["StoreName"]);
        model.StorePassword = (System.String)SqlHelper.FromDBValue(row["StorePassword"]);
        return model;
    }
    public Store[] ListAll()
    {
        DataTable table = SqlHelper.ExecuteDataTable("select StoreName,StorePassword from Store ");
        Store[] dt = new Store[table.Rows.Count];
        for (int i = 0; i < table.Rows.Count; i++)
        {
            dt[i] = ToModel(table.Rows[i]);
        }
        return dt;
    }
    public void Insert(Store model)
    {
        SqlHelper.ExecuteNonQuery(@"insert into Store(StoreName,StorePassword) 
                                    values(@StoreName,@StorePassword)",
        new SqlParameter("@StoreName", model.StoreName),
        new SqlParameter("@StorePassword", model.StorePassword));
        //new SqlParameter("@StoreIsDeleted", model.StoreIsDeleted));
    }
    public Store GetById(Guid id)
    {
        DataTable table = SqlHelper.ExecuteDataTable(@"selete StoreName,StorePassword from Store where StoreID=@Id",
        new SqlParameter("@Id", id));
        if (table.Rows.Count <= 0)
        {
            return null;
        }
        else if (table.Rows.Count > 1)
        {
            throw new Exception("Id重复");
        }
        else
        {
            return ToModel(table.Rows[0]);
        }
    }
    public void DeleteById(Guid id)
    {
        SqlHelper.ExecuteNonQuery("update Store set StoreIsDeleted=1 where StoreID=@Id",
        new SqlParameter("@Id", id));
    }
    public void Update(Store model)
    {
        SqlHelper.ExecuteNonQuery(@"UPDATE Store set 
                                    StoreName = @StoreName,
                                    StorePassword = @StorePassword,
                                    WHERE StoreID=@Id",
            new SqlParameter("@StoreName", model.StoreName),
            new SqlParameter("@StorePassword", model.StorePassword));
    }
}