using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// FoodInfoDAL 的摘要说明
/// </summary>
public class FoodInfoDAL
{
	public FoodInfoDAL()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    private FoodInfo ToModel(DataRow row)
    {
        FoodInfo model = new FoodInfo();
        model.FoodID = (System.Guid)SqlHelper.FromDBValue(row["FoodID"]);
        model.FoodBusinessID = (System.Guid)SqlHelper.FromDBValue(row["FoodBusinessID"]);
        model.FoodName = (System.String)SqlHelper.FromDBValue(row["FoodName"]);
        model.FoodImage = (System.Byte[])SqlHelper.FromDBValue(row["FoodImage"]);
        model.FoodPopularIndex = (System.Int32)SqlHelper.FromDBValue(row["FoodPopularIndex"]);
        model.FoodPrice = (System.Int32)SqlHelper.FromDBValue(row["FoodPrice"]);
        model.FoodTaste = (System.String)SqlHelper.FromDBValue(row["FoodTaste"]);
        model.FoodKind = (System.String)SqlHelper.FromDBValue(row["FoodKind"]);
        model.FoodImgURL = (System.String)SqlHelper.FromDBValue(row["FoodImgURL"]);
        //数据库中默认值为 0 
        //model.FoodIsdeleted = (System.Boolean)SqlHelper.FromDBValue(row["FoodIsdeleted"]);
        return model;
    }
    public FoodInfo[] ListAll(Guid id,string kind) 
    {
        DataTable table = SqlHelper.ExecuteDataTable(@"select * from FoodInfo where FoodIsdeleted=0 
                                                    and FoodBusinessID=@FoodBusinessID and FoodKind=@Kind",
                                                    new SqlParameter("@FoodBusinessID",id),
                                                    new SqlParameter("@Kind",kind));
        FoodInfo[] dt = new FoodInfo[table.Rows.Count];
        for (int i = 0; i < table.Rows.Count; i++) 
        {
            dt[i] = ToModel(table.Rows[i]);
        }
        return dt;
    }
    public void Insert(FoodInfo model)
    {
        SqlHelper.ExecuteNonQuery(@"insert into FoodInfo(FoodBusinessID,FoodName,FoodImgURL,FoodImage,
                                    FoodPrice,FoodKind,FoodTaste) values(@FoodBusinessID,@FoodName,@FoodImgURL,
                                    @FoodImage,@FoodPrice,@FoodKind,@FoodTaste)",
        new SqlParameter("@FoodBusinessID", model.FoodBusinessID),
        new SqlParameter("@FoodName", model.FoodName),
        new SqlParameter("@FoodImgURL", model.FoodImgURL),
        new SqlParameter("@FoodImage", model.FoodImage),
        new SqlParameter("@FoodPrice", model.FoodPrice),
        new SqlParameter("@FoodKind", model.FoodKind),
        new SqlParameter("@FoodTaste", model.FoodTaste));
        //数据库中默认值为 0 
        //new SqlParameter("@FoodIsdeleted", model.FoodIsdeleted));
    }
    public FoodInfo GetById(Guid id)
    {
        DataTable table = SqlHelper.ExecuteDataTable(@"select * from FoodInfo where FoodID=@FoodID and FoodIsdeleted=0",
        new SqlParameter("@FoodID", id));
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

    public FoodInfo[] GetByName(string name, Guid id)
    {
        DataTable table = SqlHelper.ExecuteDataTable(@"select * from FoodInfo where FoodName like @name and 
                                                        FoodBusinessID=@Bid and FoodIsdeleted=0",
        new SqlParameter("@name", "%"+name+"%"),
        new SqlParameter("@Bid", id));
        if (table.Rows.Count <= 0)
        {
            return null;
        }
        else
        {
            FoodInfo[] dt = new FoodInfo[table.Rows.Count];
            for (int i = 0; i < table.Rows.Count; i++)
            {
                dt[i] = ToModel(table.Rows[i]);
            }
            return dt;
        }
    }

    public void DeleteById(Guid id)
    {
        SqlHelper.ExecuteNonQuery("update FoodInfo set FoodIsdeleted=1 where FoodID=@Id",
        new SqlParameter("@Id", id));
    }
    public void Update(FoodInfo model)
    {
        SqlHelper.ExecuteNonQuery(@"UPDATE FoodInfo set 
                                    FoodName = @FoodName,
                                    FoodImgURL = @FoodImgURL,
                                    FoodImage = @FoodImage,
                                    FoodPrice = @FoodPrice,
                                    FoodTaste = @FoodTaste,
                                    FoodIsdeleted = @FoodIsdeleted WHERE FoodID=@FoodID",
            new SqlParameter("@FoodID", model.FoodID),
            new SqlParameter("@FoodName", model.FoodName),
            new SqlParameter("@FoodImgURL", model.FoodImgURL),
            new SqlParameter("@FoodImage", model.FoodImage),
            new SqlParameter("@FoodPrice", model.FoodPrice),
            new SqlParameter("@FoodTaste", model.FoodTaste),
            new SqlParameter("@FoodIsdeleted", model.FoodIsdeleted));
    }
}