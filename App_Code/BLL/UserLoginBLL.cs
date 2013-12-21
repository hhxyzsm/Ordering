using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// CheckUserLoginBLL 的摘要说明
/// </summary>
public class UserLoginBLL
{
	public UserLoginBLL()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}
    public Store CheckStoreUserLogin(string username, string password)
    {
        Store store = new StoreDAL().CheckUserInfo(username, password);

        if (store!=null)
        {
            store.StoreName = username;
            store.StorePassword = password;
            return store;
        }
        else
        {
            return null;
        }
        
    }
    public void InsertUser(Store store)
    {
        //ID 和 IsDeleted 在数据库中有默认值 newid() 和 0 
        string sql = "Insert into Store(StoreName,StorePassword) values(@Name,@Password) ";
        SqlHelper.ExecuteNonQuery(sql, new SqlParameter("Name", store.StoreName),
            new SqlParameter("Password", store.StorePassword));
    }
}