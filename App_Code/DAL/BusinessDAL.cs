using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// BusinessDAL 的摘要说明
/// </summary>
public class BusinessDAL
{
	public BusinessDAL()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    private Business ToModel(DataRow row)
    {
        Business model = new Business();
        model.BusinessID = (System.Guid)SqlHelper.FromDBValue(row["BusinessID"]);
        model.BusinessName = (System.String)SqlHelper.FromDBValue(row["BusinessName"]);
        model.BusinessImage = (System.Byte[])SqlHelper.FromDBValue(row["BusinessImage"]);
        model.BusinessGrade = (System.Double?)SqlHelper.FromDBValue(row["BusinessGrade"]);
        model.BusinessAvePay = (System.Int32)SqlHelper.FromDBValue(row["BusinessAvePay"]);
        model.BusinessStyle = (System.String)SqlHelper.FromDBValue(row["BusinessStyle"]);
        model.BusinessAddress = (System.String)SqlHelper.FromDBValue(row["BusinessAddress"]);
        model.BusinessPhone = (System.String)SqlHelper.FromDBValue(row["BusinessPhone"]);
        model.BusinessDescribe = (System.String)SqlHelper.FromDBValue(row["BusinessDescribe"]);
        model.BusinessRemind = (System.String)SqlHelper.FromDBValue(row["BusinessRemind"]);
        model.BusinessIsdeleted = (System.Boolean)SqlHelper.FromDBValue(row["BusinessIsdeleted"]);
        return model;
    }

    public Business[] ListAll()
    {
        DataTable table = SqlHelper.ExecuteDataTable("select * from Business where BusinessIsdeleted=0");
        Business[] dt = new Business[table.Rows.Count];
        for (int i = 0; i < table.Rows.Count; i++)
        {
            dt[i] = ToModel(table.Rows[i]);
        }
        return dt;
    }

    public void Insert(Business model)
    {
        SqlHelper.ExecuteNonQuery(@"insert into Business(BusinessID,BusinessName,BusinessImage,BusinessGrade,
                                    BusinessAvePay,BusinessStyle,BusinessAddress,BusinessPhone,BusinessDescribe,
                                    BusinessRemind,BusinessIsdeleted) values(
                                    @BusinessID,@BusinessName,@BusinessImage,@BusinessGrade,@BusinessAvePay,
                                    @BusinessStyle,@BusinessAddress,@BusinessPhone,@BusinessDescribe,@BusinessRemind)",
                                    new SqlParameter("@BusinessID", model.BusinessID),
                                    new SqlParameter("@BusinessName", model.BusinessName),
                                    new SqlParameter("@BusinessImage", model.BusinessImage),
                                    new SqlParameter("@BusinessGrade", model.BusinessGrade),
                                    new SqlParameter("@BusinessAvePay", model.BusinessAvePay),
                                    new SqlParameter("@BusinessStyle", model.BusinessStyle),
                                    new SqlParameter("@BusinessAddress", model.BusinessAddress),
                                    new SqlParameter("@BusinessPhone", model.BusinessPhone),
                                    new SqlParameter("@BusinessDescribe", model.BusinessDescribe),
                                    new SqlParameter("@BusinessRemind", model.BusinessRemind));
    }

    public Business GetById(Guid id)
    {
        Business model = new Business();
        DataTable table = SqlHelper.ExecuteDataTable(@"select * from Business where BusinessID=@Id and BusinessIsdeleted=0",
                                                        new SqlParameter("@Id", id));
        if (table.Rows.Count <= 0)
        {
            return null;
        }
        else if (table.Rows.Count > 1)
        {
            throw new Exception("存在重复ID");
        }
        else
        {
            return ToModel(table.Rows[0]);
        }
    }

    public void DeleteById(Guid id)
    {
        SqlHelper.ExecuteNonQuery("update Orders set BusinessIsdeleted=1 where BusinessID=@Id",
                                    new SqlParameter("@Id", id));
    }


    public void Update(Business model)
    {
        SqlHelper.ExecuteNonQuery(@"UPDATE Business set
                                    BusinessName = @BusinessName,
                                    BusinessImage = @BusinessImage,
                                    BusinessStyle = @BusinessStyle,
                                    BusinessAddress = @BusinessAddress,
                                    BusinessPhone = @BusinessPhone,
                                    BusinessDescribe = @BusinessDescribe
                                    WHERE BusinessID=@BusinessID",
                                    new SqlParameter("@BusinessID", model.BusinessID),
                                    new SqlParameter("@BusinessName", model.BusinessName),
                                    new SqlParameter("@BusinessImage", model.BusinessImage),
                                    new SqlParameter("@BusinessStyle", model.BusinessStyle),
                                    new SqlParameter("@BusinessAddress", model.BusinessAddress),
                                    new SqlParameter("@BusinessPhone", model.BusinessPhone),
                                    new SqlParameter("@BusinessDescribe", model.BusinessDescribe));
    }
}

