using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
/// <summary>
/// FodKindDAL 的摘要说明
/// </summary>
public class FoodKindDAL
{
	public FoodKindDAL()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}
    private FoodKind ToModel(DataRow row)
    {
        FoodKind model = new FoodKind();
        model.KindID = (System.Guid)SqlHelper.FromDBValue(row["KindID"]);
        model.KindName = (System.String)SqlHelper.FromDBValue(row["KindName"]);
        model.BusinessID = (System.Guid)SqlHelper.FromDBValue(row["BusinessID"]);
        return model;
    }
    public FoodKind[] ListAllById(Guid id)
    {
        DataTable table = SqlHelper.ExecuteDataTable(@"select * from FoodKind where BusinessID=@BusinessID 
                                                        and IsDeleted=0 order by Sort",
                                    new SqlParameter("@BusinessID",id));
        FoodKind[] dt = new FoodKind[table.Rows.Count];
        for (int i = 0; i < table.Rows.Count; i++)
        {
            dt[i] = ToModel(table.Rows[i]);
        }
        return dt;
    }
    public void Insert(FoodKind model)
    {
        SqlHelper.ExecuteNonQuery(@"insert into FoodKind(KindName,BusinessID) 
                                    values(@KindName,@BusinessID)",
        new SqlParameter("@KindName", model.KindName),
        new SqlParameter("@BusinessID", model.BusinessID));
    }
    public FoodKind GetById(Guid id)
    {
        DataTable table = SqlHelper.ExecuteDataTable(@"select KindName from FoodKind where KindID=@Id 
                                                        and  and IsDeleted=0",
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
        SqlHelper.ExecuteNonQuery("update FoodKind set IsDeleted=1  where KindID=@Id",
        new SqlParameter("@Id", id));
    }
    public void Update(FoodKind model)
    {
        SqlHelper.ExecuteNonQuery(@"UPDATE FoodKind set
                                    KindName = @KindName,
                                    BusinessID = @BusinessID WHERE KindID=@KindID",
            new SqlParameter("@KindID", model.KindID),
            new SqlParameter("@KindName", model.KindName),
            new SqlParameter("@BusinessID", model.BusinessID));
    }
}