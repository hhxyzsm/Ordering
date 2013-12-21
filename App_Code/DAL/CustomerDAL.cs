using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// CustomerDAL 的摘要说明
/// </summary>
public class CustomerDAL
{
	public CustomerDAL()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}
    private Customer ToModel(DataRow row)
    {
        Customer model = new Customer();
        model.CustID = (System.Guid)SqlHelper.FromDBValue(row["CustID"]);
        model.CustName = (System.String)SqlHelper.FromDBValue(row["CustName"]);
        model.CustPassword = (System.String)SqlHelper.FromDBValue(row["CustPassword"]);
        model.CustPhone = (System.String)SqlHelper.FromDBValue(row["CustPhone"]);
        //model.CustIsDeleted = (System.Boolean)SqlHelper.FromDBValue(row["CustIsDeleted"]);
        return model;
    }
    public Customer[] ListAll() 
    {
        DataTable table = SqlHelper.ExecuteDataTable("select * from Customer where CustIsDeleted=0");
        Customer[] dt = new Customer[table.Rows.Count];
        for (int i = 0; i < table.Rows.Count; i++) 
        {
            dt[i] = ToModel(table.Rows[i]);
        }
        return dt;
    }
    public void Insert(Customer model)
    {
        SqlHelper.ExecuteNonQuery(@"insert into Customer(CustID,CustName,CustPassword,CustPhone) 
                                    values(@CustID,@CustName,@CustPassword,@CustPhone)",
                                    new SqlParameter("@CustID", model.CustID),
                                    new SqlParameter("@CustName", model.CustName),
                                    new SqlParameter("@CustPassword", model.CustPassword),
                                    new SqlParameter("@CustPhone", model.CustPhone));
    }
    public Customer GetById(Guid id)
    {
        DataTable table = SqlHelper.ExecuteDataTable(@"selete * from Customer where CustID=@Id and CustIsDeleted=0",
        new SqlParameter("@Id", id));
        if (table.Rows.Count <= 0)
        {
            return null;
        }
        else if (table.Rows.Count > 1)
        {
            throw new Exception("存在Id重复");
        }
        else
        {
            return ToModel(table.Rows[0]);
        }
    }
    public void DeleteById(Guid id)
    {
        SqlHelper.ExecuteNonQuery("update Customer set CustIsdeleted=1 where CustID=@Id",
        new SqlParameter("@Id", id));
    }
    public void Update(Customer model)
    {
        SqlHelper.ExecuteNonQuery(@"UPDATE Customer set 
                                    CustID = @CustID,
                                    CustName = @CustName,
                                    CustPassword = @CustPassword,
                                    CustPhone = @CustPhone,
                                    WHERE CustID=@Id",
                                    new SqlParameter("@CustID", model.CustID),
                                    new SqlParameter("@CustName", model.CustName),
                                    new SqlParameter("@CustPassword", model.CustPassword),
                                    new SqlParameter("@CustPhone", model.CustPhone));
    }
}