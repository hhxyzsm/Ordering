using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// FoodManagementBLL 的摘要说明
/// </summary>
public class FoodManagementBLL
{
	public FoodManagementBLL()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    public static void InsertFood(FoodInfo food)
    {
        new FoodInfoDAL().Insert(food);
    }

    public static FoodKind[] ListAllKind(Guid id)
    {
        return new FoodKindDAL().ListAllById(id);
    }

    public static FoodInfo[] ListAll(Guid id,string kind)
    {
        return new FoodInfoDAL().ListAll(id,kind);
    }

    public static byte[] GetImage(Guid FoodID)
    {
        FoodInfo food = new FoodInfoDAL().GetById(FoodID);
        return food.FoodImage;
    }
    public static FoodInfo GetFoodById(Guid id)
    {
        return new FoodInfoDAL().GetById(id);
    }

    public static FoodInfo[] GetFoodByName(string name,Guid bid)
    {
        return new FoodInfoDAL().GetByName(name,bid);
    }

    public static void UpdateFood(FoodInfo food)
    {
        food.FoodIsdeleted = false;
        new FoodInfoDAL().Update(food);
    }

    public static void DeleteFood(Guid id)
    {
        new FoodInfoDAL().DeleteById(id);
    }
}