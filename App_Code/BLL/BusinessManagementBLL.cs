using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// BusinessManagementBLL 的摘要说明
/// </summary>
public class BusinessManagementBLL
{
	public BusinessManagementBLL()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}
    public static Business GetById(Guid id)
    {
        return new BusinessDAL().GetById(id);
    }
    public static byte[] GetImgById(Guid id)
    {
        Business business = GetById(id);
        return business.BusinessImage;
    }
    public static void Update(Business business)
    {
        new BusinessDAL().Update(business);
    }
}