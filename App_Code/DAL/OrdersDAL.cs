using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// OrdersDAL 的摘要说明
/// </summary>
public class OrdersDAL
{
	public OrdersDAL()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    /// <summary>
    /// 将 DataTable 中的 DataRow 的数据转换成 Orders 类型的对象
    /// </summary>
    /// <param name="row"></param>
    /// <returns></returns>
    private Orders ToModel(DataRow row)
    {
        Orders model = new Orders();
        model.OrdersID = (System.Guid)SqlHelper.FromDBValue(row["OrdersID"]);
        model.OrdersBusinessID = (System.Guid)SqlHelper.FromDBValue(row["OrdersBusinessID"]);
        model.OrdersCustID = (System.Guid)SqlHelper.FromDBValue(row["OrdersCustID"]);
        model.OrdersFoodInfoID = (System.Guid)SqlHelper.FromDBValue(row["OrdersFoodInfoID"]);
        model.OrdersData = (System.DateTime)SqlHelper.FromDBValue(row["OrdersData"]);
        model.OrdersNum = (System.Int32)SqlHelper.FromDBValue(row["OrdersNum"]);
        return model;
    }



    public Orders[] ListAll()
    {
        DataTable table = SqlHelper.ExecuteDataTable("select * from Orders and OrdersIsDeleted=0");
        Orders[] dt = new Orders[table.Rows.Count];
        for (int i = 0; i < table.Rows.Count; i++)
        {
            dt[i] = ToModel(table.Rows[i]);
        }
        return dt;
    }


    /// <summary>
    /// 将 Orders 类型的对象插入数据库
    /// </summary>
    /// <param name="model"></param>
    public void Insert(Orders model)
    {
        SqlHelper.ExecuteNonQuery(@"insert into Orders(OrdersID,OrdersBusinessID,OrdersCustID,OrdersFoodInfoID,
                                    OrdersData,OrdersNum) values(@OrdersID,@OrdersBusinessID,@OrdersCustID,
                                    @OrdersFoodInfoID,@OrdersData,@OrdersNum)",
                        new SqlParameter("@OrdersID", model.OrdersID),
                        new SqlParameter("@OrdersBusinessID", model.OrdersBusinessID),
                        new SqlParameter("@OrdersCustID", model.OrdersCustID),
                        new SqlParameter("@OrdersFoodInfoID", model.OrdersFoodInfoID),
                        new SqlParameter("@OrdersData", model.OrdersData),
                        new SqlParameter("@OrdersNum", model.OrdersNum));
    }


    /// <summary>
    /// 根据 ID 获得 Orders 类的对象
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Orders GetById(Guid id)
    {
        DataTable td = SqlHelper.ExecuteDataTable(@"selete * from Orders where OrdersID=@Id and OrdersIsDeleted=0",
                                new SqlParameter("Id", id));
        if (td.Rows.Count <= 0)
        {
            return null;
        }
        else if (td.Rows.Count > 1)
        {
            throw new Exception("数据库中存在重复ID");
        }
        else
        {
            return ToModel(td.Rows[0]);
        }       
    }

    /// <summary>
    /// 根据 ID 软删除数据，设置 IsDeleted 属性值 1
    /// </summary>
    /// <param name="id"></param>
    public void DeletedById(Guid id)
    {
        SqlHelper.ExecuteNonQuery("update Orders set OrdersIsdeleted=1 where OrdersID=@Id",
        new SqlParameter("@Id", id));
    }

    /// <summary>
    /// 将 Orders 类对象更新到数据库
    /// </summary>
    /// <param name="model"></param>
    public void Update(Orders model)
    {
        SqlHelper.ExecuteNonQuery(@"UPDATE Orders set 
                                    OrdersID = @OrdersID,
                                    OrdersBusinessID = @OrdersBusinessID,
                                    OrdersCustID = @OrdersCustID,
                                    OrdersFoodInfoID = @OrdersFoodInfoID,
                                    OrdersData = @OrdersData,
                                    OrdersNum = @OrdersNum WHERE OrdersID=@Id",
            new SqlParameter("@OrdersID", model.OrdersID),
            new SqlParameter("@OrdersBusinessID", model.OrdersBusinessID),
            new SqlParameter("@OrdersCustID", model.OrdersCustID),
            new SqlParameter("@OrdersFoodInfoID", model.OrdersFoodInfoID),
            new SqlParameter("@OrdersData", model.OrdersData),
            new SqlParameter("@OrdersNum", model.OrdersNum));
    }

}